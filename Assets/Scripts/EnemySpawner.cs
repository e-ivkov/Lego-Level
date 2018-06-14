using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
    public int waves;
    public float waitBetweenWaves;
    public float waitBetweenUnits;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey(KeyCode.Alpha4))
            StartCoroutine(Spawn());
	}

    IEnumerator Spawn(){
        int nUnits = 10;
        for (int i = 0; i < waves; i++){
            for (int j = 0; j < nUnits; j++)
            {
                var gates = GameObject.FindGameObjectsWithTag("gate");
                var unit = Instantiate(enemy,transform.position, Quaternion.identity);
                unit.GetComponent<EnemyScript>().Goal = gates[Random.Range(0, gates.Length)].transform;
                yield return new WaitForSeconds(waitBetweenUnits);
            }
            yield return new WaitForSeconds(waitBetweenWaves);
        }
    }
}
