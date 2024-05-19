using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleScreen : MonoBehaviour, IInteractable
{
    [SerializeField] string message;
    
    public void Interact()
    {
        Debug.Log($"hit console: {message}");
    }
}
