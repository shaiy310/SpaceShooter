using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    // Camera rotation
    [SerializeField] GameObject playerCamera;
    float mouseX;
    float mouseY;
    float lookSpeed;
    float cameraRange;

    // Movement
    CharacterController cc;
    float runSpeed;
    float walkSpeed;
    float xAxis;
    float zAxis;
    Vector3 localDirection;
    bool isWalking;

    // Gravity
    float radius;
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] Transform HeightOfSphereAboveGround;
    [SerializeField] bool groundCheck;
    float gravity;
    Vector3 gravitymove;

    // Start is called before the first frame update
    void Start()
    {
        // Rotation
        lookSpeed = 600f;

        // Movement
        cc = GetComponent<CharacterController>();
        runSpeed = 15f;
        walkSpeed = 5f;

        // Gravity
        groundCheck = false;
        radius = 0.5f;
        gravity = -9.81f;
    }

    // Update is called once per frame
    void Update()
    {
        cameraRotation();
        Movement();
        isTouchTheGround();
        Jump();
    }

    void cameraRotation()
    {
        mouseX = Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime;
        transform.Rotate(0, mouseX, 0);

        mouseY = Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime;

        cameraRange -= mouseY;
        cameraRange = Mathf.Clamp(cameraRange, -30, 30);

        playerCamera.transform.localRotation = Quaternion.Euler(cameraRange, 0, 0);
    }

    void Movement()
    {
        xAxis = Input.GetAxis("Horizontal") * Time.deltaTime;
        zAxis = Input.GetAxis("Vertical") * Time.deltaTime;

        if (Input.GetKey(KeyCode.Z))
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        Bend();
        WalkOrRun(isWalking);

        localDirection = transform.forward * zAxis + transform.right * xAxis;
        cc.Move(localDirection);
    }

    void isTouchTheGround()
    {
        if (Physics.CheckSphere(HeightOfSphereAboveGround.position, radius, groundLayerMask))
            groundCheck = true;
        else
            groundCheck = false;

        if (!groundCheck)
            gravitymove.y += gravity * Time.deltaTime;
        else
            gravitymove.y = 0;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && groundCheck)
        {
            gravitymove.y += 10;
        }

        cc.Move(gravitymove * Time.deltaTime);
    }

    void Bend()
    {
        if (Input.GetKey(KeyCode.X))
        {
            playerCamera.transform.localPosition = new Vector3(0, 0.8f, 0);
            isWalking = true;
        }
        else
        {
            playerCamera.transform.localPosition = new Vector3(0, 1.8f, 0);
        }
    }

    void WalkOrRun(bool isWalking)
    {
        if (isWalking)
        {
            xAxis *= walkSpeed;
            zAxis *= walkSpeed;
        }
        else
        {
            xAxis *= runSpeed;
            zAxis *= runSpeed;
        }
    }
}
