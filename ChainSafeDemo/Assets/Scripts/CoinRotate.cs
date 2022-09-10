using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    void Update()
    {
        // rotates object, you can change the numbers here to increase rotation speed
        gameObject.transform.Rotate(50 * Time.deltaTime, 0, 0);
    }
}
