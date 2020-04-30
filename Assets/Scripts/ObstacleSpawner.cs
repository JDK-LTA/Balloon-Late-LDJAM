using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    //HAY QUE SPAWNEARLOS DESDE EL CENTRO Y CREAR PREFABS QUE TENGAN COMO HIJOS LOS OBSTACULOS DE VERDAD
    //PARA PODER CREAR DISTINTOS TIPOS DE OBSTACULOS CON DIFERENTES CONFIGURACIONES:        o    oo     <-UN PREFAB
    //                                                                                      ooo   o     <-UN PREFAB
    //                                                                                      o  o  o     <-UN PREFAB
    public Transform spawnPoint;
    public int difficulty = 1;
    int maxDifficulty = 4;
    List<List<GameObject>> allLevelObstacles = new List<List<GameObject>>();
    public List<GameObject> diff1Obstacles;
    public List<GameObject> diff2Obstacles;
    public List<GameObject> diff3Obstacles;
    public List<GameObject> diff4Obstacles;
    [SerializeField] List<GameObject> obstacles = new List<GameObject>();

    public GameObject[] powerups;

    int lastObstacleIndex = -1;

    public float cooldownToSpawn = 2f;
    public float cdMultToSpawnPU = 3;
    float t = 0;
    public float cooldownBetweenLevels = 14f;
    float l = 0;
    public float obstacleSpeed = 2f;

    [Range(1, 100)]
    public int explosivePercentage = 15;

    List<Vector2> spawnPoss = new List<Vector2>();

    private void Start()
    {
        allLevelObstacles.Add(diff1Obstacles);
        allLevelObstacles.Add(diff2Obstacles);
        allLevelObstacles.Add(diff3Obstacles);
        allLevelObstacles.Add(diff4Obstacles);

        for (int i = 0; i < difficulty; i++)
        {
            obstacles.AddRange(allLevelObstacles[i]);
        }

        float aux = -2.25f;
        for (int i = 0; i < 5; i++)
        {
            spawnPoss.Add(new Vector2(aux, spawnPoint.position.y));
            aux += 1.125f;
        }
    }

    int puCounter = 0;
    private void Update()
    {
        t += Time.deltaTime;
        l += Time.deltaTime;
        if (t >= cooldownToSpawn)
        {
            t = 0;
            SpawnObstacle();

            puCounter++;
            if (puCounter >= cdMultToSpawnPU)
            {
                puCounter = 0;
                SpawnPU();
            }
        }
        if (l >= cooldownBetweenLevels && difficulty < maxDifficulty)
        {
            l = 0;
            UpTheDifficulty();
        }
    }

    private void SpawnObstacle()
    {
        int index;
        do
        {
            index = Random.Range(0, obstacles.Count);
        } while (index == lastObstacleIndex);
        lastObstacleIndex = index;

        GameObject go = Instantiate(obstacles[index], spawnPoint.position, spawnPoint.rotation);
        SpikeBehaviour sb = go.GetComponent<SpikeBehaviour>();
        sb.downSpeed = obstacleSpeed;
        GameManager.Instance.obsStacks.Add(sb.stack);


        CheckAndTurnIntoGrenade(go);
    }

    int k = -1, lastK = -1;
    private void SpawnPU()
    {
        do
        {
            k = Random.Range(0, powerups.Length);
        } while (k == lastK);
        lastK = k;

        int posIndex = GetAvailablePosition();
        Vector3 spPos;
        if (posIndex != -1)
        {
            spPos = spawnPoss[posIndex];
            GameObject go = Instantiate(powerups[k], spPos, transform.rotation);
            Powerup pu = go.GetComponent<Powerup>();
            pu.SetSize(pu.sizes[Random.Range(0, pu.sizes.Count)]);
        }
    }

    private int GetAvailablePosition()
    {
        List<int> l = new List<int>();
        ObsStack os = GameManager.Instance.obsStacks[GameManager.Instance.obsStacks.Count - 1];
        for (int i = 0; i < os.obs.Length; i++)
        {
            if (!os.obs[i])
            {
                l.Add(i);
                Debug.Log(i);
            }
        }

        if (l.Count > 1)
        {
            return l[Random.Range(0, l.Count)];
        }
        else
        {
            return -1;
        }
    }

    private void CheckAndTurnIntoGrenade(GameObject go)
    {
        if (Random.Range(0, 100) < explosivePercentage - 1)
        {
            SpriteRenderer[] srs = go.GetComponentsInChildren<SpriteRenderer>();
            int k = Random.Range(0, srs.Length);
            srs[k].color = Color.red; //CAMBIAR SPRITE AQUI. EL CAMBIO DE COLOR ES SOLO PARA DEBUG
            srs[k].gameObject.AddComponent<Explosive>();
        }
    }

    private void UpTheDifficulty()
    {
        difficulty++;
        obstacles.AddRange(allLevelObstacles[difficulty - 1]);
    }
    private void DownTheDifficulty()
    {
        obstacles = obstacles.Except(allLevelObstacles[difficulty - 1]).ToList();
        difficulty--;
    }
}
