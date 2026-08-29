
using UnityEngine;

public class EnemyDetection : MonoBehaviour

{
    [Header("Refernce")]

    [SerializeField] private Enemy_MainController enemy;
    [SerializeField] private Transform EyePoint;

    [Header(" Detection")]

    [SerializeField] private float DetectionDistance = 6f; // chase pandra thooram
    [SerializeField] private float DetectionAngle = 90f;

    [Header("Backside detection check")]
    [SerializeField] private float BackSideDetectionDistance = 3.1f;

    [Header("LayerMask")]

    [SerializeField] private LayerMask ObstacleLayerMask;


    private void Awake()

    {
        if (enemy == null)
            enemy = GetComponent<Enemy_MainController>();

        if (enemy == null)
            enemy = GetComponentInParent<Enemy_MainController>();

    }

    public bool CanSeePlayer()
    {

        if (enemy == null || enemy.player == null)
            return false;
        Transform player = enemy.player;

        // player die na, enemy kandukka koodathu (ingore dead player)

        Player_DeathRespwan playerstattus = player.GetComponent<Player_DeathRespwan>();
        if (playerstattus != null && playerstattus.isDead)
        {
            return false;
        }

        Vector3 directionToPlayer = player.position - transform.position;
        float distance = directionToPlayer.magnitude;
      
        // distance check (chase distance kulla irukkana?)
        if (distance > DetectionDistance)
            return false;

        // Wall check

        Vector3 origin = EyePoint != null ? EyePoint.position : transform.position + Vector3.up * 1.5f;
        Vector3 target = player.position + Vector3.up * 1f;
        Vector3 raydirection = (target - origin).normalized;
        float distanceToPlayer = Vector3.Distance(origin, target);

        if (Physics.Raycast(origin, raydirection, out RaycastHit hit, distanceToPlayer, ObstacleLayerMask))

        {
            return false; //  wall irukku so player ah paaka mudiyathu
        }

        // Backside Dectection

        if (distance <= BackSideDetectionDistance)
        {
            Debug.DrawRay(origin, raydirection * distanceToPlayer, Color.yellow);
            return true;
        }
  
        // angle check (forward view  iruka)

        Vector3 direction = directionToPlayer.normalized;
        float angle = Vector3.Angle(transform.forward, direction);
        if (angle <= DetectionAngle * 0.4f)
        {
            Debug.DrawRay(origin, raydirection * distanceToPlayer, Color.green);
            return true;

        }
        return false;



    }
}

            
    

