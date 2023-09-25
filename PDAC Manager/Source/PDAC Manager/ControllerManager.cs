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
using System.Drawing;

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
            public const byte ColorTest =           0x06;
            public const byte TrailTest =           0x07;
            public const byte NoTouchColorTest =    0x08;
            public const byte TouchColorTest =      0x09;
            public const byte Debug =               0x0A;
            public const byte SliderDebug =         0x0B;

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
        static byte LEDsBrightnessDefault = 0x00;
        static byte[] Sensitivity = new byte[]      { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };
        static byte SensitivityDefault = 0x04;
        static byte[] Release = new byte[]          { 0x04, 0x03, 0x02, 0x01, 0x05, 0x06 };
        static byte ReleaseDefault = 0x02;
        static byte[] Calibration = new byte[]      { 0x03, 0x04, 0x00, 0x01, 0x02 };
        static byte CalibrationDefault = 0x04;
        static byte[] Navigation = new byte[]       { 0x00, 0x01 };
        static byte[] MprFilter = new byte[]        { 0x02, 0x00, 0x01 };
        static byte[] CustomFilter = new byte[]     { 0x02, 0x01, 0x00 };
        static byte[] LedsMode = new byte[]         { 0x00, 0x01, 0x02 };
        static byte[] HalfLedsMode = new byte[]     { 0x00, 0x01 };
        static byte NoDefault = 0x00;

        public static int version = 514;
        static Form1 form1;
        static Form_Color formColor;
        /*static HidDevice device;
        static HidStream stream;*/

        static public void Initialize(Form1 form)
        {
            form1 = form;
        }
        static public void InitializeColor(Form_Color form)
        {
            formColor = form;
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
                        {
                            form1.UpdateLog($"Your controller is using firmware v{d.Attributes.Version}, this version of PDAC Manager need Firmware v{version}");
                            return;
                        }


                        form1.SetConnectState(true);
                        controller = d;

                        // If controller never been used with the manager before
                        if (AskController(PDAC_Commands.Used)[2] != PDAC_Used.AlreadyUsed)
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
            catch (Exception e)
            {

                Error(e.Message);
            }
            
        }

        // Old function to retrieve config from controller
        /*static private byte GetConfigFromController(byte val, byte replace, byte offset)
        {
            byte config_val = AskController(PDAC_Commands.GetSetting, (byte)(val + offset))[2];
            
            if (config_val == 0xFF)
                config_val = replace;

            return config_val;
        }*/

        static private int GetConfigFromController(byte[] array, byte config_id, byte default_config_val, byte offset)
        {
            byte config_val = AskController(PDAC_Commands.GetSetting, (byte)(config_id + offset))[2];
            int result = Array.IndexOf<byte>(array, config_val);
            if (result == -1 || config_val == 0xFF)
                result = Array.IndexOf<byte>(array, default_config_val);

            return result;
        }

        static public void LoadConfigFromController(bool backup = false, string mode = "settings")
        {
            byte offset = 0x00;
            if (backup)
                offset = 0x0A;
            


            try
            {
                
                form1.trackBar_LEDsBrightness.Value =
                    GetConfigFromController(LEDsBrightness, PDAC_Configs.LedsBrightness, LEDsBrightnessDefault, offset);

                form1.trackBar_sensitivityTouch.Value =
                    GetConfigFromController(Sensitivity, PDAC_Configs.Sensitivity, SensitivityDefault, offset);

                form1.comboBox_MprFilter.SelectedIndex =
                    GetConfigFromController(MprFilter, PDAC_Configs.MprFilter, NoDefault, offset);

                form1.comboBox_customFilter.SelectedIndex =
                    GetConfigFromController(CustomFilter, PDAC_Configs.CustomFilter, NoDefault, offset);

                form1.comboBox_ledsMode.SelectedIndex =
                    GetConfigFromController(LedsMode, PDAC_Configs.LedsMode, NoDefault, offset);

                form1.trackBar_sensitivityRelease.Value =
                    GetConfigFromController(Release, PDAC_Configs.Release, ReleaseDefault, offset);

                form1.trackBar_calibration.Value =
                    GetConfigFromController(Calibration, PDAC_Configs.Calibration, CalibrationDefault, offset);

                form1.comboBox_navMode.SelectedIndex =
                    GetConfigFromController(Navigation, PDAC_Configs.Navigation, NoDefault, offset);

                if (GetConfigFromController(HalfLedsMode, PDAC_Configs.HalfLeds, NoDefault, offset) == 0x01)
                    form1.checkBox_halfLeds.Checked = true;
                else
                    form1.checkBox_halfLeds.Checked = false;

                form1.Update();
            }
            catch (Exception e)
            {
                Error(e.Message);
                
            }
            

        }
        static private void Error(string errorMsg = "Unknown Error")
        {
            if (error)
                return;
            form1.UpdateLog("An error occured, Device disconnected.\nError: "+errorMsg);
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
            catch (Exception e)
            {

                Error(e.Message);
            }
            
        }

        static public byte[] AskController(byte command, byte value = 0x00, byte value2 = 0x00, byte value3 = 0x00)
        {

   
            byte[] data = new byte[] {REPORT_ID, command, value, value2, value3 };

            if (!CheckingDeviceIsAvailable())
            {
                Error("No Device found.");
                return new byte[] { 0xFF};
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

            return data;
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

        public static void ColorUpdate(int mode, Color color)
        {
            AskController(PDAC_Commands.ColorTest, color.R, color.G, color.B);
        }

        public static void TrailTest(int r, int g, int b)
        {
            AskController(PDAC_Commands.TrailTest, (byte)r, (byte)g, (byte)b);
        }

        public static void NoTouchColorTest(int r, int g, int b)
        {
            AskController(PDAC_Commands.NoTouchColorTest, (byte)r, (byte)g, (byte)b);
        }

        public static void TouchColorTest(int r, int g, int b)
        {
            AskController(PDAC_Commands.TouchColorTest, (byte)r, (byte)g, (byte)b);
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
