
namespace Final_Project___Ivgeni_Flieshman
{
    public class Enemy
    {
        public int Health { get; set; }
        public int Damage { get; set; }
        public string EnemyName { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        private string[] enemyNames = new string[4] { "Zombie", "Warrior", "Vampire", "Statue" };
        private string[] enemySideNames = new string[4] { "Dirty", "Broken", "Weak", "Strong" };

        public Enemy()
        {
            Health = 1000;
            Damage = 1;
            GenerateName();
        }

        private void GenerateName()
        {
            Random random = new Random();
            string randomEnemyName = enemyNames[random.Next(0, enemyNames.Length)];
            string randomSideName = enemySideNames[random.Next(0, enemySideNames.Length)];

            EnemyName = $"{randomSideName} {randomEnemyName}";
        }

        public void FightLoop(Player player, MapManager map)
        {
                this.Health -= player.damage;
                
                if (this.Health <= 0)
                {
                    map.currentMap[this.X][this.Y] = map.FreeSpaceIcon;
                    map.EnemyCounter--;
                }

                player.health -= this.Damage;
 
                if (player.health <= 0)
                {
                    this.X = 999;
                    this.Y = 999;
                }
        }
    }
}
