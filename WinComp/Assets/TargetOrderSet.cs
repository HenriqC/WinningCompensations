using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOrderSet : MonoBehaviour
{
    public static TargetOrderSet instance = null;
    public GameObject[] targetChildren;
    public int orderFlag = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    public void CreateArray(GameObject current_shape)
    {
        orderFlag = 0;
        // Esta secção adiciona todos os targets da forma a um array com length igual ao número de targets da forma
        targetChildren = new GameObject[current_shape.transform.childCount - 1];
        for (int i = 0; i < targetChildren.Length; i++)
        {
            targetChildren[i] = current_shape.transform.GetChild(i + 1).gameObject;
        }
    }
    public void SetOrder()
    {        
        targetChildren[orderFlag].GetComponent<Renderer>().material.color = Color.blue;
    }
}
