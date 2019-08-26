using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TowerDefense
{
    class Graphics
    {
        public static Point center(Rectangle rect)
        {
            int x = rect.X + rect.Width / 2;
            int y = rect.Y + rect.Height / 2;
            return new Point(x, y);
        }
    }
}
