using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class ObsStack
{
    public bool[] obs = new bool[5];
}
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

    public TextMeshProUGUI crntScore = null;
    public GameObject pauseMenu = null;

    public List<ObsStack> obsStacks = new List<ObsStack>();

    public BalloonBehaviour balloon;

    public GameObject highScoreMenu;


    public void Start()
    {
        if (pauseMenu)
        {
            pauseMenu.SetActive(false);
        }
        if (highScoreMenu)
        {
            highScoreMenu.SetActive(false);
        }
        /*else
        {
            //Debug.LogError("Missing 'pauseMenu' variable in GameManager");
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        score += scoreXSec * Time.deltaTime;

        if (crntScore)
        {
            crntScore.text = score.ToString();
        }
        else
        {
            //Debug.LogError("Missing 'crntScore' variable in GameManager");
        }
    }


    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);

    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);

        //GO TO MENU
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


    public void ActiveHighScore()
    {
        Time.timeScale = 0;
        highScoreMenu.SetActive(true);
        GetComponent<HighScored>().SetHighScored(score);
    }

}
