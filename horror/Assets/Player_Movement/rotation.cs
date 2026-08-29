using UnityEngine;

public class rotation : MonoBehaviour
{
    [Header("Mouse Sensitivity")]
    [SerializeField] private float sensitvity = 2f;

    [Header("Vertical Rotation")]
    [SerializeField] private float minpitch = -50f;
    [SerializeField] private float maxpitch = 40f;

    [Header("Smoothness")]
    [SerializeField] float smoothspeed = 10f;

    private float Yaw;
    private float Pitch;

    private float targetYaw;
    private float targetPitch;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Starting rotation
        targetYaw = transform.eulerAngles.y;
        targetPitch = transform.eulerAngles.x;

        targetYaw = Yaw;
        targetPitch = Pitch;



    }



    private void Update()
    {
        Mousemovement();
        clamPitch();
    }
    private void LateUpdate()
    {
        smoothrotation();
    }


    public void Mousemovement()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
            return;
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        targetYaw += mouseX * sensitvity;
        targetPitch -= mouseY * sensitvity;

    }
    void clamPitch()
    {
        targetPitch = Mathf.Clamp(targetPitch, minpitch, maxpitch);
    }
    void smoothrotation()
    {
        Yaw = Mathf.Lerp(Yaw, targetYaw, Time.deltaTime * smoothspeed);
        Pitch = Mathf.Lerp(Pitch, targetPitch, Time.deltaTime * smoothspeed);
        transform.rotation = Quaternion.Euler(Pitch, Yaw, 0f);

    }
    public float Getpitch()
    {
        return targetPitch;

    }
}
