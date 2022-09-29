
using System;

namespace RolePlayGame
{
    internal class Menu
    {
        private delegate Player _menu();
        private static readonly string[] menu = new string[4] { "|New Game|", "|Continue|", "|Characters|", "|Exit|" };
        private static bool ActiveMenu = true;
        private static bool args = false;
        private static Player hero = new();
        public static bool NewPlayer(bool FirstLoggin)
        {
            return FirstLoggin ? args : (args = true);
        }
        private static Player NewGame()
        {
            (Player, Player[], int) next = BuildPlayer.BuildMenuPlayer();
            hero = next.Item1;
            Console.Clear();
            Map.DisplayMap(hero, next.Item2, next.Item3);
            return null;
        }
        private static Player Continue()
        {
            return null;
        }
        private static Player Characters()
        {
            (Player, Player[], int) next = BuildPlayer.BuildMenuPlayer();
            hero = next.Item1;
            Menu._Menu();
            return null;
        }
        private static Player Exit()
        {
            return null;
        }
        public static void _Menu()
        {
            
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
            char[] MyChar = { '=', '<' };
            for (int i = 0; i < menu.Length; i++)
            {
                menu[i] = menu[i].TrimEnd(MyChar);
            }
            Console.ResetColor();
            Console.Clear();
            _menu[] _Menus = new _menu[4] { NewGame, Continue, Characters, Exit };
            int top = Console.CursorTop;
            while (ActiveMenu)
            {
                for (int i = 0; i < menu.Length; ++i)
                {
                    Console.WriteLine(menu[i]);
                    if (i != menu.Length - 1)
                        Console.Write("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬" + "\n");
                }
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
                            menu[Console.CursorTop] = menu[Console.CursorTop].TrimEnd(MyChar);
                        menu[Console.CursorTop] += "<==";
                        menu[Console.CursorTop + 1] = menu[Console.CursorTop + 1].TrimEnd(MyChar);
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
                        menu[Console.CursorTop] += "<==";
                        menu[Console.CursorTop - 1] = menu[Console.CursorTop - 1].TrimEnd(MyChar);
                        top = Console.CursorTop;
                    }
                    else
                        Console.CursorTop = 4;
                }
                if (keyN.Key == ConsoleKey.Enter)
                {
                    _ = _Menus[Console.CursorTop]();
                    ActiveMenu = false;
                }
                Console.Clear();
            }
        }
    }
}

