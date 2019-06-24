using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Shapes : IState
{
    //private int n_shapes;

    //private Vector3 originPoint;
    private Transform cursor;
    private GameObject[] new_shape;
    //private GameObject ownerGameObject;
    private string tagToLookFor;
    private Color color;

    //public int poor_count;
    //public int exc_count;
    private int subState_index;
    private AudioClip beep;
    private DDA_Exercise_Grid Objects;
    private bool shapeComplete;
    private GameObject currentShape;
    private float timer = 5f;
    private int poor_count;
    private bool sobe;
    private bool desce;

    public State_Shapes(/*Vector3 originPoint,*/ Transform cursor, GameObject[] new_shape, /*GameObject ownerGameObject,*/ string tagToLookFor, Color color)
    {
        
        //this.originPoint = originPoint;
        this.cursor = cursor;
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
        State.exerciseName = "Shape Fill";
        //CognitiveSphereSpawner.instance.stopSpawning_B = true;
        CognitiveSphereSpawner.instance.stopSpawning_P = true;
        var remTargets = GameObject.FindGameObjectsWithTag("TargetCollider");
        //var blueSphere = GameObject.FindGameObjectsWithTag("CognitiveCollider_B");
        var purpleSphere = GameObject.FindGameObjectsWithTag("CognitiveCollider_P");

        foreach (GameObject rem in remTargets)
        {
            Object.Destroy(rem);
        }

        /*foreach (GameObject target_b in blueSphere)
        {
            Object.Destroy(target_b);
        }*/

        foreach (GameObject target_p in purpleSphere)
        {
            Object.Destroy(target_p);
        }

        DDA_Exercise_Grid.instance.nShapes = false;
        DDA_Exercise_Grid.instance.nTargets = false;
        Debug.Log ("Entrou Shapes");

        subState_index = 1;
        new_shape[0].SetActive(true);
        currentShape = this.new_shape[0];

      //originPoint = ownerGameObject.transform.position;
      //Instantiate_target.instance.InstantiateObject(new_shape[0], originPoint, Quaternion.identity);

        if (State.maxReps <= 0)
        {
            State.maxReps = 20;
        }
        
    }

    public void Execute()
    {
        RaycastHit hit;
        Ray landingRay = new Ray(cursor.position, Vector3.back);
        //originPoint = ownerGameObject.transform.position;
        
        if (State.isTherapyOnGoing)
        {
            TargetOrderSet.instance.SetOrder();
            if (Physics.Raycast(landingRay, out hit))
            {                
                if (hit.collider.tag == this.tagToLookFor && hit.collider.gameObject.GetComponent<Renderer>().material.color == Color.blue)
                {

                    //Debug.Log("colidiu");
                    //Debug.Log(new_shape);
                    //Debug.Log(originPoint);
                    //Debug.LogError(subState_index);                   
                    hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.green;
                    if(TargetOrderSet.instance.orderFlag < TargetOrderSet.instance.targetChildren.Length-1)
                    {
                        TargetOrderSet.instance.orderFlag++;
                    }                        
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
                    //sobe = true;
                    desce = true;
                    Exit();
                    Debug.Log("Acabou Estrela");
                    Debug.Log("muda de estado");                    
                }                
            }
            if (shapeComplete == true)
            {
                Instantiate_target.instance.PlayClip();
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
        currentShape.SetActive(false);
        /*if (sobe)
        {
            DDA_Exercise_Grid.instance.nFreeDraw = true;
        }*/
        if (desce)
        {
            DDA_Exercise_Grid.instance.nShapes = false;
            DDA_Exercise_Grid.instance.nTargets = true;
        }
    }
}
