using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyScreenController : MonoBehaviour {

    public GameObject startButton;
    public GameObject stopButton;
    public GameObject restartButton;
    public GameObject exerciseScreen;
    public GameObject patientCanvas;

    public Text leaned;
    public Text shoulderLift;
    public Text outOfPath;
    public Text exerciseName;
    public Text Angles;

    private bool hasWroteReport;

    public void StartTherapy() {
        startButton.SetActive(false);
        stopButton.SetActive(true);
        patientCanvas.SetActive(true);
    }

    public void StopTherapy() {
        stopButton.SetActive(false);
        restartButton.SetActive(true);
        State.hasFinishedExercise = true;
        State.isTherapyOnGoing = false;
        if (!hasWroteReport) {
            new ReportGenerator().Savecsv();
            hasWroteReport = true;
        }        
    }

    public void Restart() {
        State.resetState();
        Application.LoadLevel("Everything Mixed");
    }

    public void Update() {
        leaned.text = "" + (State.leaningLeft + State.leaningRight);
        shoulderLift.text = "" + (State.leftShoulderUp + State.rightShoulderUp);
        outOfPath.text = "" + State.outOfPath;
        exerciseName.text = State.exerciseName;
        Angles.text = "" + KinectDataManager.result;

        if(State.hasFinishedExercise) {
            StopTherapy();
        }
    }
}
