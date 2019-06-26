using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeAdjustments : MonoBehaviour
{
    public void SetCompTolerance(float count)
    {
        Instantiate_target.instance.compCount = count;
    }

    public void ManualOnOff_Shapes(bool isToggled)
    {
        if (isToggled)
        {
            Instantiate_target.instance.manualDiff_Shapes.isOn = true;
        }
        else
        {
            Instantiate_target.instance.manualDiff_Shapes.isOn = false;
        }
    }
}
