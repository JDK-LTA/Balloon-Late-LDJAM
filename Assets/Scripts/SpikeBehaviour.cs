using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBehaviour : MonoBehaviour
{
    public float downSpeed = 2;
    //public int difficulty;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * downSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ObstacleDestroyer")
        {
            Destroy(gameObject);
        }
    }
}
