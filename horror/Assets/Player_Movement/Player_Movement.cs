using UnityEngine;

public class Player_Movement :MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float deceleration = 15f;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 12f;

    [Header(" Jump")]
    [SerializeField] private float Jump = 4f;

    [Header("Camera")]
    [SerializeField] private Transform cameraTransform;

    [Header("ReferncesType")]
    [SerializeField] private Ground_Check groundcheck;

    private Rigidbody rb;
    private Player_DeathRespwan deathRespwan;

    private Vector3 movementInput;
    private Vector3 currentVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        deathRespwan = GetComponent<Player_DeathRespwan>();
    }

    private void Update()
    {
        // player die na movement upadate stop!

        if (deathRespwan != null && deathRespwan.isDead)
            return;

        GetMovementInput();
        if (Input.GetKeyDown(KeyCode.Space) && groundcheck.IsGrounded)
        {

            jump();
        }
    }

    private void FixedUpdate()
    {
        // player die na physics movement stop aaganum!

        if (deathRespwan != null && deathRespwan.isDead)
            return;
        Move();
        RotatePlayer();
    }

    public void GetMovementInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Get camera directions
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        // Remove Y because we only want horizontal movement
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        // Camera-relative movement
        movementInput =
            cameraForward * vertical +
            cameraRight * horizontal;

        // Prevent diagonal speed boost
        movementInput = Vector3.ClampMagnitude(movementInput, 1f);
    }
    void jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up *Jump, ForceMode.Impulse);
        

    
     }

    private void Move()
    {
        Vector3 targetVelocity =
            movementInput * moveSpeed;

        float speedChange;

        if (movementInput.sqrMagnitude > 0.01f)
        {
            speedChange = acceleration;
        }
        else
        {
            speedChange = deceleration;
        }

        currentVelocity = Vector3.MoveTowards(
            currentVelocity,
            targetVelocity,
            speedChange * Time.fixedDeltaTime
        );

        rb.linearVelocity = new Vector3(
            currentVelocity.x,
            rb.linearVelocity.y,
            currentVelocity.z
        );
    }

    private void RotatePlayer()
    {
        if (movementInput.sqrMagnitude < 0.01f)
            return;

        // Direction player should face
        Quaternion targetRotation =
            Quaternion.LookRotation(movementInput);

        // Smooth rotation
        Quaternion smoothRotation =
            Quaternion.Slerp(
                rb.rotation,
                targetRotation,
                rotationSpeed * Time.fixedDeltaTime
            );

        rb.MoveRotation(smoothRotation);
    }
}
