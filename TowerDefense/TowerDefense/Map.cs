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
        public enum TileType { grass, obstacle, road };
        const int TILE_WIDTH = 50, TILE_HEIGHT = 50;
        public Size TileSize { get { return new Size(TILE_WIDTH, TILE_HEIGHT); } }
        Size size;
        Point location;
        public Rectangle MapRect { get { return new Rectangle(location, new Size(size.Width * TILE_WIDTH, size.Height * TILE_HEIGHT)); } }
        Tile[,] tiles;

        public Map(Point location, Size size)
        {
            this.location = location;
            this.size.Width = size.Width / TILE_WIDTH;
            this.size.Height = size.Height / TILE_HEIGHT;
            tiles = new Tile[size.Height, size.Width];
            generateMap();
        }

        void generateMap()
        {
            for (int i = 0; i < size.Width; i++)
            {
                for (int j = 0; j < size.Height; j++)
                {
                    Rectangle temp = new Rectangle();
                    temp.Size = new Size(TILE_WIDTH, TILE_HEIGHT);
                    temp.Location = new Point(location.X + i * TILE_WIDTH, location.Y + j * TILE_HEIGHT);
                    tiles[i, j] = new Tile(temp, TileType.grass, Properties.Resources.grass);
                }
            }
        }

        public Point mapLocation(Point screenLocation)
        {
            int x = (screenLocation.X - location.X) / TILE_WIDTH;
            if (x < 0) x = 0;
            else if (x > size.Width - 1) x = size.Width - 1;
            int y = (screenLocation.Y - location.Y) / TILE_HEIGHT;
            if (y < 0) y = 0;
            else if (y > size.Height - 1) y = size.Height - 1;
            return new Point(x, y);
        }

        public bool canPlace(Rectangle rect)
        {
            Point mapLocation = this.mapLocation(rect.Location);
            return tiles[mapLocation.X, mapLocation.Y].Type == TileType.grass;

        }

        public TileType changeTileType(Rectangle rect, TileType type)
        {
            Point mapLocation = this.mapLocation(rect.Location);
            return tiles[mapLocation.X, mapLocation.Y].changeType(type);
        }

        public void paint(PaintEventArgs e, bool placeItem)
        {
            //Pen blackPen = new Pen(Color.Black, 1);
            for (int i = 0; i < size.Width; i++)
            {
                for (int j = 0; j < size.Height; j++)
                {
                    //Rectangle rect = new Rectangle(j * TILE_WIDTH, i * TILE_HEIGHT, TILE_WIDTH, TILE_HEIGHT);
                    //e.Graphics.DrawRectangle(blackPen, rect);
                    tiles[i, j].paint(e, placeItem);
                }
            }
        }
    }

    class Tile
    {
        Rectangle rect;
        public Rectangle Rect { get { return rect; } }
        Map.TileType type;
        public Map.TileType Type { get { return type; } }
        Bitmap image;
        public Bitmap Image { get { return image; } }

        public Tile(Rectangle rect, Map.TileType type, Bitmap image)
        {
            this.rect = rect;
            this.type = type;
            this.image = image;
        }

        public Map.TileType changeType(Map.TileType newType)
        {
            Map.TileType originalType = type;
            type = newType;
            return originalType;
        }

        public void paint(PaintEventArgs e, bool placeItem)
        {
            if (placeItem && type == Map.TileType.grass)
            {
                e.Graphics.DrawImage(Properties.Resources.placeItemSign, rect);
            }
            else
            {
                e.Graphics.DrawImage(image, rect);
            }
        }
    }
}
