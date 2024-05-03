using System.Security.Cryptography.X509Certificates;

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
                    break;
                default:
                    break;
            }
        }

        public int ReadNumberKeys()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            ConsoleKey key = keyInfo.Key;
            int chosenKey = 0;

            switch (key)
            {
                case ConsoleKey.D1:
                    chosenKey = 1;
                    break;
                case ConsoleKey.D2:
                    chosenKey = 2;
                    break;
                case ConsoleKey.D3:
                    chosenKey = 3;
                    break;
                default:
                    break;
            }

            return chosenKey;
        }
    }
}
