using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
    public int waves;
    public float waitBetweenWaves;
    public float waitBetweenUnits;
    public int increaseFactor;
    public int startingNumber;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Alpha4))
            StartCoroutine(Spawn());
	}

    public IEnumerator Spawn(){
        int nUnits = startingNumber;
        for (int i = 0; i < waves; i++){
            for (int j = 0; j < nUnits; j++)
            {
                var unit = Instantiate(enemy,transform.position, Quaternion.identity);
                yield return new WaitForSeconds(waitBetweenUnits);
            }
            yield return new WaitForSeconds(waitBetweenWaves);
            nUnits += increaseFactor;
        }
    }
}
