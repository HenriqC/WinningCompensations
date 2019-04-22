﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exercise : MonoBehaviour {

    public GameObject cursor;
    public bool hasSecondaryCursor;

    public Text avgTime;    

    public string exerciseName;

    //public GameObject leftExerciseBox;
    //public GameObject rightExerciseBox;
    //private GameObject exerciseBoxGroup;

    public GameObject leftTargets;
    public GameObject rightTargets;
    private GameObject targets;

    private AudioClip beep;
    private AudioSource audioSource;

    private bool reversePath;

    private bool isGroing;

    private bool hasRegisteredOutOfPath;
    private bool isBlinking;

    private int repCounter;

    // Use this for initialization
    void Start () {
    }

    void init() {
        if (State.maxReps <= 0) {
            State.maxReps = 10;
        }

        State.hasSecondaryCursor = hasSecondaryCursor;
        State.exerciseName = exerciseName;
        State.currentTarget = 0;
        State.hasStartedExercise = false;
        reversePath = false;
        setArea();
        beep = (AudioClip)Resources.Load("Sounds/beep");
        audioSource = GetComponent<AudioSource>();
        Renderer renderer = targets.transform.GetChild(0).gameObject.GetComponent<Renderer>();
        Color color = renderer.material.color;
        color.r = color.b = 0;
        color.g += 1;

        renderer.material.color = color;
    }

    private void OnEnable() {
        init();
    }

    private void OnDisable() {
        CancelInvoke();
    }

    private void setArea() {
        if (State.isLeftArmSelected()) {
            activate(true);
            //exerciseBoxGroup = leftExerciseBox;
            targets = leftTargets;

        }
        else if (State.isRightArmSelected()) {
            activate(false);
            //exerciseBoxGroup = rightExerciseBox;
            targets = rightTargets;
        }
    }

    private void activate(bool left) {
        leftTargets.SetActive(left);
        //leftExerciseBox.SetActive(left);
        rightTargets.SetActive(!left);
        //rightExerciseBox.SetActive(!left);
    }

    // Update is called once per frame
    void Update () {
        RaycastHit hit;
        Ray landingRay = new Ray(cursor.transform.position, Vector3.back);

        if(State.isTherapyOnGoing) {
            if(!isBlinking) {
                InvokeRepeating("blinkTarget", 0, 0.05f);
                isBlinking = true;
            }

            if (Physics.Raycast(landingRay, out hit)) {
                hasRegisteredOutOfPath = false;
                if (hit.collider.tag == "ExerciseCollider") { // is inside of the exercise area
                    /*if (State.hasStartedExercise) {
                        foreach (Transform exerciseBox in exerciseBoxGroup.transform) {
                            exerciseBox.gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 0.3f);
                        }
                    }*/
                }
                if (hit.collider.tag == "TargetCollider") { // has hit a target
                    bool hasHitTheCurrentTarget = hit.transform.position == targets.transform.GetChild(State.currentTarget).transform.position;

                    if (hasHitTheCurrentTarget) {
                        if (!State.hasStartedExercise) {
                            State.hasStartedExercise = true;
                        }

                        targets.transform.GetChild(State.currentTarget).gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
                        if (State.currentTarget == (targets.transform.childCount - 1)) { // end of one correct repetition, need to start from the first target again
                            reversePath = true;
                            State.currentTarget--;

                        }
                        else if (reversePath && State.currentTarget == 0) {
                            State.currentTarget = 0;
                            State.tries++;
                            State.correctReps++;
                            reversePath = false;

                            int minutes = (State.sessionTimeInt / State.correctReps) / 60;
                            int seconds = (State.sessionTimeInt / State.correctReps) % 60;

                            avgTime.text = minutes.ToString("00") + ":" + seconds.ToString("00");

                            if (State.correctReps >= State.maxReps) { // done all the needed reps
                                State.hasFinishedExercise = true;
                            }
                        }
                        else if (!reversePath) {
                            State.currentTarget++;
                        }
                        else if (reversePath) {
                            State.currentTarget--;
                        }
                        audioSource.PlayOneShot(beep);
                    }
                }
            }
            /* else { // the cursor isn't inside of the area
                if (State.hasStartedExercise) {
                    foreach (Transform exerciseBox in exerciseBoxGroup.transform) {
                        exerciseBox.gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0.3f);
                    }
                    if (!hasRegisteredOutOfPath) {
                        State.outOfPath++;
                        State.tries++;
                        hasRegisteredOutOfPath = true;
                    }
                    State.compensationInCurrentRep = true;
                }
            }*/

            if (State.compensationInCurrentRep) { // if is detected a compensation we restart the repetition
                State.compensationInCurrentRep = false;
                reversePath = false;
                if(State.currentTarget != 0) {
                    targets.transform.GetChild(State.currentTarget).gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
                }
                State.currentTarget = 0;
            }
        }
    }

    private void blinkTarget() {
        if (targets.transform.childCount == 0)
            return;

        Renderer renderer = null;
        int target = State.currentTarget;
        while (renderer == null) {
            Renderer rendererTemp = targets.transform.GetChild(target).gameObject.GetComponent<Renderer>();

            if (rendererTemp.enabled) {
                renderer = rendererTemp;
            }
            else if (reversePath) {
                target--;
            }
            else {
                target++;
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
        color.b += delta;

        renderer.material.color = color;
    }
}