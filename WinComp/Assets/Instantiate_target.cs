using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instantiate_target : MonoBehaviour
{
    public GameObject center;
    public float radius;
    public static Instantiate_target instance = null;
    public Transform parent;
    public Text avgTime;
    public State_Targets radial_Data;

    // -------- Variáveis de instanciação dos alvos cognitivos --------//
    public GameObject cognitTarget;                                    //
    public Transform cogPosition;                                        //
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
    public GameObject circularGrid;                                               //
    public GameObject easyArea;                                                   // 
    public GameObject mediumArea;                                                 //
    public GameObject hardArea;                                                   //
    public bool mudouDeCor;                                                       //
    // -------------------------------------------------------------------------- //
    private void Start()
    {
        InvokeRepeating("SpawnObject", spawnStart, spawnRate);
    }
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

    public void InstantiateObject (GameObject NewTarget, Vector3 position, Quaternion rotation)
    {
        Instantiate(NewTarget, Random.insideUnitSphere * radius + position, rotation, parent);
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

    public void SpawnObject()
    {
        Instantiate(cognitTarget, Random.insideUnitSphere * radius + cogPosition.position, Quaternion.identity, parent);

        if (stopSpawning)
        {
            CancelInvoke("SpawnObject");
        }
    }
}
