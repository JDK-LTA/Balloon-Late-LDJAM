using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundInfinite : MonoBehaviour
{
    public Transform lastPoint, startPoint;
    public float speed = 2;
    public bool shouldDestroy = false;
    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < lastPoint.position.y)
        {
            transform.position = startPoint.position;
            if (shouldDestroy)
            {
                Destroy(gameObject);
            }
        }
    }
}
