using UnityEngine;
using UnityEngine.Timeline;

public class ParallaxController : MonoBehaviour {
    [SerializeField] Transform[] backgrounds;
    [SerializeField] float smoothing = 10f;
    [SerializeField] float multiplier = 15f;

    Transform mainCamera;
    Vector3 previousCameraPosition;

    bool isActive = false;
    public void Activate(bool active = true) 
    { 
        isActive = active; 
        if(active)
            previousCameraPosition = mainCamera.position; 
    }

    private void Awake()
    {
        mainCamera = Camera.main.transform;
    }

    private void Start()
    {
        previousCameraPosition = mainCamera.position;
    }

    private void Update()
    {
        if (!isActive) return;


        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (previousCameraPosition.x - mainCamera.position.x) * (i * multiplier);
            float TargetX = backgrounds[i].position.x + parallax;

            Vector3 backgroundTarget = new Vector3(TargetX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTarget, smoothing * Time.deltaTime);
        }

        previousCameraPosition = mainCamera.position;
    }

}