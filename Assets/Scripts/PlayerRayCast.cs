using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{
    // Shots
    Vector3 origin;
    RaycastHit hit;
    int maxDistance;
    [SerializeField] GameObject cameraPos;
    [SerializeField] LayerMask rayCastHitable;

    // Start is called before the first frame update
    void Start()
    {
        maxDistance = 500;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            origin = cameraPos.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(origin, cameraPos.transform.forward, out hit, maxDistance, rayCastHitable))
            {
                hit.transform.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
    }
}
