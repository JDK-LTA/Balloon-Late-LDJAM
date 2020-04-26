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
    public bool isX2 = false;
    public float scoreXSec;
    public ObstacleSpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        score += scoreXSec * Time.deltaTime;
    }

    public void RocketActive()
    {
        spawner.obstacleSpeed *= 2;
        isX2 = true;
        scoreXSec *= 2;
    }
    public void RocketDeactive()
    {
        spawner.obstacleSpeed /= 2;
        isX2 = false;
        scoreXSec /= 2;
    }
}
