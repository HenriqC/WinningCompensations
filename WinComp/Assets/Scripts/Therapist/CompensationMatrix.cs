using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompensationMatrix : MonoBehaviour {

    public GameObject leftHip; 
    public GameObject rightHip;

    public GameObject leftShoulder;
    public GameObject rightShoulder;

    public GameObject spineBase;
    public GameObject spineMid;
    public GameObject spineShoulder;

    public GameObject shoulderBar;
    public GameObject hipBar;
    public GameObject spineBar;

    public GameObject shoulderToggle;
    public GameObject hipToggle;
    public GameObject spineToggle;

    public GameObject shoulderSlider;
    public GameObject hipSlider;
    public GameObject spineSlider;

    public GameObject shoulderArrow;
    public GameObject hipArrow;
    public GameObject spineLeftArrow;
    public GameObject spineRightArrow;

    public GameObject shoulders;
    public GameObject hips;
    public GameObject spine;

    private bool hasRegistredShoulderCompensation;
    private bool hasRegistredHipCompensation;
    private bool hasRegistredSpineCompensation;

    private float arrowOffset = 0.1f;

    // Use this for initialization
    void Start () {
		
	}
	
    private void setBarColorToRed(GameObject bar) {
        bar.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0.3f);
    }

    private void setBarColorToGreen(GameObject bar) {
        bar.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 0.3f);
    }

    // Update is called once per frame
    void Update () {
        if (leftHip.transform.position == rightHip.transform.position)
            return;

        //shoulder
        bool shoulderToggleIsOn = shoulderToggle.GetComponent<Toggle>().isOn;
        if (shoulderToggleIsOn) {
            shoulderBar.transform.position = new Vector3(spineBase.transform.position.x, spineShoulder.transform.position.y, shoulderBar.transform.position.z);
            shoulderToggle.transform.position = new Vector3(shoulderToggle.transform.position.x, shoulderBar.transform.position.y, shoulderToggle.transform.position.z);

            bool hasShoulderCompensation = Math.Abs(leftShoulder.transform.position.y - rightShoulder.transform.position.y) > shoulderSlider.GetComponent<Slider>().value;
            if (hasShoulderCompensation) {
                State.compensationInCurrentRep = true;

                setBarColorToRed(shoulderBar);
                shoulderArrow.SetActive(true);

                if (leftShoulder.transform.position.y > rightShoulder.transform.position.y) {
                    if(!hasRegistredShoulderCompensation && State.isTherapyOnGoing) {
                        State.leftShoulderUp++;
                        State.tries++;
                    }
                    shoulderArrow.transform.position = new Vector3(leftShoulder.transform.position.x - arrowOffset, leftShoulder.transform.position.y + arrowOffset, leftShoulder.transform.position.z);
                }
                else {
                    if (!hasRegistredShoulderCompensation && State.isTherapyOnGoing) { 
                        State.rightShoulderUp++;
                        State.tries++;
                    }
                shoulderArrow.transform.position = new Vector3(rightShoulder.transform.position.x + arrowOffset, rightShoulder.transform.position.y + arrowOffset, rightShoulder.transform.position.z);
                }
                hasRegistredShoulderCompensation = true;
            }
            else {
                setBarColorToGreen(shoulderBar);
                shoulderArrow.SetActive(false);
                hasRegistredShoulderCompensation = false;
            }
        }

        //hip
        bool hipToggleIsOn = hipToggle.GetComponent<Toggle>().isOn;
        if (hipToggleIsOn) {
            hipBar.transform.position = new Vector3(spineBase.transform.position.x, spineBase.transform.position.y, hipBar.transform.position.z);
            hipToggle.transform.position = new Vector3(hipToggle.transform.position.x, hipBar.transform.position.y, hipToggle.transform.position.z);

            bool hasHipCompensation = Math.Abs(leftHip.transform.position.y - rightHip.transform.position.y) > hipSlider.GetComponent<Slider>().value;
            if (hasHipCompensation) {
                State.compensationInCurrentRep = true;

                setBarColorToRed(hipBar);
                hipArrow.SetActive(true);

                if (leftHip.transform.position.y > rightHip.transform.position.y) {
                    if (!hasRegistredHipCompensation && State.isTherapyOnGoing) {
                        State.leftHipUp++;
                        State.tries++;
                    }
                    hipArrow.transform.position = new Vector3(leftHip.transform.position.x - arrowOffset, leftHip.transform.position.y + arrowOffset, leftHip.transform.position.z);
                }
                else {
                    if (!hasRegistredHipCompensation && State.isTherapyOnGoing) {
                        State.rightHipUp++;
                        State.tries++;
                    }
                    hipArrow.transform.position = new Vector3(rightHip.transform.position.x + arrowOffset, rightHip.transform.position.y + arrowOffset, rightHip.transform.position.z);
                }
                hasRegistredHipCompensation = true;
            }
            else {
                setBarColorToGreen(hipBar);
                hipArrow.SetActive(false);
                hasRegistredHipCompensation = false;
            }
        }

        //spine
        bool spineToggleIsOn = spineToggle.GetComponent<Toggle>().isOn;
        if (spineToggleIsOn) {
            spineBar.transform.position = new Vector3(spineBase.transform.position.x, spineMid.transform.position.y, spineBar.transform.position.z);
            spineToggle.transform.position = new Vector3(spineBar.transform.position.x, spineToggle.transform.position.y, spineToggle.transform.position.z);

            bool hasSpineCompensation = Math.Abs(spineShoulder.transform.position.x - spineBase.transform.position.x) > spineSlider.GetComponent<Slider>().value;
            if (hasSpineCompensation) {
                State.compensationInCurrentRep = true;

                setBarColorToRed(spineBar);

                if (spineShoulder.transform.position.x > spineBase.transform.position.x) {
                    if (!hasRegistredSpineCompensation && State.isTherapyOnGoing) {
                        State.leaningRight++;
                        State.tries++;
                    }
                    spineRightArrow.SetActive(true);
                    spineRightArrow.transform.position = new Vector3(spineBar.transform.position.x + arrowOffset, spineBar.transform.position.y, spineBar.transform.position.z);

                }
                else {
                    if (!hasRegistredSpineCompensation && State.isTherapyOnGoing) {
                        State.leaningLeft++;
                        State.tries++;
                    }
                    spineLeftArrow.SetActive(true);
                    spineLeftArrow.transform.position = new Vector3(spineBar.transform.position.x - arrowOffset, spineBar.transform.position.y, spineBar.transform.position.z);
                }
                hasRegistredSpineCompensation = true;
            }
            else {
                setBarColorToGreen(spineBar);
                spineLeftArrow.SetActive(false);
                spineRightArrow.SetActive(false);
                hasRegistredSpineCompensation = false;
            }
        }
    }
    public void shoulderToggleBar() {
        shoulders.SetActive(shoulderToggle.GetComponent<Toggle>().isOn);
    }

    public void hipToggleBar() {
        hips.SetActive(hipToggle.GetComponent<Toggle>().isOn);
    }
    public void spineToggleBar() {
        spine.SetActive(spineToggle.GetComponent<Toggle>().isOn);
    }
}
