using Godot;
using System;

public partial class EnemyTimer : Timer
{
    public override void _Ready()
    {
        Timeout += EnemyTimer_Timeout;
    }

    private void EnemyTimer_Timeout()
    {
        if (Random.Shared.Next(10) == 0)
        {
            GetParent<EnemyManager>().AddEnemy();
        }
        
    }
}
