namespace PDAC_Manager
{
    partial class Form_Color
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Color));
            this.trackBar_R = new System.Windows.Forms.TrackBar();
            this.trackBar_G = new System.Windows.Forms.TrackBar();
            this.trackBar_B = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_reset_trail = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_reset_notouch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar_noTouchR = new System.Windows.Forms.TrackBar();
            this.trackBar_noTouchB = new System.Windows.Forms.TrackBar();
            this.trackBar_noTouchG = new System.Windows.Forms.TrackBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_reset_touch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBar_touchR = new System.Windows.Forms.TrackBar();
            this.trackBar_touchB = new System.Windows.Forms.TrackBar();
            this.trackBar_touchG = new System.Windows.Forms.TrackBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_R)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_G)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_B)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_noTouchR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_noTouchB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_noTouchG)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_touchR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_touchB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_touchG)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar_R
            // 
            this.trackBar_R.Location = new System.Drawing.Point(17, 19);
            this.trackBar_R.Maximum = 70;
            this.trackBar_R.Minimum = 30;
            this.trackBar_R.Name = "trackBar_R";
            this.trackBar_R.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_R.Size = new System.Drawing.Size(45, 207);
            this.trackBar_R.TabIndex = 0;
            this.toolTip1.SetToolTip(this.trackBar_R, resources.GetString("trackBar_R.ToolTip"));
            this.trackBar_R.Value = 65;
            this.trackBar_R.Scroll += new System.EventHandler(this.trackBar_R_Scroll);
            // 
            // trackBar_G
            // 
            this.trackBar_G.Location = new System.Drawing.Point(68, 19);
            this.trackBar_G.Maximum = 70;
            this.trackBar_G.Minimum = 30;
            this.trackBar_G.Name = "trackBar_G";
            this.trackBar_G.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_G.Size = new System.Drawing.Size(45, 207);
            this.trackBar_G.TabIndex = 0;
            this.toolTip1.SetToolTip(this.trackBar_G, resources.GetString("trackBar_G.ToolTip"));
            this.trackBar_G.Value = 49;
            this.trackBar_G.Scroll += new System.EventHandler(this.trackBar_R_Scroll);
            // 
            // trackBar_B
            // 
            this.trackBar_B.Location = new System.Drawing.Point(119, 19);
            this.trackBar_B.Maximum = 70;
            this.trackBar_B.Minimum = 30;
            this.trackBar_B.Name = "trackBar_B";
            this.trackBar_B.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_B.Size = new System.Drawing.Size(45, 207);
            this.trackBar_B.TabIndex = 0;
            this.toolTip1.SetToolTip(this.trackBar_B, resources.GetString("trackBar_B.ToolTip"));
            this.trackBar_B.Value = 51;
            this.trackBar_B.Scroll += new System.EventHandler(this.trackBar_R_Scroll);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_reset_trail);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.trackBar_R);
            this.groupBox1.Controls.Add(this.trackBar_B);
            this.groupBox1.Controls.Add(this.trackBar_G);
            this.groupBox1.Location = new System.Drawing.Point(387, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(177, 298);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Trail Color";
            // 
            // btn_reset_trail
            // 
            this.btn_reset_trail.Location = new System.Drawing.Point(17, 269);
            this.btn_reset_trail.Name = "btn_reset_trail";
            this.btn_reset_trail.Size = new System.Drawing.Size(147, 23);
            this.btn_reset_trail.TabIndex = 2;
            this.btn_reset_trail.Text = "Reset to default";
            this.btn_reset_trail.UseVisualStyleBackColor = true;
            this.btn_reset_trail.Click += new System.EventHandler(this.btn_reset_trail_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 229);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "- R            - G            - B";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_reset_notouch);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.trackBar_noTouchR);
            this.groupBox2.Controls.Add(this.trackBar_noTouchB);
            this.groupBox2.Controls.Add(this.trackBar_noTouchG);
            this.groupBox2.Location = new System.Drawing.Point(21, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(177, 298);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "No Touch Color";
            // 
            // btn_reset_notouch
            // 
            this.btn_reset_notouch.Enabled = false;
            this.btn_reset_notouch.Location = new System.Drawing.Point(17, 269);
            this.btn_reset_notouch.Name = "btn_reset_notouch";
            this.btn_reset_notouch.Size = new System.Drawing.Size(147, 23);
            this.btn_reset_notouch.TabIndex = 2;
            this.btn_reset_notouch.Text = "Reset to default";
            this.btn_reset_notouch.UseVisualStyleBackColor = true;
            this.btn_reset_notouch.Click += new System.EventHandler(this.btn_reset_notouch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 229);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "R               G               B";
            // 
            // trackBar_noTouchR
            // 
            this.trackBar_noTouchR.Enabled = false;
            this.trackBar_noTouchR.Location = new System.Drawing.Point(17, 19);
            this.trackBar_noTouchR.Maximum = 255;
            this.trackBar_noTouchR.Name = "trackBar_noTouchR";
            this.trackBar_noTouchR.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_noTouchR.Size = new System.Drawing.Size(45, 207);
            this.trackBar_noTouchR.TabIndex = 0;
            this.toolTip1.SetToolTip(this.trackBar_noTouchR, "Not implemented yet.\r\n(As the brightness is managed by RGB values, \r\nI have to ma" +
        "ke more adjustment to make it works on other colors than White.)");
            this.trackBar_noTouchR.Value = 255;
            this.trackBar_noTouchR.Scroll += new System.EventHandler(this.trackBar_noTouchR_Scroll);
            // 
            // trackBar_noTouchB
            // 
            this.trackBar_noTouchB.Enabled = false;
            this.trackBar_noTouchB.Location = new System.Drawing.Point(119, 19);
            this.trackBar_noTouchB.Maximum = 255;
            this.trackBar_noTouchB.Name = "trackBar_noTouchB";
            this.trackBar_noTouchB.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_noTouchB.Size = new System.Drawing.Size(45, 207);
            this.trackBar_noTouchB.TabIndex = 0;
            this.toolTip1.SetToolTip(this.trackBar_noTouchB, "Not implemented yet.\r\n(As the brightness is managed by RGB values, \r\nI have to ma" +
        "ke more adjustment to make it works on other colors than White.)");
            this.trackBar_noTouchB.Value = 255;
            this.trackBar_noTouchB.Scroll += new System.EventHandler(this.trackBar_noTouchR_Scroll);
            // 
            // trackBar_noTouchG
            // 
            this.trackBar_noTouchG.Enabled = false;
            this.trackBar_noTouchG.Location = new System.Drawing.Point(68, 19);
            this.trackBar_noTouchG.Maximum = 255;
            this.trackBar_noTouchG.Name = "trackBar_noTouchG";
            this.trackBar_noTouchG.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_noTouchG.Size = new System.Drawing.Size(45, 207);
            this.trackBar_noTouchG.TabIndex = 0;
            this.toolTip1.SetToolTip(this.trackBar_noTouchG, "Not implemented yet.\r\n(As the brightness is managed by RGB values, \r\nI have to ma" +
        "ke more adjustment to make it works on other colors than White.)");
            this.trackBar_noTouchG.Value = 255;
            this.trackBar_noTouchG.Scroll += new System.EventHandler(this.trackBar_noTouchR_Scroll);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_reset_touch);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.trackBar_touchR);
            this.groupBox3.Controls.Add(this.trackBar_touchB);
            this.groupBox3.Controls.Add(this.trackBar_touchG);
            this.groupBox3.Location = new System.Drawing.Point(204, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(177, 298);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Touch Color";
            // 
            // btn_reset_touch
            // 
            this.btn_reset_touch.Location = new System.Drawing.Point(17, 269);
            this.btn_reset_touch.Name = "btn_reset_touch";
            this.btn_reset_touch.Size = new System.Drawing.Size(147, 23);
            this.btn_reset_touch.TabIndex = 2;
            this.btn_reset_touch.Text = "Reset to default";
            this.btn_reset_touch.UseVisualStyleBackColor = true;
            this.btn_reset_touch.Click += new System.EventHandler(this.btn_reset_touch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "R               G               B";
            // 
            // trackBar_touchR
            // 
            this.trackBar_touchR.Location = new System.Drawing.Point(17, 19);
            this.trackBar_touchR.Maximum = 255;
            this.trackBar_touchR.Name = "trackBar_touchR";
            this.trackBar_touchR.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_touchR.Size = new System.Drawing.Size(45, 207);
            this.trackBar_touchR.TabIndex = 0;
            this.toolTip1.SetToolTip(this.trackBar_touchR, "Change color for when you touch a sensor.");
            this.trackBar_touchR.Value = 255;
            this.trackBar_touchR.Scroll += new System.EventHandler(this.trackBar_touchR_Scroll);
            // 
            // trackBar_touchB
            // 
            this.trackBar_touchB.Location = new System.Drawing.Point(119, 19);
            this.trackBar_touchB.Maximum = 255;
            this.trackBar_touchB.Name = "trackBar_touchB";
            this.trackBar_touchB.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_touchB.Size = new System.Drawing.Size(45, 207);
            this.trackBar_touchB.TabIndex = 0;
            this.toolTip1.SetToolTip(this.trackBar_touchB, "Change color for when you touch a sensor.");
            this.trackBar_touchB.Value = 255;
            this.trackBar_touchB.Scroll += new System.EventHandler(this.trackBar_touchR_Scroll);
            // 
            // trackBar_touchG
            // 
            this.trackBar_touchG.Location = new System.Drawing.Point(68, 19);
            this.trackBar_touchG.Maximum = 255;
            this.trackBar_touchG.Name = "trackBar_touchG";
            this.trackBar_touchG.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_touchG.Size = new System.Drawing.Size(45, 207);
            this.trackBar_touchG.TabIndex = 0;
            this.toolTip1.SetToolTip(this.trackBar_touchG, "Change color for when you touch a sensor.");
            this.trackBar_touchG.Value = 255;
            this.trackBar_touchG.Scroll += new System.EventHandler(this.trackBar_touchR_Scroll);
            // 
            // Form_Color
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 322);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form_Color";
            this.Text = "Color Settings";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_R)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_G)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_B)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_noTouchR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_noTouchB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_noTouchG)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_touchR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_touchB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_touchG)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar_R;
        private System.Windows.Forms.TrackBar trackBar_G;
        private System.Windows.Forms.TrackBar trackBar_B;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TrackBar trackBar_noTouchR;
        private System.Windows.Forms.TrackBar trackBar_noTouchB;
        private System.Windows.Forms.TrackBar trackBar_noTouchG;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TrackBar trackBar_touchR;
        private System.Windows.Forms.TrackBar trackBar_touchB;
        private System.Windows.Forms.TrackBar trackBar_touchG;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_reset_notouch;
        private System.Windows.Forms.Button btn_reset_trail;
        private System.Windows.Forms.Button btn_reset_touch;
    }
}