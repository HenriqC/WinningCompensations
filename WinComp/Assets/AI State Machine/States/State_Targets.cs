using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Targets : IState
{
    public int n_targets;
    public bool nShapes;
    public int subState_index;    
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
        Debug.Log("Entrou Targets");
        originPoint = ownerGameObject.transform.position;
        State.hasSecondaryCursor = true;
        State.exerciseName = "Target Reach";

        n_targets = 0;
        subState_index = 1;

        Instantiate_target.instance.exName.text = "Target Reach";
        Instantiate_target.instance.InstantiateObject(new_target, originPoint, Quaternion.identity);
        Instantiate_target.instance.circularGrid.SetActive(true);
        Instantiate_target.instance.easyArea.SetActive(true);

        if (State.maxReps <= 0)
        {
            State.maxReps = 10;
        }
    }

    public void Execute()
    {
        Instantiate_target.instance.radius = instantiateRadius;

        RaycastHit hit;
        Ray landingRay = new Ray (cursor.position, Vector3.back);
        Ray secondaryRay = new Ray (secondaryCursor.position, Vector3.back);

        if (State.isTherapyOnGoing)
        {
            originPoint = ownerGameObject.transform.position;
            if (Physics.Raycast(landingRay, out hit) || Physics.Raycast(secondaryRay, out hit))
            {
                if (hit.collider.tag == "CognitiveCollider_B")
                {
                    Object.Destroy(hit.collider.gameObject);
                    State.correctReps++;
                }
                else if (hit.collider.tag == "CognitiveCollider_P")
                {
                    Object.Destroy(hit.collider.gameObject);
                    if (State.correctReps > 0)
                    {
                        State.correctReps--;
                    }                    
                }

                if (hit.collider.tag == tagToLookFor)
                {
                    //Debug.Log(new_target);
                    //Debug.Log(originPoint);
                    //Debug.LogError(subState_index);
                    Debug.Log(n_targets);
                    n_targets++;                    
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
                        //Debug.LogError(icorrectness);
                    }

                    // ------------------- Sub índices dos sub niveis de dificuldade ------------------- //
                    if (n_targets <= (State.maxReps/4))
                    {
                        subState_index = 1; //Poor                        
                    }
                    else if (n_targets > (State.maxReps / 4) && n_targets <= (State.maxReps / 2))
                    {
                        subState_index = 2; // Mediocre                        
                    }
                    else if (n_targets > (State.maxReps / 2) && n_targets <= 3*(State.maxReps / 4))
                    {
                        subState_index = 3; // Avg
                    }
                    else if (n_targets >= 3 * (State.maxReps / 4) && n_targets < State.maxReps)
                    {
                        subState_index = 4; // Exc
                    }

                    // ------------------- ---------------------------------------- ---------------------------------- //

                    if (n_targets < State.maxReps && subState_index == 1) // Poor Sub-state -------------------------------------
                    {
                        Debug.LogError("Poor");

                        instantiateRadius = 0.1f;                        
                        CognitiveSphereSpawner.instance.spawnStart_B = 1f;
                        CognitiveSphereSpawner.instance.spawnRate_B = 15f;

                        Instantiate_target.instance.changeSpeed.speed = 0.4f;
                        Instantiate_target.instance.easyArea.SetActive(true);
                        Instantiate_target.instance.mediumArea.SetActive(false);
                        Instantiate_target.instance.hardArea.SetActive(false);

                        Instantiate_target.instance.InstantiateObject (new_target, originPoint, Quaternion.identity);
                        Object.Destroy(hit.collider.gameObject);
                    }
                    else if (n_targets < State.maxReps && subState_index == 2) //Med Sub-state ----------------------------------
                    {
                        Debug.LogError("Mediocre");

                        instantiateRadius = 0.3f;
                        Instantiate_target.instance.changeSpeed.speed = 0.5f;

                        CognitiveSphereSpawner.instance.spawnStart_B = 1f;
                        CognitiveSphereSpawner.instance.spawnRate_B = 10f;

                        CognitiveSphereSpawner.instance.spawnStart_P = 1f;
                        CognitiveSphereSpawner.instance.spawnRate_P = 8f;

                        Instantiate_target.instance.easyArea.SetActive(false);
                        Instantiate_target.instance.mediumArea.SetActive(true);
                        Instantiate_target.instance.hardArea.SetActive(false);

                        Instantiate_target.instance.InstantiateObject (new_target, originPoint, Quaternion.identity);
                        Object.Destroy(hit.collider.gameObject);

                    }
                    else if (n_targets < State.maxReps && subState_index == 3) //Avg Sub-state -----------------------------------
                    {
                        Debug.LogError("Avg");

                        instantiateRadius = 0.4f;
                        Instantiate_target.instance.changeSpeed.speed = 0.8f;
                        CognitiveSphereSpawner.instance.spawnStart_B = 1f;
                        CognitiveSphereSpawner.instance.spawnRate_B = 6f;

                        CognitiveSphereSpawner.instance.spawnStart_P = 1f;
                        CognitiveSphereSpawner.instance.spawnRate_P = 6f;

                        Instantiate_target.instance.easyArea.SetActive(false);
                        Instantiate_target.instance.mediumArea.SetActive(false);
                        Instantiate_target.instance.hardArea.SetActive(true);

                        Instantiate_target.instance.InstantiateObject(new_target, originPoint, Quaternion.identity);
                        Object.Destroy(hit.collider.gameObject);
                    }
                    else if (n_targets < State.maxReps && subState_index == 4) // Exc Sub-state -----------------------------------
                    {
                        Debug.LogError("Exc");

                        instantiateRadius = 0.45f;
                        Instantiate_target.instance.changeSpeed.speed = 1f;
                        CognitiveSphereSpawner.instance.spawnStart_B = 1f;
                        CognitiveSphereSpawner.instance.spawnRate_B = 5f;

                        CognitiveSphereSpawner.instance.spawnStart_P = 1f;
                        CognitiveSphereSpawner.instance.spawnRate_P = 4f;

                        Instantiate_target.instance.InstantiateObject(new_target, originPoint, Quaternion.identity);
                        Object.Destroy(hit.collider.gameObject);
                    }                    
                }
                if (State.correctReps == State.maxReps /*&& subState_index == 4*/)
                {
                    Debug.Log(DDA_Exercise_Grid.instance.nShapes);
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
                if (State.correctReps > 0)
                {
                    State.correctReps--;
                }
                if (subState_index > 1 && n_targets > 0)
                {
                    n_targets--;
                }
                else
                {
                    n_targets = 0;
                }
            }            

            if (Instantiate_target.instance.mudouDeCor == true && Instantiate_target.instance.cooldownTimer == 0)
            {                
                State.tries++;
                Instantiate_target.instance.mudouDeCor = false;
                Instantiate_target.instance.CooldownTimer(2);
                Object.Destroy(Instantiate_target.instance.ObInstance);
                Instantiate_target.instance.InstantiateObject(new_target, originPoint, Quaternion.identity);

                if (subState_index > 1 && n_targets > 0)
                {
                    n_targets--;
                }
                else
                {
                    n_targets = 0;
                }
                
            }            
        }
    }
    public void Exit()
    {
        State.correctReps = 0;
        State.tries = 0;
        Instantiate_target.instance.DestroyObject(new_target);
        Instantiate_target.instance.circularGrid.SetActive(false);
        Instantiate_target.instance.easyArea.SetActive(false);
        Instantiate_target.instance.mediumArea.SetActive(false);
        Instantiate_target.instance.hardArea.SetActive(false);
        DDA_Exercise_Grid.instance.nTargets = false;
        DDA_Exercise_Grid.instance.nShapes = true;
    }    
}

