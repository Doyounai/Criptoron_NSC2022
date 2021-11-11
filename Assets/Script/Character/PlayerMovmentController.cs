using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovmentController : MonoBehaviour
{
    private CharacterController controller;
    public Transform mainCamera;

    public float speed = 0;
    public float Accelation = 3f;
    public float MaxSpeed = 7f;

    public bool is_Canmove = true;
    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Y))
            Application.Quit();

        if (!is_Canmove)
            return;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 diraction = new Vector3(horizontal, 0, vertical).normalized;

        if(diraction.magnitude >= 0.1f)
        {
            if(speed < MaxSpeed)
            {
                speed += Accelation;
            }
        }
        else
        {
            speed = 0;
        }

        movement(diraction);
    }

    void movement(Vector3 diraction)
    {
        Vector3 cameraForward = Vector3.Scale(mainCamera.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveDiracter = diraction.z * cameraForward + diraction.x * mainCamera.right;

        controller.Move(moveDiracter.normalized * speed * Time.deltaTime);
    }

    public void restart()
    {
        transform.position = startPosition;
    }
}
