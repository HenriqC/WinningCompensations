using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instantiate_target : MonoBehaviour
{
    public static Instantiate_target instance = null;
    public GameObject center;
    public GameObject ObInstance;
    public GameObject rightShoulder;
    public GameObject leftShoulder;
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
    public void InstantiateObject (GameObject NewTarget, Vector3 position, Quaternion rotation)
    {
        Vector3 pos = RandomCircle(position);
        ObInstance = Instantiate(NewTarget, pos, rotation, parent);
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
