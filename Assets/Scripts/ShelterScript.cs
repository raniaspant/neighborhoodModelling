using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class ShelterScript : MonoBehaviour {

    public Text numberOfPeople;
    public int peopleInsideShelter = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Person")
        {
            peopleInsideShelter += 1;
            numberOfPeople.text = peopleInsideShelter.ToString();
        }
    }
}
