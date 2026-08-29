using Unity.Cinemachine;
using UnityEngine;

public class cameraPosition : MonoBehaviour
{
    [Header("Refence's")]
    [SerializeField] private CinemachineCamera cinecamera;
    [SerializeField] private rotation mouserotation;

    [Header("Fov")]
    [SerializeField] private float CurrentFov = 40f;
    [SerializeField] private float ZoomFov = 34.5f;

    [Header("ZoomSmooth")]
    [SerializeField] private float Smoothzoom = 3.5f;

    private float targetfov;

    private void Start()
    {
        targetfov = CurrentFov;
        if (cinecamera != null)
        {
            cinecamera.Lens.FieldOfView = CurrentFov;
        }
    }
    private void Update()
    {
        Fieldofview();
    }
    private void LateUpdate()
    {
        smoothzoom();
    }

    private void Fieldofview()
    {
        if (mouserotation == null)
            return;

        //Mouse rotation la iruthu Getpitch get pandrom;
        float Pitch = mouserotation.Getpitch();

        // camera oda pitch Up & down value;
        float pitchvalue = Mathf.InverseLerp(0f, 60f, Mathf.Abs(Pitch));
       

        // range 0to1;
        float pitchValue = Mathf.Clamp01(pitchvalue);

        //smooth move ku;
        targetfov = Mathf.Lerp(CurrentFov, ZoomFov, pitchValue);
    }

    private void smoothzoom()
    {
        // cinemechine  assign panalana error varum;
        if (cinecamera == null)
            return;
        // get current fov;
        float CurrentFov = cinecamera.Lens.FieldOfView;

        // smoothzoom ku;
        cinecamera.Lens.FieldOfView = Mathf.Lerp(CurrentFov, targetfov, Time.deltaTime * Smoothzoom);


    }
}
