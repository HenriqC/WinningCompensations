﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToCompViz : MonoBehaviour
{

    public bool show = true;
    public string tag;


    // Update is called once per frame
    public void toggleTheSke()
    {
        
        if (show)
        {
            var spines = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject fooObj in spines)
            {
                LineRenderer lr = fooObj.GetComponent<LineRenderer>();
                lr.SetWidth(0.01f, 0.01f);
            }
            show = false;
        }
        else
        {
            var spines = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject fooObj in spines)
            {
                LineRenderer lr = fooObj.GetComponent<LineRenderer>();
                lr.SetWidth(0f, 0f);
            }
            show = true;
        }
    }
}
