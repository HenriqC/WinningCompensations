using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameters : MonoBehaviour {
    public void setMaxReps(string value) {
        if (value == "")
            value = "" + 0;
        State.maxReps = Int32.Parse(value);
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
}
