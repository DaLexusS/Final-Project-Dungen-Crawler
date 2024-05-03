namespace Final_Project___Ivgeni_Flieshman
{
    public class Trap
    {
        private MessagesHandler messagesHandler = new MessagesHandler();
        public int X { get; set; }
        public int Y { get; set; }
        public int Damage { get; set; }

        public bool IsSet { get; set; } = false;

        public Trap(MapManager map)
        {
            Damage = 1 + map.CurrentMapLevel;
        }

        public void DealDamage(Player player)
        {
            IsSet = true;
            messagesHandler.SteppedOnTrap(Damage);
            player.TakeDamage(Damage);
        }

        public void Reset()
        {
            X = 999;
            Y = 999;
        }
    }
}
