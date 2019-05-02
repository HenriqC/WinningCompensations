using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatientController : MonoBehaviour {
    public Text sessionTimePatient;
    public Text correctRepetitionsPatient;
    public Text setTimePatient;
    public Text restTimePatient;
    public Text startCounterPatient;

    public Text sessionTimeTherapist;
    public Text correctRepetitionsTherapist;
    public Text setTimeTherapist;
    public Text restTimeTherapist;
    public Text triesTherapist;

    public GameObject restPatient;
    public GameObject restTherapist;
    public GameObject completed;

    public RadialCompletionBar completion;
    public AsyncOperation completionCalculation;

    private int sessionTimeInt;
    private int setTimeInt;
    private int restTimeInt;
    private int startCounterInt;
    public float completionPercentage = 0f;

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
        startCounterPatient.transform.gameObject.SetActive(true);
        if (startCounterInt <= 0) {
            initSessionTime();
            initSetTimer();
            startCounterPatient.transform.gameObject.SetActive(false);
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

    private void initSetTimer() {
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

    private void activateRest() {
        restPatient.SetActive(true);
        restTherapist.SetActive(true);
        State.isTherapyOnGoing = false;
        State.restCount++;
        initRestTimer();
    }

    private void activateSet() {
        restPatient.SetActive(false);
        restTherapist.SetActive(false);
        State.isTherapyOnGoing = true;
        initSetTimer();
    }

    private void setTimeDec() {
        if (setTimeInt == 0) {
            CancelInvoke("setTimeDec");
            activateRest();
            return;
        }

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
        completion.OnChange(this.OnRadialBarChange);
        completion.OnDone(this.OnRadialBarDone);
        StartCoroutine(this.CalculateCompletion(completionCalculation));
    }

    // Update is called once per frame
    void Update() {

        if (State.hasFinishedExercise) {
            startCounterPatient.transform.gameObject.SetActive(false);
            CancelInvoke("countDown");
            CancelInvoke("restTimeDec");
            CancelInvoke("setTimeDec");
            CancelInvoke("sessionTimeInc"); 
        }
        completed.SetActive(State.hasFinishedExercise);

        correctRepetitionsPatient.text = "" + State.correctReps;
        correctRepetitionsTherapist.text = "" + State.correctReps;
        triesTherapist.text = "" + State.tries;        
    }

    public void CompletionCalculation(float result)
    {
        if (!State.hasFinishedExercise && completionPercentage <= 100f)
        {
            completionPercentage = ((State.correctReps / State.maxReps) * 100);
        }
        result = completionPercentage;
        return;
    }

    void OnRadialBarChange (float value)
    {
        print("Radial completion is : " + value);
    }

    void OnRadialBarDone()
    {
        print("Radial completion is done");
    }

    IEnumerator CalculateCompletion(object completionCalculation)
    {
        completionCalculation = CalculateCompletion(this.completionPercentage);

        while (!completionCalculation.Equals(100f))
        {
            completion.SetValue(completionPercentage / 0.9f);
            yield return null;
        }
    }
}
