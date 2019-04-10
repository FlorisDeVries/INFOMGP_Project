﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BodyNames{ Sun, Mercury, Venus, Earth, Mars, Jupiter, Saturn, Uranus, Neptune, Pluto };

public class GravitySystem : MonoBehaviour
{
    
    public List<GravityObject> gravityObjects;
    public float gravitationalConstant = 6.67408e-11F;

    public float timeScale = 1;

    public bool paused = true;

    float startTime = 0;
    bool printedHalfyearlyTime = false;

    public TextMeshProUGUI scaleValueText;

    public GameObject winPanel, losePanel;
    public TextMeshProUGUI loseExplanation;

    void Awake(){
        pauseGame();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Add all gravity objects to the list
        gravityObjects = new List<GravityObject>(GameObject.FindObjectsOfType<GravityObject>());
        print($"Found {gravityObjects.Count} gravity objects.");

        pauseGame();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < gravityObjects.Count - 1; i++){
            GravityObject gO_1 = gravityObjects[i];

            if(gO_1.name == "Earth"){
                if(Mathf.Abs(gO_1.transform.position.x) < 0.05){
                    if(!printedHalfyearlyTime){
                        float currTime = Time.time;
                        startTime = currTime;
                        printedHalfyearlyTime = true;
                    }
                } else if (printedHalfyearlyTime){
                    printedHalfyearlyTime = false;
                }
            }

            for(int j = 1 + i; j < gravityObjects.Count; j++){
                GravityObject gO_2 = gravityObjects[j];
                Vector3 r = (gO_2.gameObject.transform.position - gO_1.gameObject.transform.position) * 10e10F;
                //print($"Distance ({gO_1.name} -> {gO_2.name}): {r.magnitude:E3} meters");
                float dist = r.sqrMagnitude;
                float force = gravitationalConstant * gO_1.mass * gO_2.mass / dist;
                //print($"Gravity magnitude ({gO_1.name} -> {gO_2.name}): {force}");

                float acc1 = force / gO_1.mass;
                float scaledAcc1 = acc1 / 10e-5F;
                gO_1.velocity += scaledAcc1 * r.normalized * Time.deltaTime;

                float acc2 = force / gO_2.mass;
                float scaledAcc2 = acc2 / 10e-5F;
                gO_2.velocity += scaledAcc2 * -r.normalized * Time.deltaTime;
            }
        }
    }

    public void unPauseGame(){
        paused = false;
        Time.timeScale = timeScale;
        startTime = Time.time;
    }

    public void pauseGame(){
        paused = true;
        Time.timeScale = 0;
    }

    public void SetTimeScale(float timeScale){
        this.timeScale = timeScale;
        string s = $"{timeScale * 10.0e9F:E4}";
        scaleValueText.text = $"{s.Split("E+".ToCharArray())[0]}x10<sup>{s.Split('+')[1].TrimStart('0')}</sup>";
        if(!paused)
            Time.timeScale = timeScale;
    }

    public GameObject GetPlanetByInt(int i){
        return GetPlanetByName(((BodyNames)i).ToString());
    }

    public GameObject GetPlanetByName(string s){
        return gravityObjects.Find(a => a.name == s).gameObject;
    }

    public void ResetScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToNextLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

    public void WinLevel(){
        pauseGame();
        winPanel.SetActive(true);
    }

    public void LoseLevel(string reason)
    {
        pauseGame();
        loseExplanation.text = reason;
        losePanel.SetActive(true);
    }
}
