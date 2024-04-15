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
    Dictionary<State, Vector3> shootingPositions;

    // Weapons
    [SerializeField] WeaponBase weapon;

    // Start is called before the first frame update
    void Start()
    {
        maxDistance = 500;
        shootingPositions = new Dictionary<State, Vector3>() {
            { State.Standing, new Vector3(.25f, 1.6f, 1.5f) }
        };
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("TODO: get state from player movement script");
        State state = State.Standing;

        if (Input.GetMouseButton(0))
        {
            origin = cameraPos.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            var rotation = transform.localRotation.eulerAngles + 90 * Vector3.right;
            Instantiate(weapon.Ammo.Bullet,
                transform.TransformPoint(shootingPositions[state]),
                Quaternion.Euler(rotation)
                //Quaternion.identity,
            //Quaternion.AngleAxis(90, Vector3.right),
                //transform
            );

            //if (Physics.Raycast(origin, cameraPos.transform.forward, out hit, maxDistance, rayCastHitable)) {
            //    hit.transform.GetComponent<MeshRenderer>().material.color = Color.red;

            //}
        }
    }
}
