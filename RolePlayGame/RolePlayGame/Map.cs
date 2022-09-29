using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace RolePlayGame
{
    internal class Map
    {
        private static readonly string[,] map = new string[25, 25];
        private static readonly int size = (int)Math.Sqrt(map.Length);
        private static Enemy[] e;
        private static Player[] PP = null;
        private static readonly string[] stats = new string[9] { "Name: ", "Race: ", "Age: ", "Gender: ", "HP: ", "Mana: ", "EXP: ", "Damage: ", "Current state: " };
        private static void GenerationMap()
        {
            for (int i = 0; i < size; ++i)
                for (int j = 0; j < size; ++j)
                    map[i, j] = j == 0 || i == 0 || j == size - 1 || i == size - 1 ? "X" : "O";
            Random random = new();
            e = new Enemy[random.Next(2, 10)];
            for (int i = 0; i < e.Length; i++)
            {
                e[i] = new Enemy
                {
                    PosX = random.Next(2, size - 2),
                    PosY = random.Next(2, size - 2)
                };
            }
        }

        private static void DisplayStats(Player p)
        {
            string[] FixStr = new string[4] { p.Name.ToString(), p.Race.ToString(), p.Age.ToString(), p.Gender.ToString() };
            Console.SetCursorPosition(size + 73, 2);
            Console.Write("Your Hero");
            for (int i = 0; i < stats.Length; i++)
            {
                Console.SetCursorPosition(size + 66, i + 3);
                if (i < 4)
                {
                    Console.Write(stats[i] + FixStr[i]);
                }
                if (i == 4)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(stats[i]);
                    Console.Write((p.HP * 10) + "%");
                    Console.ResetColor();
                }
                if (i == 5)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(stats[i]);
                    Console.Write((p.Mana * 10) + "%");
                    Console.ResetColor();
                }
                if (i == 6)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(stats[i] + Math.Round(p.EXP));
                    Console.ResetColor();
                    Console.SetCursorPosition(Console.CursorLeft + 3, Console.CursorTop);
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write("Level: " + p.Level);
                    Console.ResetColor();

                }
                if (i == 7)
                    Console.Write(stats[i] + p.Damage);
                if (i == 8)
                {
                    if (p.HP < 10)
                        p.Setstate((State)1);
                    else if (p.HP <= 0)
                        p.Setstate((State)5);
                    Console.Write(stats[i] + p.Getstate());
                }

            }
            for (int i = 1; i < size - 12; ++i)
            {

                for (int j = 0; j < size; ++j)
                {
                    Console.SetCursorPosition(size + 65 + j, i);
                    if ((i == 1 || i == size - 1 - 12) && j != 0 && j != size - 1)
                        Console.Write("_");
                    else if ((j == 0 || j == size - 1) && (i != 1))
                        Console.Write("|");

                }
            }
        }
        private static bool RandomChest()
        {
            Random random = new();
            return random.Next(0, 1000) < 2;
        }
        public static void DisplayMap(Player p, Player[] P, int index)
        {
            BinaryFormatter formatter = new();
            Console.SetBufferSize(120, 30);
            GenerationMap();
            int CursorTop = Console.CursorTop;
            Random random = new();
            int NPX = random.Next(2, size - 2), NPY = random.Next(2, size - 2);
            int CountMoveHero = 0;
            bool running = false;
            bool SpawnChest = true;
            while (true)
            {

                HeroOnMap(p, e, ref NPX, ref NPY, ref CountMoveHero);
                if (CountMoveHero == 3)
                {
                    if (p.Mana < 10)
                        p.Mana += 1;
                    for (int i = 0; i < e.Length; i++)
                    {
                        running = true;
                        CountMoveHero = 0;
                        EnemyOnMap(e[i], p, ref running);
                        if (e[i].HP <= 0)
                            e[i].Live = false;
                    }
                }
                Console.Clear();
                DisplayStats(p);
                for (int i = 0; i < size; ++i)
                {
                    Console.SetCursorPosition(3, ++CursorTop);
                    for (int j = 0; j < size; ++j)
                    {

                        if ((j == 0 || i == 0 || j == size - 1 || i == size - 1) && map[i, j] != "H" && map[i, j] != "E")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(map[i, j] + " ");

                        }
                        else if (map[i, j] == "H")
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(map[i, j] + " ");
                        }
                        else if (map[i, j] == "E")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write(map[i, j] + " ");
                        }
                        else if (map[i, j] == "C")
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(map[i, j] + " ");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(map[i, j] + " ");
                            if (SpawnChest)
                            {
                                if (RandomChest())
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    map[i, j] = "C";
                                    SpawnChest = false;
                                    Console.ResetColor();
                                }
                            }
                        }
                        Console.ResetColor();
                    }
                    Console.WriteLine();
                }
                CursorTop = 0;
                using FileStream fs = new("Hero.dat", FileMode.OpenOrCreate);
                P[index] = p;
                PP = P;
                formatter.Serialize(fs, P);
            }
        }
        private static void HeroOnMap(Player p, Enemy[] e, ref int PosX, ref int PosY, ref int CountMove)
        {
            BinaryFormatter formatter = new();
            int Move = Player.Control();
            ++CountMove;
            if (p.Live)
            {
                if (Move == 1)
                    if (PosX > 1)
                        if (map[PosX - 1, PosY] is not "E" and not "C")
                        {
                            PosX = --PosX;
                            map[PosX, PosY] = "H";
                            map[PosX + 1, PosY] = "O";
                        }
                if (Move == 2)
                    if (PosX < size - 2)
                        if (map[PosX + 1, PosY] is not "E" and not "C")
                        {
                            PosX = ++PosX;
                            map[PosX, PosY] = "H";
                            map[PosX - 1, PosY] = "O";
                        }
                if (Move == 3)
                    if (PosY > 1)
                        if (map[PosX, PosY - 1] is not "E" and not "C")
                        {
                            PosY = --PosY;
                            map[PosX, PosY] = "H";
                            map[PosX, PosY + 1] = "O";
                        }
                if (Move == 4)
                    if (PosY < size - 2)
                        if (map[PosX, PosY + 1] is not "E" and not "C")
                        {
                            PosY = ++PosY;
                            map[PosX, PosY] = "H";
                            map[PosX, PosY - 1] = "O";
                        }
                if (Move == 5)
                    if (map[PosX - 1, PosY] == "E" || map[PosX + 1, PosY] == "E" || map[PosX, PosY - 1] == "E" || map[PosX, PosY + 1] == "E")
                        for (int i = 0; i < e.Length; i++)
                        {
                            if (e[i].PosX == PosX && e[i].PosY == PosY + 1)
                                Attachment(p, e[i], 1);
                            if (e[i].PosX + 1 == PosX && e[i].PosY == PosY)
                                Attachment(p, e[i], 1);
                            if (e[i].PosX - 1 == PosX && e[i].PosY == PosY)
                                Attachment(p, e[i], 1);
                            if (e[i].PosX == PosX && e[i].PosY == PosY - 1)
                                Attachment(p, e[i], 1);
                        }
                    else
                    {
                        if (map[PosX - 1, PosY] == "C")
                        {
                            Player.OpenChest();
                            map[PosX - 1, PosY] = "O";
                        }

                        if (map[PosX + 1, PosY] == "C")
                        {
                            Player.OpenChest();
                            map[PosX + 1, PosY] = "O";
                        }

                        if (map[PosX, PosY - 1] == "C")
                        {
                            Player.OpenChest();
                            map[PosX, PosY - 1] = "O";
                        }

                        if (map[PosX, PosY + 1] == "C")
                        {
                            Player.OpenChest();
                            map[PosX, PosY + 1] = "O";
                        }
                    }
                if (Move == 6)
                {
                    using (FileStream fs = new("Hero.dat", FileMode.OpenOrCreate))
                    {
                        //сериализуем весь массив people
                        formatter.Serialize(fs, PP);
                    }
                    Menu._Menu();
                }
                if (Move == 7)
                    p.PrintInventory(false, "");
            }
            else
            {
                map[PosX, PosY] = "O";
                Console.SetCursorPosition(size + 40, 5);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Your Hero is Dead!");
                Console.SetCursorPosition(size + 28, 6);
                Console.Write("Please press any key to exit the menu");
                Thread.Sleep(1000);
                _ = Console.ReadKey();
                Menu._Menu();
            }
        }
        private static void Attachment(Player p, Enemy e, int Who)
        {
            Random random = new();
            if (Who == 2)
                if (p.HP > 0)
                {
                    int d = random.Next(1, 4);
                    p.HP -= d;
                    Console.SetCursorPosition(size + 40, 5);
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write("-" + d + " hp of Hero");
                    Console.ResetColor();
                    Thread.Sleep(500);
                }
                else
                {
                    p.HP = -1;
                    p.Live = false;

                }
            else if (Who == 1)
                if (e.HP > 0)
                    if (p.Mana > 0)
                        if ((int)Math.Round((double)(p.Damage / 2)) < p.Mana)
                        {
                            e.HP -= p.Damage;
                            p.Mana -= (int)Math.Round((double)(p.Damage / 2));
                            Console.SetCursorPosition(size + 40, 4);
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("-" + p.Damage + " hp of Enemy");
                            Console.ResetColor();
                            Thread.Sleep(500);
                            if (e.HP <= 0)
                            {
                                p.Exp();
                                int r = random.Next(0, 11);
                                if (r > 5)
                                {
                                    p.Inventory(random.Next(1, 5), false);
                                    Console.SetCursorPosition(size + 40, 4);
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write("New item has been added");

                                    Console.ResetColor();
                                    Thread.Sleep(1000);
                                }
                            }
                        }
                        else
                            e.Live = false;
        }
        private static void EnemyOnMap(Enemy e, Player p, ref bool running)
        {
            int Move = e.Control(ref running);
            if (e.Live)
            {
                if (map[e.PosX - 1, e.PosY] == "H" || map[e.PosX + 1, e.PosY] == "H" || map[e.PosX, e.PosY - 1] == "H" || map[e.PosX, e.PosY + 1] == "H")
                {
                    Random random = new();
                    if (random.Next(0, 100) < 51)
                        Attachment(p, e, 2);
                }
                if (Move == 1)
                    if (e.PosX > 1)
                        if (map[e.PosX - 1, e.PosY] != "H" && map[e.PosX + 1, e.PosY] != "E")
                        {
                            e.PosX = --e.PosX;
                            map[e.PosX, e.PosY] = "E";
                            map[e.PosX + 1, e.PosY] = "O";
                        }
                        else
                            Attachment(p, e, 2);
                if (Move == 2)
                    if (e.PosX < size - 2)
                        if (map[e.PosX + 1, e.PosY] is not "H" and not "E")
                        {
                            e.PosX = ++e.PosX;
                            map[e.PosX, e.PosY] = "E";
                            map[e.PosX - 1, e.PosY] = "O";
                        }
                        else
                            Attachment(p, e, 2);
                if (Move == 3)
                    if (e.PosY > 1)
                        if (map[e.PosX, e.PosY - 1] != "H" && map[e.PosX + 1, e.PosY] != "E")
                        {
                            e.PosY = --e.PosY;
                            map[e.PosX, e.PosY] = "E";
                            map[e.PosX, e.PosY + 1] = "O";
                        }
                        else
                            Attachment(p, e, 2);
                if (Move == 4)
                    if (e.PosY < size - 2)
                        if (map[e.PosX, e.PosY + 1] != "H" && map[e.PosX + 1, e.PosY] != "E")
                        {
                            e.PosY = ++e.PosY;
                            map[e.PosX, e.PosY] = "E";
                            map[e.PosX, e.PosY - 1] = "O";
                        }
                        else
                            Attachment(p, e, 2);
            }
            else
                map[e.PosX, e.PosY] = "O";
        }
    }
}
