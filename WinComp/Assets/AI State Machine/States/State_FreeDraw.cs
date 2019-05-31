using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_FreeDraw : IState
{
    private int subState_index;

    public void Enter()
    {
        //DDA_Exercise_Grid.instance.nFreeDraw = false;
        DDA_Exercise_Grid.instance.nShapes = false;
        DDA_Exercise_Grid.instance.nTargets = false;
        Debug.Log("Entrou FreeDraw");

        subState_index = 1;
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}
