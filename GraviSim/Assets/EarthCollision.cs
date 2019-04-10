using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthCollision : MonoBehaviour
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

    void OnTriggerEnter(Collider other){
        if(other.name == "Rocket"){
            gS.LoseLevel("Crashed into the earth!");
        }
    }
}
