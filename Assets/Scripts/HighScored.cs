using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; //Sirve para habilitar las funciones de DateTime, PlayerPrefs, TimeSpan....
using UnityEngine.UI;


public class HighScored : MonoBehaviour
{

    float highScored = 0;

    public Text highScoreText;
    public Text currentScoreText;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {          
        LoadGame();
        highScoreText.text = ""+ highScored;
    }

    // Update is called once per frame
    void Update()
    {
        //Activar panel con puntuación Max y botón de Restart
        //Configurarlo, Canvas 
        if (Input.GetKeyDown(KeyCode.Numlock))
        {

            GameManager.Instance.ActiveHighScore();

            Debug.Log("Dead");

        }
    }


    public void SetHighScored(float currentScored) //Llamar al método cuando el globo muera
    {
        if (highScored <= currentScored)
        {
            highScored = currentScored;

            SaveGame();

            highScoreText.text = "" + highScored;
        }

        currentScoreText.text = "" + currentScored;
    }

    public void SaveGame()
    {
        PlayerPrefs.SetFloat("HighScored", highScored); //Guardo valor
        
        /*DateTime dateWhenSave = DateTime.UtcNow; //Objeto con información de fecha. Hora universal. Meridiano Greenwitch
        print("fecha: " + dateWhenSave); //Por si queremos poner el dia y hora en que consiguió la mejor puntuacion*/

    }

    public void LoadGame()
    {
        highScored=PlayerPrefs.GetFloat("HighScored");
    }
    

}
