using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_FreeDraw : IState
{
    // ------------------------------ Variáveis serializadas ------------------------------ //
    //private Vector3 originPoint;
    private Transform cursor_fd;
    private GameObject[] new_shapeVert_fd;
    private GameObject[] new_shapeHoriz_fd;
    //private GameObject ownerGameObject;
    private string tagToLookFor_fd;
    private Color color_fd;
    // ------------------------------ Fim das variáveis serializadas ------------------------------ //

    private int subState_index_V;
    private int subState_index_H;
    private int MaxReps;
    private AudioClip beep;
    private GameObject currentShape_fd;
    private bool shapeComplete;
    private bool sobe;
    private bool desce;
    private float timer = 5f;
    private float poor_countMaxFD;

    public State_FreeDraw( Transform cursor_fd, GameObject[] new_shape_Vert_fd, GameObject[] new_shape_Horiz_fd, string tagToLookFor_fd, Color color_fd)
    {

        //this.originPoint = originPoint;
        this.cursor_fd = cursor_fd;
        this.new_shapeVert_fd = new_shape_Vert_fd;
        this.new_shapeHoriz_fd = new_shape_Horiz_fd;
        //this.ownerGameObject = ownerGameObject;
        //this.instantiateRadius = instantiateRadius;
        this.tagToLookFor_fd = tagToLookFor_fd;
        this.color_fd = color_fd;
    }
    void init()
    {
        beep = (AudioClip)Resources.Load("Sounds/beep");
    }    

    public void Enter()
    {
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

        CognitiveSphereSpawner.instance.stopSpawning_P = true;
        State.exerciseName = "Free Draw";
        DDA_Exercise_Grid.instance.nShapes = false;
        DDA_Exercise_Grid.instance.nTargets = false;
        DDA_Exercise_Grid.instance.nFreeDraw = false;
        Debug.Log("Entrou FreeDraw");

        
        subState_index_V = 1;
        subState_index_H = 1;
        poor_countMaxFD = 4;
        
        // ------------------------------------------------------------------------------------- //
        if (Instantiate_target.instance.horizontalFD.isOn == true)
        {
            new_shapeHoriz_fd[0].SetActive(true);
            currentShape_fd = this.new_shapeHoriz_fd[0];
            TargetOrderSet.instance.CreateArray(currentShape_fd);
            TargetOrderSet.instance.SetOrder();
        }
        else if (Instantiate_target.instance.verticalFD.isOn == true)
        {
            if (State.leftArmSelected)
            {
                new_shapeVert_fd[3].SetActive(true);
                currentShape_fd = this.new_shapeVert_fd[3];
                TargetOrderSet.instance.CreateArray(currentShape_fd);
                TargetOrderSet.instance.SetOrder();
            }
            else if (!State.leftArmSelected)
            {
                new_shapeVert_fd[2].SetActive(true);
                currentShape_fd = this.new_shapeVert_fd[2];
                TargetOrderSet.instance.CreateArray(currentShape_fd);
                TargetOrderSet.instance.SetOrder();
            }
        }
        // ------------------------------------------------------------------------------------- //

        Instantiate_target.instance.compCount = 0;

        if (State.maxReps <= 0)
        {
            State.maxReps = 20;
        }
    }

    public void Execute()
    {
        RaycastHit hit;
        Ray landingRay = new Ray(cursor_fd.position, Vector3.back);

        if (State.isTherapyOnGoing)
        {
            if (Physics.Raycast(landingRay, out hit))
            {
                if (hit.collider.tag == this.tagToLookFor_fd && hit.collider.gameObject.GetComponent<Renderer>().material.color == Color.blue)
                {
                    Debug.Log(Instantiate_target.instance.compCount);
                    hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.green;

                    if (TargetOrderSet.instance.orderFlag < TargetOrderSet.instance.targetChildren.Length - 1)
                    {
                        TargetOrderSet.instance.orderFlag++;
                        TargetOrderSet.instance.SetOrder();
                    }

                    // O Free Draw usa o toggle de tolerância de compensações manual do exercício das shapes

                    if (Instantiate_target.instance.manualDiff_Shapes.isOn)
                    {
                        poor_countMaxFD = Instantiate_target.instance.maxComp;
                    }
                    else
                    {
                        poor_countMaxFD = 4f;
                    }
                    // ------------------------------------------------------------------------------------- //                    
                }
                if (Instantiate_target.instance.verticalFD && !State.leftArmSelected)
                {
                    // -------------------------------------- Verticais Direita ----------------------------------------------- //
                    if (hit.collider.tag == "ExerciseCollider_V2R" && TargetOrderSet.instance.orderFlag == TargetOrderSet.instance.targetChildren.Length - 1)
                    {
                        shapeComplete = true;
                        subState_index_V += 1;
                        currentShape_fd.SetActive(false);

                        if (subState_index_V == 1) //Poor Sub-state
                        {
                            this.new_shapeVert_fd[2].SetActive(true);
                            currentShape_fd = this.new_shapeVert_fd[2];
                        }
                        else if (subState_index_V == 2) //Avg Sub-state
                        {
                            this.new_shapeVert_fd[1].SetActive(true);
                            currentShape_fd = this.new_shapeVert_fd[1];
                        }
                        else if (subState_index_V == 3) //Exc Sub-state
                        {
                            this.new_shapeVert_fd[0].SetActive(true);
                            currentShape_fd = this.new_shapeVert_fd[0];
                        }
                    }
                    if (hit.collider.tag == "ExerciseCollider_V3R" && TargetOrderSet.instance.orderFlag == TargetOrderSet.instance.targetChildren.Length - 1)
                    {
                        shapeComplete = true;
                        subState_index_V += 1;
                        currentShape_fd.SetActive(false);

                        if (subState_index_V == 1) //Poor Sub-state
                        {
                            this.new_shapeVert_fd[2].SetActive(true);
                            currentShape_fd = this.new_shapeVert_fd[2];
                        }
                        else if (subState_index_V == 2) //Avg Sub-state
                        {
                            this.new_shapeVert_fd[1].SetActive(true);
                            currentShape_fd = this.new_shapeVert_fd[1];
                        }
                        else if (subState_index_V == 3) //Exc Sub-state
                        {
                            this.new_shapeVert_fd[0].SetActive(true);
                            currentShape_fd = this.new_shapeVert_fd[0];
                        }
                    }
                    if (hit.collider.tag == "ExerciseCollider_V4R" && TargetOrderSet.instance.orderFlag == TargetOrderSet.instance.targetChildren.Length - 1)
                    {
                        shapeComplete = true;
                        subState_index_V = 1;
                        currentShape_fd.SetActive(false);
                        new_shapeVert_fd[2].SetActive(true);
                        currentShape_fd = new_shapeVert_fd[2];
                    }
                }
                // -------------------------------------- Fim Verticais Direita ----------------------------------------------- //
                else if (Instantiate_target.instance.verticalFD && State.leftArmSelected)
                {
                    // -------------------------------------- Verticais Esquerda ----------------------------------------------- //
                    if (hit.collider.tag == "ExerciseCollider_V2L" && TargetOrderSet.instance.orderFlag == TargetOrderSet.instance.targetChildren.Length - 1)
                    {
                        shapeComplete = true;
                        subState_index_V += 1;
                        currentShape_fd.SetActive(false);

                        if (subState_index_V == 1) //Poor Sub-state
                        {
                            this.new_shapeVert_fd[5].SetActive(true);
                            currentShape_fd = this.new_shapeVert_fd[5];
                        }
                        else if (subState_index_V == 2) //Avg Sub-state
                        {
                            this.new_shapeVert_fd[4].SetActive(true);
                            currentShape_fd = this.new_shapeVert_fd[4];
                        }
                        else if (subState_index_V == 3) //Exc Sub-state
                        {
                            this.new_shapeVert_fd[3].SetActive(true);
                            currentShape_fd = this.new_shapeVert_fd[3];
                        }
                    }
                    if (hit.collider.tag == "ExerciseCollider_V3L" && TargetOrderSet.instance.orderFlag == TargetOrderSet.instance.targetChildren.Length - 1)
                    {
                        shapeComplete = true;
                        subState_index_V += 1;
                        currentShape_fd.SetActive(false);

                        if (subState_index_V == 1) //Poor Sub-state
                        {
                            this.new_shapeVert_fd[5].SetActive(true);
                            currentShape_fd = this.new_shapeVert_fd[5];
                        }
                        else if (subState_index_V == 2) //Avg Sub-state
                        {
                            this.new_shapeVert_fd[4].SetActive(true);
                            currentShape_fd = this.new_shapeVert_fd[4];
                        }
                        else if (subState_index_V == 3) //Exc Sub-state
                        {
                            this.new_shapeVert_fd[3].SetActive(true);
                            currentShape_fd = this.new_shapeVert_fd[3];
                        }
                    }
                    if (hit.collider.tag == "ExerciseCollider_V4L" && TargetOrderSet.instance.orderFlag == TargetOrderSet.instance.targetChildren.Length - 1)
                    {
                        shapeComplete = true;
                        subState_index_V = 1;
                        currentShape_fd.SetActive(false);
                        new_shapeVert_fd[5].SetActive(true);
                        currentShape_fd = new_shapeVert_fd[5];
                    }
                    // -------------------------------------- Fim Verticais Esquerda ----------------------------------------------- //
                }
                else if (Instantiate_target.instance.horizontalFD)
                {
                    // -------------------------------------- Horizontais ----------------------------------------------- //
                    if (hit.collider.tag == "ExerciseCollider_Hlow" && TargetOrderSet.instance.orderFlag == TargetOrderSet.instance.targetChildren.Length - 1)
                    {
                        shapeComplete = true;
                        subState_index_H += 1;
                        currentShape_fd.SetActive(false);

                        if (subState_index_H == 1) //Poor Sub-state
                        {
                            this.new_shapeHoriz_fd[2].SetActive(true);
                            currentShape_fd = this.new_shapeHoriz_fd[2];
                        }
                        else if (subState_index_H == 2) //Avg Sub-state
                        {
                            this.new_shapeHoriz_fd[1].SetActive(true);
                            currentShape_fd = this.new_shapeHoriz_fd[1];
                        }
                        else if (subState_index_H == 3) //Exc Sub-state
                        {
                            this.new_shapeHoriz_fd[0].SetActive(true);
                            currentShape_fd = this.new_shapeHoriz_fd[0];
                        }
                    }
                    if (hit.collider.tag == "ExerciseCollider_Hmid" && TargetOrderSet.instance.orderFlag == TargetOrderSet.instance.targetChildren.Length - 1)
                    {
                        shapeComplete = true;
                        subState_index_H += 1;
                        currentShape_fd.SetActive(false);

                        if (subState_index_H == 1) //Poor Sub-state
                        {
                            this.new_shapeHoriz_fd[0].SetActive(true);
                            currentShape_fd = this.new_shapeHoriz_fd[0];
                        }
                        else if (subState_index_H == 2) //Avg Sub-state
                        {
                            this.new_shapeHoriz_fd[1].SetActive(true);
                            currentShape_fd = this.new_shapeHoriz_fd[1];
                        }
                        else if (subState_index_H == 3) //Exc Sub-state
                        {
                            this.new_shapeHoriz_fd[2].SetActive(true);
                            currentShape_fd = this.new_shapeHoriz_fd[2];
                        }
                    }
                    if (hit.collider.tag == "ExerciseCollider_Hhigh" && TargetOrderSet.instance.orderFlag == TargetOrderSet.instance.targetChildren.Length - 1)
                    {
                        shapeComplete = true;
                        currentShape_fd.SetActive(false);
                        subState_index_H = 1;
                        this.new_shapeHoriz_fd[0].SetActive(true);
                        currentShape_fd = this.new_shapeHoriz_fd[0];

                        /*if (subState_index_H == 1) //Poor Sub-state
                        {

                        }
                        else if (subState_index_H == 2) //Avg Sub-state
                        {
                            this.new_shapeHoriz_fd[1].SetActive(true);
                            currentShape_fd = this.new_shapeHoriz_fd[1];
                        }
                        else if (subState_index_H == 3) //Exc Sub-state
                        {
                            this.new_shapeHoriz_fd[0].SetActive(true);
                            currentShape_fd = this.new_shapeHoriz_fd[0];
                        }*/

                    }
                }
                        // -------------------------------------- Fim Horizontais ----------------------------------------------- //
            }
            if (shapeComplete == true)
            {
                Instantiate_target.instance.compCount = 0;
                TargetOrderSet.instance.CreateArray(currentShape_fd);
                TargetOrderSet.instance.SetOrder();
                Instantiate_target.instance.PlayClip();

                State.correctReps += 1;
                if (State.correctReps == State.maxReps)
                {
                    sobe = true;
                    Exit();
                }

                State.tries += 1;
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

            // -------------------------------------------------- Compensações -------------------------------------------------- //

            // Caso seja detetada uma compensação o nível é reduzido de novo
            if (State.compensationInCurrentRep && Instantiate_target.instance.cooldownTimer == 0)
            {
                Instantiate_target.instance.compCount += 1;
                State.compensationInCurrentRep = false;
                Instantiate_target.instance.CooldownTimer(5);

                if (Instantiate_target.instance.compCount == poor_countMaxFD)
                {
                    if (currentShape_fd == this.new_shapeVert_fd[2] || currentShape_fd == this.new_shapeVert_fd[5] || currentShape_fd == this.new_shapeHoriz_fd[0])
                    {
                        Instantiate_target.instance.compCount = 0;
                        desce = true;                        
                        Exit();
                    }
                    else
                    {
                        if (Instantiate_target.instance.horizontalFD.isOn == true)
                        {
                            if (subState_index_H > 1)
                            {
                                subState_index_H -= 1;
                                Instantiate_target.instance.compCount = 0;
                                currentShape_fd.SetActive(false);
                                if (subState_index_H == 1)
                                {
                                    this.new_shapeHoriz_fd[0].SetActive(true);
                                    currentShape_fd = this.new_shapeHoriz_fd[0];
                                    TargetOrderSet.instance.CreateArray(currentShape_fd);
                                    TargetOrderSet.instance.SetOrder();
                                }
                                else if (subState_index_H == 2)
                                {
                                    this.new_shapeHoriz_fd[1].SetActive(true);
                                    currentShape_fd = this.new_shapeHoriz_fd[1];
                                    TargetOrderSet.instance.CreateArray(currentShape_fd);
                                    TargetOrderSet.instance.SetOrder();
                                }
                                else if (subState_index_H == 3)
                                {
                                    this.new_shapeHoriz_fd[2].SetActive(true);
                                    currentShape_fd = this.new_shapeHoriz_fd[2];
                                    TargetOrderSet.instance.CreateArray(currentShape_fd);
                                    TargetOrderSet.instance.SetOrder();
                                }
                            }                            
                        }
                        else if (Instantiate_target.instance.verticalFD.isOn == true)
                        {
                            if (!State.leftArmSelected)
                            {
                                if (subState_index_V > 1)
                                {
                                    subState_index_V -= 1;
                                    Instantiate_target.instance.compCount = 0;
                                    currentShape_fd.SetActive(false);

                                    if (subState_index_V == 1)
                                    {
                                        this.new_shapeVert_fd[2].SetActive(true);
                                        currentShape_fd = this.new_shapeVert_fd[2];
                                        TargetOrderSet.instance.CreateArray(currentShape_fd);
                                        TargetOrderSet.instance.SetOrder();
                                    }
                                    else if (subState_index_V == 2)
                                    {
                                        this.new_shapeVert_fd[1].SetActive(true);
                                        currentShape_fd = this.new_shapeVert_fd[1];
                                        TargetOrderSet.instance.CreateArray(currentShape_fd);
                                        TargetOrderSet.instance.SetOrder();
                                    }
                                    else if (subState_index_V == 3)
                                    {
                                        this.new_shapeVert_fd[0].SetActive(true);
                                        currentShape_fd = this.new_shapeVert_fd[0];
                                        TargetOrderSet.instance.CreateArray(currentShape_fd);
                                        TargetOrderSet.instance.SetOrder();
                                    }
                                }
                            }
                            else if (State.leftArmSelected)
                            {
                                if (subState_index_V > 1)
                                {
                                    subState_index_V -= 1;
                                    Instantiate_target.instance.compCount = 0;
                                    currentShape_fd.SetActive(false);

                                    if (subState_index_V == 1)
                                    {
                                        this.new_shapeVert_fd[5].SetActive(true);
                                        currentShape_fd = this.new_shapeVert_fd[5];
                                        TargetOrderSet.instance.CreateArray(currentShape_fd);
                                        TargetOrderSet.instance.SetOrder();
                                    }
                                    else if (subState_index_V == 2)
                                    {
                                        this.new_shapeVert_fd[4].SetActive(true);
                                        currentShape_fd = this.new_shapeVert_fd[4];
                                        TargetOrderSet.instance.CreateArray(currentShape_fd);
                                        TargetOrderSet.instance.SetOrder();
                                    }
                                    else if (subState_index_V == 3)
                                    {
                                        this.new_shapeVert_fd[3].SetActive(true);
                                        currentShape_fd = this.new_shapeVert_fd[3];
                                        TargetOrderSet.instance.CreateArray(currentShape_fd);
                                        TargetOrderSet.instance.SetOrder();
                                    }
                                }
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
        currentShape_fd.SetActive(false);
        if (desce)
        {
            currentShape_fd.SetActive(false);
            DDA_Exercise_Grid.instance.nFreeDraw = false;
            DDA_Exercise_Grid.instance.nTargets = true;
        }
        else if (sobe)
        {
            currentShape_fd.SetActive(false);
            DDA_Exercise_Grid.instance.nFreeDraw = false;
            DDA_Exercise_Grid.instance.nShapes = true;
        }
    }
}
