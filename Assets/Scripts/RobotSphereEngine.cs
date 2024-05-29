using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSphereEngine : MonoBehaviour
{
    [SerializeField] List<Vector3> Routes;

    Animator animator;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        speed = 2f;

        if (Routes.Count > 0) {
            StartCoroutine(Patrol());
        }
    }

    IEnumerator Patrol()
    {
        while (true) {
            foreach (var route in Routes) {
                animator.SetBool("Walk_Anim", true);
                yield return Walk(route);

                animator.SetBool("Walk_Anim", false);
                yield return new WaitForSeconds(2);
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
