namespace Final_Project___Ivgeni_Flieshman
{
    public class Player
    {
        public int MaxHealth { get; set; }
        public int Health { get; set; }
        public string CurrentWeapon { get; set; }
        public int Damage { get; set; }
        public int AttackRange { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public bool isAlive = true;

        public Player()
        {
            MaxHealth = 10;
            Health = MaxHealth;
            CurrentWeapon = "Hands";
            Damage = 1;
            AttackRange = 1;
        }

        public void TakeDamage(int damage)
        {
            int calculatedHealth = Health - damage;
            Health = Math.Max(0, calculatedHealth);

            if (Health <= 0)
            {
                isAlive = false;
                Reset();
            }
        }

        public void Heal(int heal)
        {
            int calculatedHealth = Health + heal;
            Health = Math.Min(MaxHealth, calculatedHealth);
        }

        public void IncreaseMaxHealth(int healthIncrease)
        {
            MaxHealth += healthIncrease;
        }

        public void EquipWeapon(string weaponName, int damageIncrease)
        {
            Damage += damageIncrease;
            CurrentWeapon = weaponName;
        }

        public void Reset()
        {
            isAlive = false;
            X = 1000;
            Y = 1000;
        }
    }
}
