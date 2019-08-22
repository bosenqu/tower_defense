using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TowerDefense
{
    class Placement
    {
        //info about map
        Size tileSize;
        Size mapSize;
        Point mapLocation;
        //moving item
        bool active = false;
        Rectangle tempItem;
        //all the items
        List<Rectangle> items = new List<Rectangle>();

        public Placement(Point location, Size size, Size tileSize)
        {
            this.tileSize = tileSize;
            mapLocation = location;
            mapSize = size;
        }

        public bool add(Rectangle toAdd)
        {
            if (active) return false;
            tempItem.Location = placementLocation(toAdd.Location);
            tempItem.Size = tileSize;
            active = true;
            return true;
        }

        Point placementLocation(Point p)
        {
            int x = p.X - (p.X - mapLocation.X) % tileSize.Width;
            if (x > mapLocation.X + mapSize.Width - tileSize.Width) x = mapLocation.X + mapSize.Width - tileSize.Width;
            else if (x < mapLocation.X) x = mapLocation.X;

            int y = p.Y - (p.Y - mapLocation.Y) % tileSize.Height;
            if (y > mapLocation.Y + mapSize.Height - tileSize.Height) y = mapLocation.Y + mapSize.Height - tileSize.Height;
            else if (y < mapLocation.Y) y = mapLocation.Y;

            return new Point(x, y);
        }

        bool noItems(Point p)
        {
            for (int i = 0; i < items.Count; i++) if (items[i].Contains(p)) return false;
            return true;
        }

        public void mouseClick(MouseEventArgs e, bool addResponse)
        {
            if(active && noItems(tempItem.Location) && addResponse == false)
            {
                items.Add(tempItem);
                active = false;
            }
        }

        public Point mouseMove(MouseEventArgs e)
        {
            if(active)
            {
                tempItem.Location = placementLocation(e.Location);
            }
            return tempItem.Location;
        }

        public void paint(PaintEventArgs e)
        {
            Pen redPen = new Pen(Color.Red, 3);
            SolidBrush blueBrush = new SolidBrush(Color.Blue);
            if (active)
            {
                e.Graphics.DrawRectangle(redPen, tempItem);
                e.Graphics.FillRectangle(blueBrush, tempItem);
            }

            for(int i = 0; i < items.Count; i++)
            {
                e.Graphics.DrawRectangle(redPen, items[i]);
                e.Graphics.FillRectangle(blueBrush, items[i]);
            }
        }
    }
}
