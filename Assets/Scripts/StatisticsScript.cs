using UnityEngine;
using System.Collections;

public class StatisticsScript : MonoBehaviour
{
    //tower
    public int projectileHit = 0;
    public int projectileMiss = 0;
    public float hitRatio;

    // Use this for initialization

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((projectileHit + projectileMiss) > 0)
            hitRatio = (float)projectileHit / (float)(projectileHit + projectileMiss);
    }
}
