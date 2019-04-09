﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour
{
    private bool paused = true;
    public GravitySystem gs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseToggle(Button button){
        if(paused){
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Pause";
            gs.unPauseGame();
            paused = false;
        } else {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Unpause";
            gs.pauseGame();
            paused = true;
        }
    }
}
