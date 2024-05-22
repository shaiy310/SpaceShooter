using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenMessage : MonoBehaviour
{
    [SerializeField] TextMeshPro message;
    
    public void PostMessage(string s)
    {
        message.text = s;
    }
}
