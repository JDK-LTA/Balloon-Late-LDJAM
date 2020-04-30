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
    private void Start()
    {
        os = FindObjectOfType<ObstacleSpawner>();
        speed = os.obstacleSpeed;
    }
    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
    public void SetSize(float s)
    {
        size = s;
        transform.localScale *= s;
    }
}
