using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instantiate_target : MonoBehaviour
{
    public GameObject center;
    public float radius;
    public static Instantiate_target instance = null;
    public GameObject parent;
    public Text avgTime;
    public State_Targets radial_Data;

    public float completion;
    public float correctness;
    public int icorrectness;


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
    // Start is called before the first frame update
    public void InstantiateObject (GameObject NewTarget, Vector3 position, Quaternion rotation)
    {
        Instantiate(NewTarget, position, rotation, parent.transform);        
    }

    public void ColorChanger(GameObject NewTarget, float duration)
    {
        Renderer colorChange = NewTarget.gameObject.GetComponent<Renderer>();
        colorChange.sharedMaterial.color = Color.Lerp(Color.red, Color.green, Mathf.PingPong(Time.time, duration));
    }

    public void DestroyObject(GameObject oldShape)
    {
        Object.Destroy(oldShape);
    }

    /*public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(radial_Data.originPoint, radial_Data.instantiateRadius);
    }*/
}
