using UnityEngine;
using UnityEngine.AI;

public class Enemy_Patrol : MonoBehaviour
{
    [Header("Navemesh")]
    [SerializeField] private NavMeshAgent agent;

    [Header("Waypoint")]
    [SerializeField] private Transform[] WayPoint;

    [Header("distnation speed")]
    [SerializeField] private float ReachDistance = 0.5f;
    [SerializeField] private float WaitTime = 30f;

    private int CurrentWayPoint = -1;
    private float WaitTimer;

    private bool Waiting;
    private bool patrolActive;
    private void Awake()
    {
        if (agent == null)

            agent = GetComponent<NavMeshAgent>();

    }
    public void StartPatrol() 
    {
        patrolActive = true;
        Waiting = false;
        WaitTimer = 0f;
        agent.isStopped = false;
        GoToWayPoint();
    
    
    
    }   
    
    public void UpdatePatrol()
{
        if (!patrolActive)
            return;
        if (WayPoint == null || WayPoint.Length == 0)
            return;

        //Waiting
        if (Waiting) {

            WaitTimer -= Time.deltaTime;
            if (WaitTimer <= 0f)
            {
                Waiting = false;
                GoToWayPoint();

            }
            return;
          }

        if (!agent.pathPending && agent.remainingDistance <= ReachDistance)
            {
            Waiting = true;
            WaitTimer = WaitTime;
            agent.isStopped = true;
            Debug.Log("Waypoint reached - waitTime"+WaitTime+ "second");         
             }
}

    private void GoToWayPoint() 
    {
       if (WayPoint == null || WayPoint.Length == 0)
            return;

        int RandomIndex;

        do
        {
            RandomIndex = Random.Range(0,WayPoint.Length);

        }
        while (RandomIndex == CurrentWayPoint && WayPoint.Length >1);
        CurrentWayPoint = RandomIndex;
        agent.isStopped = false;
        agent.SetDestination(WayPoint[CurrentWayPoint].position);
        Debug.Log("Going to WayPoint: " + WayPoint[CurrentWayPoint].name);



    }
    public void StopPatrol() 
    {
        patrolActive = false;
        if (agent != null)
        {
            agent.ResetPath();
            agent.isStopped = true;
        
        }
        Waiting = false;



        Waiting = false;
        agent.isStopped = true;
    
    
    }
}
