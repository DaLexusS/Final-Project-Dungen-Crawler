using Final_Project___Ivgeni_Flieshman;

MapManager Map = new MapManager();
Player Player = new Player();
KeyboardManager KeyboardManager = new KeyboardManager();
MessagesHandler MessagesHandler = new MessagesHandler();

Map.InitializeMap(Player);

Thread combatThread = new Thread(() =>
{
    while (true)
    {
        
        Thread.Sleep(1000);

        if (Player.health <= 0)
        {
            Map.currentMapLevel = 0;
            Map.InitializeMap(Player);
            break;
        }

        Enemy nearbyEnemy = Map.CheckNearbyEnemy();

        if (nearbyEnemy != null)
        {
            if (nearbyEnemy.Health > 0)
            {
                MessagesHandler.ResetMessages(Map, Player);
                MessagesHandler.EnemyNearby(nearbyEnemy.EnemyName, nearbyEnemy.Health, nearbyEnemy.Damage);
                nearbyEnemy.FightLoop(Player, Map);
            }
        }
    }
});

combatThread.Start();

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

    Map.MoveEnemiesTowardsPlayer();
    /*
    Enemy nearbyEnemy = Map.CheckNearbyEnemy();
   
    if (nearbyEnemy != null)
    {
        if (nearbyEnemy.Health > 0) 
        {
            MessagesHandler.EnemyNearby(nearbyEnemy.EnemyName, nearbyEnemy.Health, nearbyEnemy.Damage);

        }
    }*/
}

