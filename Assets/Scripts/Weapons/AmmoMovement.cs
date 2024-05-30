using Mono.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class AmmoMovement : MonoBehaviour
{
    [SerializeField] LayerMask hitable;
    [SerializeField] float speed = 35;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(SelfDestruct), 3);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
        CheckCollision();
    }

    private void CheckCollision()
    {
        if (!Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 1f, hitable)) {
            return;
        }

        if (hit.collider.CompareTag("Laser")) {
            // Ignore colliding lasers
            return;
        }

        if (hit.collider.CompareTag("Enemy")) {
            Destroy(hit.collider.gameObject);
        }

        if (hit.collider.CompareTag("Player")) {
            HealthManager.instance.TakeDamage(5);
        }

        SelfDestruct();
    }

    void SelfDestruct() {
        Destroy(gameObject); 
    }

}
