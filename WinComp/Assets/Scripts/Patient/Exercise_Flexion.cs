using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exercise_Flexion : MonoBehaviour
{

    public GameObject cursor;
    public bool hasSecondaryCursor;

    public Text avgTime;

    public string exerciseName;


    private GameObject exerciseBoxGroup;

    public GameObject leftExerciseBox;
    public GameObject leftExerciseBox0;
    public GameObject leftExerciseBox1;
    public GameObject leftExerciseBox2;
    public GameObject leftExerciseBox3;

    public GameObject rightExerciseBox;
    public GameObject rightExerciseBox0;
    public GameObject rightExerciseBox1;
    public GameObject rightExerciseBox2;
    public GameObject rightExerciseBox3;


    public GameObject targets;
    public GameObject leftTargets;
    public GameObject leftTarget0;
    public GameObject leftTarget20;
    public GameObject leftTarget50;
    public GameObject leftTarget80;
    public GameObject leftTarget100;

    public GameObject rightTargets;
    public GameObject rightTarget0;
    public GameObject rightTarget20;
    public GameObject rightTarget50;
    public GameObject rightTarget80;
    public GameObject rightTarget100;

    public float completion;
    public float correctness;
    private AudioClip beep;
    private AudioSource audioSource;

    private bool reversePath;

    private bool isGroing;

    //private bool hasRegisteredOutOfPath;
    private bool isBlinking;

    private int repCounter;

    // Use this for initialization
    void Start()
    {
    }

    void init()
    {
        if (State.maxReps <= 0)
        {
            State.maxReps = 10;
        }

        leftExerciseBox.SetActive(false);
        leftExerciseBox0.SetActive(false);
        leftExerciseBox1.SetActive(false);
        leftExerciseBox2.SetActive(false);
        leftExerciseBox3.SetActive(false);

        rightExerciseBox.SetActive(false);
        rightExerciseBox0.SetActive(false);
        rightExerciseBox1.SetActive(false);
        rightExerciseBox2.SetActive(false);
        rightExerciseBox3.SetActive(false);

        rightTargets.SetActive(false);
        rightTarget0.SetActive(false);
        rightTarget20.SetActive(false);
        rightTarget50.SetActive(false);
        rightTarget80.SetActive(false);
        rightTarget100.SetActive(false);

        leftTargets.SetActive(false);
        leftTarget0.SetActive(false);
        leftTarget20.SetActive(false);
        leftTarget50.SetActive(false);
        leftTarget80.SetActive(false);
        leftTarget100.SetActive(false);

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

    private void OnEnable()
    {
        init();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void setArea()
    {
        if (State.isLeftArmSelected())
        {

            targets = leftTargets;
            leftTargets.SetActive(true);
            leftTarget0.SetActive(true);
            leftTarget20.SetActive(true);

            exerciseBoxGroup = leftExerciseBox;
            leftExerciseBox.SetActive(true);
            leftExerciseBox0.SetActive(true);

        }
        else if (State.isRightArmSelected())
        {
            targets = rightTargets;
            rightTargets.SetActive(true);
            rightTarget0.SetActive(true);
            rightTarget20.SetActive(true);

            exerciseBoxGroup = rightExerciseBox;
            rightExerciseBox.SetActive(true);
            rightExerciseBox0.SetActive(true);
            
        }
    }

    /* 
    private void activate(bool left)
    {

        targets.SetActive(targetgrid);

        leftTargets.SetActive(left);
        //leftExerciseBox.SetActive(left);
        rightTargets.SetActive(!left);
        //rightExerciseBox.SetActive(!left);
        
    } 
    
    */

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray landingRay = new Ray(cursor.transform.position, Vector3.back);


        if (State.isTherapyOnGoing)
        {
            if (!isBlinking)
            {
                InvokeRepeating("blinkTarget", 0, 0.05f);
                isBlinking = true;
            }

            if (Physics.Raycast(landingRay, out hit))
            {

                // ------------------------------- Regista se o doente está dentro do caminho --------------------------------// 

                /*hasRegisteredOutOfPath = false;
                if (hit.collider.tag == "ExerciseCollider") { // is inside of the exercise area
                    if (State.hasStartedExercise) {
                        foreach (Transform exerciseBox in exerciseBoxGroup.transform) {
                            exerciseBox.gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 0.3f);
                        }
                    }
            }*/

                // -----------------------------------------------------------------------------------------------------------//

                if (hit.collider.tag == "TargetCollider")
                { // has hit a target
                    bool hasHitTheCurrentTarget = hit.transform.position == targets.transform.GetChild(State.currentTarget).transform.position;

                    // Esta secção faz o preenchimento das barras radiais de acordo com correção e progressão do ex
                    float complete = State.correctReps;

                    completion = complete / State.maxReps * 100f;

                    if (State.tries > 0)
                    {
                        correctness = complete / State.tries * 100f;
                        int icorrectness = (int)correctness;
                        //Debug.LogError(icorrectness);
                    }

                    /*Debug.LogError(State.correctReps);
                    Debug.LogError(State.maxReps);                            
                    Debug.LogError(completion);*/


                    if (hasHitTheCurrentTarget)
                    {
                        if (!State.hasStartedExercise)
                        {
                            State.hasStartedExercise = true;
                        }

                        // Grau de dificuldade elevado // - Pensar em como implementar a dificuldade adaptativa a partir daqui

                        // Neste "if" o código ativa o caminho inverso da repetição assim que o cursor toca num target com índice menor que o anterior

                        targets.transform.GetChild(State.currentTarget).gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
                        if (State.currentTarget == (targets.transform.childCount - 1))
                        { // end of one correct repetition, need to start from the first target again
                            reversePath = true;
                            State.currentTarget--;
                            State.correctReps++;
                            State.tries++;                           
                        }

                        // Neste "elseif" ele procura se o caminho inverso já foi ativo e o cursor já voltou ao primeiro target. Se sim, incrementa o número de reps corretas (sem compensações)
                        else if (reversePath && State.currentTarget == 0)
                        {
                            State.currentTarget = 0;
                            State.tries++;
                            State.correctReps++;
                            reversePath = false;

                            int minutes = (State.sessionTimeInt / State.correctReps) / 60;
                            int seconds = (State.sessionTimeInt / State.correctReps) % 60;

                            avgTime.text = minutes.ToString("00") + ":" + seconds.ToString("00");

                            if (State.correctReps >= State.maxReps)
                            { // done all the needed reps
                                State.hasFinishedExercise = true;
                            }
                        }
                        else if (!reversePath)
                        {
                            State.currentTarget++;
                        }
                        else if (reversePath)
                        {
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

            if (State.compensationInCurrentRep)
            { // Caso seja detetada uma compensação o exercício recomeça do início
                State.compensationInCurrentRep = false;
                reversePath = false;
                if (State.currentTarget != 0)
                {
                    targets.transform.GetChild(State.currentTarget).gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
                }
                State.currentTarget = 0;
            }
        }
    }

    private void blinkTarget()
    {
        if (targets.transform.childCount == 0)
            return;

        Renderer renderer = null;
        int target = State.currentTarget;
        Debug.Log(target);
        while (renderer == null)
        {
            Renderer rendererTemp = targets.transform.GetChild(target).gameObject.GetComponent<Renderer>();

            if (rendererTemp.enabled)
            {
                renderer = rendererTemp;
            }
            else if (reversePath)
            {
                target--;
            }
            else
            {
                target++;
            }
        }

        float delta;

        if (isGroing && renderer.material.color.r > 1)
        {
            isGroing = false;
        }
        else if (!isGroing && renderer.material.color.r < 0)
        {
            isGroing = true;
        }

        if (isGroing)
        {
            delta = 0.1f;
        }
        else
        {
            delta = -0.1f;
        }

        Color color = renderer.material.color;
        color.r += delta;
        color.b += delta;

        renderer.material.color = color;
    }
}
