using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Statki.ShipFolder;

namespace Statki
{
    public class Board
    {
        private const int A_CHAR = 'A';

        public int[,] board { get; }
        public List<Ship> ships { get; }

        public Dictionary<Tuple<int, int>, Ship> shipPos { get; }

        public List<Tuple<int, int>> boardHits { get; }

        private ConsoleColor color1 { get; } = ConsoleColor.White;
        private ConsoleColor color2 { get; } = ConsoleColor.Gray;
        private ConsoleColor color3 { get; } = ConsoleColor.DarkBlue;

        public Board()
        {
            board = new int[10, 10];
            ships = new List<Ship>();
            shipPos = new Dictionary<Tuple<int, int>, Ship>();
            boardHits = new List<Tuple<int, int>>();
        }

        public bool AddShip(Ship ship)
        {
            foreach (Tuple<int, int> pos in ship.positions)
            {
                if (shipPos.ContainsKey(pos))
                    return false;


                int x = pos.Item1;
                int y = pos.Item2;

                Tuple<int, int> pos1 = new Tuple<int, int>(x - 1, y);
                Tuple<int, int> pos2 = new Tuple<int, int>(x + 1, y);
                Tuple<int, int> pos3 = new Tuple<int, int>(x, y - 1);
                Tuple<int, int> pos4 = new Tuple<int, int>(x, y + 1);
                if (shipPos.ContainsKey(pos1)
                    || shipPos.ContainsKey(pos2)
                    || shipPos.ContainsKey(pos3)
                    || shipPos.ContainsKey(pos4))
                    return false;

                if (x < 0 || y < 0
                    || x >= board.GetLength(0)
                    || y >= board.GetLength(1))
                    return false;
            }

            ships.Add(ship);
            foreach (Tuple<int, int> pos in ship.positions)
            {
                shipPos.Add(pos, ship);
            }

            ship.board = this;
            return true;
        }

        public bool AreAllShipsDestroyed()
        {
            int counter = 0;
            foreach (Ship ship in ships)
            {
                ++counter;
                if (!ship.IsDestroyed())
                    return false;
            }
            return true;
        }

        public bool Hit(Tuple<int, int> pos)
        {
            if(pos.Item1 < 0 || pos.Item1 >= board.GetLength(0)
                || pos.Item2 < 0 || pos.Item2 >= board.GetLength(1)) 
                return true;

            if (boardHits.Contains(pos))
                return true;

            boardHits.Add(pos);
            if (shipPos.ContainsKey(pos))
            {
                Ship ship = shipPos[pos];
                ship.Hit(pos);
                if (ship.IsDestroyed())
                    DestroyShip(ship);
                return true;
            }

            return false;
        }

        public void DestroyShip(Ship ship)
        {
            if (!ship.IsDestroyed())
                return;
            foreach (Tuple<int, int> shipPos in ship.positions)
            {
                Tuple<int, int> tmpPos = new Tuple<int, int>(shipPos.Item1 - 1, shipPos.Item2);
                if(!boardHits.Contains(tmpPos))
                    boardHits.Add(tmpPos);
                tmpPos = new Tuple<int, int>(shipPos.Item1 + 1, shipPos.Item2);
                if (!boardHits.Contains(tmpPos))
                    boardHits.Add(tmpPos);
                tmpPos = new Tuple<int, int>(shipPos.Item1, shipPos.Item2 - 1);
                if (!boardHits.Contains(tmpPos))
                    boardHits.Add(tmpPos);
                tmpPos = new Tuple<int, int>(shipPos.Item1, shipPos.Item2 + 1);
                if (!boardHits.Contains(tmpPos))
                    boardHits.Add(tmpPos);
            }
        }

        public void PrintHitBoard()
        {
            for(int i = 0; i < board.GetLength(0); i++)
            {
                Console.Write((char)(A_CHAR + i) + " ");
            }
            Console.WriteLine();

            for (int i = board.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Tuple<int, int> tmpPos = new Tuple<int, int>(j, i);

                    if((i+j)%2 == 0)
                    {
                        Console.BackgroundColor = color1;
                    }
                    else
                    {
                        Console.BackgroundColor = color2;
                    }
                    Console.ForegroundColor = color3;

                    if(boardHits.Contains(tmpPos))
                    {
                        if (shipPos.ContainsKey(tmpPos))
                        {
                            if (shipPos[tmpPos].IsHit(tmpPos))
                            {
                                Console.Write("X");
                            } 
                            else
                            {
                                Console.Write(" ");
                            }
                        } 
                        else
                        {
                            Console.Write("*");
                        }
                    } 
                    else
                    {
                        Console.Write(" ");
                    }

                    Console.Write(" ");
                    Console.BackgroundColor = GameManager.Inst.backgroundColor;
                    Console.ForegroundColor = GameManager.Inst.foregroundColor;

                }
                Console.WriteLine(i + 1);
            }
        }

        public void printFullBoard()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                Console.Write((char)(A_CHAR + i) + " ");
            }
            Console.WriteLine();

            for (int i = board.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Tuple<int, int> tmp = new Tuple<int, int>(j, i);

                    if ((i + j) % 2 == 0)
                    {
                        Console.BackgroundColor = color1;
                    }
                    else
                    {
                        Console.BackgroundColor = color2;
                    }
                    Console.ForegroundColor = color3;
                    if (boardHits.Contains((Tuple<int, int>)tmp))
                    {
                        if (shipPos.ContainsKey(tmp))
                        {
                            if (shipPos[tmp].IsHit(tmp))
                            {
                                Console.Write("X");
                            }
                            else
                            {
                                Console.Write("O");
                            }
                        }
                        else
                        {
                            Console.Write("*");
                        }
                    }
                    else
                    {
                        if (shipPos.ContainsKey(tmp))
                        {
                            Console.Write("O");
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }

                    Console.Write(" ");
                    Console.BackgroundColor = GameManager.Inst.backgroundColor;
                    Console.ForegroundColor = GameManager.Inst.foregroundColor;
                }
                Console.WriteLine(i + 1);
            }
        }

    }
}
