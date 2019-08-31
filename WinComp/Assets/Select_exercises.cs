using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_exercises : MonoBehaviour
{
    public static Select_exercises instance = null;
    public Toggle targetReach;
    public Toggle lineDraw;
    public Toggle Shapes;

    public bool TargetsOn;
    public bool LineOn;
    public bool ShapesOn;

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

    public void Update()
    {
        if (targetReach.isOn)
        {
            TargetsOn = true;
        }
        else
        {
            TargetsOn = false;
        }

        if (lineDraw.isOn)
        {
            LineOn = true;
        }
        else
        {
            LineOn = false;
        }

        if (Shapes.isOn)
        {
            ShapesOn = true;
        }
        else
        {
            ShapesOn = false;
        }
    }
}
