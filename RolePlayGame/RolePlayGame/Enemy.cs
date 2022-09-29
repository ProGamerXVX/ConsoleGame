using System;

namespace RolePlayGame
{
    internal class Enemy
    {
        private int hp;
        public bool Live = true;
        public int HP
        {
            get => hp;
            set
            {
                if (value <= 0)
                {
                    Live = false;
                    hp = value;
                }


                else
                    hp = value;
            }
        }
        private int posX;
        public int PosX
        {
            get => posX;
            set
            {
                if (value is > 1 and < 25)
                    posX = value;
            }
        }
        private int posY;
        public int PosY
        {
            get => posY;
            set
            {
                if (value is > 1 and < 25)
                    posY = value;
            }
        }
        public Enemy()
        {
            HP = 10;
        }
        public int Control(ref bool running)
        {
            if (running)
            {
                if (Live)
                {
                    Random random = new();
                    int r = random.Next(0, 5);
                    if (r == 1)
                        return 1;
                    if (r == 2)
                        return 2;
                    if (r == 3)
                        return 3;
                    if (r == 4)
                        return 4;
                }
                running = false;
            }
            return 0;
        }
    }
}
