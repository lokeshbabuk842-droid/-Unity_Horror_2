using UnityEngine;

public abstract class  Enemy_State 
{
    protected Enemy_MainController Enemy;

    public Enemy_State(Enemy_MainController enemy)
    {
        Enemy = enemy;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();

    
}
