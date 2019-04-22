using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedHandPosition : MonoBehaviour {

    public GameObject secondaryCursor;
    public GameObject leftHandMarker;
    public GameObject rightHandMarker;

    private GameObject activeMarker;
    private bool isGroing;
    private bool hasCountedCompensation;
    private bool isBlinking;


    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (State.isLeftArmSelected()) {
            leftHandMarker.SetActive(false);
            rightHandMarker.SetActive(true);
            activeMarker = rightHandMarker;
        }
        else if (State.isRightArmSelected()) {
            leftHandMarker.SetActive(true);
            rightHandMarker.SetActive(false);
            activeMarker = leftHandMarker;
        }

        if (State.isTherapyOnGoing) {            
            
            if(State.hasStartedExercise && !State.hasFinishedExercise) {
                RaycastHit hit;
                Ray landingRay = new Ray(secondaryCursor.transform.position, Vector3.back);

                if (Physics.Raycast(landingRay, out hit)) {
                    if (!(hit.collider.tag == "FixedHandPosition") && !hasCountedCompensation) { // is inside of the exercise area
                        State.compensationInCurrentRep = true;
                        State.outOfPath++;
                        State.tries++;
                        hasCountedCompensation = true;
                        blink();
                    }
                    else {
                        cancelBlink();
                        hasCountedCompensation = false;
                    }
                }
                else {
                    if(!hasCountedCompensation) {
                        State.compensationInCurrentRep = true;
                        State.outOfPath++;
                        State.tries++;
                        hasCountedCompensation = true;
                        blink();
                    }                    
                }
            }
            else {
                RaycastHit hit;
                Ray landingRay = new Ray(secondaryCursor.transform.position, Vector3.back);

                if (Physics.Raycast(landingRay, out hit)) {
                    if (!(hit.collider.tag == "FixedHandPosition") & !isBlinking) { // is inside of the exercise area
                        isBlinking = true;
                        blink();
                    }
                    else {
                        isBlinking = false;
                        cancelBlink();
                    }
                }
                else {
                    if (!isBlinking) {
                        isBlinking = true;
                        blink();
                    }
                }
            }
        }
    }

    private void blink() {
        InvokeRepeating("blinkTarget", 0, 0.05f);
    }

    private void cancelBlink() {
        CancelInvoke("blinkTarget");

        Renderer renderer = activeMarker.gameObject.GetComponent<Renderer>();
        Color color = renderer.material.color;
        color.r = color.g = 0;
        color.b = 1;

        renderer.material.color = color;
    }

    private void blinkTarget() {
        Renderer renderer = null;
        int target = State.currentTarget;
        while (renderer == null) {
            Renderer rendererTemp = activeMarker.gameObject.GetComponent<Renderer>();

            if (rendererTemp.enabled) {
                renderer = rendererTemp;
            }
        }

        float delta;

        if (isGroing && renderer.material.color.r > 1) {
            isGroing = false;
        }
        else if (!isGroing && renderer.material.color.r < 0) {
            isGroing = true;
        }

        if (isGroing) {
            delta = 0.1f;
        }
        else {
            delta = -0.1f;
        }

        Color color = renderer.material.color;
        color.r += delta;
        color.g += delta;

        renderer.material.color = color;
    }
}
