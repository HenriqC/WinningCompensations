using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CognitiveSphereSpawner : MonoBehaviour
{
    public static CognitiveSphereSpawner instance = null;

    public GameObject toSpawn;
    public Transform parent;
    private float radius;
    public bool stopSpawning = false;
    public float spawnStart;
    public float spawnRate;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        spawnStart = 1f;
        spawnRate = 15f;
        InvokeRepeating("SpawnObject", spawnStart, spawnRate);
    }

    void Update()
    {
        radius = Instantiate_target.instance.radius;
    }

    Vector3 RandomCircle(Vector3 center)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

    public void SpawnObject()
    {
        Debug.Log(spawnRate);
        
        Vector3 center = transform.position;
        Vector3 pos = RandomCircle(center);
        Instantiate(toSpawn, pos, Quaternion.identity, parent);

        if (stopSpawning)
        {
            CancelInvoke("SpawnObject");
        }
    }
}
