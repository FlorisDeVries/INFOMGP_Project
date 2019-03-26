using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityObject : MonoBehaviour
{
    // Mass of object (in kg/100000)
    public float mass = 1;
    public Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position += velocity * Time.deltaTime;
        transform.eulerAngles = new Vector3(90, Mathf.Atan2(velocity.normalized.x, velocity.normalized.z) * Mathf.Rad2Deg, 0);
    }
}
