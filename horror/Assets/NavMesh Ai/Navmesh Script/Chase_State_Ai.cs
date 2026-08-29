using UnityEngine;

public class Chase_State_Ai : Enemy_State
{
    public Chase_State_Ai(Enemy_MainController Enemy)
    : base(Enemy) 
    {
    }

    public override void Enter()
    {
        Enemy.Agent.isStopped = false;
    }
    public override void Update()
    {
        if (Enemy.detection.CanSeePlayer())
        {
            Enemy.Agent.SetDestination(Enemy.player.position);

        }
        else
        { 
        
            Enemy.ChangeState(Enemy.SearchState);
        }
        
    }
    public override void Exit()
    {
        

    }
    
}
