using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Rocket : MonoBehaviour
{
    // Start is called before the first frame update
    bool selected = false, launched = false;
    public Vector3 direction = Vector3.zero;
    public float force, maxforce = 3;
    public GravitySystem gS;

    public float damping = 1000;

    public GameObject launchPad;

    public Image liftoffMeter;
    public TextMeshProUGUI velocityText, angleText;
    public GameObject flames;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(launched)
            return;

        if(!selected && Input.GetMouseButtonDown(0)){
            // Trace mouse
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit)) {
                Transform objectHit = hit.transform;
                if(objectHit.gameObject == gameObject){
                    // Hit the rocket, enable selection mode
                    selected = true;
                }
            }
        }

        if(selected && Input.GetMouseButtonUp(0)){
            // Launch the rocket
            gS.unPauseGame();
            flames.SetActive(true);
            launched = true;
            return;
        }

        if(selected){
            // this creates a horizontal plane passing through this object's center
            Plane plane = new Plane(Vector3.up, transform.position);
            // create a ray from the mousePosition
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // plane.Raycast returns the distance from the ray start to the hit point
            float distance;
            if (plane.Raycast(ray, out distance)){
                // some point of the plane was hit - get its coordinates
                Vector3 hitPoint = ray.GetPoint(distance);
                // use the hitPoint to aim your cannon

                direction = (hitPoint - launchPad.transform.position);
                force = direction.magnitude;
                direction = direction * (1/force);
                force /= damping;
                force = Mathf.Min(force, maxforce);

                gameObject.transform.position = launchPad.transform.position + ((launchPad.transform.localScale.x)/2 + 0.5f) * direction;
                gameObject.GetComponent<GravityObject>().velocity = direction * force;

                float forcePercent = force / maxforce;
                liftoffMeter.fillAmount = forcePercent;

                velocityText.text = $"{force * 1000:F1} km/h";
                angleText.text = $"{(Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + 360)%360:F1} °";
            }
        }
    }

    IEnumerator changeFontSize(){

        yield return new WaitForSeconds(.1f);
    }


}
