namespace Final_Project___Ivgeni_Flieshman
{
    public class MessagesHandler
    {
        public void ResetMessages(MapManager map, Player player)
        {
            Console.Clear();
            Console.ResetColor();
            map.DrawMap();
            PrintStats(map.CurrentMapLevel, player);
        }

        public void PrintStats(int currentLevel, Player player)
        {
            Console.WriteLine($"Current Level: {currentLevel}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Player Health: {player.Health}/{player.MaxHealth}");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Player Damage: {player.Damage}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Player Weapon: {player.CurrentWeapon}");
            Console.ResetColor();
        }

        public void EnemyNearby(Enemy enemy, Player player)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"You damaged {enemy.Name} with {player.CurrentWeapon}, he damaged you {enemy.Damage} back!");
            Console.WriteLine($"Enemy has {enemy.Health} HP");
            Console.ResetColor();
        }

        public void PlayerTriedExit()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("");
            Console.WriteLine("You need to kill all the enemies on the level in order to exit.");
            Console.ResetColor();
        }

        public void TreasureOptions(Treasure treasure)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("You opened the chest choose 1 of those 3 options to go on:");
            Console.WriteLine($"[1] - Increase health by {treasure.HealthIncrease}, [2] - {treasure.WeaponInside} increasing damage by {treasure.DamageIncrease}, [3] - Heal yourself {treasure.Heal}");
            Console.ResetColor();
            Console.WriteLine();
        }

        public void SteppedOnTrap(int damage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"You stepped on a trap and lose {damage} HP.");
            Console.ResetColor();
        }
    }
}
