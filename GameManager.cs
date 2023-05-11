using Statki.ShipFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statki
{
    public class GameManager
    {

        private static GameManager manager = new GameManager();

        private bool inGame;
        private Board playerBoard;
        private Board computerBoard;

        public ConsoleColor backgroundColor { get; set; } 
        public ConsoleColor foregroundColor { get; set; }

        private GameManager() { }

        public static GameManager Inst { get {  return manager; } }

        public void StartGame()
        {
            if (inGame)
                return;
            inGame = true;
            backgroundColor = Console.BackgroundColor; 
            foregroundColor = Console.ForegroundColor;

            playerBoard = new Board();
            computerBoard = new Board();

            bool choice;
            Console.WriteLine("Chcesz samodzielnie ustawic statki? (1 - TAK; 0 - NIE)");
            if (int.Parse(Console.ReadLine()) == 0)
                choice = false;
            else
                choice = true;

            SetPlayerShips(choice);

            SetComputerShips();

            LetTheGameBegins();
        }

        public void LetTheGameBegins()
        {
            //MIEJSCE NA LOGIKE GRY
            while (!playerBoard.AreAllShipsDestroyed() && !computerBoard.AreAllShipsDestroyed())
            {
                int x, y;
                do
                {
                    Console.Clear();
                    PrintPlayBoard();
                    Console.WriteLine();
                    Console.WriteLine("Podaj koordy do strzalu:");
                    Console.Write("x: ");
                    x = char.Parse(Console.ReadLine()) - 'A';
                    Console.Write("y: ");
                    y = int.Parse(Console.ReadLine()) - 1;
                } while (computerBoard.Hit(new Tuple<int, int>(x, y)) && !computerBoard.AreAllShipsDestroyed());

                if (computerBoard.AreAllShipsDestroyed())
                    break;

                Random generator = new Random();
                do
                {
                    x = generator.Next(10);
                    y = generator.Next(10);
                } while (playerBoard.Hit(new Tuple<int, int>(x, y)) && !playerBoard.AreAllShipsDestroyed());
            }


            Console.Clear();
            if (computerBoard.AreAllShipsDestroyed())
            {
                Console.WriteLine("\t\t\t! BRAWO !");
                Console.WriteLine("\tZNISZCZYLES WSZYSTKIE STATKI PRZECIWNIKA");
            } 
            else
            {
                Console.WriteLine("\t\t\t! PRZEGRALES !");
                Console.WriteLine("\tPRZECIWNIK ZNISZCZYL WSZYSTKIE STATKI");
            }
            Console.WriteLine();
            Console.WriteLine();
            PrintPlayBoard();
            Console.ReadLine();
        }

        public void SetPlayerShips(bool choice)
        {
            if (choice)
            {
                for (int i = 0; i < 1; ++i)
                {
                    Ship ship;
                    do
                    {
                        playerBoard.printFullBoard();
                        Console.WriteLine();

                        Console.WriteLine("Podaj koordy Czteromasztowca nr " + (i + 1) + ":");

                        Console.Write("x: ");
                        int x = char.Parse(Console.ReadLine()) - 'A';
                        Console.Write("y: ");
                        int y = int.Parse(Console.ReadLine()) - 1;
                        Console.Write("Ustawic pionowo? (True/False) ");
                        bool vertical = bool.Parse(Console.ReadLine());

                        ship = new FourShip(new Tuple<int, int>(x, y), playerBoard, vertical);

                        Console.Clear();
                    } while (!playerBoard.AddShip(ship));
                }


                for (int i = 0; i < 2; ++i)
                {
                    Ship ship;
                    do
                    {
                        playerBoard.printFullBoard();
                        Console.WriteLine();

                        Console.WriteLine("Podaj koordy Trojmasztowca nr " + (i + 1) + ":");

                        Console.Write("x: ");
                        int x = char.Parse(Console.ReadLine()) - 'A';
                        Console.Write("y: ");
                        int y = int.Parse(Console.ReadLine()) - 1;
                        Console.Write("Ustawic pionowo? (True/False) ");
                        bool vertical = bool.Parse(Console.ReadLine());

                        ship = new ThreeShip(new Tuple<int, int>(x, y), playerBoard, vertical);

                        Console.Clear();
                    } while (!playerBoard.AddShip(ship));
                }

                for (int i = 0; i < 3; ++i)
                {
                    Ship ship;
                    do
                    {
                        playerBoard.printFullBoard();
                        Console.WriteLine();

                        Console.WriteLine("Podaj koordy Dwumasztowca nr " + (i + 1) + ":");

                        Console.Write("x: ");
                        int x = char.Parse(Console.ReadLine()) - 'A';
                        Console.Write("y: ");
                        int y = int.Parse(Console.ReadLine()) - 1;
                        Console.Write("Ustawic pionowo? (True/False) ");
                        bool vertical = bool.Parse(Console.ReadLine());

                        ship = new TwoShip(new Tuple<int, int>(x, y), playerBoard, vertical);

                        Console.Clear();
                    } while (!playerBoard.AddShip(ship));
                }

                for (int i = 0; i < 4; ++i)
                {
                    Ship ship;
                    do
                    {
                        playerBoard.printFullBoard();
                        Console.WriteLine();

                        Console.WriteLine("Podaj koordy Jednomasztowca nr " + (i + 1) + ":");

                        Console.Write("x: ");
                        int x = char.Parse(Console.ReadLine()) - 'A';
                        Console.Write("y: ");
                        int y = int.Parse(Console.ReadLine()) - 1;

                        ship = new OneShip(new Tuple<int, int>(x, y), playerBoard);

                        Console.Clear();
                    } while (!playerBoard.AddShip(ship));
                }
            }
            else
            {
                Random generator = new Random();

                for (int i = 0; i < 1; ++i)
                {
                    Ship ship;
                    do
                    {
                        int x = generator.Next(10);
                        int y = generator.Next(10);
                        int los = generator.Next(2);
                        bool vertical = los == 1 ? true : false;
                        ship = new FourShip(new Tuple<int, int>(x, y), playerBoard, vertical);
                    } while (!playerBoard.AddShip(ship));
                }

                for (int i = 0; i < 2; ++i)
                {
                    Ship ship;
                    do
                    {
                        int x = generator.Next(10);
                        int y = generator.Next(10);
                        int los = generator.Next(2);
                        bool vertical = los == 1 ? true : false;
                        ship = new ThreeShip(new Tuple<int, int>(x, y), playerBoard, vertical);
                    } while (!playerBoard.AddShip(ship));
                }

                for (int i = 0; i < 3; ++i)
                {
                    Ship ship;
                    do
                    {
                        int x = generator.Next(10);
                        int y = generator.Next(10);
                        int los = generator.Next(2);
                        bool vertical = los == 1 ? true : false;
                        ship = new TwoShip(new Tuple<int, int>(x, y), playerBoard, vertical);
                    } while (!playerBoard.AddShip(ship));
                }

                for (int i = 0; i < 4; ++i)
                {
                    Ship ship;
                    do
                    {
                        int x = generator.Next(10);
                        int y = generator.Next(10);
                        ship = new OneShip(new Tuple<int, int>(x, y), playerBoard);
                    } while (!playerBoard.AddShip(ship));
                }
            }
            
        }

        public void SetComputerShips()
        {
            Random generator = new Random();

            for(int i = 0; i < 1; ++i)
            {
                Ship ship;
                do
                {
                    int x = generator.Next(10);
                    int y = generator.Next(10);
                    int los = generator.Next(2);
                    bool vertical = los == 1 ? true : false;
                    ship = new FourShip(new Tuple<int, int>(x, y), computerBoard, vertical);
                } while (!computerBoard.AddShip(ship));
            }

            for (int i = 0; i < 2; ++i)
            {
                Ship ship;
                do
                {
                    int x = generator.Next(10);
                    int y = generator.Next(10);
                    int los = generator.Next(2);
                    bool vertical = los == 1 ? true : false;
                    ship = new ThreeShip(new Tuple<int, int>(x, y), computerBoard, vertical);
                } while (!computerBoard.AddShip(ship));
            }

            for (int i = 0; i < 3; ++i)
            {
                Ship ship;
                do
                {
                    int x = generator.Next(10);
                    int y = generator.Next(10);
                    int los = generator.Next(2);
                    bool vertical = los == 1 ? true : false;
                    ship = new TwoShip(new Tuple<int, int>(x, y), computerBoard, vertical);
                } while (!computerBoard.AddShip(ship));
            }

            for (int i = 0; i < 4; ++i)
            {
                Ship ship;
                do
                {
                    int x = generator.Next(10);
                    int y = generator.Next(10);
                    ship = new OneShip(new Tuple<int, int>(x, y), computerBoard);
                } while (!computerBoard.AddShip(ship));
            }
        }

        public void PrintPlayBoard()
        {
            Console.WriteLine("\tTY");
            Console.WriteLine();
            playerBoard.printFullBoard();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\tPRZECIWNIK");
            Console.WriteLine();
            computerBoard.PrintHitBoard();
        }

    }
}
