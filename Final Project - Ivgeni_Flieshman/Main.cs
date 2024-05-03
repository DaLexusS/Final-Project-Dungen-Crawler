using Final_Project___Ivgeni_Flieshman;

MapManager mapManager = new MapManager();
Player player = new Player();
KeyboardManager keyboardManager = new KeyboardManager();
MessagesHandler messagesHandler = new MessagesHandler();

mapManager.InitializeMap(player);

Thread combatThread = new Thread(() =>
{
    while (true)
    {
        Thread.Sleep(1000);

        if (player.isAlive)
        {
            Enemy nearbyEnemy = mapManager.CheckNearbyEnemy();

            if (nearbyEnemy != null)
            {
                if (nearbyEnemy.Health <= 0)
                {
                    nearbyEnemy.Reset();
                    mapManager.UpdateSprites();
                    nearbyEnemy = null;
                }
                else
                {
                    nearbyEnemy.Fight(player, mapManager);
                    mapManager.DrawMap();
                }
            }
        }
    }
});

combatThread.Start();

while (true)
{
    if (!player.isAlive || player.Health <= 0)
    {
        player.Reset();
        player = null;
        player = new Player();
        mapManager.DeathScreen(player);
    }

    keyboardManager.ReadKeys(mapManager, player);
    mapManager.DrawMap();

    Treasure nearbyTreasure = mapManager.CheckNearbyChest();

    if (nearbyTreasure != null && mapManager.EnemyCounter <= 0)
    {
        if (nearbyTreasure.Looted)
        {
            nearbyTreasure.ResetChest();
        }
        else
        {
            nearbyTreasure.GiveOption();
        }

        mapManager.DrawMap();
        Console.WriteLine(nearbyTreasure.OutCome);
        nearbyTreasure = null;
    }

    bool canExit = mapManager.CheckNearbyExit();

    if (canExit)
    {
        if (mapManager.EnemyCounter > 0)
        {
            messagesHandler.PlayerTriedExit();
        }
        else
        {
            mapManager.CurrentMapLevel++;

            if (mapManager.CurrentMapLevel > 10)
            {
                mapManager.WinScreen(player);
            }
            else
            {
                mapManager.InitializeMap(player);
            }
        }
    }
}
