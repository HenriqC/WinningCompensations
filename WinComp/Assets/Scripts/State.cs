using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class State {
    public static int maxReps;
    public static int maxSets;
    public static int completedSets;
    public static int setDuration;
    public static int restDuration;
    public static int sessionTimeInt;
    public static float Correctness;

    public static int currentTarget;

    public static int tries;
    public static int correctReps;
    public static int previousSet;
    public static int restCount;

    public static bool leftArmSelected;
    public static bool hasSecondaryCursor;

    public static bool isTherapyOnGoing;
    public static bool isRestingTime;
    public static bool hasStartedExercise;
    public static bool hasFinishedExercise;

    public static bool compensationInCurrentRep;

    public static string exerciseName;

    public static int leftShoulderUp;
    public static int rightShoulderUp;
    public static int leftHipUp;
    public static int rightHipUp;
    public static int leaningLeft;
    public static int leaningRight;
    public static int outOfPath;

    public static bool isLeftArmSelected() {
        return leftArmSelected;
    }

    public static bool isRightArmSelected() {
        return !leftArmSelected;
    }

    public static void resetState()
    {
        tries = 0;
        correctReps = 0;
        isRestingTime = false;
        isTherapyOnGoing = false;
        compensationInCurrentRep = false;
        hasStartedExercise = false;
        hasFinishedExercise = false;
        hasSecondaryCursor = false;
        leftShoulderUp = 0;
        rightShoulderUp = 0;
        leftHipUp = 0;
        rightHipUp = 0;
        leaningLeft = 0;
        leaningRight = 0;
        outOfPath = 0;
        restCount = 0;
        setDuration = 60;
        restDuration = 60;
        maxReps = 10;
        maxSets = 3;

    }
}
