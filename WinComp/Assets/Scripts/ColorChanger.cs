﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    // -------- Color change variables -------- //
    public GameObject Target;
    public float speed;
    public Color startColor;
    public Color endColor;
    float startTime;
    // -------- Color change variables -------- //

    public bool mudou;
    void Start()
    {
        Instantiate_target.instance.mudouDeCor = false;
        startTime = Time.time;
    }

    void Update()
    {
        float t = (Time.time - startTime) * speed;
        Target.GetComponent<Renderer>().sharedMaterial.color = Color.Lerp(startColor, endColor, t);

        if (Target.GetComponent<Renderer>().sharedMaterial.color == endColor)
        {
            Instantiate_target.instance.mudouDeCor = true;
        }
    }
}
