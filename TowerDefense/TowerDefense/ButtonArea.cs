using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TowerDefense
{
    class ButtonArea
    {
        Size size;
        Point location;
        public Size Size { get { return size; } }
        Rectangle clickBox;
        Placement placement;

        public ButtonArea(Point location, Size size, Placement placement)
        {
            this.size = size;
            this.location = location;
            clickBox.Location = location;
            clickBox.Size = new Size(50, 50);
            this.placement = placement;
        }

        public bool mouseClick(MouseEventArgs e)
        {
            bool addResponse = false;
            if(clickBox.Contains(e.Location))
            {
                addResponse = placement.add(clickBox);
            }
            return addResponse;
        }

        public void paint(PaintEventArgs e)
        {
            Pen redPen = new Pen(Color.Red, 3);
            SolidBrush blueBrush = new SolidBrush(Color.Blue);
            e.Graphics.DrawRectangle(redPen, clickBox);
            e.Graphics.FillRectangle(blueBrush, clickBox);
        }
    }
}
