using UnityEngine;

public class BirdController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Baloon"))
        {
            CheckBaloonPosition(collision.transform);
        }
    }

    private void CheckBaloonPosition(Transform baloon)
    {
        if (IsInLeft(baloon.position, transform.position))
        {
            Debug.Log("Behind");
            direction = Vector2.right;
        }
        else if (IsInRight(baloon.position, transform.position))
        {
            Debug.Log("Before");
            direction = Vector2.left;
        }
        else if (IsBelow(baloon.position, transform.position))
        {
            Debug.Log("Below");
            direction = Vector2.up;
        }
        else if (IsAbove(baloon.position, transform.position))
        {
            Debug.Log("Above");
            direction = Vector3.down;
        }
    }

    private bool IsInLeft(Vector3 a, Vector3 b)
    {
        return (int)a.x < (int)b.x && Difference(a.y, b.y) <= 1f;
    }

    private bool IsInRight(Vector3 a, Vector3 b)
    {
        return (int)a.x > (int)b.x && Difference(a.y, b.y) <= 1f;
    }

    private bool IsBelow(Vector3 a, Vector3 b)
    {
        return (int)a.y < (int)b.y && Difference(a.x, b.x) <= 1f;
    }

    private bool IsAbove(Vector3 a, Vector3 b)
    {
        return (int)a.y > (int)b.y && Difference(a.x, b.x) <= 1f;
    }

    private float Difference(float a, float b)
    {
        return Mathf.Abs(a) - Mathf.Abs(b);
    }
}
