using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetColor : MonoBehaviour
{
    public GameObject parent;
    private void OnEnable()
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            if (parent.transform.GetChild(i).tag == "TargetCollider")
            {
                parent.transform.GetChild(i).GetComponent<Renderer>().material.color = Color.white;
                Debug.Log("Funcionou");
            }
        }
    }
}
