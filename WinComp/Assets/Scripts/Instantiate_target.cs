using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instantiate_target : MonoBehaviour
{
    public static Instantiate_target instance = null;
    public Vector3 cogCenter;
    public GameObject ObInstance;
    public Toggle manualDiff;
    public Toggle manualDiff_Shapes;
    public Toggle verticalFD;
    public Toggle horizontalFD;
    public float maxComp;
    public float compCount;
    public float subState;
    public float manualRadius;
    public float manualSpeed;
    public float radius;

    public Vector3 targetScale = new Vector3 (0.125f, 0.125f, 0.125f);

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

    public Transform parent;
    public Text levelDiff;
    public Text exName;
    public Text avgTime;

    public ColorChanger changeSpeed;

    // -------- Variáveis das barras radiais --------//
    public float completion;                         //
    public float correctness;                        //
    public int icorrectness;                         //
    public float cooldownTimer;                      //
    // ----------------------------------------------//

    public float maxTimer;
    public AudioSource Source;

    // -------- Grelha circular de amplitudes e raios de dificuldade -------- //
    public GameObject circularGrid_Left;                                          //
    public GameObject circularGrid_Right;                                         //
    public GameObject R_Area_1;                                                   // 
    public GameObject R_Area_2;                                                   //
    public GameObject R_Area_3;                                                   //
    public GameObject R_Area_4;                                                   //
    public GameObject R_Area_5;                                                   //
    public GameObject L_Area_1;                                                   //
    public GameObject L_Area_2;                                                   //
    public GameObject L_Area_3;                                                   //
    public GameObject L_Area_4;                                                   //
    public GameObject L_Area_5;                                                   //
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
        if (maxTimer > 0)
        {
            maxTimer -= Time.deltaTime;
        }
        else if(maxTimer < 0)
        {
            maxTimer = 0;
        }

        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else if (cooldownTimer < 0)
        {
            cooldownTimer = 0;
        }
    }

    /*Vector3 RandomCircle(Vector3 center)
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
    }*/

    public Vector3 OrbitPosition(Vector3 centerPoint, float radius, float angle)
    {
        Vector3 tmp;
        tmp.x = Mathf.Cos(angle * (Mathf.PI / 180)) * radius + centerPoint.x;
        tmp.y = Mathf.Sin(angle * (Mathf.PI / 180)) * radius + centerPoint.y;
        tmp.z = centerPoint.z;
        return tmp;
    }

    public GameObject InstantiateRandom (GameObject toInstantiate, Vector3 centerPoint)
    {
        // Calcular a posição para instanciação
        if (State.leftArmSelected)
        {
            Vector3 newPosition_1 = OrbitPosition(centerPoint, radius, Random.Range(minRange_L1, maxRange_L1));
            Vector3 newPosition_2 = OrbitPosition(centerPoint, radius, Random.Range(minRange_L2, maxRange_L2));

            // Instanciar uma nova instância do objeto
            ObInstance = Instantiate(toInstantiate,parent) as GameObject;
            ObInstance.transform.localScale = targetScale;
            int toggle = Random.Range(0, 2);

            if (ObInstance && toggle == 0)
            {
                ObInstance.transform.position = newPosition_1;
            }
            else if (ObInstance && toggle == 1)
            {
                ObInstance.transform.position = newPosition_2;
            }
            return ObInstance;
            
        }
        else
        {
            Vector3 newPosition_1 = OrbitPosition(centerPoint, radius, Random.Range(minRange_R1, maxRange_R1));
            Vector3 newPosition_2 = OrbitPosition(centerPoint, radius, Random.Range(minRange_R2, maxRange_R2));
            // Instanciar uma nova instância do objeto
            ObInstance = Instantiate(toInstantiate,parent) as GameObject;
            ObInstance.transform.localScale = targetScale;
            int toggle = Random.Range(0, 2);

            if (ObInstance && toggle == 0)
            {
                ObInstance.transform.position = newPosition_1;
                Debug.Log(toggle);
            }
            else if (ObInstance && toggle == 1)
            {
                ObInstance.transform.position = newPosition_2;
                Debug.Log(toggle);
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

    public void MaxRepTimer (float maxTime)
    {
        maxTimer = maxTime;
    }
    public void PlayClip()
    {
        Source.Play();
    }
}
