using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public static int mainCoins;
    public static int shopCoins;

    // Start is called before the first frame update
    void Start()
    {
        shopCoins = 2;
        mainCoins = 10;
    }
}
