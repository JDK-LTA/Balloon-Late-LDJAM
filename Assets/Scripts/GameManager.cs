using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public float score;
    public float scoreXSec;
    public bool isRocket = false;
    public float multWithRocket = 2f;
    public ObstacleSpawner spawner;
    public BackgroundInfinite bg1, bg2;

    // Update is called once per frame
    void Update()
    {
        score += scoreXSec * Time.deltaTime;
    }

    public void RocketActive()
    {
        SpikeBehaviour[] createdObs = FindObjectsOfType<SpikeBehaviour>();
        for (int i = 0; i < createdObs.Length; i++)
        {
            createdObs[i].downSpeed *= multWithRocket;
        }
        spawner.obstacleSpeed *= multWithRocket;
        spawner.cooldownToSpawn /= multWithRocket;
        isRocket = true;
        scoreXSec *= multWithRocket;
        bg1.speed *= multWithRocket;
        bg2.speed *= multWithRocket;
    }
    public void RocketDeactive()
    {
        SpikeBehaviour[] createdObs = FindObjectsOfType<SpikeBehaviour>();
        for (int i = 0; i < createdObs.Length; i++)
        {
            createdObs[i].downSpeed /= multWithRocket;
        }
        spawner.obstacleSpeed /= multWithRocket;
        spawner.cooldownToSpawn *= multWithRocket;
        isRocket = false;
        scoreXSec /= multWithRocket;
        bg1.speed /= multWithRocket;
        bg2.speed /= multWithRocket;
    }
}
