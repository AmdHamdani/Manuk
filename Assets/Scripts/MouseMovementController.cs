using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovementController : MonoBehaviour
{
    private bool canBeControlled;

    private void Update()
    {
        if (canBeControlled)
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
