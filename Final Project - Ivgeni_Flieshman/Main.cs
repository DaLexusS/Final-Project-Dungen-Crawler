using Final_Project___Ivgeni_Flieshman;

MapManager Map = new MapManager();
Player Player = new Player();
KeyboardManager KeyboardManager = new KeyboardManager();
MessagesHandler MessagesHandler = new MessagesHandler();

Map.InitializeMap();

while (true)
{
    KeyboardManager.ReadKeys(Map, Player);

    MessagesHandler.PrintStats(Map.currentMapLevel, Player);

    if (Map.CheckNearbySymbol(Map.ExitIcon))
    {
        MessagesHandler.PlayerTriedExit(Map.EnemyCounter);
    }
    else if (Map.CheckNearbySymbol(Map.ChestIcon))
    {
        MessagesHandler.TreasureNearby();
    }

    Enemy nearbyEnemy = Map.CheckNearbyEnemy();

    if (nearbyEnemy != null)
    {
        if (nearbyEnemy.Health > 0) 
        {
            MessagesHandler.EnemyNearby(nearbyEnemy.EnemyName, nearbyEnemy.Health, nearbyEnemy.Damage);
        }
    }
}

