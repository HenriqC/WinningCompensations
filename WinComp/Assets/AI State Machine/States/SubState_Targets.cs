using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubState_Targets : IState
{
    public int n_targets;
    public void Enter()
    {
        
    }

    public void Execute()
    {
        if (hit.collider.tag == "Target Collider")
        {

        }
    }

    public void Exit()
    {
        
    }
}

