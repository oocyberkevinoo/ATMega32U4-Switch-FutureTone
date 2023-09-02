namespace PDAC_Manager
{
    partial class Form_SliderDebuging
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
            this.txt_sliderOutput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txt_sliderOutput
            // 
            this.txt_sliderOutput.Location = new System.Drawing.Point(12, 12);
            this.txt_sliderOutput.Multiline = true;
            this.txt_sliderOutput.Name = "txt_sliderOutput";
            this.txt_sliderOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_sliderOutput.Size = new System.Drawing.Size(776, 426);
            this.txt_sliderOutput.TabIndex = 2;
            // 
            // Form_SliderDebuging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txt_sliderOutput);
            this.Name = "Form_SliderDebuging";
            this.Text = "Slider Debug";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_SliderDebuging_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_sliderOutput;
    }
}