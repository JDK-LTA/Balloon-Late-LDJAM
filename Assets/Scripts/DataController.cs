using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; //Sirve para habilitar las funciones de DateTime, PlayerPrefs, TimeSpan....

public class DataController : MonoBehaviour
{
    GameManager gm;

    private void Awake()
    {
        gm = GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveGame()
    {
        DateTime dateWhenSave = DateTime.UtcNow; //Objeto con información de fecha. Hora universal. Meridiano Greenwitch
        print("fecha: " + dateWhenSave);

        //PlayerPrefs.SetFloat("",);


    }

}
