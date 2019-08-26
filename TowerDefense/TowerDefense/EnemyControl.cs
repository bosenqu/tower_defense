using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TowerDefense
{
    class EnemyControl
    {
        List<Enemy> enemies;
        public List<Enemy> Enemies { get { return enemies; } }
        int hitDownMoney;

        public EnemyControl(Point startLocation, Size enemySize, int enemyCount, int enemyDistance, 
                            int enemyHealth, Path.Direction startDirection, int hitDownMoney, int speed)
        {
            this.hitDownMoney = hitDownMoney;
            for (int i = 0; i < enemyCount; i++)
            {
                Point tempLoc = new Point(0, 0);
                Enemy tempEnemy;
                switch (startDirection)
                {      
                    case Path.Direction.Down:
                        tempLoc = new Point(startLocation.X - enemySize.Width / 2, startLocation.Y - enemyDistance * i);               
                        break;
                    case Path.Direction.Up:
                        tempLoc = new Point(startLocation.X - enemySize.Width / 2, startLocation.Y + enemyDistance * i);
                        break;
                    case Path.Direction.Left:
                        tempLoc = new Point(startLocation.X + enemyDistance * i, startLocation.Y - enemySize.Height / 2);      
                        break;
                    case Path.Direction.Right:
                        tempLoc = new Point(startLocation.X - enemyDistance * i, startLocation.Y - enemySize.Height / 2);
                        break;
                    default:
                        break;
                }
                tempEnemy = new Enemy(new Rectangle(tempLoc, enemySize), enemyHealth, speed, startDirection);
                enemies.Add(tempEnemy);
            }
        }

        public void timerSetting(Map map, Player player)
        {
            foreach (var enemy in enemies)
            {
                if (enemy.Health <= 0)
                {
                    enemies.Remove(enemy);
                    player.addMoney(hitDownMoney);
                }
                else if (enemy.InMap && !map.inMap(enemy.Rect))
                {
                    enemies.Remove(enemy);
                    player.loseOneHealth();
                }
                else
                {
                    enemy.timerSetting(map);
                }
            }
        }

        public void paint(PaintEventArgs e, Map map)
        {
            foreach (var enemy in enemies)
            {
                enemy.paint(e, map);
            }
        }
    }

    class Enemy
    {
        Rectangle rect;
        public Rectangle Rect { get { return rect; } }
        int health;
        public int Health { get { return health; } }
        Image image;
        bool inMap = false;
        public bool InMap { get { return inMap; } }
        Path.Direction currentDirection;
        int speed;

        public Enemy(Rectangle rect, int health, int speed, Path.Direction startDirection)
        {
            this.rect = rect;
            this.health = health;
            this.speed = speed;
            currentDirection = startDirection;
        }

        void move()
        {
            switch(currentDirection)
            {
                case Path.Direction.Down:
                    rect.Y += speed;
                    break;
                case Path.Direction.Up:
                    rect.Y += speed;
                    break;
                case Path.Direction.Left:
                    rect.X -= speed;
                    break;
                case Path.Direction.Right:
                    rect.X += speed;
                    break;
                default:
                    break;                 
            }
        }

        public void timerSetting(Map map)
        {
            // update inMap variable
            if (!inMap) inMap = map.inMap(rect);
            // check if need to change direction
            Path.Direction tempDirection = map.Path.currentDirection(rect, currentDirection);
            if (tempDirection != Path.Direction.None) currentDirection = tempDirection;
            // move the enemy
            move();
        }

        public void paint(PaintEventArgs e, Map map)
        {
            if (!inMap) return;
            
        }
    }
}
