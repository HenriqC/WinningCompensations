using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Windows.Kinect;

public class BodyScreenController : MonoBehaviour {

    public KinectData Kinect;
    public GameObject pausePanel_T, pausePanel_P;
    public GameObject startButton;
    public GameObject pauseButton;
    public GameObject stopButton;
    public GameObject restartButton;
    public GameObject exerciseScreen;
    public GameObject therapistCanvas;

    public Text leaned;
    public Text shoulderLift;
    public Text outOfPath;
    public Text exerciseName;

    private bool hasWroteReport;

    public void StartTherapy() {

        startButton.SetActive(false);
        pauseButton.SetActive(true);
        stopButton.SetActive(true);
        therapistCanvas.SetActive(true);
    }

    public void StopTherapy() {
        stopButton.SetActive(false);
        restartButton.SetActive(true);
        State.hasFinishedExercise = true;
        State.isTherapyOnGoing = false;     
    }

    public void Restart() {
        State.resetState();
        Application.LoadLevel("Everything Mixed");
    }

    public void Pause()
    {
        Kinect.Close();
        pausePanel_T.SetActive(true);
        pausePanel_P.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Kinect.Start();
        pausePanel_T.SetActive(false);
        pausePanel_P.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
    }

    public void Update() {
        leaned.text = "" + (State.leaningLeft + State.leaningRight);
        shoulderLift.text = "" + (State.leftShoulderUp + State.rightShoulderUp);
        outOfPath.text = "" + State.outOfPath;
        exerciseName.text = State.exerciseName;

        if(State.hasFinishedExercise) {
            StopTherapy();
        }
    }
}
