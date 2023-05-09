using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDAC_Manager
{
    public partial class Form_Color : Form
    {

        private int noTouchR;
        private int noTouchG;
        private int noTouchB;
        private int touchR;
        private int touchG;
        private int touchB;
        private int trailR;
        private int trailG;
        private int trailB;

        public Form_Color()
        {
            InitializeComponent();
            noTouchR = trackBar_noTouchR.Value;
            noTouchG = trackBar_noTouchG.Value;
            noTouchB = trackBar_noTouchB.Value;
            touchR = trackBar_touchR.Value;
            touchG = trackBar_touchG.Value;
            touchB = trackBar_touchB.Value;
            trailR = trackBar_R.Value;
            trailG = trackBar_G.Value;
            trailB = trackBar_B.Value;
            ControllerManager.InitializeColor(this);
        }



        private void trackBar_R_Scroll(object sender, EventArgs e)
        {
            ControllerManager.TrailTest(trackBar_R.Value, trackBar_G.Value, trackBar_B.Value);
        }

        private void trackBar_noTouchR_Scroll(object sender, EventArgs e)
        {
            ControllerManager.NoTouchColorTest(trackBar_noTouchR.Value, trackBar_noTouchG.Value, trackBar_noTouchB.Value);
        }

        private void trackBar_touchR_Scroll(object sender, EventArgs e)
        {
            ControllerManager.TouchColorTest(trackBar_touchR.Value, trackBar_touchG.Value, trackBar_touchB.Value);
        }


        private void btn_reset_notouch_Click(object sender, EventArgs e)
        {
            trackBar_noTouchR.Value = noTouchR;
            trackBar_noTouchG.Value = noTouchG;
            trackBar_noTouchB.Value = noTouchB;
            ControllerManager.NoTouchColorTest(trackBar_noTouchR.Value, trackBar_noTouchG.Value, trackBar_noTouchB.Value);
            
        }

        private void btn_reset_touch_Click(object sender, EventArgs e)
        {
            trackBar_touchR.Value = touchR;
            trackBar_touchG.Value = touchG;
            trackBar_touchB.Value = touchB;
            ControllerManager.TouchColorTest(trackBar_touchR.Value, trackBar_touchG.Value, trackBar_touchB.Value);
        }

        private void btn_reset_trail_Click(object sender, EventArgs e)
        {
            trackBar_R.Value = trailR;
            trackBar_G.Value = trailG;
            trackBar_B.Value = trailB;
            ControllerManager.TrailTest(trackBar_R.Value, trackBar_G.Value, trackBar_B.Value);
        }
    }
}
