using System.ComponentModel.Design;
using System.Reflection.Emit;

namespace Final_Project___Ivgeni_Flieshman
{
    public class MapManager
    {
        public int currentMapLevel = 1;
        public int playerLocationX, playerLocationY;
        public char[][]? currentMap;
        public char ChestIcon = '#';
        public char PlayerIcon = '@';
        public char ExitIcon = 'X';
        public char EnemeyIcon = '*';
        public char SpawnPoint = 'E';
        public char FreeSpaceIcon = ' ';
        public int EnemyCounter = 0;
        public List<Enemy> Enemylist = new List<Enemy>();
        public int AttackRange = 2;

        public void InitializeMap()
        {
            currentMap = MapProvider.GetMapForLevel(currentMapLevel);

            DrawMap();
            CountEnemies();
        }

        public void DrawMap()
        {
            Console.Clear();

            foreach (Enemy enemy in Enemylist)
            {
                int DirX = -1;
                int DirY = 0;

                if (currentMap[enemy.Y + DirY][enemy.X + DirX] == FreeSpaceIcon)
                {
                    currentMap[enemy.Y][enemy.X] = FreeSpaceIcon;
                    enemy.Y += DirY;
                    enemy.X += DirX;

                    currentMap[enemy.Y][enemy.X] = EnemeyIcon;
                }
            }

            for (int i = 0; i < currentMap.Length; i++)
            {
                for (int j = 0; j < currentMap[i].Length; j++)
                {
                    if (currentMap[i][j] == SpawnPoint)
                    {
                        currentMap[i][j] = PlayerIcon;

                        playerLocationX = i;
                        playerLocationY = j;

                    }
                }
                Console.WriteLine(currentMap[i]);
            }
        }

        public void CountEnemies()
        {
            for (int i = 0; i < currentMap.Length; i++)
            {
                for (int j = 0; j < currentMap[i].Length; j++)
                {
                    if (currentMap[i][j] == EnemeyIcon)
                    {
                        Enemy enemy = new Enemy();
                        enemy.X = j;
                        enemy.Y = i;
                        Enemylist.Add(enemy);
                        EnemyCounter++;
                    }
                }
            }
        }


        public Enemy CheckNearbyEnemy()
        {
            foreach (var enemy in Enemylist)
            {

                int distX = Math.Abs(enemy.X - playerLocationX);
                int distY = Math.Abs(enemy.Y - playerLocationY);

                if (distX <= AttackRange && distY <= AttackRange)
                {
                    return enemy;
                }
            }

            return null;
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

        public void Interact(Player player)
        {
            Enemy nearbyEnemy = CheckNearbyEnemy();

            if (nearbyEnemy != null)
            {
                nearbyEnemy.FightLoop(player, this);
                return;
            }
            else if (CheckNearbySymbol(ChestIcon))
            {
                // Remove icon from map
                // Treasure Logic
                // Update Player damage and weapon/armor
            }
            else if (CheckNearbySymbol(ExitIcon))
            {
                if (EnemyCounter != 0)
                {
                    return;
                }

                currentMapLevel++;
                InitializeMap();
            }

            DrawMap();
        }


        public void MovePlayer(int deltaX, int deltaY)
        {
            int newX = playerLocationX + deltaX;
            int newY = playerLocationY + deltaY;

            if (IsValidMove(newX, newY))
            {
                currentMap[playerLocationX][playerLocationY] = FreeSpaceIcon;
                playerLocationX = newX;
                playerLocationY = newY;
                currentMap[playerLocationX][playerLocationY] = PlayerIcon;
                Console.Clear();
            }

            DrawMap();
        }


        private bool IsValidMove(int x, int y)
        {
            return x >= 0 && x < currentMap.Length &&
                   y >= 0 && y < currentMap[x].Length &&
                   currentMap[x][y] == FreeSpaceIcon;
        }

        public bool CheckNearbySymbol(char symbol)
        {
            int proximityDistance = 1;

            for (int i = Math.Max(0, playerLocationX - proximityDistance); i <= Math.Min(currentMap.Length - 1, playerLocationX + proximityDistance); i++)
            {
                for (int j = Math.Max(0, playerLocationY - proximityDistance); j <= Math.Min(currentMap[playerLocationX].Length - 1, playerLocationY + proximityDistance); j++)
                {
                    if (currentMap[i][j] == symbol && (i != playerLocationX || j != playerLocationY))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}

