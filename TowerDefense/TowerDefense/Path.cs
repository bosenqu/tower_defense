using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TowerDefense
{
    class Path
    {
        public enum Direction { Up, Down, Left, Right, None };
        Point enterLocation;
        public Point EnterLocation { get { return enterLocation; } }
        Direction enterDirection;
        public Direction EnterDirection { get { return enterDirection; } }
        Point exitLocation;
        public Point ExitLocation { get { return exitLocation; } }
        List<TurningPoint> turningPoints;
        public List<TurningPoint> TurningPoints { get { return turningPoints; } }

        public Path(Point enterLocation, Direction enterDirection, Point exitLocation, List<TurningPoint> turningPoints)
        {
            this.enterDirection = enterDirection;
            this.enterLocation = enterLocation;
            this.exitLocation = exitLocation;
            this.turningPoints = turningPoints;
        }

        public Direction currentDirection(Rectangle rect)
        {
            foreach (var tp in turningPoints)
            {
                if (rect.Contains(tp.ScreenLocation))
                {
                    return tp.NewDirection;
                }
            }
            return Direction.None;
        }

        public bool reachEndPath(Rectangle rect)
        {
            Point pt;
            Direction dir;
            if (turningPoints.Count == 0)
            {
                pt = enterLocation;
                dir = enterDirection;
            }
            else
            {
                pt = turningPoints[turningPoints.Count - 1].ScreenLocation;
                dir = turningPoints[turningPoints.Count - 1].NewDirection;
            }
            switch (dir)
            {
                case Direction.Down:
                    return rect.X >= pt.X && rect.X + rect.Width <= pt.X && rect.Y > pt.Y;
                case Direction.Up:
                    return rect.X >= pt.X && rect.X + rect.Width <= pt.X && rect.Y + rect.Height <= pt.Y;
                case Direction.Left:
                    return rect.Y >= pt.Y && rect.Y + rect.Height <= pt.Y && rect.X + rect.Width <= pt.X;
                case Direction.Right:
                    return rect.Y >= pt.Y && rect.Y + rect.Height <= pt.Y && rect.X > pt.X;
                default:
                    break;
            }
            return false;
        }
    }

    class TurningPoint
    {
        Path.Direction newDirection;
        public Path.Direction NewDirection { get { return newDirection; } }
        Point screenLocation;
        public Point ScreenLocation { get { return screenLocation; } }

        public TurningPoint(Point screenLocation, Path.Direction newDirection)
        {
            this.newDirection = newDirection;
            this.screenLocation = screenLocation;
        }
    }
}
