using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Shapes : IState
{
    // ------------------------------ Variáveis serializadas ------------------------------ //
    private Transform cursor;
    private GameObject[] new_shape;
    private string tagToLookFor;
    private Color color;
    // ------------------------------ Variáveis serializadas ------------------------------ //

    private int subState_index;
    private AudioClip beep;
    private bool shapeComplete;
    private GameObject currentShape;
    private float timer = 5f;
    private float poor_countMax;
    private bool sobe;
    private bool desce;

    public State_Shapes(Transform cursor, GameObject[] new_shape, string tagToLookFor, Color color)
    {
        this.cursor = cursor;
        this.new_shape = new_shape;
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

        DDA_Exercise_Grid.instance.nShapes = false;
        DDA_Exercise_Grid.instance.nTargets = false;
        DDA_Exercise_Grid.instance.nFreeDraw = false;
        Debug.Log ("Entrou Shapes");

        subState_index = 1;
        poor_countMax = 4;
        new_shape[0].SetActive(true);
        currentShape = this.new_shape[0];
        
        TargetOrderSet.instance.CreateArray(currentShape);
        TargetOrderSet.instance.SetOrder();
        
        Instantiate_target.instance.compCount = 0;

        if (State.maxReps <= 0)
        {
            State.maxReps = 20;
        }
        State.correctReps = 0;
        State.tries = 0;
    }

    public void Execute()
    {
        RaycastHit hit;
        Ray landingRay = new Ray(cursor.position, Vector3.back);
        
        if (State.isTherapyOnGoing)
        {
            if (currentShape = this.new_shape[0])
            {
                Instantiate_target.instance.levelDiff.text = "1";
            }
            else if (currentShape = this.new_shape[1])
            {
                Instantiate_target.instance.levelDiff.text = "2";
            }
            else if (currentShape = this.new_shape[2])
            {
                Instantiate_target.instance.levelDiff.text = "3";
            }
            else if (currentShape = this.new_shape[3])
            {
                Instantiate_target.instance.levelDiff.text = "4";
            }
            else if (currentShape = this.new_shape[4])
            {
                Instantiate_target.instance.levelDiff.text = "5";
            }
            else if (currentShape = this.new_shape[5])
            {
                Instantiate_target.instance.levelDiff.text = "6";
            }
            if (Physics.Raycast(landingRay, out hit))
            {                
                if (hit.collider.tag == this.tagToLookFor && hit.collider.gameObject.GetComponent<Renderer>().material.color == Color.blue)
                {                    
                    Debug.Log(Instantiate_target.instance.compCount);  
                    hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.green;
                    if(TargetOrderSet.instance.orderFlag < TargetOrderSet.instance.targetChildren.Length-1)
                    {
                        TargetOrderSet.instance.orderFlag++;
                        TargetOrderSet.instance.SetOrder();
                    }

                    if (Instantiate_target.instance.manualDiff_Shapes.isOn == true)
                    {                        
                        poor_countMax = Instantiate_target.instance.maxComp;
                    }
                    else
                    {
                        poor_countMax = 4f;
                    }                    
                }
                
                if (hit.collider.tag == "ExerciseCollider_0" && TargetOrderSet.instance.orderFlag == TargetOrderSet.instance.targetChildren.Length - 1)
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
                        currentShape = this.new_shape[5];
                    }             

                }
                else if (hit.collider.tag == "ExerciseCollider_1" && TargetOrderSet.instance.orderFlag == TargetOrderSet.instance.targetChildren.Length - 1)
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
                    
                }
                else if (hit.collider.tag == "ExerciseCollider_2" && TargetOrderSet.instance.orderFlag == TargetOrderSet.instance.targetChildren.Length - 1)
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
                    
                }
                else if (hit.collider.tag == "ExerciseCollider_3" && TargetOrderSet.instance.orderFlag == TargetOrderSet.instance.targetChildren.Length - 1)
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

                }
                else if (hit.collider.tag == "ExerciseCollider_4" && TargetOrderSet.instance.orderFlag == TargetOrderSet.instance.targetChildren.Length - 1)
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

                }
                else if (hit.collider.tag == "ExerciseCollider_5" && TargetOrderSet.instance.orderFlag == TargetOrderSet.instance.targetChildren.Length -1)
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
                Instantiate_target.instance.compCount = 0;
                TargetOrderSet.instance.CreateArray(currentShape);
                TargetOrderSet.instance.SetOrder();
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
                Instantiate_target.instance.compCount += 1;
                State.compensationInCurrentRep = false;                
                Instantiate_target.instance.CooldownTimer(5);                

                if (Instantiate_target.instance.compCount == poor_countMax)
                {          
                    
                    if (currentShape == this.new_shape[0])
                    {
                        Instantiate_target.instance.compCount = 0;
                        desce = true;
                        Exit();
                    }
                    else
                    {
                        if (subState_index > 1)
                        {
                            subState_index -= 1;
                            Instantiate_target.instance.compCount = 0;
                            currentShape.SetActive(false);

                            if (subState_index == 1)
                            {
                                this.new_shape[0].SetActive(true);
                                currentShape = this.new_shape[0];
                                TargetOrderSet.instance.CreateArray(currentShape);
                                TargetOrderSet.instance.SetOrder();
                            }
                            else if (subState_index == 2)
                            {
                                this.new_shape[1].SetActive(true);
                                currentShape = this.new_shape[1];
                                TargetOrderSet.instance.CreateArray(currentShape);
                                TargetOrderSet.instance.SetOrder();
                            }
                            else if (subState_index == 3)
                            {
                                this.new_shape[2].SetActive(true);
                                currentShape = this.new_shape[2];
                                TargetOrderSet.instance.CreateArray(currentShape);
                                TargetOrderSet.instance.SetOrder();
                            }
                            else if (subState_index == 4)
                            {
                                this.new_shape[3].SetActive(true);
                                currentShape = this.new_shape[3];
                                TargetOrderSet.instance.CreateArray(currentShape);
                                TargetOrderSet.instance.SetOrder();
                            }
                            else if (subState_index == 5)
                            {
                                this.new_shape[4].SetActive(true);
                                currentShape = this.new_shape[4];
                                TargetOrderSet.instance.CreateArray(currentShape);
                                TargetOrderSet.instance.SetOrder();
                            }
                            else if (subState_index == 6)
                            {
                                this.new_shape[5].SetActive(true);
                                currentShape = this.new_shape[5];
                                TargetOrderSet.instance.CreateArray(currentShape);
                                TargetOrderSet.instance.SetOrder();
                            }
                        }                        
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
        if (sobe == true)
        {
            currentShape.SetActive(false);
            DDA_Exercise_Grid.instance.nShapes = false;
            DDA_Exercise_Grid.instance.nTargets = true;
        }
        else if (desce == true)
        {
            currentShape.SetActive(false);
            DDA_Exercise_Grid.instance.nShapes = false;
            DDA_Exercise_Grid.instance.nFreeDraw = true;
        }
        
    }
}
