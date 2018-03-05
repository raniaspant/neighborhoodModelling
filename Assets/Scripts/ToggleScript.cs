using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleScript : MonoBehaviour {

    public Image tornado;
    public Image fire;
    public Image gunfire;
    public Image noDanger;

    public GameObject FireObject;
    public GameObject TornadoObject;
    public GameObject GunfireObject;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void toggleFire()
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

    public void toggleTornado()
    {
        tornado.enabled = true;
        fire.enabled = false;
        gunfire.enabled = false;
        noDanger.enabled = false;
        FireObject.SetActive(false);
        GunfireObject.SetActive(false);
        TornadoObject.SetActive(true);
    }
}
