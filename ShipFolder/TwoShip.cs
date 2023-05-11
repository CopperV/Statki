using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statki.ShipFolder
{
    public class TwoShip : Ship
    {

        public TwoShip(Tuple<int, int> startPos, Board board, bool vertical) : base()
        {

            positions.Add(startPos);
            hits.Add(new ShipPosition(startPos.Item1, startPos.Item2));
            for (int i = 1; i < 2; ++i)
            {
                Tuple<int, int> nextPos;
                if (vertical)
                {
                    nextPos = new Tuple<int, int>(startPos.Item1, startPos.Item2 - i);
                }
                else
                {
                    nextPos = new Tuple<int, int>(startPos.Item1 + i, startPos.Item2);
                }
                positions.Add(nextPos);
                hits.Add(new ShipPosition(nextPos.Item1, nextPos.Item2));
            }
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
