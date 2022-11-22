using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    void Update()
    {
        // rotates object
        gameObject.transform.Rotate(50 * Time.deltaTime, 0, 0);
    }
}
