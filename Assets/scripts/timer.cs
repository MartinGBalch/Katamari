using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class timer : NetworkBehaviour
{
    public Text timerText;
    public Image gameOver;

    [SyncVar] public float roundTime;
    [SyncVar] public float roundOverTimer;
    

    int Gmin;
    int Gsec;


    // Use this for initialization
    void Start() { gameOver.gameObject.SetActive(false); }

    // Update is called once per frame
    void Update()
    {
        if (isServer && roundTime > 0)
        {
            roundTime -= Time.deltaTime;
            Gmin = (int)roundTime / 60;
            Gsec = (int)roundTime % 60;
        }
        else if (roundTime < 0)
        {
            roundOverTimer -= Time.deltaTime;
        }
       
        GameTime();

    }

    void GameTime()
    {
        if (Gsec >= 10)
        {
            timerText.text = Gmin + ":" + Gsec;
        }
        else
        {
            timerText.text = Gmin + ":0" + Gsec;
        }
    }

    public void GameOver()
    {
        if(roundOverTimer <= 0)
        {
            gameOver.gameObject.SetActive(true);
        }
    }
}
