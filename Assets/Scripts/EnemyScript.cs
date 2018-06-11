using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour {

    public float Health;
    public float Speed;
    public Transform Goal;
    private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        agent.destination = Goal.position;
	}

    public Vector3 GetPositionAfterTime(float seconds){
        return agent.velocity*seconds + transform.position;
    }
}
