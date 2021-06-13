using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility 
{
    public static Vector3 MousePosition()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

    public static float GetRandomDirection()
    {
        return Random.Range(0f, 1f);
    }
}
