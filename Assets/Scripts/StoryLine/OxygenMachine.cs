using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OxygenMachine : MonoBehaviour
{
    [SerializeField] List<ParticleSystem> particleSystems;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>().ToList();
        index = 0;
    }

    public void ActivateMachine()
    {
        if (index < particleSystems.Count) {
            particleSystems[index].Play();
            ++index;

            if (index == particleSystems.Count) {
                // End Mission - Success!
                Debug.Log("TODO: End Mission - Success!");
            }
        }
    }
}
