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
    private AudioSource audio;
    private Transform cursor;
    private Transform secondaryCursor;
    public Vector3 originPoint;
    //public int exc_count;
    private AudioClip beep;
    private DDA_Exercise_Grid Objects;

    public State_Targets(Vector3 originPoint, Transform cursor, Transform secondaryCursor, AudioSource audio, GameObject new_target, GameObject ownerGameObject, float instantiateRadius, string tagToLookFor)
    {
        this.originPoint = originPoint;
        this.cursor = cursor;
        this.secondaryCursor = secondaryCursor;
        this.audio = audio;
        this.new_target = new_target;
        this.ownerGameObject = ownerGameObject;
        this.instantiateRadius = instantiateRadius;
        this.tagToLookFor = tagToLookFor;
    }

    void init()
    {
        beep = (AudioClip)Resources.Load("Sounds/beep");
    }

    public void Enter()
    {
        DDA_Exercise_Grid.instance.nShapes = false;
        DDA_Exercise_Grid.instance.nTargets = true;
        Debug.Log("Entrou Targets");

        State.hasSecondaryCursor = true;

        n_targets = 0;
        subState_index = 1;
        originPoint = ownerGameObject.transform.position;
        Instantiate_target.instance.InstantiateObject(new_target, originPoint, Quaternion.identity);

        if (State.maxReps <= 0)
        {
            State.maxReps = 10;
        }
        //spawnRadius = 30;
        //personal_space = 2;        
    }

    public void Execute()
    {
        Instantiate_target.instance.radius = this.instantiateRadius;

        RaycastHit hit;
        //Debug.Log(cursor);
        Ray landingRay = new Ray (cursor.position, Vector3.back);
        Ray secondaryRay = new Ray (secondaryCursor.position, Vector3.back);
        //Debug.DrawRay(cursor.position, Vector3.back);

        if (State.isTherapyOnGoing)
        {
            if (Physics.Raycast(landingRay, out hit) || Physics.Raycast(secondaryRay, out hit))
            {
                Vector2 randomVector = new Vector2(Random.value, Random.value);
                randomVector = randomVector.normalized;

                float d = Random.Range(-instantiateRadius, instantiateRadius);


                if (hit.collider.tag == this.tagToLookFor)
                {
                    //Debug.Log(new_target);
                    //Debug.Log(originPoint);
                    //Debug.LogError(subState_index);
                    Debug.Log(n_targets);
                    n_targets++;
                    State.correctReps++;
                    State.tries++;
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
                    audio.PlayOneShot(beep);

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
                        this.instantiateRadius = 0.15f;
                        originPoint += (Vector3)randomVector * d;
                        
                        Instantiate_target.instance.InstantiateObject (new_target, originPoint, Quaternion.identity);                        
                        Object.Destroy(hit.collider.gameObject);
                    }
                    else if (n_targets < 10 && subState_index == 2) //Med Sub-state ----------------------------------
                    {
                        Debug.LogError("Mediocre");
                        this.instantiateRadius = 0.2f;
                        originPoint += (Vector3)randomVector * d;

                        Instantiate_target.instance.InstantiateObject (new_target, originPoint, Quaternion.identity);
                        //Instantiate_target.instance.ColorChanger(new_target, 1.5f);
                        Object.Destroy(hit.collider.gameObject);
                    }
                    else if (n_targets < 10 && subState_index == 3) //Avg Sub-state -----------------------------------
                    {
                        Debug.LogError("Avg");
                        this.instantiateRadius = 0.3f;
                        originPoint += (Vector3)randomVector * d;

                        Instantiate_target.instance.InstantiateObject(new_target, originPoint, Quaternion.identity);
                        //Instantiate_target.instance.ColorChanger(new_target, 1f);
                        Object.Destroy(hit.collider.gameObject);
                    }
                    else if (n_targets <10 && subState_index == 4) // Exc Sub-state -----------------------------------
                    {
                        Debug.LogError("Exc");
                        this.instantiateRadius = 0.4f;
                        originPoint += (Vector3)randomVector * d;

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
                    nShapes = true;
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
            
                if (subState_index == 1)
                {
                    Instantiate_target.instance.ColorChanger(new_target, 10f);
                    if (new_target.GetComponent<Renderer>().sharedMaterial.color == Color.red)
                    {
                        Object.Destroy(new_target);
                        Debug.Log(subState_index);
                        n_targets = 0;
                    }
                }
                if (subState_index == 2)
                {
                    Instantiate_target.instance.ColorChanger(new_target, 6f);
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
                    Instantiate_target.instance.ColorChanger(new_target, 4f);
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
                    Instantiate_target.instance.ColorChanger(new_target, 2f);
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
        }
    }
    public void Exit()
    {

        DDA_Exercise_Grid.instance.nShapes = true;
    }    
}

