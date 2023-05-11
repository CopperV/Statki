using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statki.ShipFolder
{
    public class OneShip : Ship
    {

        public OneShip(Tuple<int, int> startPos, Board board) : base()
        {
            positions.Add(startPos);
            hits.Add(new ShipPosition(startPos.Item1, startPos.Item2));
        }

        public override bool IsDestroyed()
        {
            foreach (ShipPosition hit in hits)
            {
                if (!hit.hit)
                    return false;
            }
            return true;
        }

        public override bool IsHit(Tuple<int, int> pos)
        {
            if (!positions.Contains(pos))
                return false;
            foreach (ShipPosition hit in hits)
            {
                if (hit.x != pos.Item1 || hit.y != pos.Item2)
                    continue;
                return hit.hit;
            }
            return false;
        }

    }
}
