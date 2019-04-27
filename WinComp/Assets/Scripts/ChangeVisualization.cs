using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVisualization : MonoBehaviour
{
    public LineRenderer[] LineView;
    public MeshRenderer[] CapsuleView;


    // Start is called before the first frame update
    void Start()
    {
        LineView = GetComponentsInChildren<LineRenderer>();
        CapsuleView = GetComponentsInChildren<MeshRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            EnableChildComponents();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            DisableChildComponents();
        }
    }

    void EnableChildComponents()
    {
        foreach (LineRenderer line in LineView)
        {
            line.enabled = true;

        }

        foreach (MeshRenderer capsule in CapsuleView)
        {
            capsule.enabled = false;
        }
    }

    void DisableChildComponents()
    {
        foreach (LineRenderer line in LineView)
        {
            line.enabled = false;
        }

        foreach (MeshRenderer capsule in CapsuleView)
        {
            capsule.enabled = true;
        }
    }
}
