using System.Security;

namespace Final_Project___Ivgeni_Flieshman
{

    public class Player
    {
        public int health { get; set; }
        public string currentWeapon { get; set; }
        public int damage { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Player() 
        {
            health = 6;
            currentWeapon = "None";
            damage = 1;
        }

        public void DamagePlayer(int damage)
        {
            int playerHealthCalculated = health - damage;

            health = Math.Max(0, playerHealthCalculated);
        }

        public void ResetPlayer()
        {
            this.X = 1000;
            this.Y = 1000;
        }
    }
}
