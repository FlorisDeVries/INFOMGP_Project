using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportFlyBy : MonoBehaviour
{
    public GoalCollision gC;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider){
        print($"Flew by {this.transform.parent.name}");
        if(collider.name == "Rocket"){
            gC.alertFlyBy(this.gameObject);
        }
    }
}
