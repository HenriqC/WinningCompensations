using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterScreenController : MonoBehaviour {

    public GameObject nextButton;
    public GameObject BodyScreen;

    public void next() {
        BodyScreen.SetActive(true);
        gameObject.SetActive(false);
    }
}
