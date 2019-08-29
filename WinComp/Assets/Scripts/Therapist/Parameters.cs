using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parameters : MonoBehaviour {

    public InputField reps;
    public InputField sets;
    public InputField duration;
    public InputField rest;


    public void setMaxReps(string value) {
        if (value == "")
            value = "" + 0;
        State.maxReps = Int32.Parse(value);
    }

    public void setMaxSets(string value)
    {
        if (value == "")
        {
            value = "" + 0;
        }
        State.maxSets = Int32.Parse(value);
    }

    public void setDuration(string value) {
        if (value == "")
            value = "" + 0;
        State.setDuration = Int32.Parse(value);
    }

    public void setRestDuration(string value) {
        if (value == "")
            value = "" + 0;
        State.restDuration = Int32.Parse(value);
    }

    public void ArrowUpReps()
    {

        if (reps.text == "")
            reps.text = "1";
        else
        {
            var value = Int32.Parse(reps.text);
            value += 1;
            reps.text = value.ToString();
        }
    }

    public void ArrowUpDuration()
    {
        if (duration.text == "")
            duration.text = "1";
        else
        {
            var value = Int32.Parse(duration.text);
            value += 1;
            duration.text = value.ToString();
        }
    }

    public void ArrowUpRest()
    {
        if (rest.text == "")
            rest.text = "1";
        else
        {
            var value = Int32.Parse(rest.text);
            value += 1;
            rest.text = value.ToString();
        }
    }

    public void ArrowDReps()
    {
        if (reps.text != "" && reps.text != "0")
        {
            var value = Int32.Parse(reps.text);
            value -= 1;
            reps.text = value.ToString();
        }
    }

    public void ArrowDDuration()
    {
        if (duration.text != "" && duration.text != "0")
        {
            var value = Int32.Parse(duration.text);
            value -= 1;
            duration.text = value.ToString();
        }
    }

    public void ArrowDRest()
    {
        if (rest.text != "" && rest.text != "0"){
            var value = Int32.Parse(rest.text);
            value -= 1;
            rest.text = value.ToString();
        }
    }

}
