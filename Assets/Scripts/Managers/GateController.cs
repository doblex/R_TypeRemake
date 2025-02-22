
using System.Collections.Generic;
using UnityEngine;
public class GateController : MonoBehaviour 
{
    //Delegate

    public delegate void OnTouchGate(List<GameObject> prefabSpawners, Transform nextStart);

    //Event
    public OnTouchGate onTouchGate;

    [SerializeField] List<GameObject> prefabSpawners;
    [SerializeField] bool isBossGate;
    [SerializeField] GameObject background;
    [SerializeField] Transform  nextStart;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GateTrigger"))
        { 
            onTouchGate?.Invoke(prefabSpawners, nextStart);
            if (background != null)
            { 
                background.GetComponent<ParallaxController>().Activate(!isBossGate);
            }
        }
    }
}