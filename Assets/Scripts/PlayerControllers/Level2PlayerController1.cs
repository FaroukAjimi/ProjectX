using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2PlayerController : MonoBehaviour
{
    public Transform Pivot;
    public Transform Camera;
    private CharacterController controller;
    public float rotationSpeed = 5f;
    public float cameraRotation = 50f; 
    public float speed = 5f;

    private bool moveFlag = false;// Flag to track player movement

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float hrzntl = Input.GetAxis("Horizontal");
        float vrtcl = Input.GetAxis("Vertical")/2;
        float mouse = Input.GetAxis("Mouse X");

        // Calculate the rotation angle based on mouse input
        float rotationAngle = mouse * Time.deltaTime * cameraRotation;
        Vector3 MoveDirection = new Vector3(hrzntl, 0, vrtcl);

        if (MoveDirection.magnitude > 0.1f)
        {
            if (moveFlag)
            {
            StartCoroutine(SmoothCameraRotation());
            moveFlag = false;
            }
            // Rotate the player around its own axis
            transform.Rotate(Vector3.up, rotationAngle);

            // Get the forward direction of the player after rotation
            Vector3 forwardDirection = transform.forward;

            // Calculate the movement direction relative to the rotated player
            MoveDirection = hrzntl * transform.right + vrtcl * forwardDirection;
        }
  


        // Rotate the camera pivot when standing still
        if (MoveDirection.magnitude < 0.1f)
        {
            Pivot.Rotate(Vector3.up, rotationAngle);
            moveFlag = true;
        }

        // Move the player based on the new direction vector
        controller.Move(MoveDirection.normalized * Time.deltaTime * speed);
    }

    IEnumerator SmoothCameraRotation()
    {
        // Get initial camera rotation
        Quaternion initialRotation = Pivot.rotation;

        // Calculate target rotation to look back at the player
        Quaternion targetRotation = transform.rotation;

        // Smoothly interpolate between initial and target rotation over time
        float elapsedTime = 0f;
        float smoothTime = 1f; // Adjust this value to control the smoothness
        while (elapsedTime < smoothTime)
        {
            Pivot.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / smoothTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final rotation is exactly the target rotation
        Pivot.rotation = targetRotation;
    }
}

