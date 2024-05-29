using Mono.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class AmmoMovement : MonoBehaviour
{
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 50f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
        CheckCollision();
    }

    private void CheckCollision()
    {
        if (!Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 0.1f)) {
            return;
        }

        Debug.Log($"laser collided with {hit.collider.tag} ({hit.collider.name})", hit.collider);
        if (hit.collider.CompareTag("Laser")) {
            // Ignore colliding lasers
            return;
        }

        if (hit.collider.CompareTag("Enemy")) {
            Destroy(hit.collider.gameObject);
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
