using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    // -------- Color change variables -------- //
    public GameObject Target;
    public float speed = 1.0f;
    public Color startColor;
    public Color endColor;
    float startTime;
    // -------- Color change variables -------- //

    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float t = (Time.time - startTime) * speed;
        Target.GetComponent<Renderer>().sharedMaterial.color = Color.Lerp(startColor, endColor, t);
    }
}
