using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSphereEngine : MonoBehaviour, IEnemy
{
    [SerializeField] int health = 25;
    [SerializeField] AmmoBase laser;
    [SerializeField] LayerMask hitable;
    [SerializeField] List<Vector3> Routes;

    Animator animator;
    float speed;
    float shootingCoolDown;
    float lastShotTime;
    bool pausePatrol;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        speed = 2f;
        shootingCoolDown = .5f;
        pausePatrol = false;

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

        //if (!Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 8f, hitable)) {
        //    return;
        //}
        if (Vector3.Distance(PlayerMovement.Instance.transform.position, transform.position) > 8) {
            pausePatrol = false;
            return;
        }

        pausePatrol = true;

        // Shoot at player
        var origin = transform.position + new Vector3(0, 0.55f, 0.06f);
        Instantiate(laser.Bullet.gameObject,
            origin,
            Quaternion.LookRotation((PlayerMovement.Instance.transform.position + Vector3.up - origin))
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
            yield return new WaitUntil(() => !pausePatrol);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0) {
            StartCoroutine(KillAnimation());
        } else {
            StartCoroutine(HitAnimation());
        }
    }

    IEnumerator HitAnimation()
    {
        var renderer = GetComponentInChildren<MeshRenderer>();
        
        for (int i = 0; i < 3; ++i) {
            renderer.material.color = Color.red;
            yield return new WaitForSeconds(0.2f);

            renderer.material.color = Color.white;
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator KillAnimation()
    {
        var childObject = transform.GetChild(0).gameObject;
        for (int i = 0; i < 3; ++i) {
            childObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);

            childObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }

        Coins.mainCoins += 2;
        Destroy(gameObject);
    }
}
