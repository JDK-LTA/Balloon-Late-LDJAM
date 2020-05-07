using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; //Sirve para habilitar las funciones de DateTime, PlayerPrefs, TimeSpan....
using UnityEngine.UI;
using UnityEngine.SceneManagement; //Sirve para habilitar el manejo de escenas, para reiniciar.

public class HighScored : MonoBehaviour
{

    float highScored = 0;

    public Text highScoreText;
    public Text currentScoreText;

    Scene currentLevel; //guardo variable por si en un futuro hacemos niveles diferentes (cambiar backgrounds)

    bool isDead = false;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
        LoadGame();
        highScoreText.text = "" + Mathf.RoundToInt(highScored);
        GameManager.Instance.DesactivarHighScore();
        currentLevel = SceneManager.GetActiveScene();
    }

    public void SetHighScored(float currentScored) //Llamar al método cuando el globo muera
    {
        if (highScored <= currentScored)
        {
            highScored = currentScored;

            SaveGame();

            highScoreText.text = "" + Mathf.RoundToInt(highScored); 
        }

        currentScoreText.text = "" + Mathf.RoundToInt(currentScored); 
    }

    public void SaveGame()
    {
        PlayerPrefs.SetFloat("HighScored", highScored); //Guardo valor

        /*DateTime dateWhenSave = DateTime.UtcNow; //Objeto con información de fecha. Hora universal. Meridiano Greenwitch
        print("fecha: " + dateWhenSave); //Por si queremos poner el dia y hora en que consiguió la mejor puntuacion*/

    }

    public void LoadGame()
    {
        highScored = PlayerPrefs.GetFloat("HighScored");
    }

    public void ReiniciarJuego()
    {
        GameManager.Instance.DesactivarHighScore();
        SceneManager.LoadScene("SampleScene");
    }
}
