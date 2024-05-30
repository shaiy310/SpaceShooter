using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Standing, Walking, Jumping, Bending}
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    // Camera rotation
    [SerializeField] GameObject playerCamera;
    float mouseX;
    float mouseY;
    float lookSpeed;
    float cameraRange;

    // Movement
    Vector3 startingPosition;
    Quaternion startingRotation;

    CharacterController cc;
    float runSpeed;
    float walkSpeed;
    float xAxis;
    float zAxis;
    Vector3 localDirection;
    bool isWalking;
    bool isRespawning;

    // Gravity
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] Transform HeightOfSphereAboveGround;
    [SerializeField] bool groundCheck;
    float gravity;
    Vector3 gravitymove;

    // Animations
    Animator playerMovementAnim;
    //bool animateMove;

    // Inventory
    Inventory skin;
    [SerializeField] Renderer playerWeaponMaterial;
    [SerializeField] Renderer[] playerBodyMaterial;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        Cursor.visible = false;
        startingPosition = transform.position;
        startingRotation = transform.rotation;

        // Rotation
        lookSpeed = 600f;

        // Movement
        cc = GetComponent<CharacterController>();
        runSpeed = 5f;
        walkSpeed = 2f;
        isRespawning = false;

        // Gravity
        groundCheck = false;
        gravity = -9.81f;

        // Animations
        playerMovementAnim = GetComponent<Animator>();

        // Inventory
        playerWeaponMaterial.material = skin.WeaponMaterials[Inventory.WeaponIndex];

        foreach (var part in playerBodyMaterial)
        {
            part.material = skin.BodyMaterials[Inventory.bodyIndex];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PopUpScreen.Instance.gameObject.activeSelf) {
            return;
        }

        if (isRespawning) {
            return;
        }

        CameraRotation();
        Movement();
        IsTouchTheGround();
        Jump();
        
        if (transform.position.y < 105) {
            // In case the player falls off the map.
            StartCoroutine(Respawn());
        }
    }

    void CameraRotation()
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

        if (xAxis != 0 || zAxis != 0)
        {
            playerMovementAnim.SetBool("isRunning", true);
            playerMovementAnim.SetFloat("xAxis", xAxis);
            playerMovementAnim.SetFloat("yAxis", zAxis);
        }
        else
        {
            playerMovementAnim.SetBool("isRunning", false);
        }

        isWalking = Input.GetKey(KeyCode.Z);     

        Bend();
        WalkOrRun(isWalking);

        localDirection = transform.forward * zAxis + transform.right * xAxis;
        cc.Move(localDirection);
    }

    void IsTouchTheGround()
    {
        if (Physics.CheckSphere(HeightOfSphereAboveGround.position, HeightOfSphereAboveGround.localScale.y / 2, groundLayerMask)) {
            groundCheck = true;
        }
        else {
            groundCheck = false;
        }

        if (!groundCheck) {
            gravitymove.y += gravity * Time.deltaTime;
        }
        else { 
            gravitymove.y = 0;
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && groundCheck)
        {
            gravitymove.y += 4f;
        }

        cc.Move(gravitymove * Time.deltaTime);
    }

    void Bend()
    {
        if (Input.GetKeyDown(KeyCode.X)) {
            playerCamera.transform.localPosition += Vector3.down;
            isWalking = true;

        } else if (Input.GetKeyUp(KeyCode.X)) {
            playerCamera.transform.localPosition += Vector3.up;
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

    public IEnumerator Respawn()
    {
        isRespawning = true;
        yield return Fadder.Instance.FadeOut(Color.black, 0.25f);

        transform.SetPositionAndRotation(startingPosition, startingRotation);

        yield return Fadder.Instance.FadeIn(Color.black, 0.25f);
        isRespawning = false;
    }
}
