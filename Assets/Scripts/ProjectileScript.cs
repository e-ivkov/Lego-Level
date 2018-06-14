using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour
{
    public float damage;

    [HideInInspector]
    public GameObject tower;

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
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, tower.transform.position) > tower.GetComponent<TowerScript>().visionR)
            Destroy(gameObject);
    }
}