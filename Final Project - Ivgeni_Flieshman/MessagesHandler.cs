namespace Final_Project___Ivgeni_Flieshman
{
    public class MessagesHandler
    {

        public void ResetMessages(MapManager map, Player player)
        {
            Console.Clear();
            Console.ResetColor();
            map.DrawMap();
            PrintStats(map.currentMapLevel, player);
        }

        public void PrintStats(int currentLevel, Player player)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Current Level : {currentLevel}");
            Console.WriteLine("Press the A/W/S/D or the Arrow keys to move.");
            Console.WriteLine($"Player health - {player.health}");
            Console.WriteLine($"Player Damage - {player.damage}");
            Console.WriteLine($"Player Weapon - {player.currentWeapon}");
            Console.ResetColor();
        }

        public void EnemyNearby(string enemyName, int enemyHealth, int enemyDamage)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"You found {enemyName} press [ENTER] to fight.");
            Console.WriteLine($"Health - {enemyHealth}");
            Console.WriteLine($"Damage - {enemyDamage}");
            Console.ResetColor();
        }

        public void TreasureNearby()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("You found a treasure chest Press [ENTER] to open it.");
            Console.ResetColor();
        }

        public void PlayerTriedExit(int enemyCounter)
        {
            if (enemyCounter != 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("");
                Console.WriteLine("You need to deafet all the enemies on the level in order to exit.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You found the exit Press [ENTER] to exit to the next level.");
            }

            Console.ResetColor();
        }
    }
}
