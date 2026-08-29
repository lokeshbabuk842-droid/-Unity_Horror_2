using UnityEngine;
using UnityEngine.AI;
public class Patrol_State_AI : Enemy_State
{
    public Patrol_State_AI(Enemy_MainController Enemy)
        : base (Enemy)
       {
       }

    public override void Enter()
    {
        Enemy.patrol.StartPatrol();
    }
    public override void Update()
    {
        if (Enemy.detection.CanSeePlayer())
        {
            Enemy.ChangeState(Enemy.ChaseState);
            return;
        
        }
        Enemy.patrol.UpdatePatrol();
    }
    public override void Exit()
    {
        Enemy.patrol.StopPatrol();
    }
    
}
