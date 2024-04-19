using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    // Variables
    public float rotationSpeed;
    private bool isDragging;
    private Vector3 lastMousePosition;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        SpinWeapon();
    }

    void SpinWeapon()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 currentMousePosition = lastMousePosition;
            Vector3 mouseDelta = currentMousePosition - lastMousePosition;

            transform.Rotate(Vector3.up, -mouseDelta.x * rotationSpeed, Space.Self);
            lastMousePosition = currentMousePosition;
        }
    }
}
