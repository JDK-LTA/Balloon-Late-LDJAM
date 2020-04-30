using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject[] powerups;
    public float timer = 5f;
    float t = 0;

    public Transform spawnMidPos;
    List<Vector2> spawnPoss;
    private void Start()
    {
        spawnPoss = new List<Vector2>();

        float aux = -2.25f;
        for (int i = 0; i < 5; i++)
        {
            spawnPoss.Add(new Vector2(aux, spawnMidPos.position.y));
            aux += 1.125f;
        }
    }
    private void Update()
    {
        t += Time.deltaTime;
        if (t>=timer)
        {
            t = 0;
            SpawnPU();
        }
    }

    int k, lastK;
    private void SpawnPU()
    {
        do
        {
            k = Random.Range(0, powerups.Length);

        } while (k == lastK);
        lastK = k;

        Vector2 pos;
        do
        {
            pos = spawnPoss[Random.Range(0, spawnPoss.Count)];
        } while (!CheckPos(pos));

        GameObject go = Instantiate(powerups[k], pos, transform.rotation);
        Powerup pu = go.GetComponent<Powerup>();
        pu.SetSize(pu.sizes[Random.Range(0, pu.sizes.Count)]);
    }

    private bool CheckPos(Vector2 pos)
    {
        if (Physics2D.OverlapBox(pos, new Vector2(0.8f, 0.8f), 0))
        {
            return false;
        }
        return true;
    }
}
