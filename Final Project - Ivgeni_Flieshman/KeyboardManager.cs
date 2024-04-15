namespace Final_Project___Ivgeni_Flieshman
{
    public class KeyboardManager
    {
        public void ReadKeys(MapManager map, Player player)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            ConsoleKey key = keyInfo.Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    map.PlayerMovedUp();
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    map.PlayerMovedDown();
                    break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    map.PlayerMovedLeft();
                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    map.PlayerMovedRight();
                    break;
                case ConsoleKey.Enter:
                    map.Interact(player);
                    break;
                default:
                    break;
            }
        }
    }
}
