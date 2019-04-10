using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScripts : MonoBehaviour
{
    public GameObject following;
    public GravitySystem gs;

    public static float zoom = 30;
    public static float maxZoom = 5;
    public static float zoomLevel = 30;

    float startChangeDist = 1000;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 oldPos = this.transform.position;
        oldPos.x = following.transform.position.x;
        oldPos.z = following.transform.position.z;

        if((startChangeDist / 2.0f) < new Vector2(following.transform.position.x - transform.position.x,following.transform.position.z - transform.position.z).sqrMagnitude){
            oldPos.y = startChangeDist;
        } else {
            oldPos.y = Mathf.Max(zoom, maxZoom);
        }
        this.transform.position = Vector3.Lerp(this.transform.position, oldPos, Mathf.Max(0.3f * Time.timeScale * Time.deltaTime, Mathf.Max(.03f ,1 / zoom)));
        zoomLevel = transform.position.y;
    }

    // Changes the zoom for the camera
    // For reference, at level 5 only the sun is visible, at level 1100 Pluto is visible.
    public void ChangeZoom(float zoom){
        CameraScripts.zoom = zoom;
    }

    public void ChangeFollow(int i){
        following = gs.GetPlanetByInt(i);
        maxZoom = following.GetComponent<GravityObject>().maxZoom;
        startChangeDist = new Vector2(following.transform.position.x - transform.position.x,following.transform.position.z - transform.position.z).sqrMagnitude;
    }
}
