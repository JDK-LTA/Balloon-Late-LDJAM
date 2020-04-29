using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "Bullet")
    //    {
    //        // ANIMACION DE EXPLOSION
    //        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, 3f);
    //        for (int i = colls.Length - 1; i >= 0; i--)
    //        {
    //            if (colls[i].tag == "Obstacle")
    //            {
    //                Destroy(colls[i].gameObject);
    //            }
    //        }
    //    }
    //}

    public void OverlapExplosion()
    {
        // ANIMACION DE EXPLOSION
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, 3f);
        for (int i = colls.Length - 1; i >= 0; i--)
        {
            if (colls[i].tag == "Obstacle" && colls[i].gameObject != gameObject)
            {
                Destroy(colls[i].gameObject);
            }
        }
    }
    public void ExplosionFinished()
    {
        Destroy(gameObject);
    }
}
