using UnityEngine;
using UnityEngine.AI;

public class Enemy_MainController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navmesh;
    [SerializeField] private Transform Player;
    [SerializeField] private Enemy_Patrol Patrol;
    [SerializeField] private EnemyDetection Detection;

    private Enemy_State CurrentState;

    private Patrol_State_AI patrolState;
    private Chase_State_Ai chaseState;
    private Search_State searchState;


    private void Awake()
    {
        if (navmesh == null) navmesh=GetComponent<NavMeshAgent>();
        if (Patrol == null) Patrol = GetComponent<Enemy_Patrol>();
        if (Detection == null) Detection = GetComponent<EnemyDetection>();

        if (Player == null)
        {
            GameObject P = GameObject.FindGameObjectWithTag("Player");
            if (P != null)
           Player = P.transform;
        }
        
        patrolState= new Patrol_State_AI(this);
        chaseState = new Chase_State_Ai(this);
        searchState = new Search_State(this);
    }


    private void Start()
    {
        ChangeState(patrolState);
    }


    private void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.Update();
        }
    }


   
    // CHANGE STATE

    public void ChangeState( Enemy_State Enemy)
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
        }

        CurrentState = Enemy;

        CurrentState.Enter();
        Debug.Log("State ->" + CurrentState.GetType().Name);
    }
    public NavMeshAgent Agent => navmesh;
    public Transform player => Player;
    public Enemy_Patrol patrol => Patrol;
    public EnemyDetection detection => Detection;

    public Patrol_State_AI PatrolState => patrolState;
    public Chase_State_Ai ChaseState => chaseState;
    public Search_State SearchState => searchState;

    
}
