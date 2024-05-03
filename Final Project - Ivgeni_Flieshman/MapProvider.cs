public class MapProvider
{
    public static char[][] GetMapForLevel(int level)
    {
        string filename = $"level{level}.txt";
        string[] lines = File.ReadAllLines(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), $@"..\..\..\Maps\{filename}")));
        return ReturnMapListed(lines);
    }

    private static char[][] ReturnMapListed(string[] lines)
    {
        int height = lines.Length;
        int width = lines[0].Length;
        char[][] map = new char[height][];
        for (int i = 0; i < height; i++)
        {
            map[i] = lines[i].ToCharArray();
        }
        return map;
    }
}
