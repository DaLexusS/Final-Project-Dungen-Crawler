namespace Final_Project___Ivgeni_Flieshman
{
    public class Enemy
    {
        Random rng = new Random();
        public int Health { get; set; }
        public int Damage { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public int AttackRange = 1;
        public int DetectionRange = 3;

        private string[] enemyNames = new string[4] { "Zombie", "Warrior", "Vampire", "Statue" };
        private string[] enemySideNames = new string[4] { "Dirty", "Broken", "Weak", "Strong" };

        public Enemy(MapManager mapManager)
        {
            Health = mapManager.CurrentMapLevel + rng.Next(1, 4);
            Damage = mapManager.CurrentMapLevel + rng.Next(1, 2);
            GenerateName();
        }

        public void Reset()
        {
            X = 999;
            Y = 999;
        }

        private void GenerateName()
        {
            Random random = new Random();
            string randomEnemyName = enemyNames[random.Next(0, enemyNames.Length)];
            string randomSideName = enemySideNames[random.Next(0, enemySideNames.Length)];

            Name = $"{randomSideName} {randomEnemyName}";
        }

        public void Fight(Player player, MapManager mapManager)
        {
            int finalEnemyHealth = Math.Max(0, Health - player.Damage);
            Health = finalEnemyHealth;

            if (Health <= 0)
            {
                mapManager.EnemyCounter--;

                X = 999;
                Y = 999;
                return;
            }

            int finalPlayerHealth = Math.Max(0, player.Health - Damage);
            player.Health = finalPlayerHealth;

            if (player.Health <= 0)
            {
                X = 999;
                Y = 999;
            }
        }
    }
}
