using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovementController : MonoBehaviour
{
    private bool canBeControlled;

    // Update is called once per frame
    void Update()
    {
        if(canBeControlled)
        {
            transform.position = Utility.MousePosition();
        }
    }

    private void OnMouseDown()
    {
        if(Input.GetMouseButton(0))
        {
            canBeControlled = true;
        }
    }
}
