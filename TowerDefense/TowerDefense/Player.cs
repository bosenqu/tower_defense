using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TowerDefense
{
    class Player
    {
        const int SMALL_TURRENT_COST = 0, MID_TURRENT_COST = 0, MISSILE_COST = 0;
        int money;
        public int Money { get { return money; } }
        int health;
        public int Health { get { return health; } }
        List<Weapons> items = new List<Weapons>();
        public List<Weapons> Items { get { return items; } }

        public Player(int money, int health)
        {
            this.money = money;
            this.health = health;
        }

        public bool addWeapon(Weapons.Type type, Rectangle rect)
        {
            if (type == Weapons.Type.SmallTurrent && money >= SMALL_TURRENT_COST)
            {
                SmallTurrent temp = new SmallTurrent(rect.Location, rect.Size);
                items.Add(temp);
                money -= SMALL_TURRENT_COST;
                return true;
            }
            if (type == Weapons.Type.MidTurrent && money >= MID_TURRENT_COST)
            {
                MidTurrent temp = new MidTurrent(rect.Location, rect.Size);
                items.Add(temp);
                money -= MID_TURRENT_COST;
                return true;
            }
            if (type == Weapons.Type.Missile && money >= MISSILE_COST)
            {
                Missile temp = new Missile(rect.Location, rect.Size);
                items.Add(temp);
                money -= MISSILE_COST;
                return true;
            }
            return false;
        }

        public void paint(PaintEventArgs e)
        {
            foreach (var item in items)
            {
                item.paint(e);
            }
        }
    }
}
