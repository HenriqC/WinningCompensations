using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instantiate_target : MonoBehaviour
{
    public static Instantiate_target instance = null;
    public GameObject center;
    public GameObject ObInstance;
    public float radius;    
    
    public Transform parent;
    public Text exName;
    public Text avgTime;
    public State_Targets radial_Data;

    public ColorChanger changeSpeed;

    // -------- Variáveis de instanciação dos alvos cognitivos --------//
    public GameObject cognitTarget;                                    //
    public Transform cogPosition;                                      //
    public bool stopSpawning = false;                                  //
    public float spawnStart;                                           //
    public float spawnRate;                                            //
    // --------------------------------------------------------------- //

    // -------- Variáveis das barras radiais --------//
    public float completion;                         //
    public float correctness;                        //
    public int icorrectness;                         //
    public float cooldownTimer;                      //
    // ----------------------------------------------//

    public AudioSource Source;

    // -------- Grelha circular de amplitudes e diâmetros de dificuldade -------- //
    public GameObject circularGrid_Left;                                          //
    public GameObject circularGrid_Right;                                          //
    public GameObject easyArea;                                                   // 
    public GameObject mediumArea;                                                 //
    public GameObject hardArea;                                                   //
    public bool mudouDeCor;                                                       //
    // -------------------------------------------------------------------------- //   
    void Awake()
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

    public void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        if (cooldownTimer < 0)
        {
            cooldownTimer = 0;
        }
    }
    Vector3 RandomCircle(Vector3 center)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

    public Vector3 OrbitPosition (Vector3 centerPoint, float radius, float angle)
    {
        Vector3 tmp;
        tmp.x = Mathf.Cos(angle * (Mathf.PI / 180)) * radius + centerPoint.x;
        tmp.y = Mathf.Sin(angle * (Mathf.PI / 180)) * radius + centerPoint.y;
        tmp.z = centerPoint.z;
        return tmp;
    }
    public void InstantiateObject (GameObject NewTarget, Vector3 position, Quaternion rotation)
    {
        Vector3 pos = RandomCircle(position);
        ObInstance = Instantiate(NewTarget, pos, rotation, parent);
    }

    public GameObject InstantiateRandom (GameObject toInstantiate, Vector3 centerPoint)
    {
        // Calcular a posição para instanciação
        if (State.leftArmSelected)
        {
            Vector3 newPosition = OrbitPosition(centerPoint, radius, Random.Range(90f, 270f));
            // Instanciar uma nova instância do objeto
            ObInstance = Instantiate(toInstantiate,parent) as GameObject;
            if (ObInstance)
            {
                ObInstance.transform.position = newPosition;
            }
            return ObInstance;
        }
        else
        {
            Vector3 newPosition = OrbitPosition(centerPoint, radius, Random.Range(-90f, 90f));
            // Instanciar uma nova instância do objeto
            ObInstance = Instantiate(toInstantiate,parent) as GameObject;
            if (ObInstance)
            {
                ObInstance.transform.position = newPosition;
            }
            return ObInstance;
        }       
    }
    public void DestroyObject(GameObject oldShape)
    {
        Destroy(oldShape);
    }

    public void CooldownTimer(float timer)
    {        
        cooldownTimer = timer;
    }

    public void PlayClip()
    {
        Source.Play();
    }
}
