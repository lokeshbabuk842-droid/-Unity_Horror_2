using UnityEngine;
using System.Collections;

public class Player_DeathRespwan:MonoBehaviour
{
    [Header("Respawn")]
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float respawnDelay = 2.5f;
    [SerializeField] private float invincibilityTime = 1.5f;

    [Header("Distance failsafe")]
    [SerializeField] private Transform enemy;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float killAngle = 60f;
    [SerializeField] private LayerMask walllayermask;

    [Header("Cooldown")]
    [SerializeField] private float deathcooldown = 1.5f;

    public bool isDead;
   private Rigidbody rb;
   private bool isSafe;
   private float lasttimedeath = -10f;

  private void Awake()

    {
       rb = GetComponent<Rigidbody>();
        if (enemy == null)

        {
            GameObject e = GameObject.FindGameObjectWithTag("enemy");
            if (e != null) enemy = e.transform;

        }

    }

  private void Update()

    {
      if (isDead || isSafe || enemy == null)
          return;
      // distance check (2 unit kulla iruka)
      float distance = Vector3.Distance(transform.position, enemy.position);

        if (distance <= attackRange)
        {
            // angle check (Exact a forward la iruka nu check)
            Vector3 directionToPlayer = (transform.position - enemy.position).normalized;
            float angleToPlayer = Vector3.Angle(enemy.forward, directionToPlayer);

            if (angleToPlayer <= (killAngle / 2f))
            {
                // wall check raycast (naduvula eall illama irukka a check)

                Vector3 rayorigin = enemy.position + Vector3.up * 1f;
                Vector3 raytarget = transform.position + Vector3.up * 1f;
                Vector3 rayDirection = (raytarget - rayorigin).normalized;
                float rayDistance = Vector3.Distance(rayorigin, raytarget);

                // raycast walllayermask la patta player safe illna kill!

                if (!Physics.Raycast(rayorigin, rayDirection, rayDistance, walllayermask))
                {
                    Debug.DrawRay(rayorigin, rayDirection * rayDistance, Color.red);
                    Die();
                }
                else
                {
                    Debug.DrawRay(rayorigin, rayDirection * rayDistance, Color.green);
                }

            }
        }
    }

    public void Die()

    {
        if (isDead)
            return;
        if (Time.time - lasttimedeath < deathcooldown)
            return;
        lasttimedeath = Time.time;

        StartCoroutine(DieAadRespwanRoutine());

    }
    private IEnumerator DieAadRespwanRoutine()
    {
        isDead = true;
        Debug.Log("Player killed! respwaing");

        //freeze the player in place during the death pause...
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        yield return new WaitForSeconds(1f);

        if (enemy != null)
        {
            Enemy_MainController controller = enemy.GetComponent<Enemy_MainController>();
            if (controller != null)
                controller.ChangeState(controller.PatrolState);
        }

        // wait for the remaining respwan delay ( total respwan delay -1 second)

        float remainingDelay = Mathf.Max(0, respawnDelay - 1f);
        yield return new WaitForSeconds(remainingDelay);

        //  teleport to spwan point

       if (respawnPoint != null)
        { 
            transform.position = respawnPoint.position;
            transform.rotation = respawnPoint.rotation;

        }
        // unfreeze player

        if (rb != null)
            rb.isKinematic = false;
        isDead = false;
        isSafe = true; // spawn ana udane thirumba  not die

        yield return new WaitForSeconds(invincibilityTime);
        isSafe = false;

    }
}
