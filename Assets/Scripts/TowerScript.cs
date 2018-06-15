using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class TowerScript : MonoBehaviour
{
    public float visionR;
    public GameObject projectile;
    public float projectileSpeed;
    public float cooldown;

    GameObject target;
    bool hasTarget = false;
    bool firing = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hasTarget && target != null)
        {
            if (!firing)
            {
                StartCoroutine(Fire());
            }
            if (Vector3.Distance(transform.position, target.transform.position) > visionR && !CheckVisible(target))
                hasTarget = false;
        }
        else
        {
            target = FindTarget(out hasTarget);
        }

    }

    IEnumerator Fire()
    {
        //some difficult math for identifying velocities
        firing = true;
        var a = transform.position - target.transform.position;
        var b = target.GetComponent<NavMeshAgent>().velocity;

        var alpha = Vector3.Angle(a, b);
        var beta = Mathf.Asin(b.magnitude / (projectileSpeed * Mathf.Sin(alpha)));
        var c = beta > 0 ? Quaternion.Euler(0, beta, 0) * (a.normalized * -1) : (a.normalized * -1);
        var firedProj = Instantiate(projectile, transform.position, Quaternion.identity);
        firedProj.GetComponent<ProjectileScript>().maxDistance = visionR;
        firedProj.GetComponent<Rigidbody>().velocity = c.normalized * projectileSpeed;
        yield return new WaitForSeconds(cooldown);
        firing = false;
    }

    GameObject FindTarget(out bool found)
    {
        foreach (var enemy in GameObject.FindGameObjectsWithTag("enemy"))
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < visionR && CheckVisible(enemy))
            {
                found = true;
                return enemy;
            }
        }
        found = false;
        return null;
    }

    bool CheckVisible(GameObject checkObject)
    {
        RaycastHit hit;
        Physics.Raycast(new Ray(transform.position, checkObject.transform.position - transform.position), out hit);
        return (hit.collider.gameObject == checkObject);
    }
}
