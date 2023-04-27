using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using HidLibrary;
using System.IO;
using System.Windows.Forms;

namespace PDAC_Manager
{
    internal static class ControllerManager
    {
        static bool error = false;
        static byte REPORT_ID = 0x04;
        static HidDevice controller;

        static public class PDAC_Commands
        {
            public const byte Used =                0x01;
            public const byte GetSetting =          0x02;
            public const byte SetSetting =          0x03;
            public const byte Reload =              0x04;
            public const byte Calibrate =           0x05;

        }
        static public class PDAC_Configs
        {
            public const byte LedsBrightness =  0;
            public const byte Sensitivity =     1;
            public const byte MprFilter =       2;
            public const byte CustomFilter =    3;
            public const byte Release =         4;
            public const byte LedsMode =        5;
            public const byte Calibration =     6;
            public const byte Navigation =      7;
            public const byte HalfLeds =        8;

            public const byte UsedAddr = 20;

        }

        static public class PDAC_Used
        {
            public const byte AlreadyUsed = 0x01;
        }

        static byte[] LEDsBrightness = new byte[]   { 0x06, 0x05, 0x04, 0x03, 0x02, 0x01, 0x00 };
        static byte[] Sensitivity = new byte[]      { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };
        static byte[] Release = new byte[]          { 0x04, 0x03, 0x02, 0x01, 0x05, 0x06 };
        static byte[] Calibration = new byte[]      { 0x03, 0x04, 0x00, 0x01, 0x02 };
        static byte[] Navigation = new byte[]       { 0x00, 0x01 };
        static byte[] MprFilter = new byte[]        { 0x02, 0x00, 0x01 };
        static byte[] CustomFilter = new byte[]     { 0x02, 0x01, 0x00 };
        static byte[] LedsMode = new byte[]         { 0x00, 0x01, 0x02 };
        static byte[] HalfLedsMode = new byte[]     { 0x00, 0x01 };

        static int version = 512;
        static Form1 form1;
        /*static HidDevice device;
        static HidStream stream;*/

        static public void Initialize(Form1 form)
        {
            form1 = form;
        }
        static public void ConnectController()
        {
            form1.UpdateLog("Searching for your controller...");
            bool deviceFound = false;

            foreach(HidLibrary.HidDevice d in HidLibrary.HidDevices.Enumerate()) 
            {
                try
                {
                    if (GetManufacturerString(d).Equals("CyberKevin") && GetProductString(d).Equals("DIVA CONTROLLER") && d.DevicePath.Contains("mi_01"))
                    {
                        d.OpenDevice();
                        d.MonitorDeviceEvents = true;

                        form1.UpdateLog($"\r\nDIVA CONTROLLER IS CONNECTED!");
                        //form1.UpdateLog($"{d.DevicePath}");
                        if (d.Attributes.Version != version)
                            form1.UpdateLog($"WARNING! YOUR CONTROLLER IS NOT UPDATED!\r\nPlease update your controller or use a previous version of this software.");
                        form1.SetConnectState(true);
                        controller = d;

                        // If controller never been used with the manager before
                        if (AskController(PDAC_Commands.Used) != PDAC_Used.AlreadyUsed)
                        {
                            form1.UpdateLog("WARNING: this controller never been used with a manager before.\r\n" +
                                "We recommend you to save your config in a file before doing any modification.");
                            AskController(PDAC_Commands.SetSetting, PDAC_Configs.UsedAddr, PDAC_Used.AlreadyUsed);
                        }

                        LoadConfigFromController();
                        deviceFound = true;
                        error = false;
                        
                        


                    }
                    d.CloseDevice();
                    d.Dispose();
                    


                }
                catch (Exception)
                {

                    throw;
                }
            
            }

            if (!deviceFound)
            {
                form1.UpdateLog("No Device Found.");
            }

     

        }

        static public void CalibrateSlider()
        {
            try
            {
                AskController(PDAC_Commands.Calibrate);
            }
            catch (Exception)
            {

                Error();
            }
            
        }

        static public void LoadConfigFromController(bool backup = false)
        {
            byte offset = 0x00;
            if (backup)
                offset = 0x0A;
            
            try
            {
                form1.trackBar_LEDsBrightness.Value =
                Array.IndexOf<byte>(LEDsBrightness, AskController(PDAC_Commands.GetSetting, (byte) (PDAC_Configs.LedsBrightness + offset)));

                form1.trackBar_sensitivityTouch.Value =
                    Array.IndexOf<byte>(Sensitivity, AskController(PDAC_Commands.GetSetting, (byte)(PDAC_Configs.Sensitivity + offset)));

                form1.comboBox_MprFilter.SelectedIndex =
                    Array.IndexOf<byte>(MprFilter, AskController(PDAC_Commands.GetSetting, (byte)(PDAC_Configs.MprFilter + offset)));

                form1.comboBox_customFilter.SelectedIndex =
                    Array.IndexOf<byte>(CustomFilter, AskController(PDAC_Commands.GetSetting, (byte)(PDAC_Configs.CustomFilter + offset)));

                form1.comboBox_ledsMode.SelectedIndex =
                    Array.IndexOf<byte>(LedsMode, AskController(PDAC_Commands.GetSetting, (byte)(PDAC_Configs.LedsMode + offset)));

                form1.trackBar_sensitivityRelease.Value =
                    Array.IndexOf<byte>(Release, AskController(PDAC_Commands.GetSetting, (byte)(PDAC_Configs.Release + offset)));

                form1.trackBar_calibration.Value =
                    Array.IndexOf<byte>(Calibration, AskController(PDAC_Commands.GetSetting, (byte)(PDAC_Configs.Calibration + offset)));

                form1.comboBox_navMode.SelectedIndex =
                    Array.IndexOf<byte>(Navigation, AskController(PDAC_Commands.GetSetting, (byte)(PDAC_Configs.Navigation + offset)));

                if (Array.IndexOf<byte>(HalfLedsMode, AskController(PDAC_Commands.GetSetting, (byte)(PDAC_Configs.HalfLeds + offset))) == 0x01)
                    form1.checkBox_halfLeds.Checked = true;
                else
                    form1.checkBox_halfLeds.Checked = false;

                form1.Update();
            }
            catch (Exception)
            {
                Error();
                
            }
            

        }
        static private void Error()
        {
            if (error)
                return;
            form1.UpdateLog("An error occured, Device disconnected.");
            form1.SetConnectState(false);
            error = true;
        }

        static public void DisconnectController()
        {
            
            controller.CloseDevice();
            controller.Dispose();
            form1.SetConnectState(false);
            form1.UpdateLog("Disconnected");
        }

        static public void SaveConfigToController()
        {
            try
            {
                AskController(PDAC_Commands.SetSetting, PDAC_Configs.LedsBrightness, LEDsBrightness[form1.trackBar_LEDsBrightness.Value]);
                AskController(PDAC_Commands.SetSetting, PDAC_Configs.Sensitivity, Sensitivity[form1.trackBar_sensitivityTouch.Value]);
                AskController(PDAC_Commands.SetSetting, PDAC_Configs.Release, Release[form1.trackBar_sensitivityRelease.Value]);
                AskController(PDAC_Commands.SetSetting, PDAC_Configs.Calibration, Calibration[form1.trackBar_calibration.Value]);
                AskController(PDAC_Commands.SetSetting, PDAC_Configs.MprFilter, MprFilter[form1.comboBox_MprFilter.SelectedIndex]);
                AskController(PDAC_Commands.SetSetting, PDAC_Configs.CustomFilter, CustomFilter[form1.comboBox_customFilter.SelectedIndex]);
                AskController(PDAC_Commands.SetSetting, PDAC_Configs.Navigation, Navigation[form1.comboBox_navMode.SelectedIndex]);
                AskController(PDAC_Commands.SetSetting, PDAC_Configs.LedsMode, LedsMode[form1.comboBox_ledsMode.SelectedIndex]);
                byte halfLedsByte = HalfLedsMode[0];
                if (form1.checkBox_halfLeds.Checked)
                    halfLedsByte = HalfLedsMode[1];
                AskController(PDAC_Commands.SetSetting, PDAC_Configs.HalfLeds, halfLedsByte);

                AskController(PDAC_Commands.Reload);
                form1.UpdateLog("Configuration uploaded.");
            }
            catch (Exception)
            {

                Error();
            }
            
        }

        static public byte AskController(byte command, byte value = 0x00, byte value2 = 0x00)
        {

   
            byte[] data = new byte[] {REPORT_ID, command, value, value2 };

            if (!CheckingDeviceIsAvailable())
            {
                Error();
                return 0xFF;
            }

            controller.WriteFeatureData(data);
            /*string result = "Write: ";
            foreach (byte b in data)
            {
                result += $"{b} | ";
            }*/

            controller.ReadFeatureData(out data, REPORT_ID);
            /*result += "\r\nRead: ";
            foreach (byte b in data)
            {
                result += $"{b} | ";
            }
            form1.UpdateLog(result);*/

            return data[2];
        }

        public static void SaveSettingsToFile()
        {
            Stream fileStream;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "cfg files (*.cfg)|*.cfg|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if((fileStream = saveFileDialog.OpenFile()) != null)
                {
                    fileStream.Close();
                    // Building the file...
                    IniFile inifile = new IniFile(saveFileDialog.FileName);
                    inifile.Write("LedsBrightness", form1.trackBar_LEDsBrightness.Value.ToString());
                    inifile.Write("Sensitivity", form1.trackBar_sensitivityTouch.Value.ToString());
                    inifile.Write("MprFilter", form1.comboBox_MprFilter.SelectedIndex.ToString());
                    inifile.Write("CustomFilter", form1.comboBox_customFilter.SelectedIndex.ToString());
                    inifile.Write("Release", form1.trackBar_sensitivityRelease.Value.ToString());
                    inifile.Write("LedsMode", form1.comboBox_ledsMode.SelectedIndex.ToString());
                    inifile.Write("Calibration", form1.trackBar_calibration.Value.ToString());
                    inifile.Write("Navigation", form1.comboBox_navMode.SelectedIndex.ToString());
                    inifile.Write("HalfLedsMode", form1.checkBox_halfLeds.Checked.ToString());



                }
            }
        }
        public static void LoadSettingsFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "cfg files (*.cfg)|*.cfg|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;

            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog.CheckFileExists)
                {
                    IniFile inifile = new IniFile(openFileDialog.FileName);
                    form1.trackBar_LEDsBrightness.Value = int.Parse(inifile.Read("LedsBrightness"));
                    form1.trackBar_sensitivityTouch.Value = int.Parse(inifile.Read("Sensitivity"));
                    form1.comboBox_MprFilter.SelectedIndex = int.Parse(inifile.Read("MprFilter"));
                    form1.comboBox_customFilter.SelectedIndex = int.Parse(inifile.Read("CustomFilter"));
                    form1.trackBar_sensitivityRelease.Value = int.Parse(inifile.Read("Release"));
                    form1.comboBox_ledsMode.SelectedIndex = int.Parse(inifile.Read("LedsMode"));
                    form1.trackBar_calibration.Value = int.Parse(inifile.Read("Calibration"));
                    form1.comboBox_navMode.SelectedIndex = int.Parse(inifile.Read("Navigation"));
                    form1.checkBox_halfLeds.Checked = bool.Parse(inifile.Read("HalfLedsMode"));


                }
            }
        }

        public static void SaveBackup()
        {
            LoadConfigFromController(true);

        }
 

        private static string GetProductString(HidDevice d)
        {
            byte[] bs;
            d.ReadProduct(out bs);
            string ps = "";
            foreach (byte b in bs)
            {
                if (b > 0)
                    ps += ((char)b).ToString();
            }
            return ps;
        }

        private static string GetManufacturerString(HidDevice d)
        {
            byte[] bs;
            d.ReadManufacturer(out bs);
            string ps = "";
            foreach (byte b in bs)
            {
                if (b > 0)
                    ps += ((char)b).ToString();
            }
            return ps;
        }

        private static bool CheckingDeviceIsAvailable()
        {
            return controller.IsConnected;
        }


    }
}
