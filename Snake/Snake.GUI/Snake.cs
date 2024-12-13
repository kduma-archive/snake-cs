using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Snake.Engine;

namespace Snake.GUI
{
    public partial class Snake : Form
    {
        public Game Game { get; set; }

        public Snake()
        {
            InitializeComponent();

            Game = new Game(25, 25, new WinFormsRenderer(this), new WinFormsInputReceiver(this));
        }

        private void Snake_Shown(object sender, EventArgs e)
        {
            Game.RunBeforeLoop();
            timer.Interval = Game.Timeout;
            timer.Enabled = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Game.RunLoop();
            timer.Interval = Game.Timeout;

            if (!Game.Failed) return;

            timer.Enabled = false;
            Game.RunAfterLoop();
        }

        public Panel Panel
        {
            get { return panel; }
        }

        public int Score
        {
            get { return Convert.ToInt32(currentScoreToolStripStatusLabel.Text); }
            set { currentScoreToolStripStatusLabel.Text = value.ToString(CultureInfo.InvariantCulture); }
        }

        public int Speed
        {
            get { return Convert.ToInt32(currentTimeoutToolStripStatusLabel.Text); }
            set { currentTimeoutToolStripStatusLabel.Text = value.ToString(CultureInfo.InvariantCulture); }
        }
    }
}
