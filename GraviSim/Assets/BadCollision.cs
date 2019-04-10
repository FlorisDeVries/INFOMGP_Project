using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadCollision : MonoBehaviour
{
    AudioSource audio;
    public Rocket rocket;

    public string loseMessage = "Crashed into an object!";
    public GravitySystem gS;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.name == "Rocket"){
            rocket.GetComponent<AudioSource>().Stop();
            audio.Play();
            gS.LoseLevel(loseMessage);
        }
    }
}
