using System;
using System.Collections.Generic;
using System.Numerics;

namespace Final_Project___Ivgeni_Flieshman
{
    public class MapManager
    {
        public int currentMapLevel = 1;
        public char[][]? currentMap;
        public char ChestIcon = '#';
        public char PlayerIcon = '@';
        public char ExitIcon = 'X';
        public char EnemyIcon = '*';
        public char SpawnPoint = 'E';
        public char FreeSpaceIcon = ' ';
        public int EnemyCounter = 0;
        public List<Enemy> EnemyList = new List<Enemy>();
        public int AttackRange = 2;
        public int EnemyDitection = 4;
        public Player player;

        public void InitializeMap(Player givenPlayer)
        {
            player = givenPlayer;
            currentMap = MapProvider.GetMapForLevel(currentMapLevel);

            DrawMap();
            CountEnemies();
        }

        public void DrawMap()
        {
            Console.Clear();

            for (int row = 0; row < currentMap.Length; row++)
            {
                for (int col = 0; col < currentMap[row].Length; col++)
                {
                    if (currentMap[row][col] == SpawnPoint)
                    {
                        currentMap[row][col] = PlayerIcon;

                        player.X = row;
                        player.Y = col;
                    }
                }
                Console.WriteLine(currentMap[row]);
            }
        }

        public void CountEnemies()
        {
            for (int row = 0; row < currentMap.Length; row++)
            {
                for (int col = 0; col < currentMap[row].Length; col++)
                {
                    if (currentMap[row][col] == EnemyIcon)
                    {
                        Enemy enemy = new Enemy();
                        enemy.X = row;
                        enemy.Y = col;
                        EnemyList.Add(enemy);
                        EnemyCounter++;
                    }
                }
            }
        }

        public Enemy CheckNearbyEnemy()
        {
            foreach (var enemy in EnemyList)
            {
                int distX = Math.Abs(enemy.X - player.X);
                int distY = Math.Abs(enemy.Y - player.Y);

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
            if (CheckNearbySymbol(ChestIcon))
            {
                // Treasure Logic
            }
            else if (CheckNearbySymbol(ExitIcon))
            {
                if (EnemyCounter != 0)
                {
                    return;
                }

                currentMapLevel++;
                InitializeMap(player);
            }

            DrawMap();
        }

        public void MovePlayer(int deltaX, int deltaY)
        {
            int newX = player.X + deltaX;
            int newY = player.Y + deltaY;

            if (IsValidMove(newX, newY))
            {
                currentMap[player.X][player.Y] = FreeSpaceIcon;
                player.X = newX;
                player.Y = newY;
                currentMap[player.X][player.Y] = PlayerIcon;
                Console.Clear();
            }

            DrawMap();
        }

        public void MoveEnemiesTowardsPlayer()
        {
            int playerRow = player.X;
            int playerCol = player.Y;

            foreach (Enemy enemy in EnemyList)
            {
                if (enemy != null)
                {
                    int distX = Math.Abs(playerRow - enemy.X);
                    int distY = Math.Abs(playerCol - enemy.Y);

                    if (distX <= EnemyDitection && distY <= EnemyDitection)
                    {
                        int dirX = Math.Sign(playerRow - enemy.X);
                        int dirY = Math.Sign(playerCol - enemy.Y);

                        int newRow = enemy.X + dirX;
                        int newCol = enemy.Y + dirY;

                        if (IsValidMove(newRow, newCol))
                        {
                            currentMap[enemy.X][enemy.Y] = FreeSpaceIcon;
                            enemy.X = newRow;
                            enemy.Y = newCol;
                            currentMap[enemy.X][enemy.Y] = EnemyIcon;
                        }
                    }
                }
            }
        }

        private bool IsValidMove(int row, int col)
        {
            return row >= 0 && row < currentMap.Length &&
                   col >= 0 && col < currentMap[row].Length &&
                   currentMap[row][col] == FreeSpaceIcon;
        }

        public bool CheckNearbySymbol(char symbol)
        {
            int proximityDistance = 1;

            for (int row = Math.Max(0, player.X - proximityDistance); row <= Math.Min(currentMap.Length - 1, player.X + proximityDistance); row++)
            {
                for (int col = Math.Max(0, player.Y - proximityDistance); col <= Math.Min(currentMap[player.X].Length - 1, player.Y + proximityDistance); col++)
                {
                    if (currentMap[row][col] == symbol && (row != player.X || col != player.Y))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
