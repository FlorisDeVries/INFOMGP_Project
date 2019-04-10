using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoalCollision : MonoBehaviour
{
    public GravitySystem gS;
    public List<GravityObject> flyByZones;
    private bool[] visitedObjects;

    // Start is called before the first frame update
    void Start()
    {
        visitedObjects = new bool[flyByZones.Count];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void alertFlyBy(GameObject gO){
        visitedObjects[flyByZones.IndexOf(gO.GetComponentInParent<GravityObject>())] = true;
        print($"Alerted of fly-by; missing {visitedObjects.ToList().Count(p => !p)}");
        gO.SetActive(false);
    }

    void OnTriggerEnter(Collider other){
        if(other.name == "Rocket"){
            if(visitedObjects.Length == 0 || visitedObjects.ToList().All(p => p)){
                print("Reached goal");
                gS.WinLevel();
            } else {
                print("Missed some fly-bies");
                gS.LoseLevel($"Missed {visitedObjects.ToList().Count(p => !p)} fly-by{(visitedObjects.ToList().Count(p => !p)==1?"":"s")}!");
            }
        }
    }
}
