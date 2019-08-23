using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TowerDefense
{
    class Weapons
    {
        public enum Type { SmallTurrent, MidTurrent, Missile};
        Rectangle rect;
        public Rectangle Rect { get { return rect; } }

        public Weapons(Point location, Size size)
        {
            rect.Location = location;
            rect.Size = size;
        }

        public virtual void paint(PaintEventArgs e)
        {
            Pen redPen = new Pen(Color.Red, 3);
            SolidBrush blueBrush = new SolidBrush(Color.Blue);
            e.Graphics.DrawRectangle(redPen, rect);
            e.Graphics.FillRectangle(blueBrush, rect);
        }
    }

    class SmallTurrent : Weapons
    {
        public SmallTurrent(Point location, Size size) : base(location, size)
        {

        }
    }

    class MidTurrent : Weapons
    {
        public MidTurrent(Point location, Size size) : base(location, size)
        {

        }
    }

    class Missile : Weapons
    {
        public Missile(Point location, Size size) : base(location, size)
        {

        }
    }
}
