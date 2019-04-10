using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBox : MonoBehaviour
{
    public GravitySystem gS;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit(Collider other){
        if(other.name == "Rocket"){
            print("Rocket left bounding box");
            gS.LoseLevel("Flew too far away!");
        }
    }
}
