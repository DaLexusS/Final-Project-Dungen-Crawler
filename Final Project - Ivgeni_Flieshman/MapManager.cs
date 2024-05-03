using System;
using System.Security;

namespace Final_Project___Ivgeni_Flieshman
{
    public class MapManager
    {
        public int CurrentMapLevel = 1;
        public char[][]? CurrentMap;
        public int EnemyCounter = 0;

        public char ChestIcon = '#';
        public char PlayerIcon = '@';
        public char ExitIcon = 'X';
        public char EnemyIcon = '*';
        public char FreeSpaceIcon = ' ';
        public char TrapIcon = '.';

        public List<Enemy> EnemyList = new List<Enemy>();
        public List<Treasure> ChestList = new List<Treasure>();
        public List<Trap> TrapList = new List<Trap>();
        public MessagesHandler MessagesHandler = new MessagesHandler();

        public int AttackRange = 2;
        public int EnemyDetection = 4;
        public Player Player;

        public void InitializeMap(Player givenPlayer)
        {
            Player = givenPlayer;
            CurrentMap = global::MapProvider.GetMapForLevel(CurrentMapLevel);
            TrapList.Clear();
            InitializeSprites();
            DrawMap();
        }

        public void DeathScreen(Player newPlayer)
        {
            Console.Clear();
            Player.Reset();
            EnemyList.Clear();
            ChestList.Clear();
            TrapList.Clear();
            Console.Clear();
            CurrentMapLevel = 0;
            CurrentMap = global::MapProvider.GetMapForLevel(CurrentMapLevel);
            for (int row = 0; row < CurrentMap.Length; row++)
            {
                Console.WriteLine(CurrentMap[row]);
            }

            Thread.Sleep(3000);
            CurrentMapLevel = 1;
            InitializeMap(newPlayer);
        }

        public void WinScreen(Player newPlayer)
        {
            Console.Clear();
            Player.Reset();
            EnemyList.Clear();
            ChestList.Clear();
            TrapList.Clear();
            Console.Clear();
            CurrentMapLevel = 11;
            CurrentMap = global::MapProvider.GetMapForLevel(CurrentMapLevel);
            for (int row = 0; row < CurrentMap.Length; row++)
            {
                Console.WriteLine(CurrentMap[row]);
            }

            Thread.Sleep(3000);
            CurrentMapLevel = 1;
            InitializeMap(newPlayer);
        }

        public void DrawMap()
        {
            Console.Clear();

            MoveEnemiesTowardsPlayer();
            UpdateSprites();
            WriteStats();
            Enemy enemyNearby = CheckNearbyEnemy();
            if (enemyNearby != null)
            {
                MessagesHandler.EnemyNearby(enemyNearby, Player);
            }
        }

        public void InitializeSprites()
        {
            for (int row = 0; row < CurrentMap.Length; row++)
            {
                for (int col = 0; col < CurrentMap[row].Length; col++)
                {
                    if (CurrentMap[row][col] == PlayerIcon)
                    {
                        CurrentMap[row][col] = FreeSpaceIcon;
                        Player.X = row;
                        Player.Y = col;
                    }
                    else if (CurrentMap[row][col] == ChestIcon)
                    {
                        Treasure chest = new Treasure(Player, this);
                        CurrentMap[row][col] = FreeSpaceIcon;
                        chest.X = row;
                        chest.Y = col;
                        ChestList.Add(chest);
                    }
                    else if (CurrentMap[row][col] == EnemyIcon)
                    {
                        Enemy enemy = new Enemy(this);
                        CurrentMap[row][col] = FreeSpaceIcon;
                        enemy.X = row;
                        enemy.Y = col;
                        EnemyList.Add(enemy);
                        EnemyCounter++;
                    }
                    else if (CurrentMap[row][col] == TrapIcon)
                    {
                        Trap trap = new Trap(this);
                        trap.X = row;
                        trap.Y = col;
                        TrapList.Add(trap);
                    }
                }

                Console.WriteLine(CurrentMap[row]);
            }
        }

        public void UpdateSprites()
        {
            for (int row = 0; row < CurrentMap.Length; row++)
            {
                for (int col = 0; col < CurrentMap[row].Length; col++)
                {
                    if (CurrentMap[row][col] != '-' && CurrentMap[row][col] != '|' && CurrentMap[row][col] != ExitIcon && CurrentMap[row][col] != '/' && CurrentMap[row][col] != '\\')
                    {
                        CurrentMap[row][col] = FreeSpaceIcon;
                    }

                    if (col == Player.Y && row == Player.X)
                    {
                        CurrentMap[row][col] = PlayerIcon;
                    }

                    foreach (Enemy enemy in EnemyList)
                    {
                        if (enemy != null)
                        {
                            if (col == enemy.Y && row == enemy.X && enemy.Health > 0)
                            {
                                CurrentMap[row][col] = EnemyIcon;
                            }
                        }
                    }

                    foreach (Treasure chest in ChestList)
                    {
                        if (chest != null)
                        {
                            if (col == chest.Y && row == chest.X && !chest.Looted)
                            {
                                CurrentMap[row][col] = ChestIcon;
                            }
                        }
                    }

                    foreach (Trap trap in TrapList)
                    {
                        if (trap != null)
                        {
                            if (trap.IsSet)
                            {
                                if (col == trap.Y && row == trap.X)
                                {
                                    CurrentMap[row][col] = TrapIcon;
                                }
                            }

                            if (Player.Y == trap.Y && Player.X == trap.X && !trap.IsSet)
                            {
                                trap.DealDamage(Player);
                            }
                        }
                    }
                }

                foreach (char character in CurrentMap[row])
                {
                    switch (character)
                    {
                        case '@':
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case '#':
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        case '*':
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            break;
                        case 'X':
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case '.':
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        default:
                            Console.ResetColor();
                            break;
                    }
                    Console.Write(character);
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }

        public void WriteStats()
        {
            MessagesHandler.PrintStats(CurrentMapLevel, Player);
        }

        public Enemy CheckNearbyEnemy()
        {
            foreach (var enemy in EnemyList)
            {
                int distX = Math.Abs(enemy.X - Player.X);
                int distY = Math.Abs(enemy.Y - Player.Y);

                if (distX <= enemy.AttackRange && distY <= enemy.AttackRange)
                {
                    return enemy;
                }
            }

            return null;
        }

        public Treasure CheckNearbyChest()
        {
            foreach (var chest in ChestList)
            {
                int distX = Math.Abs(chest.X - Player.X);
                int distY = Math.Abs(chest.Y - Player.Y);

                if (distX <= Player.AttackRange && distY <= Player.AttackRange)
                {
                    return chest;
                }
            }

            return null;
        }

        public bool CheckNearbyExit()
        {
            int attackRange = Player.AttackRange;

            for (int i = Math.Max(0, Player.X - attackRange); i <= Math.Min(CurrentMap.Length - 1, Player.X + attackRange); i++)
            {
                for (int j = Math.Max(0, Player.Y - attackRange); j <= Math.Min(CurrentMap[Player.X].Length - 1, Player.Y + attackRange); j++)
                {
                    if (CurrentMap[i][j] == ExitIcon && (i != Player.X || j != Player.Y))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void PlayerMovedLeft()
        {
            MovePlayer(0, -1);
        }

        public void PlayerMovedRight()
        {
            MovePlayer(0, 1);
        }

        public void PlayerMovedUp()
        {
            MovePlayer(-1, 0);
        }

        public void PlayerMovedDown()
        {
            MovePlayer(1, 0);
        }

        public void MovePlayer(int deltaX, int deltaY)
        {
            int newX = Player.X + deltaX;
            int newY = Player.Y + deltaY;

            if (IsValidMove(newX, newY))
            {
                CurrentMap[Player.X][Player.Y] = FreeSpaceIcon;
                Player.X = newX;
                Player.Y = newY;
                CurrentMap[Player.X][Player.Y] = PlayerIcon;
            }
        }

        public void MoveEnemiesTowardsPlayer()
        {
            int playerRow = Player.X;
            int playerCol = Player.Y;

            foreach (Enemy enemy in EnemyList)
            {
                if (enemy != null)
                {
                    int distX = Math.Abs(playerRow - enemy.X);
                    int distY = Math.Abs(playerCol - enemy.Y);

                    if (distX <= EnemyDetection && distY <= EnemyDetection)
                    {
                        int dirX = Math.Sign(playerRow - enemy.X);
                        int dirY = Math.Sign(playerCol - enemy.Y);

                        int newRow = enemy.X + dirX;
                        int newCol = enemy.Y + dirY;

                        if (IsValidMoveEnemy(newRow, newCol))
                        {
                            CurrentMap[enemy.X][enemy.Y] = FreeSpaceIcon;
                            enemy.X = newRow;
                            enemy.Y = newCol;
                            CurrentMap[enemy.X][enemy.Y] = EnemyIcon;
                        }
                    }
                }
            }
        }

        private bool IsValidMove(int row, int col)
        {
            return row >= 0 && row < CurrentMap.Length &&
                   col >= 0 && col < CurrentMap[row].Length &&
                   CurrentMap[row][col] != '/' && CurrentMap[row][col] != '\\' &&
                   (CurrentMap[row][col] == FreeSpaceIcon || CurrentMap[row][col] == TrapIcon);
        }

        private bool IsValidMoveEnemy(int row, int col)
        {
            return IsValidMove(row, col) && row != Player.X && col != Player.Y;
        }
    }
}
