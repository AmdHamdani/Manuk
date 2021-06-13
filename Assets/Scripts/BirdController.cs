using System.Collections;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float speed = 1f;
    public float baloonDistance = 1f;
    public float intervalToStop = 1.5f;
    private Vector3 direction = Vector3.zero;
    private bool isMoving = false;
    private Transform baloon;

    private void Start()
    {
        baloon = GameObject.FindGameObjectWithTag("Baloon").transform;
        StartCoroutine(StopMovement());
    }

    private void Update()
    {
        if (direction != Vector3.zero && isMoving)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Baloon"))
        {
            baloon = collision.transform;
            CheckBaloonPosition(collision.transform);
        }
    }

    private IEnumerator StopMovement()
    {
        while(true)
        {
            yield return null;
            if(isMoving)
            {
                yield return new WaitForSeconds(intervalToStop);
                var distance = Vector2.Distance(baloon.position, transform.position);
                if (distance >= baloonDistance)
                {
                    isMoving = false;
                    direction = Vector2.zero;
                }
            }
        }
    }

    private void CheckBaloonPosition(Transform baloon)
    {
        if (IsInLeft(baloon.position, transform.position))
        {
            SetDirection(Vector2.right);
        }
        else if (IsInRight(baloon.position, transform.position))
        {
            SetDirection(Vector2.left);
        }
        else if (IsBelow(baloon.position, transform.position))
        {
            SetDirection(Vector2.up);
        }
        else if (IsAbove(baloon.position, transform.position))
        {
            SetDirection(Vector2.down);
        }
    }

    private void SetDirection(Vector2 dir)
    {
        direction = dir;
        isMoving = true;
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
