using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{
    // Shots
    Vector3 origin;
    RaycastHit hit;
    int maxDistance;
    [SerializeField] Camera cameraPos;
    [SerializeField] LayerMask rayCastHitable;
    Dictionary<State, Vector3> shootingPositions;

    // Weapons
    [SerializeField] WeaponBase weapon;

    // Start is called before the first frame update
    void Start()
    {
        maxDistance = 30;
        shootingPositions = new Dictionary<State, Vector3>() {
            { State.Standing, new Vector3(.25f, 1.6f, 1.5f) }
        };
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        Interaction();
    }

    void Interaction()
    {
        if (!Input.GetKeyDown(KeyCode.E)) {
            return;
        }

        origin = cameraPos.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Vector3 direction = cameraPos.transform.forward;
        if (!Physics.Raycast(origin, direction, out hit, maxDistance, rayCastHitable)) {
            return;
        }

        ConsoleScreen console = hit.collider.GetComponent<ConsoleScreen>();
        console?.Interact();
    }

    void Shoot()
    {
        Debug.Log("TODO: get state from player movement script");
        State state = State.Standing;

        if (Input.GetMouseButton(0)) {
            origin = cameraPos.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            var rotation = transform.localRotation.eulerAngles + 90 * Vector3.right;
            Instantiate(weapon.Ammo.Bullet,
                transform.TransformPoint(shootingPositions[state]),
                Quaternion.Euler(rotation)
            );
        }
    }
}
