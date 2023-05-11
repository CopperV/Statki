using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statki.ShipFolder
{
    public abstract class Ship
    {
        public List<Tuple<int, int>> positions { get; }
        public List<ShipPosition> hits { get; }
        public Board? board { get; set; } = null;

        public Ship()
        {
            hits = new List<ShipPosition>();
            positions = new List<Tuple<int, int>>();
        }


        public abstract bool IsHit(Tuple<int, int> pos);
        public abstract bool IsDestroyed();
        public bool Hit(Tuple<int, int> pos)
        {
            foreach(ShipPosition shipPosition in hits)
            {
                if (shipPosition.x != pos.Item1 || shipPosition.y != pos.Item2)
                    continue;
                if (shipPosition.hit)
                    return false;
                shipPosition.hit = true;
                return true;
            }
            return false;
        }

        public class ShipPosition
        {
            public int x { get; }
            public int y { get; }
            public bool hit { get; set; }

            public ShipPosition(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
    }
}
