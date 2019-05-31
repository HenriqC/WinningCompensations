using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Targets : IState
{
    private int n_targets;
    public bool nShapes;
    private int subState_index;    
    private GameObject ownerGameObject;
    private GameObject new_target;
    public float instantiateRadius;
    private string tagToLookFor;
    private Transform cursor;
    private Transform secondaryCursor;
    public Vector3 originPoint;
    //public int exc_count;
    private AudioClip beep;
    private DDA_Exercise_Grid Objects;
    private ColorChanger parameter;

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

        State.hasSecondaryCursor = true;

        n_targets = 0;
        subState_index = 1;
        originPoint = ownerGameObject.transform.position;
        Instantiate_target.instance.InstantiateObject(new_target, originPoint, Quaternion.identity);
        Instantiate_target.instance.circularGrid.SetActive(true);
        Instantiate_target.instance.easyArea.SetActive(true);

        if (State.maxReps <= 0)
        {
            State.maxReps = 10;
        }
        Debug.Log("chegou aqui");
    }

    public void Execute()
    {
        Instantiate_target.instance.radius = this.instantiateRadius;

        RaycastHit hit;
        Ray landingRay = new Ray (cursor.position, Vector3.back);
        Ray secondaryRay = new Ray (secondaryCursor.position, Vector3.back);

        if (State.isTherapyOnGoing)
        {
            if (Physics.Raycast(landingRay, out hit) || Physics.Raycast(secondaryRay, out hit))
            {
                /*Vector2 randomVector = new Vector2(1,1);
                randomVector = randomVector.normalized;

                float d = Random.Range(-instantiateRadius, instantiateRadius);*/


                if (hit.collider.tag == this.tagToLookFor)
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
                    if (n_targets <= 2)
                    {
                        subState_index = 1; //Poor
                    }
                    else if (n_targets > 2 && n_targets <= 4)
                    {
                        subState_index = 2; // Mediocre
                    }
                    else if (n_targets > 4 && n_targets <= 6)
                    {
                        subState_index = 3; // Avg
                    }
                    else if(n_targets >= 6 && n_targets < 10)
                    {
                        subState_index = 4; // Exc
                    }

                    // ------------------- ---------------------------------------- ---------------------------------- //

                    if (n_targets < 10 && subState_index == 1) // Poor Sub-state -------------------------------------
                    {
                        Debug.LogError("Poor");

                        Instantiate_target.instance.speed = 0.1f;
                        this.instantiateRadius = 0.15f;
                        //originPoint += (Vector3)randomVector * d;                        
                        Instantiate_target.instance.InstantiateObject (new_target, originPoint, Quaternion.identity);                        
                        Object.Destroy(hit.collider.gameObject);
                    }
                    else if (n_targets < 10 && subState_index == 2) //Med Sub-state ----------------------------------
                    {
                        Debug.LogError("Mediocre");

                        Instantiate_target.instance.speed = 0.5f;
                        this.instantiateRadius = 0.3f;

                        Instantiate_target.instance.easyArea.SetActive(false);
                        Instantiate_target.instance.mediumArea.SetActive(true);
                        Instantiate_target.instance.hardArea.SetActive(false);

                        //originPoint += (Vector3)randomVector * d;
                        Instantiate_target.instance.InstantiateObject (new_target, originPoint, Quaternion.identity);
                        //Instantiate_target.instance.ColorChanger(new_target, 1.5f);
                        Object.Destroy(hit.collider.gameObject);

                    }
                    else if (n_targets < 10 && subState_index == 3) //Avg Sub-state -----------------------------------
                    {
                        Debug.LogError("Avg");

                        Instantiate_target.instance.speed = 0.7f;
                        this.instantiateRadius = 0.4f;

                        Instantiate_target.instance.easyArea.SetActive(false);
                        Instantiate_target.instance.mediumArea.SetActive(false);
                        Instantiate_target.instance.hardArea.SetActive(true);

                        //originPoint += (Vector3)randomVector * d;
                        Instantiate_target.instance.InstantiateObject(new_target, originPoint, Quaternion.identity);
                        //Instantiate_target.instance.ColorChanger(new_target, 1f);
                        Object.Destroy(hit.collider.gameObject);
                    }
                    else if (n_targets <10 && subState_index == 4) // Exc Sub-state -----------------------------------
                    {
                        Debug.LogError("Exc");
                        Instantiate_target.instance.speed = 1f;
                        this.instantiateRadius = 0.5f;
                        //originPoint += (Vector3)randomVector * d;
                        Instantiate_target.instance.InstantiateObject(new_target, originPoint, Quaternion.identity);
                        //Instantiate_target.instance.ColorChanger(new_target, 0.5f);
                        Object.Destroy(hit.collider.gameObject);
                    }                    
                }
                if (n_targets >= 10 && subState_index == 4)
                {
                    /*this.instantiateRadius = 0.7f;
                    originPoint += (Vector3)randomVector * d;

                    Instantiate_target.instance.InstantiateObject(new_target, originPoint, Quaternion.identity);
                    Object.Destroy(hit.collider.gameObject);
                    */
                    //nShapes = true;
                    Debug.Log(DDA_Exercise_Grid.instance.nShapes);
                    Debug.Log("Muda de estado");
                    Object.Destroy(hit.collider.gameObject);
                    Exit();
                }
            }

            // ------------------- ---------------------------------------- ------------------------------------------- //
            if (State.compensationInCurrentRep)
                { // Caso seja detetada uma compensação o exercício recomeça do início
                    State.compensationInCurrentRep = false;
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

            // ------ Inserir aqui código dos timings das bolas, de verde a vermelho, sendo que muda o subEstado consoante o subEstado em que está e se a bola fica totalmente vermelha ------ //
            
            /*
                if (subState_index == 1)
                {
                    //Instantiate_target.instance.ColorChanger(new_target, 10f);
                    if (new_target.GetComponent<Renderer>().sharedMaterial.color == Color.red)
                    {
                        Object.Destroy(new_target);
                        Debug.Log(subState_index);
                        n_targets = 0;
                    }
                }
                if (subState_index == 2)
                {
                    //Instantiate_target.instance.ColorChanger(new_target, 6f);
                    if (new_target.GetComponent<Renderer>().sharedMaterial.color == Color.red)
                    {
                        Object.Destroy(new_target);
                    if (n_targets > 0)
                    {
                        n_targets--;
                    }
                        Debug.Log(subState_index);
                    }
                }
                if (subState_index == 3)
                {
                    //Instantiate_target.instance.ColorChanger(new_target, 4f);
                    if (new_target.GetComponent<Renderer>().sharedMaterial.color == Color.red)
                    {
                        Object.Destroy(new_target);
                    if (n_targets > 0)
                    {
                        n_targets--;
                    }
                    Debug.Log(subState_index);
                    }
                }
                if (subState_index == 4)
                {
                    //Instantiate_target.instance.ColorChanger(new_target, 2f);
                    if (new_target.GetComponent<Renderer>().sharedMaterial.color == Color.red)
                    {
                        Object.Destroy(new_target);
                    if (n_targets > 0)
                    {
                        n_targets--;
                    }
                    Debug.Log(subState_index);
                    }
                }   
                */
        }
    }
    public void Exit()
    {
        Instantiate_target.instance.DestroyObject(new_target);
        Instantiate_target.instance.circularGrid.SetActive(false);
        Instantiate_target.instance.easyArea.SetActive(false);
        Instantiate_target.instance.mediumArea.SetActive(false);
        Instantiate_target.instance.hardArea.SetActive(false);
        DDA_Exercise_Grid.instance.nTargets = false;
        DDA_Exercise_Grid.instance.nShapes = true;
    }    
}

