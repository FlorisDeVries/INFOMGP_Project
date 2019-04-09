using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GravityObject : MonoBehaviour
{
    // Mass of object (in kg/100000)
    public float mass = 1;
    public Vector3 velocity;
    public bool trackLabel = true;

    // Should be refactored into the rocket class
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position += velocity * Time.deltaTime;
        // // if(text != null)
        // //     text.text = $"{(velocity.magnitude * 1000):F1} km/h";
        // transform.eulerAngles = new Vector3(90, Mathf.Atan2(velocity.normalized.x, velocity.normalized.z) * Mathf.Rad2Deg, 0);

        if(trackLabel){
            // Scale the coordinates to screen
            Vector3 textPosition = Camera.main.WorldToScreenPoint(this.transform.position);
            // Fix for far zoom
            textPosition.z = 0;
            text.gameObject.transform.position = textPosition;
        }
    }
}
