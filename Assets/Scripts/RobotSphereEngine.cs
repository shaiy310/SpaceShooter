using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSphereEngine : MonoBehaviour
{
    [SerializeField] List<Vector3> Routes;
    [SerializeField] AmmoBase laser;
    [SerializeField] LayerMask hitable;

    Animator animator;
    float speed;
    float shootingCoolDown;
    float lastShotTime;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        speed = 2f;
        shootingCoolDown = .5f;

        if (Routes.Count > 0) {
            StartCoroutine(Patrol());
        }
    }

    private void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (lastShotTime + shootingCoolDown > Time.time) {
            // shooting cooldown
            return;
        }

        if (!Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 8f, hitable)) {
            return;
        }

        // Shoot at player
        Instantiate(laser.Bullet,
            transform.TransformPoint(new Vector3(0, 0.05f, 0.06f)),
            Quaternion.LookRotation((hit.collider.transform.position - transform.position).normalized)
        );
        lastShotTime = Time.time;
    }

    IEnumerator Patrol()
    {
        while (true) {
            foreach (var route in Routes) {
                animator.SetBool("Walk_Anim", true);
                yield return Walk(route);

                animator.SetBool("Walk_Anim", false);
                yield return new WaitForSeconds(1.5f);
            }

        }
    }

    IEnumerator Walk(Vector3 delta)
    {
        Vector3 target = transform.position + delta;
        Vector3 speedVector = speed * delta.normalized;

        transform.LookAt(target);
        while (Vector3.Distance(transform.position, target) > 0.25f) {
            transform.position += Time.deltaTime * speedVector;
            yield return null;
        }
    }
}
