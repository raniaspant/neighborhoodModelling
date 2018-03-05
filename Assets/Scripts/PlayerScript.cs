using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour {

    NavMeshAgent agent;
    public GameObject shelter;
    public Transform goal;
    public bool shouldFlee;

    // Use this for initialization
    void Start () {
        shouldFlee = false;
        agent = GetComponent<NavMeshAgent>();
        shelter = GameObject.FindGameObjectWithTag("goal");
        goal = shelter.transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (shouldFlee)
        {
            agent.SetDestination(goal.position);
        }

    }
}
