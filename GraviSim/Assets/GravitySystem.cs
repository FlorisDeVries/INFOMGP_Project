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
                
                Vector3 r = gO_2.gameObject.transform.position - gO_1.gameObject.transform.position;
                float dist = r.sqrMagnitude;
                float force = gravitationalConstant * gO_1.mass * gO_2.mass / dist;

                float acc = force / gO_1.mass;

                gO_1.velocity += acc * r * Time.deltaTime;
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
