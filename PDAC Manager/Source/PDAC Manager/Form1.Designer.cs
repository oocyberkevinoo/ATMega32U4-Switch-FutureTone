namespace PDAC_Manager
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btn_connect = new System.Windows.Forms.Button();
            this.textBox_log = new System.Windows.Forms.TextBox();
            this.trackBar_LEDsBrightness = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_upload = new System.Windows.Forms.Button();
            this.btn_settingsSaveFile = new System.Windows.Forms.Button();
            this.btn_settingsLoadFile = new System.Windows.Forms.Button();
            this.trackBar_sensitivityTouch = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBar_sensitivityRelease = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_MprFilter = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_customFilter = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox_ledsMode = new System.Windows.Forms.ComboBox();
            this.trackBar_calibration = new System.Windows.Forms.TrackBar();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox_navMode = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btn_reload = new System.Windows.Forms.Button();
            this.btn_saveBackup = new System.Windows.Forms.Button();
            this.checkBox_halfLeds = new System.Windows.Forms.CheckBox();
            this.btn_colors = new System.Windows.Forms.Button();
            this.btn_calibrate = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.button_color = new System.Windows.Forms.Button();
            this.btn_sliderDebug = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_LEDsBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_sensitivityTouch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_sensitivityRelease)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_calibration)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_connect
            // 
            this.btn_connect.Location = new System.Drawing.Point(534, 4);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(170, 38);
            this.btn_connect.TabIndex = 0;
            this.btn_connect.Text = "Connect to controller";
            this.toolTip1.SetToolTip(this.btn_connect, "Connect to your controller.\r\nPlease make sure it is plugged in your computer BEFO" +
        "RE pressing this button.\r\n\r\nIf connected, let you disconnect your controller pro" +
        "perly too.");
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // textBox_log
            // 
            this.textBox_log.Location = new System.Drawing.Point(534, 146);
            this.textBox_log.Multiline = true;
            this.textBox_log.Name = "textBox_log";
            this.textBox_log.ReadOnly = true;
            this.textBox_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_log.Size = new System.Drawing.Size(170, 165);
            this.textBox_log.TabIndex = 1;
            // 
            // trackBar_LEDsBrightness
            // 
            this.trackBar_LEDsBrightness.Enabled = false;
            this.trackBar_LEDsBrightness.Location = new System.Drawing.Point(106, 278);
            this.trackBar_LEDsBrightness.Maximum = 6;
            this.trackBar_LEDsBrightness.Name = "trackBar_LEDsBrightness";
            this.trackBar_LEDsBrightness.Size = new System.Drawing.Size(127, 45);
            this.trackBar_LEDsBrightness.TabIndex = 2;
            this.toolTip1.SetToolTip(this.trackBar_LEDsBrightness, "LEDs brightness");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 278);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "LEDs Brightness:";
            this.toolTip1.SetToolTip(this.label1, "LEDs brightness");
            // 
            // btn_upload
            // 
            this.btn_upload.Enabled = false;
            this.btn_upload.Location = new System.Drawing.Point(534, 60);
            this.btn_upload.Name = "btn_upload";
            this.btn_upload.Size = new System.Drawing.Size(170, 38);
            this.btn_upload.TabIndex = 0;
            this.btn_upload.Text = "Upload Settings to Controller";
            this.toolTip1.SetToolTip(this.btn_upload, "Upload those settings to your controller.");
            this.btn_upload.UseVisualStyleBackColor = true;
            this.btn_upload.Click += new System.EventHandler(this.btn_upload_Click);
            // 
            // btn_settingsSaveFile
            // 
            this.btn_settingsSaveFile.Enabled = false;
            this.btn_settingsSaveFile.Location = new System.Drawing.Point(358, 234);
            this.btn_settingsSaveFile.Name = "btn_settingsSaveFile";
            this.btn_settingsSaveFile.Size = new System.Drawing.Size(170, 38);
            this.btn_settingsSaveFile.TabIndex = 0;
            this.btn_settingsSaveFile.Text = "Save Settings to a file";
            this.toolTip1.SetToolTip(this.btn_settingsSaveFile, "Save the current settings, in the manager, to a file.");
            this.btn_settingsSaveFile.UseVisualStyleBackColor = true;
            this.btn_settingsSaveFile.Click += new System.EventHandler(this.btn_settingsSaveFile_Click);
            // 
            // btn_settingsLoadFile
            // 
            this.btn_settingsLoadFile.Enabled = false;
            this.btn_settingsLoadFile.Location = new System.Drawing.Point(358, 190);
            this.btn_settingsLoadFile.Name = "btn_settingsLoadFile";
            this.btn_settingsLoadFile.Size = new System.Drawing.Size(170, 38);
            this.btn_settingsLoadFile.TabIndex = 0;
            this.btn_settingsLoadFile.Text = "Load Settings from a file";
            this.toolTip1.SetToolTip(this.btn_settingsLoadFile, "Load previously saved settings into the manager.");
            this.btn_settingsLoadFile.UseVisualStyleBackColor = true;
            this.btn_settingsLoadFile.Click += new System.EventHandler(this.btn_settingsLoadFile_Click);
            // 
            // trackBar_sensitivityTouch
            // 
            this.trackBar_sensitivityTouch.Enabled = false;
            this.trackBar_sensitivityTouch.Location = new System.Drawing.Point(15, 25);
            this.trackBar_sensitivityTouch.Maximum = 8;
            this.trackBar_sensitivityTouch.Name = "trackBar_sensitivityTouch";
            this.trackBar_sensitivityTouch.Size = new System.Drawing.Size(155, 45);
            this.trackBar_sensitivityTouch.TabIndex = 2;
            this.toolTip1.SetToolTip(this.trackBar_sensitivityTouch, "How sensitive is your Slider.\r\nHigher the value is, easier it will trigger.\r\nMake" +
        " it as high possible before your slider start to register false positivie result" +
        "s.");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Sensitivity for touching the slider";
            this.toolTip1.SetToolTip(this.label2, "How sensitive is your Slider.\r\nHigher the value is, easier it will trigger.\r\nMake" +
        " it as high possible before your slider start to register false positivie result" +
        "s.");
            // 
            // trackBar_sensitivityRelease
            // 
            this.trackBar_sensitivityRelease.Enabled = false;
            this.trackBar_sensitivityRelease.Location = new System.Drawing.Point(209, 25);
            this.trackBar_sensitivityRelease.Maximum = 6;
            this.trackBar_sensitivityRelease.Name = "trackBar_sensitivityRelease";
            this.trackBar_sensitivityRelease.Size = new System.Drawing.Size(156, 45);
            this.trackBar_sensitivityRelease.TabIndex = 2;
            this.toolTip1.SetToolTip(this.trackBar_sensitivityRelease, resources.GetString("trackBar_sensitivityRelease.ToolTip"));
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(206, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Sensitivity for releasing the slider";
            this.toolTip1.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // comboBox_MprFilter
            // 
            this.comboBox_MprFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_MprFilter.Enabled = false;
            this.comboBox_MprFilter.FormattingEnabled = true;
            this.comboBox_MprFilter.Items.AddRange(new object[] {
            "None",
            "Weak",
            "Strong"});
            this.comboBox_MprFilter.Location = new System.Drawing.Point(15, 89);
            this.comboBox_MprFilter.Name = "comboBox_MprFilter";
            this.comboBox_MprFilter.Size = new System.Drawing.Size(139, 21);
            this.comboBox_MprFilter.TabIndex = 4;
            this.toolTip1.SetToolTip(this.comboBox_MprFilter, "what level of filtering the MPRs will use.\r\n\r\nMore filtering you use, more accura" +
        "te the result will be, \r\nbut the calibration process will be slower.");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Sensors Filtering level:";
            this.toolTip1.SetToolTip(this.label4, "what level of filtering the MPRs will use.\r\n\r\nMore filtering you use, more accura" +
        "te the result will be, \r\nbut the calibration process will be slower.");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(206, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Custom Sensors Filtering level:";
            this.toolTip1.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // comboBox_customFilter
            // 
            this.comboBox_customFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_customFilter.Enabled = false;
            this.comboBox_customFilter.FormattingEnabled = true;
            this.comboBox_customFilter.Items.AddRange(new object[] {
            "None",
            "Weak",
            "Strong"});
            this.comboBox_customFilter.Location = new System.Drawing.Point(209, 89);
            this.comboBox_customFilter.Name = "comboBox_customFilter";
            this.comboBox_customFilter.Size = new System.Drawing.Size(139, 21);
            this.comboBox_customFilter.TabIndex = 4;
            this.toolTip1.SetToolTip(this.comboBox_customFilter, resources.GetString("comboBox_customFilter.ToolTip"));
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 254);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "LEDs Mode:";
            this.toolTip1.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // comboBox_ledsMode
            // 
            this.comboBox_ledsMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ledsMode.Enabled = false;
            this.comboBox_ledsMode.FormattingEnabled = true;
            this.comboBox_ledsMode.Items.AddRange(new object[] {
            "HORI",
            "ARCADE",
            "FULL ARCADE"});
            this.comboBox_ledsMode.Location = new System.Drawing.Point(106, 251);
            this.comboBox_ledsMode.Name = "comboBox_ledsMode";
            this.comboBox_ledsMode.Size = new System.Drawing.Size(139, 21);
            this.comboBox_ledsMode.TabIndex = 4;
            this.toolTip1.SetToolTip(this.comboBox_ledsMode, resources.GetString("comboBox_ledsMode.ToolTip"));
            // 
            // trackBar_calibration
            // 
            this.trackBar_calibration.Enabled = false;
            this.trackBar_calibration.Location = new System.Drawing.Point(394, 25);
            this.trackBar_calibration.Maximum = 4;
            this.trackBar_calibration.Name = "trackBar_calibration";
            this.trackBar_calibration.Size = new System.Drawing.Size(97, 45);
            this.trackBar_calibration.TabIndex = 2;
            this.toolTip1.SetToolTip(this.trackBar_calibration, resources.GetString("trackBar_calibration.ToolTip"));
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(391, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Calibration strength";
            this.toolTip1.SetToolTip(this.label7, resources.GetString("label7.ToolTip"));
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 161);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(134, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Navigation Shortcut Mode:";
            this.toolTip1.SetToolTip(this.label8, resources.GetString("label8.ToolTip"));
            // 
            // comboBox_navMode
            // 
            this.comboBox_navMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_navMode.Enabled = false;
            this.comboBox_navMode.FormattingEnabled = true;
            this.comboBox_navMode.Items.AddRange(new object[] {
            "Holding Button",
            "Trigger Button"});
            this.comboBox_navMode.Location = new System.Drawing.Point(15, 177);
            this.comboBox_navMode.Name = "comboBox_navMode";
            this.comboBox_navMode.Size = new System.Drawing.Size(139, 21);
            this.comboBox_navMode.TabIndex = 4;
            this.toolTip1.SetToolTip(this.comboBox_navMode, resources.GetString("comboBox_navMode.ToolTip"));
            // 
            // btn_reload
            // 
            this.btn_reload.Enabled = false;
            this.btn_reload.Location = new System.Drawing.Point(534, 113);
            this.btn_reload.Name = "btn_reload";
            this.btn_reload.Size = new System.Drawing.Size(166, 27);
            this.btn_reload.TabIndex = 0;
            this.btn_reload.Text = "Reload settings from controller";
            this.toolTip1.SetToolTip(this.btn_reload, "Reload the settings in your controller to the manager");
            this.btn_reload.UseVisualStyleBackColor = true;
            this.btn_reload.Click += new System.EventHandler(this.btn_reload_Click);
            // 
            // btn_saveBackup
            // 
            this.btn_saveBackup.Enabled = false;
            this.btn_saveBackup.Location = new System.Drawing.Point(358, 278);
            this.btn_saveBackup.Name = "btn_saveBackup";
            this.btn_saveBackup.Size = new System.Drawing.Size(170, 28);
            this.btn_saveBackup.TabIndex = 0;
            this.btn_saveBackup.Text = "Save your BACKUP to a file";
            this.toolTip1.SetToolTip(this.btn_saveBackup, "If you used the BACKUP save in the Slider\'s setting menu, you can save it to a fi" +
        "le.");
            this.btn_saveBackup.UseVisualStyleBackColor = true;
            this.btn_saveBackup.Click += new System.EventHandler(this.btn_saveBackup_Click);
            // 
            // checkBox_halfLeds
            // 
            this.checkBox_halfLeds.AutoSize = true;
            this.checkBox_halfLeds.Location = new System.Drawing.Point(251, 253);
            this.checkBox_halfLeds.Name = "checkBox_halfLeds";
            this.checkBox_halfLeds.Size = new System.Drawing.Size(101, 17);
            this.checkBox_halfLeds.TabIndex = 5;
            this.checkBox_halfLeds.Text = "Half Leds Mode";
            this.toolTip1.SetToolTip(this.checkBox_halfLeds, "If you have trouble to power all the 32 LEDs at the same time when not touching t" +
        "he slider,\r\nyou can activate this option to power on only half of them.");
            this.checkBox_halfLeds.UseVisualStyleBackColor = true;
            // 
            // btn_colors
            // 
            this.btn_colors.Enabled = false;
            this.btn_colors.Location = new System.Drawing.Point(251, 276);
            this.btn_colors.Name = "btn_colors";
            this.btn_colors.Size = new System.Drawing.Size(97, 28);
            this.btn_colors.TabIndex = 6;
            this.btn_colors.Text = "Colors (beta)";
            this.toolTip1.SetToolTip(this.btn_colors, "Let you change some colors settings.\r\n(For now, it will not save if you unplug yo" +
        "ur controller, it\'s just for testing purpose)");
            this.btn_colors.UseVisualStyleBackColor = true;
            this.btn_colors.Click += new System.EventHandler(this.button_trail_Click);
            // 
            // btn_calibrate
            // 
            this.btn_calibrate.Enabled = false;
            this.btn_calibrate.Location = new System.Drawing.Point(373, 85);
            this.btn_calibrate.Name = "btn_calibrate";
            this.btn_calibrate.Size = new System.Drawing.Size(139, 27);
            this.btn_calibrate.TabIndex = 0;
            this.btn_calibrate.Text = "Calibrate your Slider";
            this.btn_calibrate.UseVisualStyleBackColor = true;
            this.btn_calibrate.Click += new System.EventHandler(this.btn_calibrate_Click);
            // 
            // colorDialog1
            // 
            this.colorDialog1.AnyColor = true;
            this.colorDialog1.Color = System.Drawing.Color.Turquoise;
            this.colorDialog1.FullOpen = true;
            // 
            // button_color
            // 
            this.button_color.Enabled = false;
            this.button_color.Location = new System.Drawing.Point(281, 146);
            this.button_color.Name = "button_color";
            this.button_color.Size = new System.Drawing.Size(33, 28);
            this.button_color.TabIndex = 6;
            this.button_color.Text = "Color";
            this.button_color.UseVisualStyleBackColor = true;
            this.button_color.Visible = false;
            this.button_color.Click += new System.EventHandler(this.button_color_Click);
            // 
            // btn_sliderDebug
            // 
            this.btn_sliderDebug.Enabled = false;
            this.btn_sliderDebug.Location = new System.Drawing.Point(373, 118);
            this.btn_sliderDebug.Name = "btn_sliderDebug";
            this.btn_sliderDebug.Size = new System.Drawing.Size(139, 27);
            this.btn_sliderDebug.TabIndex = 7;
            this.btn_sliderDebug.Text = "Debug your slider";
            this.btn_sliderDebug.UseVisualStyleBackColor = true;
            this.btn_sliderDebug.Click += new System.EventHandler(this.btn_sliderDebug_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 318);
            this.Controls.Add(this.btn_sliderDebug);
            this.Controls.Add(this.btn_colors);
            this.Controls.Add(this.button_color);
            this.Controls.Add(this.checkBox_halfLeds);
            this.Controls.Add(this.comboBox_customFilter);
            this.Controls.Add(this.comboBox_navMode);
            this.Controls.Add(this.comboBox_ledsMode);
            this.Controls.Add(this.comboBox_MprFilter);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trackBar_calibration);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackBar_sensitivityRelease);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar_sensitivityTouch);
            this.Controls.Add(this.trackBar_LEDsBrightness);
            this.Controls.Add(this.textBox_log);
            this.Controls.Add(this.btn_upload);
            this.Controls.Add(this.btn_settingsLoadFile);
            this.Controls.Add(this.btn_saveBackup);
            this.Controls.Add(this.btn_calibrate);
            this.Controls.Add(this.btn_reload);
            this.Controls.Add(this.btn_settingsSaveFile);
            this.Controls.Add(this.btn_connect);
            this.Name = "Form1";
            this.Text = "PDAC Manager 0.3.1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_LEDsBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_sensitivityTouch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_sensitivityRelease)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_calibration)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox_log;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.TrackBar trackBar_LEDsBrightness;
        public System.Windows.Forms.Button btn_connect;
        public System.Windows.Forms.Button btn_upload;
        public System.Windows.Forms.Button btn_settingsSaveFile;
        public System.Windows.Forms.Button btn_settingsLoadFile;
        public System.Windows.Forms.TrackBar trackBar_sensitivityTouch;
        public System.Windows.Forms.TrackBar trackBar_sensitivityRelease;
        public System.Windows.Forms.ComboBox comboBox_MprFilter;
        public System.Windows.Forms.ComboBox comboBox_customFilter;
        public System.Windows.Forms.ComboBox comboBox_ledsMode;
        public System.Windows.Forms.TrackBar trackBar_calibration;
        public System.Windows.Forms.ComboBox comboBox_navMode;
        public System.Windows.Forms.Button btn_reload;
        public System.Windows.Forms.Button btn_saveBackup;
        public System.Windows.Forms.Button btn_calibrate;
        public System.Windows.Forms.CheckBox checkBox_halfLeds;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button button_color;
        private System.Windows.Forms.Button btn_colors;
        public System.Windows.Forms.Button btn_sliderDebug;
    }
}

