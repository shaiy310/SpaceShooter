using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    // Variables
    public float rotationSpeed;
    [SerializeField] Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 10f;
    }

    void OnMouseDrag()
    {
        if (Input.GetMouseButton(0))
        {
            float rotateX = Input.GetAxis("Mouse X") * rotationSpeed;
            float rotateY = Input.GetAxis("Mouse Y") * rotationSpeed;

            Vector3 right = Vector3.Cross(cam.transform.up, transform.position - cam.transform.position);
            Vector3 up = Vector3.Cross(transform.position - cam.transform.position, right);

            transform.rotation = Quaternion.AngleAxis(-rotateX, up) * transform.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        OnMouseDrag();
    }
}
