﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SandboxController : MonoBehaviour {

    public GravitySystem gravitySystem;

    public Canvas sandboxMainUI, normalUI;

    public GameObject bodyPrefab;
    GameObject newInstance;

    public List<Button> topLayer;
    public Button cancel;

    // TODO: Change this to an array?
    bool addingBody = false;
    bool editingBody = false;

    Vector3 mousePosOnPlane;
    Vector3 dragOrigin;

    // Editing section
    GravityObject objectToEdit;
    public Image dropBackEdit;

    public TMP_InputField posX, posZ;
    public TMP_InputField velX, velZ;
    public TMP_InputField mass;

    // Start is called before the first frame update
    void Start () {
        StartEditMode ();
    }

    // Update is called once per frame
    void Update () {
        UpdateMousePosOnPlane ();

        if (!addingBody && Input.GetMouseButtonDown (0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            if (Physics.Raycast (ray, out hit)) {
                if (hit.transform.gameObject.tag == "Body") {
                    // Edit a body
                    StartEditBody (hit.transform.gameObject.GetComponent<GravityObject>());
                }
            }
        }

        if (editingBody){
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject ()) {
            return;
        }

        if (addingBody) {
            if (newInstance == null) {
                newInstance = Instantiate (bodyPrefab);
            }
            newInstance.transform.position = mousePosOnPlane;
        }

        // Clicks
        if (Input.GetMouseButtonDown (0)) {
            if (addingBody) {
                AddBody ();
            }
            dragOrigin = mousePosOnPlane;
        }

        if (Input.GetMouseButton (0)) {
            if (dragOrigin.magnitude > 0) {
                Vector3 movePos = dragOrigin - mousePosOnPlane;
                Camera.main.transform.position += movePos;
            }
        }

        if (Input.GetMouseButtonUp (0)) {
            dragOrigin = Vector3.zero;
        }
    }

    void UpdateMousePosOnPlane () {
        // this creates a horizontal plane passing through this object's center
        Plane plane = new Plane (Vector3.up, transform.position);
        // create a ray from the mousePosition
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        // plane.Raycast returns the distance from the ray start to the hit point
        float distance;
        if (plane.Raycast (ray, out distance)) {
            // some point of the plane was hit - get its coordinates
            mousePosOnPlane = ray.GetPoint (distance);
        }

    }

    public void StartSimulation () {
        sandboxMainUI.gameObject.SetActive (false);
        normalUI.gameObject.SetActive (true);
        gravitySystem.PopulateList ();
    }

    public void StartEditMode () {
        sandboxMainUI.gameObject.SetActive (true);
        normalUI.gameObject.SetActive (false);
        Cancel ();
        Time.timeScale = 0;
    }

    public void StartAddBody () {
        DisableTopLayer ();
        addingBody = true;
    }

    public void AddBody () {
        EnableTopLayer ();
        addingBody = false;
        newInstance = null;
        gravitySystem.PopulateList ();
    }

    public void StartEditBody (GravityObject toEdit) {
        DisableTopLayer ();
        editingBody = true;
        dropBackEdit.gameObject.SetActive(true);
        objectToEdit = toEdit;

        posX.text = objectToEdit.transform.position.x.ToString();
        posZ.text = objectToEdit.transform.position.z.ToString();

        velX.text = objectToEdit.velocity.x.ToString();
        velZ.text = objectToEdit.velocity.z.ToString();

        mass.text = objectToEdit.mass.ToString();
    }

    public void DoneEditingBody () {
        EnableTopLayer ();
        editingBody = false;
        dropBackEdit.gameObject.SetActive(false);
        objectToEdit = null;
        gravitySystem.PopulateList ();
    }

    public void Cancel () {
        EnableTopLayer ();
        if (newInstance != null)
            Destroy (newInstance);
        addingBody = false;
        editingBody = false;
        dropBackEdit.gameObject.SetActive(false);
        objectToEdit = null;
    }

    void DisableTopLayer () {
        foreach (Button b in topLayer) {
            b.interactable = false;
        }
        cancel.gameObject.SetActive (true);
    }

    void EnableTopLayer () {
        foreach (Button b in topLayer) {
            b.interactable = true;
        }
        cancel.gameObject.SetActive (false);
    }

    public void ApplyX(string value){
        Vector3 old = objectToEdit.transform.position;
        float.TryParse(value, out old.x);
        objectToEdit.transform.position = old;
    }

    public void ApplyZ(string value){
        Vector3 old = objectToEdit.transform.position;
        float.TryParse(value, out old.z);
        objectToEdit.transform.position = old;
    }
    
    public void ApplyVX(string value){
        Vector3 old = objectToEdit.velocity;
        float.TryParse(value, out old.x);
        objectToEdit.velocity = old;
    }

    public void ApplyVZ(string value){
        Vector3 old = objectToEdit.velocity;
        float.TryParse(value, out old.z);
        objectToEdit.velocity = old;
    }

    public void ApplyMass(string value){
        float.TryParse(value, out objectToEdit.mass);
    }

    public void DestroyEditObject(){
        Destroy(objectToEdit.gameObject);
        DoneEditingBody();
    }
}