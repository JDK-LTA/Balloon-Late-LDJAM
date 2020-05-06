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
        transform.parent.GetComponent<SpikeBehaviour>().downSpeed = 0f;
        // ANIMACION DE EXPLOSION
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, GameManager.Instance.explosiveRadius);
        gizmosbool = true;
        BalloonBehaviour bb;
        for (int i = colls.Length - 1; i >= 0; i--)
        {
            if (colls[i].gameObject != gameObject)
            {
                if (colls[i].tag == "Obstacle" || colls[i].tag == "Powerup")
                {
                    Destroy(colls[i].gameObject);
                }
                else if (bb = colls[i].gameObject.GetComponent<BalloonBehaviour>())
                {
                    Debug.Log("dead");
                    //bb.Death();
                }
            }
        }
    }

    public void ExplosionFinished()
    {
        Destroy(gameObject);
    }
    bool gizmosbool = false;
    private void OnDrawGizmos()
    {
        if (gizmosbool)
        {

            //Gizmos.color = Color.yellow;
            //Gizmos.DrawSphere(transform.position, GameManager.Instance.explosiveRadius);
        }
    }
}
