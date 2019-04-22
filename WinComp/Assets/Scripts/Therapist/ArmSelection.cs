using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmSelection : MonoBehaviour {
    private bool leftArmSelected;

    public GameObject parameterScreen;

	public void selectLeftArm() {
        State.leftArmSelected = true;
        parameterScreen.SetActive(true);
        gameObject.SetActive(false);
    }

    public void selectRightArm() {
        State.leftArmSelected = false;
        parameterScreen.SetActive(true);
        gameObject.SetActive(false);
    }
}
