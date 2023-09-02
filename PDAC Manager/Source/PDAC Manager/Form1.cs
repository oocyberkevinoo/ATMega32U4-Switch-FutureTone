using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace PDAC_Manager
{
    public partial class Form1 : Form
    {

        

        public Form1()
        {
            InitializeComponent();
            ControllerManager.Initialize(this);
            UpdateLog("Software is ready, please connect your controller now.\nController Firmware required: v"+ControllerManager.version);
        }


        public void UpdateLog(string text)
        {
            textBox_log.AppendText($"{text}\r\n\r\n");
        }
        public void SetConnectState(bool connected)
        {
            if(connected)
                btn_connect.Text = "Disconnect the controller";
            else
                btn_connect.Text = "Connect to controller";
            btn_upload.Enabled = connected;
            btn_colors.Enabled = connected;
            
            btn_settingsLoadFile.Enabled = connected;
            btn_settingsSaveFile.Enabled = connected;
            btn_saveBackup.Enabled = connected;
            btn_reload.Enabled =  connected;
            btn_calibrate.Enabled = connected;
            btn_sliderDebug.Enabled = connected;

            trackBar_calibration.Enabled = connected;
            trackBar_LEDsBrightness.Enabled = connected;   
            trackBar_sensitivityRelease.Enabled = connected;   
            trackBar_sensitivityTouch.Enabled = connected;

            comboBox_customFilter.Enabled = connected;
            comboBox_ledsMode.Enabled = connected;
            comboBox_MprFilter.Enabled = connected;
            comboBox_navMode.Enabled = connected;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            if (btn_connect.Text.Contains("Disconnect"))
            {
                ControllerManager.DisconnectController();
            }
            else
            {
                ControllerManager.ConnectController();
            }
            
        
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            ControllerManager.SaveConfigToController();
        }

        private void btn_reload_Click(object sender, EventArgs e)
        {
            ControllerManager.LoadConfigFromController();
        }

        private void btn_calibrate_Click(object sender, EventArgs e)
        {
            ControllerManager.CalibrateSlider();
        }

        private void btn_settingsSaveFile_Click(object sender, EventArgs e)
        {
            ControllerManager.SaveSettingsToFile();
        }

        private void btn_settingsLoadFile_Click(object sender, EventArgs e)
        {
            ControllerManager.LoadSettingsFile();
        }

        private void btn_saveBackup_Click(object sender, EventArgs e)
        {
            ControllerManager.LoadConfigFromController(true);
            ControllerManager.SaveSettingsToFile();
            ControllerManager.LoadConfigFromController();

        }

        private void button_color_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ControllerManager.ColorUpdate(0, colorDialog1.Color);
            }
        }

        private void button_trail_Click(object sender, EventArgs e)
        {
            Form_Color form = new Form_Color();
            form.ShowDialog();
        }

        private void btn_sliderDebug_Click(object sender, EventArgs e)
        {
            Form_SliderDebuging form = new Form_SliderDebuging();
            form.ShowDialog();
        }
    }
}
