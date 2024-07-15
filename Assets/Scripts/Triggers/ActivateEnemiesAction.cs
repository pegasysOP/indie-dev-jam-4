using System.Collections.Generic;

public class ActivateEnemiesAction : EventAction
{
    public List<BaseEnemy> enemies = new List<BaseEnemy>();

    public override void Execute()
    {
        foreach (BaseEnemy enemy in enemies)
        {
            enemy.Activate();
        }
    }
}
