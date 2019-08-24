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
        public bool Active { get { return active; } }
        

        public Placement(Point location, Size size, Size tileSize)
        {
            this.tileSize = tileSize;
            mapLocation = location;
            mapSize = size;
        }

        public bool add(Rectangle toAdd)
        {
            if (active) return false;
            tempItem.Location = screenLocation(toAdd.Location, toAdd.Size);
            tempItem.Size = tileSize;
            active = true;
            return true;
        }

        Point screenLocation(Point p, Size itemSize)
        {
            int x = p.X - (p.X - mapLocation.X) % tileSize.Width;
            if (x > mapLocation.X + mapSize.Width - itemSize.Width) x = mapLocation.X + mapSize.Width - itemSize.Width;
            else if (x < mapLocation.X) x = mapLocation.X;

            int y = p.Y - (p.Y - mapLocation.Y) % tileSize.Height;
            if (y > mapLocation.Y + mapSize.Height - itemSize.Height) y = mapLocation.Y + mapSize.Height - itemSize.Height;
            else if (y < mapLocation.Y) y = mapLocation.Y;

            return new Point(x, y);
        }

        bool noItems(Rectangle rect, List<Weapons> items, Map map)
        {
            for (int i = 0; i < items.Count; i++) if (items[i].Rect.IntersectsWith(rect)) return false;
            return map.canPlace(rect);
        }

        public void mouseClick(MouseEventArgs e, Player player, Map map)
        {
            if(active && noItems(tempItem, player.Items, map))
            {
                active = !player.addWeapon(Weapons.Type.SmallTurrent, tempItem);
                map.changeTileType(tempItem, Map.TileType.obstacle);
            }
        }

        public bool mouseMove(MouseEventArgs e)
        {
            if(active)
            {
                tempItem.Location = screenLocation(e.Location, tempItem.Size);
            }
            return active;
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
        }
    }
}
