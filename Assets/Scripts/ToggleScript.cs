using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToggleScript : MonoBehaviour {

    public Image tornado;
    public Image fire;
    public Image gunfire;
    public Image noDanger;
    
    public GameObject FireObject;
    public GameObject TornadoObject;
    public GameObject GunfireObject;

    // The following variables are just for the dummy test-presentation stuff.
    public GameObject[] peopleInHouseWithCell;
    public GameObject[] peopleInHouseWithSmartTV;
    public GameObject guyWithCell;
    public GameObject smartTV;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    // TO BE DELETED
    public void fireClick()
    {
        StartCoroutine(toggleFire());
    }

    IEnumerator toggleFire()
    {
        //Image enablers
        gunfire.enabled = false;
        tornado.enabled = false;
        fire.enabled = true;
        noDanger.enabled = false;
        //GameObject enablers
        FireObject.SetActive(true);
        GunfireObject.SetActive(false);
        TornadoObject.SetActive(false);

        // TO BE DELETED AFTER PRESENTATION - Dummy work.
        smartTV.GetComponent<Renderer>().material.color = Color.yellow;
        yield return new WaitForSeconds(1);
        foreach (GameObject person in peopleInHouseWithSmartTV)
        {
            GameObject radius = person.transform.GetChild(0).gameObject;
            radius.GetComponent<Renderer>().material.color = Color.red;
            person.GetComponent<PlayerScript>().shouldFlee = true;
        }
        yield return new WaitForSeconds(1);
        guyWithCell.GetComponent<Renderer>().material.color = Color.yellow;
        guyWithCell.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        yield return new WaitForSeconds(1);
        foreach (GameObject person in peopleInHouseWithCell)
        {
            GameObject radius = person.transform.GetChild(0).gameObject;
            radius.GetComponent<Renderer>().material.color = Color.red;
            person.GetComponent<PlayerScript>().shouldFlee = true;
        }
        guyWithCell.GetComponent<PlayerScript>().shouldFlee = true;

    }

    public void toggleGunfire()
    {
        gunfire.enabled = true;
        tornado.enabled = false;
        fire.enabled = false;
        noDanger.enabled = false;
        FireObject.SetActive(false);
        GunfireObject.SetActive(true);
        TornadoObject.SetActive(false);
    }

    //TO BE DELETED
    public void tornadoClick()
    {
        StartCoroutine(toggleTornado());
    }

    IEnumerator toggleTornado()
    {
        tornado.enabled = true;
        fire.enabled = false;
        gunfire.enabled = false;
        noDanger.enabled = false;
        FireObject.SetActive(false);
        GunfireObject.SetActive(false);
        TornadoObject.SetActive(true);

        // TO BE DELETED AFTER PRESENTATION - Dummy work.
        guyWithCell.GetComponent<Renderer>().material.color = Color.yellow;
        guyWithCell.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        yield return new WaitForSeconds(1);
        guyWithCell.GetComponent<PlayerScript>().shouldFlee = true;
        foreach (GameObject person in peopleInHouseWithCell){
            GameObject radius = person.transform.GetChild(0).gameObject;
            radius.GetComponent<Renderer>().material.color = Color.red;
            person.GetComponent<PlayerScript>().shouldFlee = true;
        }
        yield return new WaitForSeconds(1);
        smartTV.GetComponent<Renderer>().material.color = Color.yellow;
        yield return new WaitForSeconds(1);
        foreach (GameObject person in peopleInHouseWithSmartTV)
        {
            GameObject radius = person.transform.GetChild(0).gameObject;
            radius.GetComponent<Renderer>().material.color = Color.red;
            person.GetComponent<PlayerScript>().shouldFlee = true;
        }

    }

    public void resetScene()
    {
        SceneManager.LoadScene(0);
    }

}
