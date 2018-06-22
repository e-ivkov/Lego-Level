using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerLinkerScript : MonoBehaviour
{
    public float visionRFactor;
    public GameObject wallBlock;

    [HideInInspector]
    public float scaleFactor;

    public bool active;
    List<GameObject> wallLink = new List<GameObject>();

    /// <summary>
    /// Connect this tower to the towers in the vicinity with walls
    /// </summary>
    public void LinkTowers()
    {
        if (!active)
            return;
        foreach (var tower in Physics.OverlapSphere(transform.position, visionRFactor * scaleFactor))
        {
            if (tower.CompareTag("tower") || tower.CompareTag("PowerConduit"))
            {
                int nWalls = (int)(Vector3.Distance(transform.position, tower.transform.position) / scaleFactor);
                for (int i = 0; i < nWalls; i++)
                {
                    var block = Instantiate(wallBlock, transform.position + (tower.transform.position - transform.position).normalized * i * scaleFactor, Quaternion.identity);
                    block.transform.localScale = Vector3.one * scaleFactor;
                    wallLink.Add(block);
                }
            }
        }
    }


    private void OnDestroy()
    {
        foreach (var block in wallLink)
            Destroy(block);
    }
}