using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSphereEngine : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(WalkAnimation());
    }

    IEnumerator WalkAnimation()
    {
        while (true) {
            animator.SetBool("Walk_Anim", true);
            yield return new WaitForSeconds(5);

            animator.SetBool("Walk_Anim", false);
            yield return new WaitForSeconds(2);

        }
    }
}
