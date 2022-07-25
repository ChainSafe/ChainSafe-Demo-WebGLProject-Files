using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Global : MonoBehaviour
{
    public int globalCoins;
    public int globalLives;
    
    void Awake()
    {
        // keeps object alive to track coins and lives through death
        DontDestroyOnLoad(gameObject);
    }
}
