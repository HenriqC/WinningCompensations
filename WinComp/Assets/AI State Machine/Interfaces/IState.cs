using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void Enter(); // What the state-cartridge does when plugged in

    void Execute(); // What the cartridge continually does when plugged in

    void Exit(); // What the cartridge does when exited out.
    
}
 
