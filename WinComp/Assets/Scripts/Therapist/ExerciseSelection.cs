using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseSelection : MonoBehaviour {

    private bool leftArmSelected;

    public GameObject armSelectionScreen;

    public GameObject frontRaise;
    public GameObject lateralRaise;
    public GameObject handToMouth;
    public GameObject combHair;
    public GameObject fillTheCup;

    public void selectFrontRaise() {
        frontRaise.SetActive(true);
        lateralRaise.SetActive(false);
        handToMouth.SetActive(false);
        combHair.SetActive(false);
        fillTheCup.SetActive(false);

        armSelectionScreen.SetActive(true);
        gameObject.SetActive(false);
    }

    public void selectLateralRaise() {
        frontRaise.SetActive(false);
        lateralRaise.SetActive(true);
        handToMouth.SetActive(false);
        combHair.SetActive(false);
        fillTheCup.SetActive(false);

        armSelectionScreen.SetActive(true);
        gameObject.SetActive(false);
    }

    public void selectHandToMouth() {
        frontRaise.SetActive(false);
        lateralRaise.SetActive(false);
        handToMouth.SetActive(true);
        combHair.SetActive(false);
        fillTheCup.SetActive(false);

        armSelectionScreen.SetActive(true);
        gameObject.SetActive(false);
    }

    public void selectCombHair() {
        frontRaise.SetActive(false);
        lateralRaise.SetActive(false);
        handToMouth.SetActive(false);
        combHair.SetActive(true);
        fillTheCup.SetActive(false);

        armSelectionScreen.SetActive(true);
        gameObject.SetActive(false);
    }

    public void selectFillTheCup() {
        frontRaise.SetActive(false);
        lateralRaise.SetActive(false);
        handToMouth.SetActive(false);
        combHair.SetActive(false);
        fillTheCup.SetActive(true);

        armSelectionScreen.SetActive(true);
        gameObject.SetActive(false);
    }
}
