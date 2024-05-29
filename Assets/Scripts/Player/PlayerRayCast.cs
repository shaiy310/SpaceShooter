using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{
    public static PlayerRayCast instance;

    // Shots
    [SerializeField] Camera cameraPos;
    [SerializeField] LayerMask rayCastHitable;
    [SerializeField] Animator playShotAnim;
    Dictionary<State, Vector3> shootingPositions;

    // Interactions
    [SerializeField] TextMeshProUGUI instructions;
    int maxDistance;

    // Weapons
    [SerializeField] WeaponBase weapon;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        maxDistance = 3;
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
        Vector3 origin = cameraPos.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Vector3 direction = cameraPos.transform.forward;
        if (!Physics.Raycast(origin, direction, out RaycastHit hit, maxDistance, rayCastHitable)) {
            instructions.enabled = false;
            return;
        }

        if (hit.collider.TryGetComponent<IInteractable>(out var interactable)) {
            instructions.enabled = true;

            if (Input.GetKeyDown(KeyCode.C)) {
                interactable.Interact();
            }
        }
    }

    void Shoot()
    {
        Debug.Log("TODO: get state from player movement script");
        State state = State.Standing;

        if (Input.GetMouseButton(0))
        {
            playShotAnim.SetBool("isBurstShot", true);

            var rotation = transform.localRotation.eulerAngles + 90 * Vector3.right;
            Debug.Log($"w: {weapon} a: {weapon?.Ammo} b: {weapon?.Ammo?.Bullet}");
            Instantiate(weapon.Ammo.Bullet,
                transform.TransformPoint(shootingPositions[state]),
                //Quaternion.identity
                //Quaternion.LookRotation(cameraPos.transform.forward, cameraPos.transform.up) * Quaternion.Euler(90, 0, 0)
                Quaternion.LookRotation(cameraPos.transform.forward)
                //Quaternion.Euler(rotation)
            );
        }
        else
        {
            playShotAnim.SetBool("isBurstShot", false);
        }

        // Reolad function
        playShotAnim.SetBool("isReload", Input.GetKey(KeyCode.E));

    }

    public void SwitchWeapon(WeaponBase newWeapon)
    {
        weapon = newWeapon;
    }
}
