
using System.Collections.Generic;
using UnityEngine;
public class GateController : MonoBehaviour 
{
    //Delegate

    public delegate void OnTouchGate(List<GameObject> prefabSpawners);

    //Event
    public OnTouchGate onTouchGate;

    [SerializeField] List<GameObject> prefabSpawners;

    [SerializeField] GameObject background;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GateTrigger"))
        { 
            onTouchGate?.Invoke(prefabSpawners);
            if (background != null)
            { 
                background.GetComponent<ParallaxController>().Activate();
            }
        }
    }
}