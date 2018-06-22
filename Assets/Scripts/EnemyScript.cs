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

    //Updates path in a given period to improve performance of the game that has hordes of enemies
    IEnumerator DoUpdatePath(){
        while (updatePath)
        {
            var path = new NavMeshPath();
            bool reachable = agent.CalculatePath(Goal.position, path);
            agent.SetPath(path);
            yield return new WaitForSeconds(pathUpdateDelay);
        }

    }

    /// <summary>
    /// Gets the approximate position of the unit after time.
    /// </summary>
    /// <returns>The position after time.</returns>
    /// <param name="seconds">time in seconds</param>
    public Vector3 GetPositionAfterTime(float seconds){
        return agent.velocity*seconds + transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("gate"))
        {
            other.GetComponent<GateScript>().DefensePoints--;
            Destroy(gameObject);
        }
    }
}
