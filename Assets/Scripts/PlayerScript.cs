using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour {

    NavMeshAgent agent;
    public GameObject shelter;

    public bool shouldFlee;

    // Use this for initialization
    void Start () {

        shouldFlee = false;
        agent = GetComponent<NavMeshAgent>();
        shelter = GameObject.FindGameObjectWithTag("Shelter");
    }
	
	// Update is called once per frame
	void Update () {
        if (shouldFlee)
        {
            
            agent.SetDestination(shelter.transform.position);
            Debug.Log(agent.destination + " AND " + shelter.transform.position);
        }

    }
}
