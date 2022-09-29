using System;
using System.Threading;

namespace RolePlayGame
{
    internal enum State
    {
        Normal,
        Weakened,
        IsIll,
        Poisoned,
        Paralyzed,
        Dead,
    }
    [Serializable]
    internal class Player
    {
        private delegate void Bonus();
        private string name = "";
        public string Name
        {
            get => name;
            set { if (value.Length <= 7) name = value; }
        }
        private static readonly string[] InventoryPlayer = new string[4] { "(Empty)", "(Empty)", "(Empty)", "(Empty)" };
        private static readonly string[] Upgrade = new string[3] { "Damage", "Mana", "Health" };
        protected int Id;
        private int damage = 0;
        public int Damage
        {
            get => damage;
            set => damage = value;
        }
        private State State;

        public State Getstate()
        {
            return State;
        }

        public void Setstate(State value)
        { if (value < (State)6) State = value; }
        private string race = "";
        public string Race
        {
            get => race;
            set => race = value;
        }
        private string gender = "";
        public string Gender { get => gender; set => gender = value; }
        private int age = 0;
        public int Age
        {
            get => age;
            set
            {
                if (value is not > 99 or not < 0)
                    age = value;
                else
                    Console.WriteLine("Wrong age, try again.");
            }
        }
        protected int hp;
        public bool Live = true;
        public int HP
        {
            get => hp;
            set
            {
                if (value <= 0)
                    Live = false;
                if (value > -2)
                    hp = value;
            }
        }
        private int mana;
        public int Mana
        {
            get => mana;
            set
            {
                if (value > -1)
                    mana = value;
            }
        }
        private double exp;
        public int Level = 0;
        public double EXP
        {
            get => exp;
            set
            {
                if (value > 0)
                    exp = value;
            }
        }
        public Player()
        {
            Random random = new();
            Id = random.Next(1, 999);
            Name = name;
            Race = race;
            Gender = gender;
            HP = 10;
            Mana = 10;
            Damage = 5;
            EXP = 0;
            Level = 1;

        }
        public static int Control()
        {
            ConsoleKeyInfo keyN = Console.ReadKey();
            if (keyN.Key == ConsoleKey.UpArrow)
            {
                return 1;
            }
            if (keyN.Key == ConsoleKey.DownArrow)
            {
                return 2;
            }
            if (keyN.Key == ConsoleKey.LeftArrow)
            {
                return 3;
            }
            return keyN.Key == ConsoleKey.RightArrow
                ? 4
                : keyN.Key == ConsoleKey.Spacebar ? 5 : keyN.Key == ConsoleKey.Escape ? 6 : keyN.Key == ConsoleKey.I ? 7 : 0;
        }
        public void Inventory(int obj, bool use)
        {
            int count = 0;
            if (use != true)
            {
                for (int i = 0; i < InventoryPlayer.Length; i++)
                {

                    if (InventoryPlayer[i] is "(Empty)" or "(Empty)<==")
                    {
                        if (obj == 1)
                        {
                            InventoryPlayer[i] = "(" + "LivingWater" + ")";
                            break;
                        }
                        if (obj == 2)
                        {
                            InventoryPlayer[i] = "(" + "DeadWater" + ")";
                            break;
                        }
                        if (obj == 3)
                        {
                            InventoryPlayer[i] = "(" + "DecoctionFrog" + ")";
                            break;
                        }
                        if (obj == 4)
                        {
                            InventoryPlayer[i] = "(" + "BasiliskEye" + ")";
                            break;
                        }
                        break;
                    }
                    else
                        ++count;
                    if (count == InventoryPlayer.Length)
                    {
                        string[] tmpBonus = new string[4] { "(LivingWater)", "(DeadWater)", "(DecoctionFrog)", "(BasiliskEye)" };
                        Console.SetCursorPosition(25 + 40, 4);
                        Console.Write("Inventory full");
                        Thread.Sleep(1000);
                        Random r = new();
                        PrintInventory(true, tmpBonus[r.Next(0, 4)]);
                    }

                }
            }
            else
            {

            }

        }
        private void LivingWater()
        {
            HP += 3;
        }
        private void DeadWater()
        {
            Mana += 3;
        }
        private void DecoctionFrog()
        {

        }
        private void BasiliskEye()
        {

        }
        public void Exp()
        {
            Random random = new();
            EXP += random.Next(20, 31);
            if ((EXP / Level / 100) >= 1)
            {
                double NextExp = EXP / Level / 100;
                EXP = (NextExp - Level) * 100;
                UpgradeHero();
                ++Level;
            }
        }
        public void UpgradeHero()
        {
            char[] MyChar = { '=', '<' };
            Console.Clear();
            int top = 6;
            while (true)
            {
                Console.Clear();
                Console.SetCursorPosition(25 + 40, 5);
                Console.Write("Upgrade hero!");
                for (int i = 0; i < Upgrade.Length; i++)
                {
                    Console.SetCursorPosition(25 + 30, i + 6);
                    Console.Write(Upgrade[i] + "\n");
                }
                ConsoleKeyInfo keyN = Console.ReadKey();
                Console.SetCursorPosition(25 + 30, top);
                if (keyN.Key == ConsoleKey.UpArrow)
                {
                    Console.CursorVisible = false;
                    top -= 1;
                    if (top > 5)
                    {
                        Console.SetCursorPosition(25 + 30, top);
                        Console.CursorTop = top;
                    }
                    else
                        top = 7;
                    if (Console.CursorTop > 5)
                    {
                        if (Console.CursorTop == 6)
                            Upgrade[Console.CursorTop - 6] = Upgrade[Console.CursorTop - 6].TrimEnd(MyChar);
                        Upgrade[Console.CursorTop - 6] += "<==";
                        Upgrade[Console.CursorTop + 1 - 6] = Upgrade[Console.CursorTop + 1 - 6].TrimEnd(MyChar);
                        top = Console.CursorTop;
                    }
                    else
                        Console.CursorTop = 6;
                }
                if (keyN.Key == ConsoleKey.DownArrow)
                {

                    Console.CursorVisible = false;
                    Console.SetCursorPosition(25 + 30, Console.CursorTop + 1);
                    if (Console.CursorTop < 9)
                    {
                        Upgrade[Console.CursorTop - 6] += "<==";
                        Upgrade[Console.CursorTop - 1 - 6] = Upgrade[Console.CursorTop - 1 - 6].TrimEnd(MyChar);
                        top = Console.CursorTop;
                    }
                    else
                    {
                        Console.CursorTop = 9;
                    }
                }
                if (keyN.Key == ConsoleKey.Enter)
                {

                    if (Upgrade[Console.CursorTop - 6] == "Damage<==")
                    {
                        Damage++;
                        break;
                    }
                    if (Upgrade[Console.CursorTop - 6] == "Mana<==")
                    {
                        Mana++;
                        break;
                    }
                    if (Upgrade[Console.CursorTop - 6] == "Health<==")
                    {
                        HP++;
                        break;
                    }
                }

            }
        }
        public static void OpenChest()
        {
            char[] MyChar = { '=', '<' };
            Console.Clear();
            int top = 6;
            Random rand = new();
            string[] Bon = new string[2] { "(LivingWater)", "(DeadWater)" };
            string[] Str = new string[4];
            for (int i = 0; i < Str.Length; i++)
            {
                Str[i] = rand.Next(0, 5) < 3 ? "(Empty)" : Bon[rand.Next(0, 2)];
            }
            while (true)
            {
                Console.SetCursorPosition(25 + 40, 5);
                Console.Write("Chest");

                for (int i = 0; i < Str.Length; i++)
                {
                    Console.SetCursorPosition(25 + 30, i + 6);
                    Console.Write(Str[i] + "\n");
                }
                ConsoleKeyInfo keyN = Console.ReadKey();
                Console.SetCursorPosition(25 + 30, top);
                if (keyN.Key == ConsoleKey.UpArrow)
                {
                    Console.CursorVisible = false;
                    top -= 1;
                    if (top > 5)
                    {
                        Console.SetCursorPosition(25 + 30, top);
                        Console.CursorTop = top;
                    }
                    else
                        top = 7;
                    if (Console.CursorTop > 5)
                    {
                        if (Console.CursorTop == 6)
                            Str[Console.CursorTop - 6] = Str[Console.CursorTop - 6].TrimEnd(MyChar);
                        Str[Console.CursorTop - 6] += "<==";
                        Str[Console.CursorTop + 1 - 6] = Str[Console.CursorTop + 1 - 6].TrimEnd(MyChar);
                        top = Console.CursorTop;
                    }
                    else
                        Console.CursorTop = 6;
                }
                if (keyN.Key == ConsoleKey.DownArrow)
                {
                    Console.CursorVisible = false;
                    Console.SetCursorPosition(25 + 30, Console.CursorTop + 1);
                    if (Console.CursorTop < 10)
                    {
                        Str[Console.CursorTop - 6] += "<==";
                        Str[Console.CursorTop - 1 - 6] = Str[Console.CursorTop - 1 - 6].TrimEnd(MyChar);
                        top = Console.CursorTop;
                    }
                    else
                    {
                        Console.CursorTop = 10;
                    }
                }

                if (keyN.Key == ConsoleKey.Enter)
                {
                    for (int i = 0; i < InventoryPlayer.Length; i++)
                    {
                        if (InventoryPlayer[i] is "(Empty)<==" or "(Empty)")
                        {
                            if (Str[i] is not "(Empty)<==" or not "(Empty)")
                            {
                                InventoryPlayer[i] = Str[i].TrimEnd(MyChar);
                                Str[i] = "(Empty)";
                            }
                        }
                    }
                }
                if (keyN.Key == ConsoleKey.Backspace)
                {
                    int CT = Console.CursorTop;
                    if (BuildPlayer.YesOrNo("Delete bonus?"))
                    {
                        Console.SetCursorPosition(25 + 30, CT);
                        Str[Console.CursorTop - 6] = "(Empty)<==";
                    }

                }
                if (keyN.Key == ConsoleKey.Escape)
                {
                    break;
                }
                Console.Clear();
            }
        }
        public void PrintInventory(bool FullInventory, string BonusFullInventory)
        {
            char[] MyChar = { '=', '<' };
            Bonus[] bonus = new Bonus[4] { LivingWater, DeadWater, DecoctionFrog, BasiliskEye };
            Console.Clear();
            int top = 6;
            while (true)
            {
                Console.SetCursorPosition(25 + 40, 5);
                Console.Write("Your Inventory!");
                for (int i = 0; i < InventoryPlayer.Length; i++)
                {
                    Console.SetCursorPosition(25 + 30, i + 6);
                    Console.Write(InventoryPlayer[i] + "\n");
                }
                ConsoleKeyInfo keyN = Console.ReadKey();
                Console.SetCursorPosition(25 + 30, top);
                if (keyN.Key == ConsoleKey.UpArrow)
                {
                    Console.CursorVisible = false;
                    top -= 1;
                    if (top > 5)
                    {
                        Console.SetCursorPosition(25 + 30, top);
                        Console.CursorTop = top;
                    }
                    else
                        top = 7;
                    if (Console.CursorTop > 5)
                    {
                        if (Console.CursorTop == 6)
                            InventoryPlayer[Console.CursorTop - 6] = InventoryPlayer[Console.CursorTop - 6].TrimEnd(MyChar);
                        InventoryPlayer[Console.CursorTop - 6] += "<==";
                        InventoryPlayer[Console.CursorTop + 1 - 6] = InventoryPlayer[Console.CursorTop + 1 - 6].TrimEnd(MyChar);
                        top = Console.CursorTop;
                    }
                    else
                        Console.CursorTop = 6;
                }
                if (keyN.Key == ConsoleKey.DownArrow)
                {
                    Console.CursorVisible = false;
                    Console.SetCursorPosition(25 + 30, Console.CursorTop + 1);
                    if (Console.CursorTop < 10)
                    {
                        InventoryPlayer[Console.CursorTop - 6] += "<==";
                        InventoryPlayer[Console.CursorTop - 1 - 6] = InventoryPlayer[Console.CursorTop - 1 - 6].TrimEnd(MyChar);
                        top = Console.CursorTop;
                    }
                    else
                    {
                        Console.CursorTop = 10;
                    }
                }
                if (FullInventory == false)
                {
                    if (keyN.Key == ConsoleKey.Enter)
                    {
                        if (InventoryPlayer[Console.CursorTop - 6] != "(Empty)<==")
                        {

                            if (InventoryPlayer[Console.CursorTop - 6] == "(LivingWater)<==")
                                bonus[0]();
                            if (InventoryPlayer[Console.CursorTop - 6] == "(DeadWater)<==")
                                bonus[1]();
                            if (InventoryPlayer[Console.CursorTop - 6] == "(DecoctionFrog)<==")
                                bonus[2]();
                            if (InventoryPlayer[Console.CursorTop - 6] == "(BasiliskEye)<==")
                                bonus[3]();
                            InventoryPlayer[Console.CursorTop - 6] = "(Empty)<==";
                            break;
                        }


                    }
                    if (keyN.Key == ConsoleKey.Backspace)
                    {
                        int CT = Console.CursorTop;
                        if (BuildPlayer.YesOrNo("Delete bonus?"))
                        {
                            Console.SetCursorPosition(25 + 30, CT);
                            InventoryPlayer[Console.CursorTop - 6] = "(Empty)<==";
                        }

                    }
                }
                else
                {
                    if (keyN.Key == ConsoleKey.Enter)
                    {

                        if (InventoryPlayer[Console.CursorTop - 6] == "(LivingWater)<==")
                            bonus[0]();
                        if (InventoryPlayer[Console.CursorTop - 6] == "(DeadWater)<==")
                            bonus[1]();
                        if (InventoryPlayer[Console.CursorTop - 6] == "(DecoctionFrog)<==")
                            bonus[2]();
                        if (InventoryPlayer[Console.CursorTop - 6] == "(BasiliskEye)<==")
                            bonus[3]();
                        InventoryPlayer[Console.CursorTop - 6] = BonusFullInventory + "<==";
                        break;
                    }
                    if (keyN.Key == ConsoleKey.Backspace)
                    {
                        int CT = Console.CursorTop;
                        if (BuildPlayer.YesOrNo("Delete bonus?"))
                        {
                            Console.SetCursorPosition(25 + 30, CT);
                            InventoryPlayer[Console.CursorTop - 6] = BonusFullInventory + "<==";
                        }

                    }
                }
                if (keyN.Key == ConsoleKey.Escape)
                {
                    break;
                }
                Console.Clear();
            }
        }
    }
}

