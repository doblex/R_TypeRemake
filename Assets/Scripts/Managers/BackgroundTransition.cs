using UnityEngine;
using static GateController;

public class BackgroundTransition : MonoBehaviour
{
    public SpriteRenderer currentBackground;
    public float transitionSpeed = 1f;

    private bool isTransitioning = false;
    private float alpha = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GateTrigger"))
            StartTransition();
    }

    public void StartTransition()
    {
        isTransitioning = true;
    }

    void Update()
    {
        if (isTransitioning)
        {
            alpha += Time.deltaTime * transitionSpeed;
            currentBackground.color = new Color(1, 1, 1, 1 - Mathf.Clamp01(alpha));

            if (alpha >= 1f)
            {
                isTransitioning = false;
                alpha = 0f;
            }
        }
    }
}
