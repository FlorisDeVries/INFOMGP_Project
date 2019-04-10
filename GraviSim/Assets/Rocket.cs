using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    float startingDistance;
    GravityObject gO;
    public TextMeshProUGUI text;

    public AudioClip launch, flight, crash;
    AudioSource audio;

    bool waitingForLiftoff;
    Vector3 launchDirection;
    float launchStrength;
    
    void Start()
    {
        startingDistance = (this.transform.position - launchPad.transform.position).magnitude + this.transform.localScale.x / 2;
        gO = gameObject.GetComponent<GravityObject>();
        gO.velocity = this.transform.position;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(waitingForLiftoff)
            return;

        Vector3 velocity = gO.velocity;
        transform.eulerAngles = new Vector3(90, Mathf.Atan2(velocity.normalized.x, velocity.normalized.z) * Mathf.Rad2Deg, 0);
        if(launched){
            if(text != null)
                text.text = $"{(velocity.magnitude * 1000):F1} km/h";
            return;
        }

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
            launchDirection = direction;
            launchStrength = force;
            // Launch the rocket
            IEnumerator launchRoutine = launchRocket();
            StartCoroutine(launchRoutine);
            gS.unPauseGame();
            launched = true;
            audio.PlayOneShot(launch);
            waitingForLiftoff = true;
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

                gameObject.transform.position = launchPad.transform.position + (startingDistance) * direction;
                gO.velocity = direction * force;

                float forcePercent = force / maxforce;
                liftoffMeter.fillAmount = forcePercent;

                velocityText.text = $"{force * 10000:F1} km/h";
                angleText.text = $"{(Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + 360)%360:F1} °";
            }
        }
    }

    IEnumerator launchRocket(){
        yield return new WaitForSecondsRealtime(3.8f);
        waitingForLiftoff = false;
        gO.velocity = launchDirection * launchStrength;
        gO.fixated = false;
        flames.SetActive(true);
        audio.Play();
        yield return null;
    }

    IEnumerator changeFontSize(){

        yield return new WaitForSeconds(.1f);
    }
}
