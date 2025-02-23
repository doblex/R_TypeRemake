using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float speed = 2f;

    [HideInInspector] public bool isLocked = false;

    public void Lock(bool bLock) { isLocked = bLock; }

    private void Start()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }


    private void LateUpdate()
    {
        if (!isLocked)
        {   
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

    }

}
