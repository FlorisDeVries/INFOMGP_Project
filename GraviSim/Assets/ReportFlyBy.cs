using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportFlyBy : MonoBehaviour
{
    public GoalCollision gC;
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponentInParent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider){
        print($"Flew by {this.transform.parent.name}");
        if(collider.name == "Rocket"){
            audio.Play();
            gC.alertFlyBy(this.gameObject);
        }
    }
}
