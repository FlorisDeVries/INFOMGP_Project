using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySystem : MonoBehaviour
{
    
    public List<GravityObject> gravityObjects;
    public float gravitationalConstant = 6.67408e-11F;

    public float timeScale = 1;

    public bool paused = true;

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
        // For each gravity object, compute the force
        foreach(GravityObject gO_1 in gravityObjects){
            foreach(GravityObject gO_2 in gravityObjects){
                if(gO_1 == gO_2)
                    continue;
                Vector3 r = (gO_2.gameObject.transform.position - gO_1.gameObject.transform.position) * 10e9F;
                //print($"Distance ({gO_1.name} -> {gO_2.name}): {r.magnitude:E3} meters");
                float dist = r.sqrMagnitude;
                float force = gravitationalConstant * gO_1.mass * gO_2.mass / dist;
                //print($"Gravity magnitude ({gO_1.name} -> {gO_2.name}): {force}");

                float acc = force / gO_1.mass;

                float scaledAcc = acc / 10e1F;

                gO_1.velocity += scaledAcc * r.normalized * Time.deltaTime;
            }
        }
    }

    public void unPauseGame(){
        Time.timeScale = timeScale;
    }

    public void pauseGame(){
        Time.timeScale = 0;
    }
}
