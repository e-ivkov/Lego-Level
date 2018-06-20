using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour
{
    public float damage;

    [HideInInspector]
    public float maxDistance;

    Vector3 startingPosition;

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            var enemy = other.gameObject.GetComponent<EnemyScript>();
            enemy.Health -= damage;
            if (enemy.Health <= 0)
            {
                Destroy(enemy.gameObject);
            }
            GameObject.Find("Statistics").GetComponent<StatisticsScript>().projectileHit++;
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, startingPosition) > maxDistance)
        {
            GameObject.Find("Statistics").GetComponent<StatisticsScript>().projectileMiss++;
            Destroy(gameObject);
        }
    }
}