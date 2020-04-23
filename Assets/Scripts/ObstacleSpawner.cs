using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    int lastObstacleIndex = -1;

    public float cooldownToSpawn = 2f;
    float t = 0;
    public float cooldownBetweenLevels = 14f;
    float l = 0;

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
    }
    private void Update()
    {
        t += Time.deltaTime;
        l += Time.deltaTime;
        if (t >= cooldownToSpawn)
        {
            t = 0;
            SpawnObstacle();
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
        //if (Random.Range(0, 2) == 1)
        //{
        //    Vector3 ls = go.transform.localScale;
        //    ls.x *= -1;
        //    go.transform.localScale = ls;
        //}
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
