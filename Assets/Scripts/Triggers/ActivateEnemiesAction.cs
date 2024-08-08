using System.Collections.Generic;

public class ActivateEnemiesAction : EventAction
{
    public List<Enemy> enemies = new List<Enemy>();

    public override void Execute()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.Activate();
        }
    }
}
