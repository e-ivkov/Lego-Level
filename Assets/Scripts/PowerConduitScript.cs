using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerConduitScript : MonoBehaviour
{

    public float visionR;
    public int maxTowers; // maximum number of towers to support
    public float damageBonus;
    public GameObject powerLine;

    private HashSet<GameObject> towers = new HashSet<GameObject>();
    private List<GameObject> powerLines = new List<GameObject>();

    public void LinkTowers(){
        foreach (var tower in Physics.OverlapSphere(transform.position, visionR))
        {
            if (tower.CompareTag("tower") && !towers.Contains(tower.gameObject) && (towers.Count < maxTowers))
            {
                towers.Add(tower.gameObject);
                tower.GetComponent<TowerScript>().damageBonus += damageBonus;
                var line = Instantiate(powerLine);
                line.GetComponent<LineRenderer>().SetPositions(new Vector3[] { transform.position, tower.transform.position });
                powerLines.Add(line);
            }
        }
    }

    public void UnlinkTowers()
    {
        foreach(var tower in towers){
            if(tower.activeSelf)
                tower.GetComponent<TowerScript>().damageBonus -= damageBonus;
        }
        towers = new HashSet<GameObject>();
    }

    private void OnDestroy()
    {
        foreach (var line in powerLines)
            Destroy(line);
    }
}
