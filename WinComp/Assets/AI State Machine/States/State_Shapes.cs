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
    private bool terminou;

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
        Instantiate_target.instance.radialTarget_param.SetActive(false);
        Instantiate_target.instance.linearDraw_param.SetActive(false);
        Instantiate_target.instance.shapes_param.SetActive(true);

        Instantiate_target.instance.MaxRepTimer(30);
        Instantiate_target.instance.levelDiff.text = "1";
        State.exerciseName = "Shapes";
        //CognitiveSphereSpawner.instance.stopSpawning_B = true;
        CognitiveSphereSpawner.instance.stopSpawning_P = true;        

        DDA_Exercise_Grid.instance.nShapes = false;
        DDA_Exercise_Grid.instance.nTargets = false;
        DDA_Exercise_Grid.instance.nFreeDraw = false;
        Debug.Log ("Entrou Shapes");

        subState_index = 0;
        poor_countMax = 4;
        new_shape = new GameObject[Create_Arrays.instance.selectedShapes.Length];
        
        //TargetOrderSet.instance.CreateArray(currentShape);
        //TargetOrderSet.instance.SetOrder();
        
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
            if (Create_Arrays.instance.setPressed == true)
            {
                Create_Arrays.instance.setPressed = false;
                new_shape = new GameObject[Create_Arrays.instance.selectedShapes.Length];
                for (int j = 0; j < Create_Arrays.instance.selectedShapes.Length; j++)
                {
                    new_shape[j] = Create_Arrays.instance.selectedShapes[j];
                }

                new_shape[0].SetActive(true);
                currentShape = new_shape[0];
                TargetOrderSet.instance.CreateArray(currentShape);
                TargetOrderSet.instance.SetOrder();
            }

            if (Create_Arrays.instance.resetPressed == true)
            {
                State.correctReps = 0;
                State.tries = 0;
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
                }
                else if (hit.collider.tag == "ExerciseCollider_1" && TargetOrderSet.instance.orderFlag == TargetOrderSet.instance.targetChildren.Length - 1)
                {
                    shapeComplete = true;                    
                }
                else if (hit.collider.tag == "ExerciseCollider_2" && TargetOrderSet.instance.orderFlag == TargetOrderSet.instance.targetChildren.Length - 1)
                {
                    shapeComplete = true;                    
                }
                else if (hit.collider.tag == "ExerciseCollider_3" && TargetOrderSet.instance.orderFlag == TargetOrderSet.instance.targetChildren.Length - 1)
                {
                    shapeComplete = true;
                }
                else if (hit.collider.tag == "ExerciseCollider_4" && TargetOrderSet.instance.orderFlag == TargetOrderSet.instance.targetChildren.Length - 1)
                {
                    shapeComplete = true;
                }
                else if (hit.collider.tag == "ExerciseCollider_5" && TargetOrderSet.instance.orderFlag == TargetOrderSet.instance.targetChildren.Length -1)
                {
                    // Aqui é onde vou mudar para só sair deste ex quando o nr de sets feitos for igual ao nr de sets definidos pelo FT ------------------------ //
                    if (State.correctReps == State.maxReps)
                    {
                        terminou = true;
                        Exit();
                    }
                    else
                    {
                        shapeComplete = true;
                    }                                        
                }                
            }

            if (Instantiate_target.instance.maxTimer == 0)
            {
                currentShape.SetActive(false);

                if (subState_index > 0)
                {
                    subState_index --;
                }
                else
                {
                    subState_index = 0;
                }
                Instantiate_target.instance.MaxRepTimer(15);

                new_shape[subState_index].SetActive(true);
                currentShape = new_shape[subState_index];
                TargetOrderSet.instance.CreateArray(currentShape);
                TargetOrderSet.instance.SetOrder();
                Instantiate_target.instance.levelDiff.text = "" + (subState_index + 1);
            }

            if (shapeComplete == true)
            {
                currentShape.SetActive(false);

                if (subState_index < new_shape.Length - 1)
                {
                    subState_index++;
                }
                else
                {
                    subState_index = 0;
                }
                Debug.LogWarning(subState_index);
                new_shape[subState_index].SetActive(true);
                currentShape = new_shape[subState_index];
                TargetOrderSet.instance.CreateArray(currentShape);
                TargetOrderSet.instance.SetOrder();

                Instantiate_target.instance.levelDiff.text = "" + (subState_index + 1);
                Instantiate_target.instance.MaxRepTimer(10);
                Instantiate_target.instance.PlayClip();
                Instantiate_target.instance.compCount = 0;

                State.tries += 1;
                State.correctReps+=1;
                
                
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
                Instantiate_target.instance.CooldownTimer(3);                

                if (Instantiate_target.instance.compCount == poor_countMax)
                {          
                    
                    if (currentShape == new_shape[0])
                    {
                        Instantiate_target.instance.compCount = 0;
                        desce = true;
                        Exit();
                    }
                    else
                    {
                        currentShape.SetActive(false);                     

                        if (subState_index > 0)
                        {
                            subState_index --;
                            
                        }
                        else
                        {
                            subState_index = 0;
                        }

                        Instantiate_target.instance.compCount = 0;
                        new_shape[subState_index].SetActive(true);
                        currentShape = new_shape[subState_index];
                        TargetOrderSet.instance.CreateArray(currentShape);
                        TargetOrderSet.instance.SetOrder();
                        Instantiate_target.instance.levelDiff.text = "" + (subState_index + 1);
                    }
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
        else if (terminou == true)
        {
            currentShape.SetActive(false);
            DDA_Exercise_Grid.instance.nShapes = false;
            DDA_Exercise_Grid.instance.nTargets = true;
        }
        
    }
}
