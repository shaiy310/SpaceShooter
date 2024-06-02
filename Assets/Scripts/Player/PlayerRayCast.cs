using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{
    public static PlayerRayCast Instance { get; private set; }

    // Shots
    [SerializeField] Camera cameraPos;
    [SerializeField] LayerMask rayCastHitable;
    [SerializeField] Animator playShotAnim;

    // Interactions
    [SerializeField] TextMeshProUGUI instructions;
    int maxDistance;

    // Weapons
    [SerializeField] AmmoBase ammo;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        maxDistance = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (PopUpScreen.Instance.gameObject.activeSelf)
        {
            return;
        }
		
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
        if (Input.GetMouseButton(0))
        {
            playShotAnim.SetBool("isBurstShot", true);

            Instantiate(ammo.Bullet.gameObject,
                transform.TransformPoint(new Vector3(.25f, 1.6f, 1.5f)),
                Quaternion.LookRotation(cameraPos.transform.forward)
            );
        }
        else
        {
            playShotAnim.SetBool("isBurstShot", false);
        }

        // Reolad function
        playShotAnim.SetBool("isReload", Input.GetKey(KeyCode.E));

    }

    public void SwitchAmmo(AmmoBase newAmmo)
    {
        ammo = newAmmo;
    }
}
