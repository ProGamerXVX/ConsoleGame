using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace RolePlayGame
{
    internal class BuildPlayer
    {
        private delegate void _menuBuildPlayer(ref Player player);
        private static readonly bool ActiveBuildMenuPlayer = true;
        private static readonly string[] _BuildMenuPlayer = new string[] { "{Empty player}", "{Empty player}", "{Empty player}" };
        public static Player[] savePlayers = new Player[3] { new Player(), new Player(), new Player() };
        public static int PIndex = 0;
        public static bool FirstOpen = false;
        private static int CompletePoint = 0;
        public static bool YesOrNo(string YourChooseStr)
        {
            Console.Clear();
            string[] Race = new string[3] { " ", "[Yes]", "[No]" };
            bool[] YesNot = new bool[2] { true, false };
            char[] MyChar = { '=', '<' };
            int top = Console.CursorTop;
            bool Com;
            while (true)
            {
                Console.Write(YourChooseStr + "\n");
                for (int i = 1; i < Race.Length; ++i)
                    Console.WriteLine(Race[i]);
                ConsoleKeyInfo keyN = Console.ReadKey();
                Console.SetCursorPosition(10, top);
                if (keyN.Key == ConsoleKey.UpArrow)
                {
                    Console.CursorVisible = false;
                    top -= 1;
                    if (top > 0)
                    {
                        Console.SetCursorPosition(10, top);
                        Console.CursorTop = top;
                    }
                    else
                        top = 1;
                    if (Console.CursorTop > 0)
                    {
                        if (Console.CursorTop == 1)
                            Race[Console.CursorTop] = Race[Console.CursorTop].TrimEnd(MyChar);
                        Race[Console.CursorTop] += "<==";
                        Race[Console.CursorTop + 1] = Race[Console.CursorTop + 1].TrimEnd(MyChar);
                        top = Console.CursorTop;
                    }
                    else
                        Console.CursorTop = 1;
                }
                if (keyN.Key == ConsoleKey.DownArrow)
                {

                    Console.CursorVisible = false;
                    Console.SetCursorPosition(10, Console.CursorTop + 1);
                    if (Console.CursorTop < 3)
                    {
                        Race[Console.CursorTop] += "<==";
                        Race[Console.CursorTop - 1] = Race[Console.CursorTop - 1].TrimEnd(MyChar);
                        top = Console.CursorTop;
                    }
                    else
                        Console.CursorTop = 3;
                }
                if (keyN.Key == ConsoleKey.Enter)
                {
                    Com = YesNot[Console.CursorTop - 1];
                    break;
                }
                Console.Clear();
            }
            return Com;
        }
        private static void BuildNamePlayer(ref Player player)
        {
            CompletePoint++;
            Console.Clear();
            Console.Write("Name your Hero:");
            string str = Console.ReadLine();
            player.Name = str;
        }
        private static void BuildRacePlayer(ref Player player)
        {
            CompletePoint++;
            Console.Clear();
            Console.WriteLine("Please select race player\n");
            Console.WriteLine("|Press any key|");
            _ = Console.ReadKey();
            Console.Clear();
            string[] Race = new string[4] { "[Human]", "[Gnom]", "[Elf]", "[Goblin]" };
            string[] _Race = new string[4] { "Human", "Gnom", "Elf", "Goblin" };
            char[] MyChar = { '=', '<' };
            int top = Console.CursorTop;
            while (true)
            {
                for (int i = 0; i < Race.Length; ++i)
                    Console.WriteLine(Race[i]);
                ConsoleKeyInfo keyN = Console.ReadKey();
                Console.SetCursorPosition(10, top);
                if (keyN.Key == ConsoleKey.UpArrow)
                {
                    Console.CursorVisible = false;
                    top -= 1;
                    if (top > -1)
                    {
                        Console.SetCursorPosition(10, top);
                        Console.CursorTop = top;
                    }
                    else
                        top = 0;
                    if (Console.CursorTop > -1)
                    {
                        if (Console.CursorTop == 0)
                            Race[Console.CursorTop] = Race[Console.CursorTop].TrimEnd(MyChar);
                        Race[Console.CursorTop] += "<==";
                        Race[Console.CursorTop + 1] = Race[Console.CursorTop + 1].TrimEnd(MyChar);
                        top = Console.CursorTop;
                    }
                    else
                        Console.CursorTop = 0;
                }
                if (keyN.Key == ConsoleKey.DownArrow)
                {
                    Console.CursorVisible = false;
                    Console.SetCursorPosition(10, Console.CursorTop + 1);
                    if (Console.CursorTop < 4)
                    {
                        Race[Console.CursorTop] += "<==";
                        Race[Console.CursorTop - 1] = Race[Console.CursorTop - 1].TrimEnd(MyChar);
                        top = Console.CursorTop;
                    }
                    else
                        Console.CursorTop = 4;
                }
                if (keyN.Key == ConsoleKey.Enter)
                {
                    player.Race = _Race[Console.CursorTop];
                    break;
                }
                Console.Clear();
            }
        }
        private static void BuildGenderPlayer(ref Player player)
        {
            CompletePoint++;
            Console.Clear();
            Console.WriteLine("Please select gender player\n");
            Console.WriteLine("|Press any key|");
            _ = Console.ReadKey();
            Console.Clear();
            string[] Gender = new string[2] { "[Man]", "[Woman]" };
            string[] _Gender = new string[2] { "Man", "Woman" };
            char[] MyChar = { '=', '<' };
            int top = Console.CursorTop;
            while (true)
            {
                for (int i = 0; i < Gender.Length; ++i)
                    Console.WriteLine(Gender[i]);
                ConsoleKeyInfo keyN = Console.ReadKey();
                Console.SetCursorPosition(10, top);
                if (keyN.Key == ConsoleKey.UpArrow)
                {
                    Console.CursorVisible = false;
                    top -= 1;
                    if (top > -1)
                    {
                        Console.SetCursorPosition(10, top);
                        Console.CursorTop = top;
                    }
                    else
                        top = 0;
                    if (Console.CursorTop > -1)
                    {
                        if (Console.CursorTop == 0)
                            Gender[Console.CursorTop] = Gender[Console.CursorTop].TrimEnd(MyChar);
                        Gender[Console.CursorTop] += "<==";
                        Gender[Console.CursorTop + 1] = Gender[Console.CursorTop + 1].TrimEnd(MyChar);
                        top = Console.CursorTop;
                    }
                    else
                        Console.CursorTop = 0;
                }
                if (keyN.Key == ConsoleKey.DownArrow)
                {

                    Console.CursorVisible = false;
                    Console.SetCursorPosition(10, Console.CursorTop + 1);
                    if (Console.CursorTop < 2)
                    {
                        Gender[Console.CursorTop] += "<==";
                        Gender[Console.CursorTop - 1] = Gender[Console.CursorTop - 1].TrimEnd(MyChar);
                        top = Console.CursorTop;
                    }
                    else
                        Console.CursorTop = 2;
                }
                if (keyN.Key == ConsoleKey.Enter)
                {
                    player.Gender = _Gender[Console.CursorTop];
                    break;
                }
                Console.Clear();
            }
        }
        private static void BuildAgePlayer(ref Player player)
        {
            CompletePoint++;
            Console.Clear();
            Console.Write("Input Age your Hero:");
            int age;
            while (!int.TryParse(Console.ReadLine(), out age))
                Console.WriteLine("Не правильный ввод данных!");
            player.Age = age;
        }
        private static bool TempCreatePersonMenu(ref Player player)
        {
            Console.Clear();
            _menuBuildPlayer[] _Menus = new _menuBuildPlayer[4] { BuildNamePlayer, BuildRacePlayer, BuildGenderPlayer, BuildAgePlayer };
            string[] BuildPlayer = new string[4] { "[Input Name Hero]", "[Select Race Hero]", "[Select Gender Hero]", "[Input Age Hero]" };
            int top = Console.CursorTop;
            char[] MyChar = { '=', '<' };
            while (true)
            {
                for (int i = 0; i < BuildPlayer.Length; ++i)
                    Console.WriteLine(BuildPlayer[i]);
                ConsoleKeyInfo keyN = Console.ReadKey();
                Console.SetCursorPosition(10, top);
                if (keyN.Key == ConsoleKey.UpArrow)
                {
                    Console.CursorVisible = false;
                    top -= 1;
                    if (top > -1)
                    {
                        Console.SetCursorPosition(10, top);
                        Console.CursorTop = top;
                    }
                    else
                        top = 0;
                    if (Console.CursorTop > -1)
                    {
                        if (Console.CursorTop == 0)
                            BuildPlayer[Console.CursorTop] = BuildPlayer[Console.CursorTop].TrimEnd(MyChar);
                        BuildPlayer[Console.CursorTop] += "<==";
                        BuildPlayer[Console.CursorTop + 1] = BuildPlayer[Console.CursorTop + 1].TrimEnd(MyChar);
                        top = Console.CursorTop;
                    }
                    else
                        Console.CursorTop = 0;
                }
                if (keyN.Key == ConsoleKey.DownArrow)
                {
                    Console.CursorVisible = false;
                    Console.SetCursorPosition(10, Console.CursorTop + 1);
                    if (Console.CursorTop < 4)
                    {
                        BuildPlayer[Console.CursorTop] += "<==";
                        BuildPlayer[Console.CursorTop - 1] = BuildPlayer[Console.CursorTop - 1].TrimEnd(MyChar);
                        top = Console.CursorTop;
                    }
                    else
                        Console.CursorTop = 4;
                }
                if (keyN.Key == ConsoleKey.Enter)
                {
                    BuildPlayer[Console.CursorTop] = BuildPlayer[Console.CursorTop].Trim(MyChar);
                    BuildPlayer[Console.CursorTop] += " Complete!";
                    int tempcurs = Console.CursorTop;
                    _Menus[Console.CursorTop](ref player);
                    Console.SetCursorPosition(0, tempcurs);
                    _Menus[Console.CursorTop] = null;
                    if (CompletePoint == 4)
                        return true;
                }
                if (keyN.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    return false;
                }
                Console.Clear();
            }
        }
        private static bool SelectOrRedaction()
        {
            Console.Clear();
            string[] Race = new string[3] { " ", "[Redaction this Hero]", "[Select this Hero]" };
            bool[] YesNot = new bool[2] { false, true };
            char[] MyChar = { '=', '<' };
            int top = Console.CursorTop;
            bool Com;
            while (true)
            {
                Console.Write("Redaction or Select this Hero?\n");
                for (int i = 1; i < Race.Length; ++i)
                    Console.WriteLine(Race[i]);
                ConsoleKeyInfo keyN = Console.ReadKey();

                Console.SetCursorPosition(10, top);
                if (keyN.Key == ConsoleKey.UpArrow)
                {
                    Console.CursorVisible = false;
                    top -= 1;
                    if (top > 0)
                    {
                        Console.SetCursorPosition(10, top);
                        Console.CursorTop = top;
                    }
                    else
                        top = 1;
                    if (Console.CursorTop > 0)
                    {
                        if (Console.CursorTop == 1)
                            Race[Console.CursorTop] = Race[Console.CursorTop].TrimEnd(MyChar);
                        Race[Console.CursorTop] += "<==";
                        Race[Console.CursorTop + 1] = Race[Console.CursorTop + 1].TrimEnd(MyChar);
                        top = Console.CursorTop;
                    }
                    else
                        Console.CursorTop = 1;
                }
                if (keyN.Key == ConsoleKey.DownArrow)
                {
                    Console.CursorVisible = false;
                    Console.SetCursorPosition(10, Console.CursorTop + 1);
                    if (Console.CursorTop < 3)
                    {
                        Race[Console.CursorTop] += "<==";
                        Race[Console.CursorTop - 1] = Race[Console.CursorTop - 1].TrimEnd(MyChar);
                        top = Console.CursorTop;
                    }
                    else
                        Console.CursorTop = 3;
                }
                if (keyN.Key == ConsoleKey.Enter)
                {
                    Com = YesNot[Console.CursorTop - 1];
                    break;
                }
                Console.Clear();
            }
            return Com;
        }
        public static (Player, Player[], int) BuildMenuPlayer()
        {
            BinaryFormatter formatter = new();
            Console.Clear();
            string str = "[========================================================]";
            Console.SetCursorPosition(Console.WindowWidth / 2-str.Length/2, Console.WindowHeight / 2);
            
            Console.Write(str[0]);
            for (int i = 1; i < str.Length - 1; i++)
            {
                Thread.Sleep(25);
                Console.Write(str[i]);
            }
            Console.Write(str[^1]);
            Thread.Sleep(200);
            Console.SetCursorPosition((Console.WindowWidth / 2)-10, (Console.WindowHeight / 2) + 2);
            Console.Write("Please press any key");
            _ = Console.ReadKey();
            Console.Clear();
            int top = Console.CursorTop;
            Player returnPlayer = null;
            Player[] deserilizePeople = null;
            using (FileStream fs = new("Hero.dat", FileMode.OpenOrCreate))
            {
                //deserilizePeople = (Player[])formatter.Deserialize(fs);
                if (deserilizePeople != null)
                {

                    for (int i = 0; i < _BuildMenuPlayer.Length; ++i)
                    {
                        if (deserilizePeople[i].Name != "")
                            _BuildMenuPlayer[i] = "[ " + deserilizePeople[i].Name + " ]";
                    }
                }
            }
            while (ActiveBuildMenuPlayer)
            {
                Console.Clear();
                for (int i = 0; i < _BuildMenuPlayer.Length; ++i)
                {
                    Console.WriteLine(_BuildMenuPlayer[i]);
                }
                ConsoleKeyInfo keyN = Console.ReadKey();
                char[] MyChar = { '=', '<' };
                Console.SetCursorPosition(10, top);
                if (keyN.Key == ConsoleKey.UpArrow)
                {
                    Console.CursorVisible = false;
                    top -= 1;
                    if (top > -1)
                    {
                        Console.SetCursorPosition(10, top);
                        Console.CursorTop = top;
                    }
                    else
                        top = 0;
                    if (Console.CursorTop > -1)
                    {
                        if (Console.CursorTop == 0)
                            _BuildMenuPlayer[Console.CursorTop] = _BuildMenuPlayer[Console.CursorTop].TrimEnd(MyChar);
                        _BuildMenuPlayer[Console.CursorTop] += "<==";
                        _BuildMenuPlayer[Console.CursorTop + 1] = _BuildMenuPlayer[Console.CursorTop + 1].TrimEnd(MyChar);
                        top = Console.CursorTop;
                    }
                    else
                        Console.CursorTop = 0;
                }
                if (keyN.Key == ConsoleKey.DownArrow)
                {
                    Console.CursorVisible = false;
                    Console.SetCursorPosition(10, Console.CursorTop + 1);
                    if (Console.CursorTop < 3)
                    {
                        _BuildMenuPlayer[Console.CursorTop] += "<==";
                        _BuildMenuPlayer[Console.CursorTop - 1] = _BuildMenuPlayer[Console.CursorTop - 1].TrimEnd(MyChar);
                        top = Console.CursorTop;
                    }
                    else
                        Console.CursorTop = 3;
                }
                if (keyN.Key == ConsoleKey.Enter)
                {
                    char[] MyChar2 = new char[4] { '[', ']', '{', '}' };
                    _ = Menu.NewPlayer(false);
                    int tempCurs;
                    bool temp;
                    if (_BuildMenuPlayer[Console.CursorTop] != "{Empty player}<==")
                    {
                        tempCurs = Console.CursorTop;
                        if (SelectOrRedaction() == true)
                        {
                            using (FileStream fs = new("Hero.dat", FileMode.OpenOrCreate))
                            {
                                deserilizePeople = formatter.Deserialize(fs) as Player[];
                                returnPlayer = deserilizePeople[tempCurs];
                            }
                            FirstOpen = false;
                            Console.SetCursorPosition(10, tempCurs);
                            _BuildMenuPlayer[Console.CursorTop] = "[ " + deserilizePeople[Console.CursorTop].Name + " ]";
                            for (int i = 0; i < _BuildMenuPlayer.Length; i++)
                            {
                                if (i != Console.CursorTop)
                                {
                                    _BuildMenuPlayer[i] = _BuildMenuPlayer[i].Trim(MyChar2);
                                    _BuildMenuPlayer[i] = "{" + _BuildMenuPlayer[i] + "}";
                                }
                            }
                            PIndex = tempCurs;
                            return (returnPlayer, deserilizePeople, PIndex);
                        }
                        else
                        {
                            tempCurs = Console.CursorTop;
                            temp = TempCreatePersonMenu(ref savePlayers[Console.CursorTop]);
                            CompletePoint = 0;
                            Console.SetCursorPosition(10, tempCurs);
                            if (temp == true)
                            {
                                _BuildMenuPlayer[Console.CursorTop] = "[ " + deserilizePeople[Console.CursorTop].Name + " ]";
                                for (int i = 0; i < _BuildMenuPlayer.Length; i++)
                                {
                                    if (i != Console.CursorTop)
                                    {
                                        _BuildMenuPlayer[i] = _BuildMenuPlayer[i].Trim(MyChar2);
                                        _BuildMenuPlayer[i] = "{" + _BuildMenuPlayer[i] + "}";
                                    }
                                }
                                using (FileStream fs = new("Hero.dat", FileMode.OpenOrCreate))
                                {
                                    formatter.Serialize(fs, savePlayers);
                                }
                                FirstOpen = false;
                                using (FileStream fs = new("Hero.dat", FileMode.OpenOrCreate))
                                {
                                    deserilizePeople = formatter.Deserialize(fs) as Player[];
                                    returnPlayer = deserilizePeople[tempCurs];
                                }
                                PIndex = tempCurs;
                                return (returnPlayer, deserilizePeople, PIndex);
                            }

                        }
                    }
                    else
                    {
                        tempCurs = Console.CursorTop;

                        temp = TempCreatePersonMenu(ref savePlayers[Console.CursorTop]);
                        CompletePoint = 0;
                        Console.SetCursorPosition(10, tempCurs);
                        deserilizePeople = savePlayers;
                        if (temp == true)
                        {
                            _BuildMenuPlayer[Console.CursorTop] = "[ " + deserilizePeople[Console.CursorTop].Name + " ]";
                            for (int i = 0; i < _BuildMenuPlayer.Length; i++)
                            {
                                if (i != Console.CursorTop)
                                {
                                    _BuildMenuPlayer[i] = _BuildMenuPlayer[i].Trim(MyChar2);
                                    _BuildMenuPlayer[i] = "{" + _BuildMenuPlayer[i] + "}";
                                }
                            }
                            using (FileStream fs = new("Hero.dat", FileMode.OpenOrCreate))
                            {
                                formatter.Serialize(fs, savePlayers);
                            }
                            FirstOpen = false;
                        }
                        PIndex = tempCurs;
                        returnPlayer = deserilizePeople[tempCurs];
                        return (returnPlayer, deserilizePeople, PIndex);
                    }
                }
                if (keyN.Key == ConsoleKey.Backspace)
                {
                    int tempCurs = Console.CursorTop;
                    Console.Clear();
                    bool tmp = YesOrNo("Delete hero?");
                    if (tmp)
                        _BuildMenuPlayer[tempCurs] = "{Empty player}";
                }
                if (keyN.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    Menu._Menu();
                }
                Console.Clear();
            }
            return (returnPlayer, deserilizePeople, PIndex);
        }
    }
}
