using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TowerDefense
{
    class Map
    {
        const int TILE_WIDTH = 50, TILE_HEIGHT = 50;
        public Size TileSize { get { return new Size(TILE_WIDTH, TILE_HEIGHT); } }
        Size size;
        Point location;
        public Rectangle MapRect { get { return new Rectangle(location, new Size(size.Width * TILE_WIDTH, size.Height * TILE_HEIGHT)); } }

        public Map(Point location, Size size)
        {
            this.location = location;
            this.size.Width = size.Width / TILE_WIDTH;
            this.size.Height = size.Height / TILE_HEIGHT; 
        }

        public void paint(PaintEventArgs e)
        {
            Pen blackPen = new Pen(Color.Black, 1);

            for(int i = 0; i < size.Height; i++)
            {
                for(int j = 0; j < size.Width; j++)
                {
                    Rectangle rect = new Rectangle(j * TILE_WIDTH, i * TILE_HEIGHT, TILE_WIDTH, TILE_HEIGHT);
                    e.Graphics.DrawRectangle(blackPen, rect);
                }
            }
        }
    }
}
