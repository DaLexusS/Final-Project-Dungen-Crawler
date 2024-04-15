public class MapProvider
{
    public static char[][] GetMapForLevel(int level)
    {
        switch (level)
        {
            case 0:
                return DeathScreen();
            case 1:
                return MapForLevel1();
            case 2:
                return MapForLevel2();
            default:
                throw new ArgumentException("Map for the specified level not found");
        }
    }

    private static char[][] DeathScreen()
    {
        return new char[][]
        {
            "|---------------------------|".ToCharArray(),
            "|                           |".ToCharArray(),
            "|         You Died!         |".ToCharArray(),
            "|                           |".ToCharArray(),
            "|Press [ENTER] to start over|".ToCharArray(),
            "|                           |".ToCharArray(),
            "|                           |".ToCharArray(),
            "|                           |".ToCharArray(),
            "|                           |".ToCharArray(),
            "|                           |".ToCharArray(),
            "|                           |".ToCharArray(),
            "|---------------------------|".ToCharArray(),
        };
    }

    private static char[][] MapForLevel1()
    {
        return new char[][]
        {
            "|---------------------------|".ToCharArray(),
            "|    |      *            *  |".ToCharArray(),
            "|    |                      |".ToCharArray(),
            "|    |     -------          |".ToCharArray(),
            "|----|     |     |       #  |".ToCharArray(),
            "|          |     |     -----|".ToCharArray(),
            "|          |     |     |    |".ToCharArray(),
            "|          |     |     |    |".ToCharArray(),
            "|          |     |     -----|".ToCharArray(),
            "|          |     |          |".ToCharArray(),
            "|   E      |     |       X  |".ToCharArray(),
            "|---------------------------|".ToCharArray(),
        };
    }

    private static char[][] MapForLevel2()
    {
        return new char[][]
        {
            "|---------------------------|".ToCharArray(),
            "|E   |                    X |".ToCharArray(),
            "|    |                      |".ToCharArray(),
            "|    |--------------        |".ToCharArray(),
            "|-  -|                      |".ToCharArray(),
            "|                      -----|".ToCharArray(),
            "|                           |".ToCharArray(),
            "|                      |    |".ToCharArray(),
            "|-------------------------  |".ToCharArray(),
            "|  #      |           |     |".ToCharArray(),
            "|               |           |".ToCharArray(),
            "|---------------------------|".ToCharArray(),
        };
    }
}
