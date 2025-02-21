using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float smoothness = 0.1f;
    [SerializeField] float leanAngle = 15f;
    [SerializeField] float leanSpeed = 5;

    [SerializeField] GameObject model;

    [Header("Camera Bounds")]
    [SerializeField] Transform cameraFollow;
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;

    InputReader input;

    Vector3 currentVelocity;
    Vector3 targetPosition;

    private void Start()
    {
        input = GetComponent<InputReader>();
    }

    private void Update()
    {
        MovePlayer();
    }


    private void MovePlayer()
    {
        targetPosition += new Vector3(input.Move.x, input.Move.y, 0f) * (speed * Time.deltaTime);

        float minPlayerX = cameraFollow.position.x + minX;
        float maxPlayerX = cameraFollow.position.x + maxX;
        float minPlayerY = cameraFollow.position.y + minY;
        float maxPlayerY = cameraFollow.position.y + maxY;

        targetPosition.x = Mathf.Clamp(targetPosition.x, minPlayerX, maxPlayerX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minPlayerY, maxPlayerY);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothness);


        float targetRotationAngle = input.Move.y * leanAngle;

        float currentRotation = transform.localEulerAngles.z;

        float newZRotation = Mathf.LerpAngle(currentRotation, targetRotationAngle, leanSpeed * Time.deltaTime);

        transform.localEulerAngles = new Vector3(0f, 0f, newZRotation);
    }
}
