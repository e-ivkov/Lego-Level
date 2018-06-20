using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour {

    public float Health;
    public float Speed;
    public int Reward;
    public Transform Goal;
    private NavMeshAgent agent;
    public float pathUpdateDelay;
    private bool updatePath = true;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        //agent.updatePosition = false;
        var gates = GameObject.FindGameObjectsWithTag("gate");
        Goal = gates[Random.Range(0, gates.Length)].transform;
        StartCoroutine(DoUpdatePath());
	}

    IEnumerator DoUpdatePath(){
        while (updatePath)
        {
            var path = new NavMeshPath();
            bool reachable = agent.CalculatePath(Goal.position, path);
            agent.SetPath(path);
            yield return new WaitForSeconds(pathUpdateDelay);
        }

    }

    /*private void Update()
    {
        transform.position = agent.nextPosition;
    }*/

    public Vector3 GetPositionAfterTime(float seconds){
        return agent.velocity*seconds + transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("gate"))
        {
            Debug.Log(other.name);
            other.GetComponent<GateScript>().DefensePoints--;
            Destroy(gameObject);
        }
    }
}
