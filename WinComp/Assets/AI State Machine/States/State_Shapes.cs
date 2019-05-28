using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Shapes : IState
{
    //private int n_shapes;

    //private Vector3 originPoint;
    private Transform cursor;
    private AudioSource audio;
    private GameObject[] new_shape;
    //private GameObject ownerGameObject;
    private string tagToLookFor;
    private Color color;

    //public int poor_count;
    //public int exc_count;
    private int subState_index;
    private AudioClip beep;
    private DDA_Exercise_Grid Objects;
    public bool changeState;
    private bool shapeComplete;
    private GameObject currentShape;
    private float timer = 5f;
    private int poor_count;
    private bool sobe;
    private bool desce;

    public State_Shapes(/*Vector3 originPoint,*/ Transform cursor, AudioSource audio, GameObject[] new_shape, /*GameObject ownerGameObject,*/ string tagToLookFor, Color color)
    {
        
        //this.originPoint = originPoint;
        this.cursor = cursor;
        this.audio = audio;
        this.new_shape = new_shape;
        //this.ownerGameObject = ownerGameObject;
        //this.instantiateRadius = instantiateRadius;
        this.tagToLookFor = tagToLookFor;
        this.color = color;
    }

    void init()
    {
        beep = (AudioClip)Resources.Load("Sounds/beep");
    }

    public void Enter()
    {
        Debug.Log("Entrou Shapes");
        subState_index = 1;
        this.new_shape[0].SetActive(true);
        currentShape = this.new_shape[0];

      //originPoint = ownerGameObject.transform.position;
      //Instantiate_target.instance.InstantiateObject(new_shape[0], originPoint, Quaternion.identity);

        if (State.maxReps <= 0)
        {
            State.maxReps = 10;
        }
        //spawnRadius = 30;
        //personal_space = 2;
    }

    public void Execute()
    {
        RaycastHit hit;
        Ray landingRay = new Ray(cursor.position, Vector3.back);
        //originPoint = ownerGameObject.transform.position;
        
        if (State.isTherapyOnGoing)
        {             
            if (Physics.Raycast(landingRay, out hit))
            {                
                if (hit.collider.tag == this.tagToLookFor)
                {

                    //Debug.Log("colidiu");
                    //Debug.Log(new_shape);
                    //Debug.Log(originPoint);
                    //Debug.LogError(subState_index);                   
                    hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.green;                                      
                    audio.PlayOneShot(beep);

                }
                
                if (hit.collider.tag == "ExerciseCollider_0")
                {           
                    shapeComplete = true;
                    subState_index += 1;
                    this.new_shape[0].SetActive(false);
                    if (subState_index == 1) //Poor Sub-state
                    {
                        this.new_shape[0].SetActive(true);
                        currentShape = this.new_shape[0];
                    }
                    else if (subState_index == 2) //Avg Sub-state
                    {
                        this.new_shape[1].SetActive(true);
                        currentShape = this.new_shape[1];
                    }
                    else if (subState_index == 3) //Exc Sub-state
                    {
                        this.new_shape[2].SetActive(true);
                        currentShape = this.new_shape[2];
                    }
                    else if (subState_index == 4) //Exc Sub-state
                    {
                        this.new_shape[3].SetActive(true);
                        currentShape = this.new_shape[3];
                    }
                    else if (subState_index == 5) //Exc Sub-state
                    {
                        this.new_shape[4].SetActive(true);
                        currentShape = this.new_shape[4];
                    }
                    else if (subState_index == 6) //Exc Sub-state
                    {
                        this.new_shape[5].SetActive(true);
                        currentShape = this.new_shape[4];
                    }

                    //this.new_shape[subState_index - 1].SetActive(true);
                    //Debug.Log("Acabou quadrado");
                    //Debug.Log(subState_index);
                    //Debug.Log(shapeComplete);                    
                }
                else if (hit.collider.tag == "ExerciseCollider_1")
                {
                    shapeComplete = true;
                    subState_index += 1;
                    
                    this.new_shape[1].SetActive(false);
                    if (subState_index == 1) //Poor Sub-state
                    {
                        this.new_shape[0].SetActive(true);
                        currentShape = this.new_shape[0];
                    }
                    else if (subState_index == 2) //Avg Sub-state
                    {
                        this.new_shape[1].SetActive(true);
                        currentShape = this.new_shape[1];
                    }
                    else if (subState_index == 3) //Exc Sub-state
                    {
                        this.new_shape[2].SetActive(true);
                        currentShape = this.new_shape[2];
                    }
                    else if (subState_index == 4) //Exc Sub-state
                    {
                        this.new_shape[3].SetActive(true);
                        currentShape = this.new_shape[3];
                    }
                    else if (subState_index == 5) //Exc Sub-state
                    {
                        this.new_shape[4].SetActive(true);
                        currentShape = this.new_shape[4];
                    }
                    else if (subState_index == 6) //Exc Sub-state
                    {
                        this.new_shape[5].SetActive(true);
                        currentShape = this.new_shape[5];
                    }
                    //this.new_shape[subState_index - 1].SetActive(true);
                    //Debug.Log("Acabou triangulo");
                    //Debug.Log(subState_index);
                    //Debug.Log(shapeComplete);                    
                }
                else if (hit.collider.tag == "ExerciseCollider_2")
                {
                    shapeComplete = true;
                    subState_index += 1;
                    
                    this.new_shape[2].SetActive(false);
                    if (subState_index == 1) //Poor Sub-state
                    {
                        this.new_shape[0].SetActive(true);
                        currentShape = this.new_shape[0];
                    }
                    else if (subState_index == 2) //Avg Sub-state
                    {
                        this.new_shape[1].SetActive(true);
                        currentShape = this.new_shape[1];
                    }
                    else if (subState_index == 3) //Exc Sub-state
                    {
                        this.new_shape[2].SetActive(true);
                        currentShape = this.new_shape[2];
                    }
                    else if (subState_index == 4) //Exc Sub-state
                    {
                        this.new_shape[3].SetActive(true);
                        currentShape = this.new_shape[3];
                    }
                    else if (subState_index == 5) //Exc Sub-state
                    {
                        this.new_shape[4].SetActive(true);
                        currentShape = this.new_shape[4];
                    }
                    else if (subState_index == 6) //Exc Sub-state
                    {
                        this.new_shape[5].SetActive(true);
                        currentShape = this.new_shape[5];
                    }
                    //this.new_shape[subState_index - 1].SetActive(true);
                    //Debug.Log("Acabou círculo");
                    //Debug.Log(subState_index);
                    //Debug.Log(shapeComplete);                    
                }
                else if (hit.collider.tag == "ExerciseCollider_3")
                {
                    shapeComplete = true;
                    subState_index += 1;
                    
                    this.new_shape[3].SetActive(false);
                    if (subState_index == 1) //Poor Sub-state
                    {
                        this.new_shape[0].SetActive(true);
                        currentShape = this.new_shape[0];
                    }
                    else if (subState_index == 2) //Avg Sub-state
                    {
                        this.new_shape[1].SetActive(true);
                        currentShape = this.new_shape[1];
                    }
                    else if (subState_index == 3) //Exc Sub-state
                    {
                        this.new_shape[2].SetActive(true);
                        currentShape = this.new_shape[2];
                    }
                    else if (subState_index == 4) //Exc Sub-state
                    {
                        this.new_shape[3].SetActive(true);
                        currentShape = this.new_shape[3];
                    }
                    else if (subState_index == 5) //Exc Sub-state
                    {
                        this.new_shape[4].SetActive(true);
                        currentShape = this.new_shape[4];
                    }
                    else if (subState_index == 6) //Exc Sub-state
                    {
                        this.new_shape[5].SetActive(true);
                        currentShape = this.new_shape[5];
                    }
                    //this.new_shape[subState_index - 1].SetActive(true);
                    //Debug.Log("Acabou losango");
                    //Debug.Log(subState_index);
                    //Debug.Log(shapeComplete);  
                }
                else if (hit.collider.tag == "ExerciseCollider_4" )
                {
                    shapeComplete = true;
                    subState_index += 1;
                    
                    this.new_shape[4].SetActive(false);
                    if (subState_index == 1) //Poor Sub-state
                    {
                        this.new_shape[0].SetActive(true);
                        currentShape = this.new_shape[0];
                    }
                    else if (subState_index == 2) //Avg Sub-state
                    {
                        this.new_shape[1].SetActive(true);
                        currentShape = this.new_shape[1];
                    }
                    else if (subState_index == 3) //Exc Sub-state
                    {
                        this.new_shape[2].SetActive(true);
                        currentShape = this.new_shape[2];
                    }
                    else if (subState_index == 4) //Exc Sub-state
                    {
                        this.new_shape[3].SetActive(true);
                        currentShape = this.new_shape[3];
                    }
                    else if (subState_index == 5) //Exc Sub-state
                    {
                        this.new_shape[4].SetActive(true);
                        currentShape = this.new_shape[4];
                    }
                    else if (subState_index == 6) //Exc Sub-state
                    {
                        this.new_shape[5].SetActive(true);
                        currentShape = this.new_shape[5];
                    }
                    //this.new_shape[subState_index - 1].SetActive(true);
                    //Debug.Log("Acabou Trapézio");
                    //Debug.Log(subState_index);
                    //Debug.Log(shapeComplete);
                }
                else if (hit.collider.tag == "ExerciseCollider_5")
                {
                    subState_index += 1;
                    currentShape.SetActive(false);
                    sobe = true;
                    Exit();
                    Debug.Log("Acabou Estrela");
                    Debug.Log("muda de estado");                    
                }                
            }
            if (shapeComplete == true)
            {          
                State.correctReps+=1;
                State.tries+=1;
                float complete = State.correctReps;
                int minutes = (State.sessionTimeInt / State.correctReps) / 60;
                int seconds = (State.sessionTimeInt / State.correctReps) % 60;
                Instantiate_target.instance.avgTime.text = minutes.ToString("00") + ":" + seconds.ToString("00");

                // Preenchimento das radial bars ------------------------------------------//
                DDA_Exercise_Grid.instance.completion = complete / State.maxReps * 100f;

                if (State.tries > 0)
                {
                    DDA_Exercise_Grid.instance.correctness = complete / State.tries * 100f;
                    DDA_Exercise_Grid.instance.icorrectness = (int)DDA_Exercise_Grid.instance.correctness;
                    //Debug.LogError(icorrectness);
                }
                // Preenchimento das radial bars ------------------------------------------//      

                shapeComplete = false;
            }
            // Caso seja detetada uma compensação o nível é reduzido de novo
            if (State.compensationInCurrentRep && Instantiate_target.instance.cooldownTimer == 0)
            {                
                if (currentShape == this.new_shape[0])
                {
                    poor_count += 1;
                    if (poor_count == 3)
                    {
                        desce = true;
                        Exit();
                    }
                }
                
                State.compensationInCurrentRep = false;
                Instantiate_target.instance.CooldownTimer(5);
                if (subState_index > 1)
                {
                    subState_index -= 1;
                    currentShape.SetActive(false);

                    if (subState_index == 1)
                    {
                        this.new_shape[0].SetActive(true);
                        currentShape = this.new_shape[0];
                    }
                    else if (subState_index == 2)
                    {
                        this.new_shape[1].SetActive(true);
                        currentShape = this.new_shape[1];
                    }
                    else if (subState_index == 3)
                    {
                        this.new_shape[2].SetActive(true);
                        currentShape = this.new_shape[2];
                    }
                    else if (subState_index == 4)
                    {
                        this.new_shape[3].SetActive(true);
                        currentShape = this.new_shape[3];
                    }
                    else if (subState_index == 5)
                    {
                        this.new_shape[4].SetActive(true);
                        currentShape = this.new_shape[4];
                    }
                    else if (subState_index == 6)
                    {
                        this.new_shape[5].SetActive(true);
                        currentShape = this.new_shape[5];
                    }
                }
                if (State.correctReps > 0)
                {
                    State.correctReps -= 1;
                }
            }
        }
    }

    public void Exit()
    {
        /*if (sobe)
        {
            DDA_Exercise_Grid.instance.nSilhuetas = true
        }*/
        if (desce)
        {
            DDA_Exercise_Grid.instance.nTargets = true;
        }
    }
}
