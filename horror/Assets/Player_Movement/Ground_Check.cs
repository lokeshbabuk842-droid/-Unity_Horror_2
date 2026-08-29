using UnityEngine;

public class Ground_Check : MonoBehaviour
{
    [Header("GroundCheck")]
    [SerializeField] private Transform Groundcheck;
    [SerializeField] private float raydistance = 0.3f;
    [SerializeField] private LayerMask GroundMask;

    public bool IsGrounded { get; set; }


    private void Update()
    {
        if (Groundcheck == null)
            Groundcheck = transform; // Self transform as fallback
        RaycastHit hit;
        if (Physics.Raycast(Groundcheck.position, Vector3.down, out hit, raydistance, GroundMask))
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (Groundcheck == null) Groundcheck = transform;
            
        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Gizmos.DrawRay(Groundcheck.position, Vector3.down * raydistance);
    }
}
