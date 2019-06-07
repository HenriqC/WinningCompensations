using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CognitiveSphereSpawner : MonoBehaviour
{
    public static CognitiveSphereSpawner instance = null;

    public GameObject toSpawn_B;
    public GameObject toSpawn_P;
    public Transform parent;
    private float radius;
    public bool stopSpawning = false;
    public bool timesUp = false;
    public float spawnStart_B;
    public float spawnRate_B;
    public float spawnStart_P;
    public float spawnRate_P;

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
        spawnStart_B = 1f;
        spawnRate_B = 15f;
        spawnStart_P = 1f;
        spawnRate_P = 10f;

        InvokeRepeating("SpawnObject_B", spawnStart_B, spawnRate_B);
        InvokeRepeating("SpawnObject_P", spawnStart_P, spawnRate_P);
    }

    void Update()
    {
        radius = Instantiate_target.instance.radius;


        if (Instantiate_target.instance.cooldownTimer == 0)
        {
            timesUp = true;
        }
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

    public void SpawnObject_B()
    {
        Debug.Log(spawnRate_B);
        
        Vector3 center = transform.position;
        Vector3 pos = RandomCircle(center);
        Instantiate(toSpawn_B, pos, Quaternion.identity, parent);
        Instantiate_target.instance.CooldownTimer(4);

        if (stopSpawning)
        {
            CancelInvoke("SpawnObject_B");
        }
    }
    public void SpawnObject_P()
    {
        Debug.Log(spawnRate_P);

        Vector3 center = transform.position;
        Vector3 pos = RandomCircle(center);
        Instantiate(toSpawn_P, pos, Quaternion.identity, parent);
        Instantiate_target.instance.CooldownTimer(4);

        if (stopSpawning)
        {
            CancelInvoke("SpawnObject_P");
        }
    }
}
