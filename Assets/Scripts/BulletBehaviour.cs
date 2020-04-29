using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    float speed = 2f;
    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Explosive ex = collision.GetComponent<Explosive>();
        if (ex != null)
        {
            Debug.Log("ex");
            ex.OverlapExplosion();
            Destroy(gameObject);
        }
        else if (collision.tag == "Obstacle")
        {
            Debug.Log("ob");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.tag == "BulletDestroyer")
        {
            Destroy(gameObject);
        }

    }
}
