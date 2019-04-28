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
    public GameObject shoulderSphere;
    public GameObject hipArrow;
    public GameObject hipSphere;
    public GameObject spineLeftArrow;
    public GameObject spineLeftSphere;
    public GameObject spineRightArrow;
    public GameObject spineRightSphere;

    public GameObject shoulders;
    public GameObject hips;
    public GameObject spine;

    private Vector3 originalSphereSize;
    private float originalSphereScale;
    private Vector3 maxSphereSize;
    private float maxSphereScale;

    public bool SphereComp;
    private bool hasRegisteredShoulderCompensation;
    private bool hasRegisteredHipCompensation;
    private bool hasRegisteredSpineCompensation;

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

            // Define o diâmetro inicial da esfera
            originalSphereSize = new Vector3 (1f,1f,1f);
            originalSphereScale = originalSphereSize.magnitude;
            // Definem o diâmetro máximo da esfera
            maxSphereSize = new Vector3(2f, 2f, 2f);
            maxSphereScale = maxSphereSize.magnitude;

            if (hasShoulderCompensation) {
                State.compensationInCurrentRep = true;
                setBarColorToRed(shoulderBar);

                if (SphereComp)
                {
                    //Debug.LogWarning("Entrou");
                    shoulderSphere.SetActive(true);
                }
                else
                {
                    shoulderArrow.SetActive(true);
                }

                // ---- Guarda a diferença de alturas entre os ombros para aumentar o tamanho das esferas de acordo com este valor ---- //

                float shoulderOffset = Math.Abs(leftShoulder.transform.position.y - rightShoulder.transform.position.y);

                // -------------------------------------------------------------------------------------------------------------------- //
                         
                
                if (leftShoulder.transform.position.y > rightShoulder.transform.position.y) {
                    if(!hasRegisteredShoulderCompensation && State.isTherapyOnGoing) {
                        State.leftShoulderUp++;
                        State.tries++;                        
                    }
                    shoulderArrow.transform.position = new Vector3(leftShoulder.transform.position.x - arrowOffset, leftShoulder.transform.position.y + arrowOffset, leftShoulder.transform.position.z);
                    shoulderSphere.transform.position = new Vector3(leftShoulder.transform.position.x + (arrowOffset/2), leftShoulder.transform.position.y + arrowOffset, leftShoulder.transform.position.z);
                    if (shoulderSphere.transform.localScale.magnitude < maxSphereScale)
                    {
                        shoulderSphere.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f) * shoulderOffset;
                    }                                         
                    //Debug.LogWarning(shoulderSphere.transform.position);
                }
                else {
                    if (!hasRegisteredShoulderCompensation && State.isTherapyOnGoing) { 
                        State.rightShoulderUp++;
                        State.tries++;                        
                    }
                    shoulderArrow.transform.position = new Vector3(rightShoulder.transform.position.x + arrowOffset, rightShoulder.transform.position.y + arrowOffset, rightShoulder.transform.position.z);
                    shoulderSphere.transform.position = new Vector3(rightShoulder.transform.position.x + arrowOffset, rightShoulder.transform.position.y + arrowOffset, rightShoulder.transform.position.z);
                    if (shoulderSphere.transform.localScale.magnitude < maxSphereScale)
                    {
                        shoulderSphere.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f) * shoulderOffset;
                    }
                    //Debug.LogWarning(shoulderSphere.transform.position);
                }
                hasRegisteredShoulderCompensation = true;
            }
            else {
                setBarColorToGreen(shoulderBar);
                shoulderArrow.SetActive(false);
                shoulderSphere.SetActive(false);
                hasRegisteredShoulderCompensation = false;
                shoulderSphere.transform.localScale = originalSphereSize;
            }
        }

        //hip
        bool hipToggleIsOn = hipToggle.GetComponent<Toggle>().isOn;
        if (hipToggleIsOn) {
            hipBar.transform.position = new Vector3(spineBase.transform.position.x, spineBase.transform.position.y, hipBar.transform.position.z);
            hipToggle.transform.position = new Vector3(hipToggle.transform.position.x, hipBar.transform.position.y, hipToggle.transform.position.z);

            bool hasHipCompensation = Math.Abs(leftHip.transform.position.y - rightHip.transform.position.y) > hipSlider.GetComponent<Slider>().value;

            // Define o diâmetro inicial da esfera
            originalSphereSize = new Vector3(1f, 1f, 1f);
            originalSphereScale = originalSphereSize.magnitude;
            // Definem o diâmetro máximo da esfera
            maxSphereSize = new Vector3(2f, 2f, 2f);
            maxSphereScale = maxSphereSize.magnitude;


            if (hasHipCompensation) {
                State.compensationInCurrentRep = true;
                setBarColorToRed(hipBar);

                if (SphereComp)
                {
                    //Debug.LogWarning("Entrou");
                    hipSphere.SetActive(true);
                }
                else
                {
                    hipArrow.SetActive(true);
                }

                // ---- Guarda a diferença de alturas entre os ombros para aumentar o tamanho das esferas de acordo com este valor ---- //

                float hipOffset = Math.Abs(leftHip.transform.position.y - rightHip.transform.position.y);

                // -------------------------------------------------------------------------------------------------------------------- //

                if (leftHip.transform.position.y > rightHip.transform.position.y) {
                    if (!hasRegisteredHipCompensation && State.isTherapyOnGoing) {
                        State.leftHipUp++;
                        State.tries++;
                    }
                    hipArrow.transform.position = new Vector3(leftHip.transform.position.x - arrowOffset, leftHip.transform.position.y + arrowOffset, leftHip.transform.position.z);
                    hipSphere.transform.position = new Vector3(leftHip.transform.position.x - (arrowOffset / 2), leftHip.transform.position.y + arrowOffset, leftHip.transform.position.z);
                    if (hipSphere.transform.localScale.magnitude < maxSphereScale)
                    {
                        hipSphere.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f) * hipOffset;
                    }
                }
                else {
                    if (!hasRegisteredHipCompensation && State.isTherapyOnGoing) {
                        State.rightHipUp++;
                        State.tries++;
                    }
                    hipArrow.transform.position = new Vector3(rightHip.transform.position.x + arrowOffset, rightHip.transform.position.y + arrowOffset, rightHip.transform.position.z);
                    hipSphere.transform.position = new Vector3(rightHip.transform.position.x + arrowOffset, rightHip.transform.position.y + arrowOffset, rightHip.transform.position.z);
                    if (hipSphere.transform.localScale.magnitude < maxSphereScale)
                    {
                        hipSphere.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f) * hipOffset;
                    }
                }
                hasRegisteredHipCompensation = true;
            }
            else {
                setBarColorToGreen(hipBar);
                hipArrow.SetActive(false);
                hasRegisteredHipCompensation = false;
                hipSphere.transform.localScale = originalSphereSize;
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

                // ---- Guarda a diferença de alturas entre os ombros para aumentar o tamanho das esferas de acordo com este valor ---- //

                float spineOffset = Math.Abs(spineShoulder.transform.position.y - spineBase.transform.position.y);

                // -------------------------------------------------------------------------------------------------------------------- //

                if (spineShoulder.transform.position.x > spineBase.transform.position.x) {
                    if (!hasRegisteredSpineCompensation && State.isTherapyOnGoing) {
                        State.leaningRight++;
                        State.tries++;
                    }
                    if (SphereComp)
                    {
                        spineRightSphere.SetActive(true);
                    } else
                    {
                        spineRightArrow.SetActive(true);
                    }                                      
                    spineRightArrow.transform.position = new Vector3(spineBar.transform.position.x + arrowOffset, spineBar.transform.position.y, spineBar.transform.position.z);
                    spineRightSphere.transform.position = new Vector3(spineBar.transform.position.x + (arrowOffset * 2), spineBar.transform.position.y, spineBar.transform.position.z);
                    if (spineRightSphere.transform.localScale.magnitude < maxSphereScale)
                    {
                        spineRightSphere.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f) * spineOffset;
                    }

                }
                else {
                    if (!hasRegisteredSpineCompensation && State.isTherapyOnGoing) {
                        State.leaningLeft++;
                        State.tries++;
                    }
                    if (SphereComp)
                    {
                        spineLeftSphere.SetActive(true);
                    }
                    else
                    {
                        spineLeftArrow.SetActive(true);
                    }
                    spineLeftArrow.transform.position = new Vector3(spineBar.transform.position.x - arrowOffset, spineBar.transform.position.y, spineBar.transform.position.z);
                    spineLeftSphere.transform.position = new Vector3(spineBar.transform.position.x - (arrowOffset * 2), spineBar.transform.position.y, spineBar.transform.position.z);
                    if (spineLeftSphere.transform.localScale.magnitude < maxSphereScale)
                    {
                        spineLeftSphere.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f) * spineOffset;
                    }
                }
                hasRegisteredSpineCompensation = true;
            }
            else {
                setBarColorToGreen(spineBar);
                spineLeftArrow.SetActive(false);
                spineRightArrow.SetActive(false);
                spineLeftSphere.SetActive(false);
                spineRightSphere.SetActive(false);
                hasRegisteredSpineCompensation = false;
                spineRightSphere.transform.localScale = originalSphereSize;
                spineLeftSphere.transform.localScale = originalSphereSize;
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
