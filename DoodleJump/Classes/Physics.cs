using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoodleJump.Classes
{
    public class Physics
    {
        public Transform transform;
        float gravity;
        float a;

        public float dx;

        public Physics(PointF position,Size size)
        {
            transform = new Transform(position, size);
            gravity = 0;
            a = 0.4f;
            dx = 0;
        }

        public void ApplyPhysics()
        {
            CalculatePhysics();
        }

        public void CalculatePhysics()
        {

            if (dx != 0)
            {
                transform.position.X += dx;
            }

            if (transform.position.Y < 700 || a < 0)
            {
                transform.position.Y += gravity;

                gravity += a;
                Collide();
            }
        }

        public void Collide()
        {
            for (int i = 0; i < PlatformsController.platforms.Count; i++)
            {
                var platform = PlatformsController.platforms[i];
                if(transform.position.X+ transform.size.Width/2>=platform.transform.position.X && transform.position.X + transform.size.Width / 2 <= platform.transform.position.X + platform.sizeX)
                {
                    if (transform.position.Y+ transform.size.Height >= platform.transform.position.Y && transform.position.Y + transform.size.Height <= platform.transform.position.Y+platform.sizeY)
                    {
                        if (gravity > 0)
                        {
                            AddForce(a / 2);
                            if (!platform.isTouchedByPlayer)
                            {
                                PlatformsController.GenerateRandomPlatform();
                                platform.isTouchedByPlayer = true;
                            }
                        }
                    }
                }
            }
        }

        public void AddForce(float forceValue)
        {
            gravity -= a;
            gravity = -10;
        }
    }
}
