using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Targets : IState
{
    public int n_targets;
    public bool nShapes;
    public float subState_index;    
    private GameObject ownerGameObject;
    private GameObject new_target;
    public float instantiateRadius;
    private string tagToLookFor;
    private Transform cursor;
    private Transform secondaryCursor;
    public Vector3 originPoint;
    //public int exc_count;
    private AudioClip beep;

    public State_Targets(Vector3 originPoint, Transform cursor, Transform secondaryCursor, GameObject new_target, GameObject ownerGameObject, float instantiateRadius, string tagToLookFor)
    {        
        this.originPoint = originPoint;
        this.cursor = cursor;
        this.secondaryCursor = secondaryCursor;
        this.new_target = new_target;
        this.ownerGameObject = ownerGameObject;
        this.instantiateRadius = instantiateRadius;
        this.tagToLookFor = tagToLookFor;
    }

    void init()
    {
        //beep = (AudioClip)Resources.Load("Sounds/beep");
    }

    public void Enter()
    {
        DDA_Exercise_Grid.instance.nShapes = false;
        DDA_Exercise_Grid.instance.nTargets = false;
        DDA_Exercise_Grid.instance.nFreeDraw = false;

        Debug.Log("Entrou Targets");
        originPoint = ownerGameObject.transform.position;
        State.hasSecondaryCursor = true;
        State.exerciseName = "Target Reach";

        Instantiate_target.instance.nTargets = 0;
        Instantiate_target.instance.subState = 1;

        //Instantiate_target.instance.exName.text = "Target Reach";
        //Instantiate_target.instance.InstantiateObject(new_target, originPoint, Quaternion.identity);
        Instantiate_target.instance.InstantiateRandom(new_target, originPoint);
        Instantiate_target.instance.radialTarget_param.SetActive(true);
        Instantiate_target.instance.linearDraw_param.SetActive(false);
        Instantiate_target.instance.shapes_param.SetActive(false);


        if (State.leftArmSelected)
        {
            Instantiate_target.instance.circularGrid_Left.SetActive(true);
            //Instantiate_target.instance.circularGrid_Left.transform.position = Instantiate_target.instance.leftShoulder.transform.position;
            Instantiate_target.instance.L_Area_1.SetActive(true);
        }
        else
        {
            Instantiate_target.instance.circularGrid_Right.SetActive(true);
            //Instantiate_target.instance.circularGrid_Right.transform.position = Instantiate_target.instance.rightShoulder.transform.position;
            Instantiate_target.instance.R_Area_1.SetActive(true);
        }

        if (State.maxReps <= 0)
        {
            State.maxReps = 10;
        }
        State.correctReps = 0;
        State.tries = 0;
    }

    public void Execute()
    {
        Instantiate_target.instance.radius = instantiateRadius;
        n_targets = Instantiate_target.instance.nTargets;
        RaycastHit hit;
        Ray landingRay = new Ray (cursor.position, Vector3.back);
        Ray secondaryRay = new Ray (secondaryCursor.position, Vector3.back);

        if (State.isTherapyOnGoing)
        {
            originPoint = ownerGameObject.transform.position;
            Instantiate_target.instance.cogCenter = originPoint;
            if (Physics.Raycast(landingRay, out hit) || Physics.Raycast(secondaryRay, out hit))
            {

                if (hit.collider.tag == "CognitiveCollider_P")
                {
                    Object.Destroy(hit.collider.gameObject);
                    if (State.correctReps > 0)
                    {
                        State.correctReps--;
                    }                    
                }

                if (hit.collider.tag == tagToLookFor)
                {
                    Instantiate_target.instance.nTargets++;                    
                    State.correctReps++;
                    State.tries++;

                    Instantiate_target.instance.PlayClip();

                    float complete = State.correctReps;
                    int minutes = (State.sessionTimeInt / State.correctReps) / 60;
                    int seconds = (State.sessionTimeInt / State.correctReps) % 60;
                    Instantiate_target.instance.avgTime.text = minutes.ToString("00") + ":" + seconds.ToString("00");
                    
                    DDA_Exercise_Grid.instance.completion = complete / State.maxReps * 100f;

                    if (State.tries > 0)
                    {
                        DDA_Exercise_Grid.instance.correctness = complete / State.tries * 100f;
                        DDA_Exercise_Grid.instance.icorrectness = (int)DDA_Exercise_Grid.instance.correctness;
                    }

                    // ------------------- Sub índices dos sub niveis de dificuldade ------------------- //

                    if (!Instantiate_target.instance.manualDiff.isOn)
                    {
                        if (n_targets <= (State.maxReps / 3))
                        {
                            Instantiate_target.instance.subState = 1;
                            Instantiate_target.instance.changeSpeed.speed = 0.4f;

                            subState_index = Instantiate_target.instance.subState; // Poor
                            instantiateRadius = 0.4f;                            
                        }
                        else if (n_targets > (State.maxReps / 3) && n_targets <= 2 * (State.maxReps / 3))
                        {
                            Instantiate_target.instance.subState = 2;
                            Instantiate_target.instance.changeSpeed.speed = 0.5f;

                            subState_index = Instantiate_target.instance.subState; // Avg
                            instantiateRadius = 0.45f;                            
                        }
                        else if (n_targets > 2 * (State.maxReps / 3) && n_targets <= (State.maxReps))
                        {
                            Instantiate_target.instance.subState = 3;
                            Instantiate_target.instance.changeSpeed.speed = 0.7f;

                            subState_index = Instantiate_target.instance.subState; // Avg
                            instantiateRadius = 0.55f;                            
                        }
                    }
                    else
                    {
                        subState_index = Instantiate_target.instance.subState;
                        instantiateRadius = Instantiate_target.instance.manualRadius;
                        Instantiate_target.instance.changeSpeed.speed = Instantiate_target.instance.manualSpeed;                                                
                    }
                    

                    // ------------------- ---------------------------------------- ---------------------------------- //

                    if (n_targets < State.maxReps && subState_index == 1) // Poor Sub-state -------------------------------------
                    {
                        Debug.LogError("Basic");
                        Instantiate_target.instance.levelDiff.text = "1";

                        Instantiate_target.instance.shoulderLiftOffset.value = 0.06f;
                        Instantiate_target.instance.hipLeanOffset.value = 0.06f;

                        // Targets no lado esquerdo
                        Instantiate_target.instance.minRange_L1 = 175f;
                        Instantiate_target.instance.maxRange_L1 = 180f;
                        Instantiate_target.instance.minRange_L2 = 180f;
                        Instantiate_target.instance.maxRange_L2 = 210f;
                        //Targets no lado direito
                        Instantiate_target.instance.minRange_R1 = 0f;
                        Instantiate_target.instance.maxRange_R1 = 15f;
                        Instantiate_target.instance.minRange_R2 = -30f;
                        Instantiate_target.instance.maxRange_R2 = 0f;
                        // ---------------------------------------------- //
                        CognitiveSphereSpawner.instance.spawnStart_P = 1f;
                        CognitiveSphereSpawner.instance.spawnRate_P = 10f;                        
                        if (State.leftArmSelected)
                        {
                            Instantiate_target.instance.L_Area_1.SetActive(false);
                            Instantiate_target.instance.L_Area_2.SetActive(true);
                            Instantiate_target.instance.L_Area_3.SetActive(false);
                            Instantiate_target.instance.L_Area_4.SetActive(false);
                            Instantiate_target.instance.L_Area_5.SetActive(false);
                        }
                        else
                        {
                            Instantiate_target.instance.R_Area_1.SetActive(false);
                            Instantiate_target.instance.R_Area_2.SetActive(true);
                            Instantiate_target.instance.R_Area_3.SetActive(false);
                            Instantiate_target.instance.R_Area_4.SetActive(false);
                            Instantiate_target.instance.R_Area_5.SetActive(false);
                        }

                        //Instantiate_target.instance.InstantiateObject (new_target, originPoint, Quaternion.identity);
                        Instantiate_target.instance.InstantiateRandom(new_target, originPoint);
                        Object.Destroy(hit.collider.gameObject);
                    }
                    else if (n_targets < State.maxReps && subState_index == 2) //Med Sub-state ----------------------------------
                    {
                        Debug.LogError("Poor");
                        Instantiate_target.instance.levelDiff.text = "2";

                        Instantiate_target.instance.shoulderLiftOffset.value = 0.04f;
                        Instantiate_target.instance.hipLeanOffset.value = 0.04f;

                        // Targets no lado esquerdo
                        Instantiate_target.instance.minRange_L1 = 135f;
                        Instantiate_target.instance.maxRange_L1 = 150f;
                        Instantiate_target.instance.minRange_L2 = 210f;
                        Instantiate_target.instance.maxRange_L2 = 230f;
                        //Targets no lado direito
                        Instantiate_target.instance.minRange_R1 = 35f;
                        Instantiate_target.instance.maxRange_R1 = 40f;
                        Instantiate_target.instance.minRange_R2 = -40f;
                        Instantiate_target.instance.maxRange_R2 = -35f;
                        // ---------------------------------------------- //
                        //CognitiveSphereSpawner.instance.spawnStart_B = 1f;
                        //CognitiveSphereSpawner.instance.spawnRate_B = 10f;                        
                        CognitiveSphereSpawner.instance.spawnStart_P = 1f;
                        CognitiveSphereSpawner.instance.spawnRate_P = 8f;
                        if (State.leftArmSelected)
                        {
                            Instantiate_target.instance.L_Area_1.SetActive(false);
                            Instantiate_target.instance.L_Area_2.SetActive(false);
                            Instantiate_target.instance.L_Area_3.SetActive(true);
                            Instantiate_target.instance.L_Area_4.SetActive(false);
                            Instantiate_target.instance.L_Area_5.SetActive(false);
                        }
                        else
                        {
                            Instantiate_target.instance.R_Area_1.SetActive(false);
                            Instantiate_target.instance.R_Area_2.SetActive(false);
                            Instantiate_target.instance.R_Area_3.SetActive(true);
                            Instantiate_target.instance.R_Area_4.SetActive(false);
                            Instantiate_target.instance.R_Area_5.SetActive(false);
                        }

                        //Instantiate_target.instance.InstantiateObject (new_target, originPoint, Quaternion.identity);
                        Instantiate_target.instance.InstantiateRandom(new_target, originPoint);
                        Object.Destroy(hit.collider.gameObject);
                    }
                    else if (n_targets < State.maxReps && subState_index == 3) //Avg Sub-state -----------------------------------
                    {
                        Debug.LogError("Avg");
                        Instantiate_target.instance.levelDiff.text = "3";

                        Instantiate_target.instance.shoulderLiftOffset.value = 0.11f;
                        Instantiate_target.instance.hipLeanOffset.value = 0.11f;

                        // Targets no lado esquerdo
                        Instantiate_target.instance.minRange_L1 = 100f;
                        Instantiate_target.instance.maxRange_L1 = 125f;
                        Instantiate_target.instance.minRange_L2 = 240f;
                        Instantiate_target.instance.maxRange_L2 = 265f;
                        //Targets no lado direito
                        Instantiate_target.instance.minRange_R1 = 60f;
                        Instantiate_target.instance.maxRange_R1 = 80f;
                        Instantiate_target.instance.minRange_R2 = -80f;
                        Instantiate_target.instance.maxRange_R2 = -60f;
                        // ---------------------------------------------- //
                        //CognitiveSphereSpawner.instance.spawnStart_B = 1f;
                        //CognitiveSphereSpawner.instance.spawnRate_B = 6f;                       
                        CognitiveSphereSpawner.instance.spawnStart_P = 1f;
                        CognitiveSphereSpawner.instance.spawnRate_P = 6f;

                        Instantiate_target.instance.changeSpeed.speed = 0.8f;
                        if (State.leftArmSelected)
                        {
                            Instantiate_target.instance.L_Area_1.SetActive(false);
                            Instantiate_target.instance.L_Area_2.SetActive(false);
                            Instantiate_target.instance.L_Area_3.SetActive(false);
                            Instantiate_target.instance.L_Area_4.SetActive(true);
                            Instantiate_target.instance.L_Area_5.SetActive(false);
                        }
                        else
                        {
                            Instantiate_target.instance.R_Area_1.SetActive(false);
                            Instantiate_target.instance.R_Area_2.SetActive(false);
                            Instantiate_target.instance.R_Area_3.SetActive(false);
                            Instantiate_target.instance.R_Area_4.SetActive(true);
                            Instantiate_target.instance.R_Area_5.SetActive(false);
                        }                       

                        //Instantiate_target.instance.InstantiateObject(new_target, originPoint, Quaternion.identity);
                        Instantiate_target.instance.InstantiateRandom(new_target, originPoint);
                        Object.Destroy(hit.collider.gameObject);
                    }
                    else if (n_targets < State.maxReps && subState_index == 4) // Exc Sub-state -----------------------------------
                    {
                        Debug.LogError("Exc");
                        Instantiate_target.instance.levelDiff.text = "4";

                        //CognitiveSphereSpawner.instance.spawnStart_B = 1f;
                        //CognitiveSphereSpawner.instance.spawnRate_B = 5f;                       
                        if (State.leftArmSelected)
                        {
                            Instantiate_target.instance.L_Area_1.SetActive(false);
                            Instantiate_target.instance.L_Area_2.SetActive(false);
                            Instantiate_target.instance.L_Area_3.SetActive(false);
                            Instantiate_target.instance.L_Area_4.SetActive(true);
                            Instantiate_target.instance.L_Area_5.SetActive(false);
                        }
                        else
                        {
                            Instantiate_target.instance.R_Area_1.SetActive(false);
                            Instantiate_target.instance.R_Area_2.SetActive(false);
                            Instantiate_target.instance.R_Area_3.SetActive(false);
                            Instantiate_target.instance.R_Area_4.SetActive(true);
                            Instantiate_target.instance.R_Area_5.SetActive(false);
                        }
                        CognitiveSphereSpawner.instance.spawnStart_P = 1f;
                        CognitiveSphereSpawner.instance.spawnRate_P = 4f;

                        //Instantiate_target.instance.InstantiateObject(new_target, originPoint, Quaternion.identity);
                        Instantiate_target.instance.InstantiateRandom(new_target, originPoint);
                        Object.Destroy(hit.collider.gameObject);
                    }                    
                }
                if (State.correctReps == State.maxReps /*&& subState_index == 4*/)
                {
                    Debug.Log("Muda de estado");
                    Object.Destroy(hit.collider.gameObject);
                    Exit();
                }                
            }            

            // ------------------- ---------------------------------------- ------------------------------------------- //
            if (State.compensationInCurrentRep && Instantiate_target.instance.cooldownTimer == 0)
            {
                State.compensationInCurrentRep = false;
                Instantiate_target.instance.CooldownTimer(3);
                /*if (State.correctReps > 0)
                {
                    State.correctReps--;
                }*/
                if (subState_index > 1 && n_targets > 0)
                {
                    Instantiate_target.instance.nTargets--;
                }
                else
                {
                    Instantiate_target.instance.nTargets = 0;
                }
            }            

            if (Instantiate_target.instance.mudouDeCor == true && Instantiate_target.instance.cooldownTimer == 0)
            {                
                State.tries++;
                Instantiate_target.instance.mudouDeCor = false;
                Instantiate_target.instance.CooldownTimer(2);
                Object.Destroy(Instantiate_target.instance.ObInstance);
                //Instantiate_target.instance.InstantiateObject(new_target, originPoint, Quaternion.identity);
                Instantiate_target.instance.InstantiateRandom(new_target, originPoint);

                if (subState_index > 1 && n_targets > 0)
                {
                    Instantiate_target.instance.nTargets--;
                }
                else
                {
                    Instantiate_target.instance.nTargets = 0;
                }
                
            }            
        }
    }
    public void Exit()
    {
        State.correctReps = 0;
        State.tries = 0;
        Instantiate_target.instance.DestroyObject(Instantiate_target.instance.ObInstance);
        Instantiate_target.instance.circularGrid_Left.SetActive(false);
        Instantiate_target.instance.circularGrid_Right.SetActive(false);

        // Desativação dos semiperímetros de dificuldade
        Instantiate_target.instance.L_Area_1.SetActive(false);
        Instantiate_target.instance.L_Area_2.SetActive(false);
        Instantiate_target.instance.L_Area_3.SetActive(false);
        Instantiate_target.instance.L_Area_4.SetActive(false);
        Instantiate_target.instance.L_Area_5.SetActive(false);

        Instantiate_target.instance.R_Area_1.SetActive(false);
        Instantiate_target.instance.R_Area_2.SetActive(false);
        Instantiate_target.instance.R_Area_3.SetActive(false);
        Instantiate_target.instance.R_Area_4.SetActive(false);
        Instantiate_target.instance.R_Area_5.SetActive(false);

        // Variáveis de mudança de exercício
        DDA_Exercise_Grid.instance.nTargets = false;
        DDA_Exercise_Grid.instance.nShapes = false;
        DDA_Exercise_Grid.instance.nFreeDraw = true;
        //Instantiate_target.instance.horizontalFD.isOn = true;
    }    
}

