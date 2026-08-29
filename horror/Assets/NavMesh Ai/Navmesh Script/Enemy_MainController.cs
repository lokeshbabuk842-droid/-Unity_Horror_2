using UnityEngine;
using UnityEngine.AI;

public class NavmeshEnemyAAANavmesh_Enemy_Ai : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navmesh;
    [SerializeField] private Transform Player;

    private void Awake()
    {
        navmesh = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        navmesh.destination = Player.position;
    }
}
