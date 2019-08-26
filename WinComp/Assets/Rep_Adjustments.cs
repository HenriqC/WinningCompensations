using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rep_Adjustments : MonoBehaviour
{
    public Slider pathTimer;

    public void setTimer(float timer)
    {
        Instantiate_target.instance.lineDrawTimer = timer;
    }
}
