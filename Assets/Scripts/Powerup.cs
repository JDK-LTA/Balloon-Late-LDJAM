using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public PU powerUp;
    public int akAmmo;
    public List<float> sizes;
    public float timer;

    float speed;
    [HideInInspector] public float size = 0.6f;
    ObstacleSpawner os;
    SpriteRenderer sr;
    private void Start()
    {
        os = FindObjectOfType<ObstacleSpawner>();
        speed = os.obstacleSpeed;

        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (size > GameManager.Instance.balloon.size)
        {
            sr.color = new Color32(255, 95, 95, 255);
        }
    }
    public void SetSize(float s)
    {
        size = s;
        transform.localScale *= s;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ObstacleDestroyer")
        {
            Destroy(gameObject);
        }
    }
}
