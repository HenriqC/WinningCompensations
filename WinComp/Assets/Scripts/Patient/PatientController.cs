﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatientController : MonoBehaviour {
    public static PatientController instance = null;
    public Text sessionTimePatient;
    public Text correctRepetitionsPatient;
    public Text setTimePatient;
    public Text restTimePatient;
    public Text startCounterPatient;
    public Text countdownSubtitle;

    public Text sessionTimeTherapist;
    public Text correctRepetitionsTherapist;
    public Text maxRepetitionsPatient;
    public Text setTimeTherapist;
    public Text restTimeTherapist;
    public Text triesTherapist;
    public Text completedSets;
    public Text maxSets;
    public Text previousSetReps;

    public GameObject restPatient;
    public GameObject restTherapist;
    public GameObject completed;

    private int sessionTimeInt;
    public int setTimeInt;
    private int restTimeInt;
    private int startCounterInt;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    void init() {
        if (State.setDuration <= 0) {
            State.setDuration = 60;
        }

        if (State.restDuration <= 0) {
            State.restDuration = 60;
        }

        restPatient.SetActive(false);
        restTherapist.SetActive(false);

        startCounterInt = 4;
        InvokeRepeating("countDown", 0, 1);
    }

    private void countDown() {
        countdownSubtitle.transform.gameObject.SetActive(true);
        startCounterPatient.transform.gameObject.SetActive(true);
        if (startCounterInt <= 0) {
            initSessionTime();
            initSetTimer();
            startCounterPatient.transform.gameObject.SetActive(false);
            countdownSubtitle.transform.gameObject.SetActive(false);
            State.isTherapyOnGoing = true;
            CancelInvoke("countDown");
        }
        startCounterInt--;
        startCounterPatient.text = "" + startCounterInt;
    }

    private void initSessionTime() {
        sessionTimeInt = 0;
        InvokeRepeating("sessionTimeInc", 0, 1);
    }

    public void initSetTimer() {
        setTimeInt = State.setDuration;
        InvokeRepeating("setTimeDec", 0, 1);
    }

    private void initRestTimer() {
        restTimeInt = State.restDuration;
        InvokeRepeating("restTimeDec", 0, 1);
    }

    private void sessionTimeInc() {
        sessionTimeInt++;
        int minutes = sessionTimeInt / 60;
        int seconds = sessionTimeInt % 60;
        sessionTimePatient.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        sessionTimeTherapist.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        State.sessionTimeInt = sessionTimeInt;
    }

    public void activateRest() {
        restPatient.SetActive(true);
        restTherapist.SetActive(true);
        State.isTherapyOnGoing = false;
        State.restCount++;
        State.previousSet = State.correctReps;
        Instantiate_target.instance.setChanged = true;
        State.Correctness = ((float)State.correctReps / (float)State.tries) * 100f;
        new ReportGenerator().Savecsv();
        initRestTimer();
    }

    public void activateSet() {
        restPatient.SetActive(false);
        restTherapist.SetActive(false);
        State.isTherapyOnGoing = true;
        initSetTimer();
    }

    public void setTimeDec() {        

        setTimeInt--;
        int minutes = setTimeInt / 60;
        int seconds = setTimeInt % 60;
        setTimePatient.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        setTimeTherapist.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    private void restTimeDec() {
        if (restTimeInt == 0) {
            CancelInvoke("restTimeDec");
            activateSet();
            return;
        }
        

        restTimeInt--;
        int minutes = restTimeInt / 60;
        int seconds = restTimeInt % 60;
        restTimePatient.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        restTimeTherapist.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    private void OnEnable() {
        init();
    }

    private void OnDisable() {
        CancelInvoke();
    }

    // Use this for initialization
    void Start() {
    }


    // Update is called once per frame
    void Update() {

        if (State.hasFinishedExercise)
        {
            startCounterPatient.transform.gameObject.SetActive(false);
            CancelInvoke("countDown");
            CancelInvoke("restTimeDec");
            CancelInvoke("setTimeDec");
            CancelInvoke("sessionTimeInc");
        }
        completed.SetActive(State.hasFinishedExercise);

        correctRepetitionsPatient.text = "" + State.correctReps;
        maxRepetitionsPatient.text = "" + State.maxReps;
        correctRepetitionsTherapist.text = "" + State.correctReps;
        triesTherapist.text = "" + State.tries;
        completedSets.text = "" + State.completedSets;
        maxSets.text = "" + State.maxSets;
        previousSetReps.text = "" + State.previousSet;
    }
}
