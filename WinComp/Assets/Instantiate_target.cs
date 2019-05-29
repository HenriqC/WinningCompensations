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

    public float completion;
    public float correctness;
    public int icorrectness;
    public float cooldownTimer;

    public GameObject circularGrid;
    public GameObject easyArea;
    public GameObject mediumArea;
    public GameObject hardArea;

    // -------- Color change variables -------- //
    public GameObject Target;
    public float speed = 1.0f;
    public Color startColor;
    public Color endColor;
    float startTime;
    // -------- Color change variables -------- //

    private void Start()
    {
        startTime = Time.time;
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

        float t = (Time.time - startTime) * speed;
        Target.GetComponent<Renderer>().sharedMaterial.color = Color.Lerp(startColor, endColor, t);
    }

    public void InstantiateObject (GameObject NewTarget, Vector3 position, Quaternion rotation)
    {
        Instantiate(NewTarget, Random.insideUnitSphere * radius + position, rotation, parent);
    }

    /*public void ColorChanger(GameObject NewTarget, float duration)
    {
        Renderer colorChange = NewTarget.gameObject.GetComponent<Renderer>();
        colorChange.sharedMaterial.color = Color.Lerp(Color.red, Color.green, Mathf.PingPong(Time.time, duration));
    }*/

    public void DestroyObject(GameObject oldShape)
    {
        Object.Destroy(oldShape);
    }

    public void CooldownTimer(float timer)
    {        
        cooldownTimer = timer;
    }

    /*public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(radial_Data.originPoint, radial_Data.instantiateRadius);
    }*/
}
