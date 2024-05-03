namespace Final_Project___Ivgeni_Flieshman
{
    public class Treasure
    {
        private MessagesHandler messagesHandler = new MessagesHandler();
        private KeyboardManager keyboardManager = new KeyboardManager();

        private Player player;
        public int DamageIncrease { get; set; }
        public int HealthIncrease { get; set; }
        public string WeaponInside { get; set; }
        public int Heal { get; set; }

        public bool Looted = false;
        public int X { get; set; }
        public int Y { get; set; }

        public string OutCome = "";

        private string[] weaponNames = new string[4] { "Sword", "Axe", "Dagger", "Longsword" };
        private string[] weaponSideNames = new string[4] { "Broken", "Enchanted", "", "Steel" };

        public Treasure(Player player, MapManager map)
        {
            this.player = player;
            DamageIncrease = 1 + map.CurrentMapLevel;
            HealthIncrease = 1 + map.CurrentMapLevel;
            Heal = 1 + map.CurrentMapLevel;
            WeaponInside = string.Empty;

            NameWeapon();
        }

        private void NameWeapon()
        {
            Random random = new Random();
            string randomWeaponName = weaponNames[random.Next(0, weaponNames.Length)];
            string randomSideName = weaponSideNames[random.Next(0, weaponSideNames.Length)];

            WeaponInside = $"{randomSideName} {randomWeaponName}";
        }

        public void ResetChest()
        {
            X = 1001;
            Y = 1001;
        }

        public void GiveOption()
        {
            messagesHandler.TreasureOptions(this);

            if (!Looted)
            {
                int option = keyboardManager.ReadNumberKeys();
                if (option == 1)
                {
                    int oldHealth = player.MaxHealth;
                    player.IncreaseMaxHealth(HealthIncrease);
                    OutCome = $"You increased your max health from {oldHealth} to {player.MaxHealth}";
                    Looted = true;
                    ResetChest();
                }
                else if (option == 2)
                {
                    int oldDamage = player.Damage;
                    player.EquipWeapon(WeaponInside, DamageIncrease);
                    OutCome = $"You picked {WeaponInside} increasing your damage from {oldDamage} to {player.Damage}";
                    Looted = true;
                    ResetChest();
                }
                else if (option == 3)
                {
                    player.Heal(Heal);
                    OutCome = $"You healed yourself + {Heal}";
                    Looted = true;
                    ResetChest();
                }
            }
        }
    }
}
