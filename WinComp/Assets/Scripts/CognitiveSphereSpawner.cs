using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CognitiveSphereSpawner : MonoBehaviour
{
    public static CognitiveSphereSpawner instance = null;

    //public GameObject ObInstance_B;
    public GameObject ObInstance_P;
    //public GameObject toSpawn_B;
    public GameObject toSpawn_P;
    public Vector3 cognitScale = new Vector3(0.110f, 0.110f, 0.110f);
    public Transform parent;
    private float radius;
    public  bool stopSpawning_B;
    public  bool stopSpawning_P;
    public bool timesUp = false;

    // Primeiro range Esq
    public float minRange_L1;
    public float maxRange_L1;
    // Segundo range Esq
    public float minRange_L2;
    public float maxRange_L2;
    // Primeiro range Dir
    public float minRange_R1;
    public float maxRange_R1;
    // Segundo range Dir
    public float minRange_R2;
    public float maxRange_R2;

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
        //spawnStart_B = 1f;
        //spawnRate_B = 15f;
        spawnStart_P = 1f;
        spawnRate_P = 10f;

        stopSpawning_B = false;
        stopSpawning_P = false;
        //InvokeRepeating("SpawnObject_B", spawnStart_B, spawnRate_B);
        InvokeRepeating("SpawnObject_P", spawnStart_P, spawnRate_P);        
    }

    void Update()
    {
        radius = Instantiate_target.instance.radius;
        // Ranges lado esquerdo
        minRange_L1 = Instantiate_target.instance.minRange_L1;
        maxRange_L1 = Instantiate_target.instance.maxRange_L1;
        minRange_L2 = Instantiate_target.instance.minRange_L2;
        maxRange_L2 = Instantiate_target.instance.maxRange_L2;
        // Ranges lado direito
        minRange_R1 = Instantiate_target.instance.minRange_R1;
        maxRange_R1 = Instantiate_target.instance.maxRange_R1;
        minRange_R2 = Instantiate_target.instance.minRange_R2;
        maxRange_R2 = Instantiate_target.instance.maxRange_R2;


        if (Instantiate_target.instance.cooldownTimer == 0)
        {
            timesUp = true;
        }
    }

    /*Vector3 RandomCircle(Vector3 center)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }*/


    /*public void SpawnObject_B()
    {
        Debug.Log(spawnRate_B);
        if (stopSpawning_B == false)
        {
            Vector3 center = transform.position;
            Vector3 pos = RandomCircle(center);
            ObInstance_B = Instantiate(toSpawn_B, pos, Quaternion.identity, parent);
            Instantiate_target.instance.CooldownTimer(4);
        }
        else
        {
            CancelInvoke("SpawnObject_B");
        }
    }*/
    public Vector3 OrbitPosition(Vector3 centerPoint, float radius, float angle)
    {
        Vector3 tmp;
        tmp.x = Mathf.Cos(angle * (Mathf.PI / 180)) * radius + centerPoint.x;
        tmp.y = Mathf.Sin(angle * (Mathf.PI / 180)) * radius + centerPoint.y;
        tmp.z = centerPoint.z;
        return tmp;
    }

    public GameObject InstantiateRandom(GameObject toInstantiate, Vector3 centerPoint)
    {
        // Calcular a posição para instanciação
        if (State.leftArmSelected)
        {
            Vector3 newPosition_1 = OrbitPosition(centerPoint, radius, Random.Range(minRange_L1, maxRange_L1));
            Vector3 newPosition_2 = OrbitPosition(centerPoint, radius, Random.Range(minRange_L2, maxRange_L2));

            // Instanciar uma nova instância do objeto
            ObInstance_P = Instantiate(toInstantiate, parent) as GameObject;
            ObInstance_P.transform.localScale = cognitScale;
            int toggle = Random.Range(0, 2);

            if (ObInstance_P && toggle == 0)
            {
                ObInstance_P.transform.position = newPosition_1;
            }
            else if (ObInstance_P && toggle == 1)
            {
                ObInstance_P.transform.position = newPosition_2;
            }
            return ObInstance_P;

        }
        else
        {
            Vector3 newPosition_1 = OrbitPosition(centerPoint, radius, Random.Range(minRange_R1, maxRange_R1));
            Vector3 newPosition_2 = OrbitPosition(centerPoint, radius, Random.Range(minRange_R2, maxRange_R2));
            // Instanciar uma nova instância do objeto
            ObInstance_P = Instantiate(toInstantiate, parent) as GameObject;
            ObInstance_P.transform.localScale = cognitScale;
            int toggle_p = Random.Range(0, 2);

            if (ObInstance_P && toggle_p == 0)
            {
                ObInstance_P.transform.position = newPosition_1;
                Debug.Log(toggle_p);
            }
            else if (ObInstance_P && toggle_p == 1)
            {
                ObInstance_P.transform.position = newPosition_2;
                Debug.Log(toggle_p);
            }
            return ObInstance_P;
        }
    }
    public void SpawnObject_P()
    {
        Debug.Log(spawnRate_P);
        if (stopSpawning_P == false)
        {
            Vector3 center = Instantiate_target.instance.cogCenter;            
            ObInstance_P = InstantiateRandom(toSpawn_P, center);
            Instantiate_target.instance.CooldownTimer(4);

            if (Instantiate_target.instance.subState == 1)
            {
                ObInstance_P.transform.localScale = cognitScale;
            }
            else if (Instantiate_target.instance.subState == 2)
            {
                ObInstance_P.transform.localScale = cognitScale;
            }
            else if (Instantiate_target.instance.subState == 3)
            {
                ObInstance_P.transform.localScale = cognitScale;
            }
        }
        else
        {
            CancelInvoke("SpawnObject_P");
        }
    }
}
