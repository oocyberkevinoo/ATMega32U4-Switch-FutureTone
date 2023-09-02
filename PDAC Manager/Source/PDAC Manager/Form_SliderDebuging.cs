using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace PDAC_Manager
{
    public partial class Form_SliderDebuging : Form
    {
        byte sensor = 0x01;
        string text = "";
        int i = 1;
        private Timer timer1;

        public Form_SliderDebuging()
        {
            InitializeComponent();
            InitTimer();
        }

       
        public void InitTimer()
        {
            if(timer1 != null)
            {
                timer1.Start();
                return;
            }
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 4; // in miliseconds
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            byte[] result_data = ControllerManager.AskController(ControllerManager.PDAC_Commands.SliderDebug);

            for (int i = 0; i < 32; i++)
            {
                int result = (sbyte)result_data[i+2];
                text += $" {result}";


                if (i == 12 || i == 24)
                    text += "  -|- ";
                
            }

            text += $"\r\n";
            txt_sliderOutput.AppendText(text);
            text = "";



        }

        private void Form_SliderDebug_Activated(object sender, EventArgs e)
        {
            
        }

        private void Form_SliderDebuging_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
        }
    }
}
