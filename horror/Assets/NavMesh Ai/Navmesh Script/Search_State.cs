using UnityEngine;

public class Search_State : Enemy_State
{
    private float searchTimer;
    private Vector3 lastknownposition;
    private bool reachedlastknownposition;

    private const float SearchDuration = 4f;
    private const float LookAroundSpeed = 50f;


    public Search_State(Enemy_MainController Enemy)
        : base(Enemy)
    {
    }


    public override void Enter()
    {
        lastknownposition = Enemy.player != null ? Enemy.player.position : Enemy.Agent.transform.position;
        Enemy.Agent.isStopped = false;
        Enemy.Agent.SetDestination(lastknownposition);

        searchTimer = SearchDuration;
        reachedlastknownposition = false;
    }


    public override void Update()
    {
        // Player found again
        if (Enemy.detection.CanSeePlayer())
        {
            Enemy.ChangeState(Enemy.ChaseState);

            return;
        }


        // Countdown
        searchTimer -= Time.deltaTime;


        // Search finished
        if (!reachedlastknownposition && !Enemy.Agent.pathPending && Enemy.Agent.remainingDistance <= Enemy.Agent.stoppingDistance + 0.15f)
        {
            reachedlastknownposition = true;
            Enemy.Agent.isStopped = true;
        }
        if (reachedlastknownposition)
        {
            Enemy.Agent.transform.Rotate(Vector3.up, LookAroundSpeed * Time.deltaTime);
        }
        if (searchTimer <= 0f)
        {
            Enemy.ChangeState(Enemy.PatrolState);
        }

    
    }


    public override void Exit()
    {
        Debug.Log("SEARCH COMPLETE");
        Enemy.Agent.isStopped = false;
    }



}
