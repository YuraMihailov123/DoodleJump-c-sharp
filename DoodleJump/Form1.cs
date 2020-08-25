using DoodleJump.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoodleJump
{
    public partial class Form1 : Form
    {
        Player player;
        Timer timer1;
        public Form1()
        {
            InitializeComponent();
            Init();
            timer1 = new Timer();
            timer1.Interval = 15;
            timer1.Tick += new EventHandler(Update);
            timer1.Start();
            this.KeyDown += new KeyEventHandler(OnKeyboardPressed);
            this.KeyUp += new KeyEventHandler(OnKeyboardUp);
            this.BackgroundImage = Properties.Resources.back;
            this.Text = "Doodle Jump";
            this.Height = 600;
            this.Width = 330;
            this.Paint += new PaintEventHandler(OnRepaint);
        }

        public void Init()
        {
            PlatformsController.platforms = new List<Platform>();
            PlatformsController.AddPlatform(new PointF(100, 300));
            PlatformsController.AddPlatform(new PointF(160, 300));
            PlatformsController.AddPlatform(new PointF(160, 250));
            player = new Player();
        }

        private void OnKeyboardUp(object sender, KeyEventArgs e)
        {
            player.physics.dx = 0;
        }

        private void OnKeyboardPressed(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "Right":
                    player.physics.dx = 5;
                    break;
                case "Left":
                    player.physics.dx = -5;
                    break;
            }
        }

        private void Update(object sender, EventArgs e)
        {
            this.Text = player.physics.transform.position.X.ToString() + " - " + player.physics.transform.position.Y.ToString();
            if (player.physics.transform.position.Y >= 700)
                Init();
            player.physics.ApplyPhysics();
            
            
            Invalidate();
        }

        private void OnRepaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (PlatformsController.platforms.Count > 0)
            {
                for (int i = 0; i < PlatformsController.platforms.Count; i++)
                    PlatformsController.platforms[i].DrawSprite(g);
            }
            player.DrawSprite(g);
        }
    }
}
