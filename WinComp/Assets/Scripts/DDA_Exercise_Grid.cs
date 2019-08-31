using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDA_Exercise_Grid : MonoBehaviour
{
    private StateMachine stateMachine = new StateMachine();

    // ----------------- Final da descriminação das variáveis privadas de cada estado ----------------- //

    // Primeiro estado da máquina, exercício dos targets

    public bool nTargets; //Se o doente atingir o sub-estado "poor" 3x. Serve para fazer a mudança de estado

    [SerializeField]
    private Vector3 originPoint_t;
    [SerializeField]
    private float radius;
    [SerializeField]
    private GameObject owner_target_t;
    [SerializeField]
    private GameObject new_target_t;
    [SerializeField]
    private string Targets_Tag_t;
    [SerializeField]
    private Transform cPosition_t;
    [SerializeField]
    private Transform scPosition_t;

    // Segundo estado da máquina, exercício FreeDraw
    public bool nFreeDraw;

    [SerializeField]
    private Transform cPosition_fd;
    [SerializeField]
    private GameObject[] new_shapeVert_fd;
    [SerializeField]
    private GameObject[] new_shapeHoriz_fd;
    [SerializeField]
    private string Targets_Tag_fd;
    [SerializeField]
    private Color color_fd;



    // Terceiro estado da máquina, exercício das formas
    public bool nShapes;

    [SerializeField]
    private Transform cPosition_sp;
    [SerializeField]
    private GameObject []new_shape_sp;
    [SerializeField]
    private string Targets_Tag_sp;
    [SerializeField]
    private Color color;

    // ----------------- Final da descriminação das variáveis privadas de cada estado ----------------- //

    public GameObject cursor;
    public GameObject secondaryCursor;
    public GameObject leftHand;
    public GameObject rightHand;
    public Vector3 cursorPos;
    public Vector3 secondaryCursorPos;

    public static DDA_Exercise_Grid instance = null;
    public float completion;
    public float correctness;
    public int icorrectness;

    void Awake()
    {

        if (Select_exercises.instance.TargetsOn == true)
        {
            nTargets = true;
            nFreeDraw = false;
            nShapes = false;
        }
        else if (Select_exercises.instance.LineOn == true && Select_exercises.instance.TargetsOn == false)
        {
            nTargets = false;
            nFreeDraw = true;
            nShapes = false;
        }
        else if (Select_exercises.instance.ShapesOn == true && Select_exercises.instance.TargetsOn == false && Select_exercises.instance.TargetsOn == false)
        {
            nTargets = false;
            nFreeDraw = false;
            nShapes = true;
        }
        
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        completion = 0;
        correctness = 0f;
        icorrectness = 0;

        if (State.isLeftArmSelected())
        {
            cursor.transform.position = new Vector3(leftHand.transform.position.x, leftHand.transform.position.y, leftHand.transform.position.z);
            cursorPos = cursor.transform.position;
            secondaryCursor.transform.position = new Vector3(rightHand.transform.position.x, rightHand.transform.position.y, rightHand.transform.position.z);
            secondaryCursorPos = secondaryCursor.transform.position;
        }
        else if (State.isRightArmSelected())
        {
            cursor.transform.position = new Vector3(rightHand.transform.position.x, rightHand.transform.position.y, rightHand.transform.position.z);
            cursorPos = cursor.transform.position;
            secondaryCursor.transform.position = new Vector3(leftHand.transform.position.x, leftHand.transform.position.y, leftHand.transform.position.z);
            secondaryCursorPos = secondaryCursor.transform.position;
        }

        //this.stateMachine.ChangeState(new State_Targets(this.originPoint_t, this.cPosition_t, this.scPosition_t, this.audio, this.new_target_t, /*this.owner_target_t,*/ this.radius, this.Targets_Tag_t));
        
    }

    private void Update()
    {
        if (nShapes == true)
        {
            nShapes = false;
            this.stateMachine.ChangeState(new State_Shapes(this.cPosition_sp, this.new_shape_sp, this.Targets_Tag_sp, this.color));
        }
        else if (nTargets == true)
        {
            nTargets = false;
            this.stateMachine.ChangeState(new State_Targets(this.originPoint_t, this.cPosition_t, this.scPosition_t, this.new_target_t, this.owner_target_t, this.radius, this.Targets_Tag_t));
        }
        else if (nFreeDraw == true)
        {
            nFreeDraw = false;
            this.stateMachine.ChangeState(new State_FreeDraw(this.cPosition_fd, this.new_shapeVert_fd, this.new_shapeHoriz_fd, this.Targets_Tag_fd, this.color_fd));
        }

        this.stateMachine.ExecuteStateUpdate(); //Alteração para um estado diferente
    }





}
