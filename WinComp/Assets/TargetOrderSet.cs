using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOrderSet : MonoBehaviour
{
    public static TargetOrderSet instance = null;
    public GameObject[] targetChildren;
    public int orderFlag;

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

    // Start is called before the first frame update
    void Start()
    {
        // Esta secção adiciona todos os targets da forma a um array com length igual ao número de targets da forma

        targetChildren = new GameObject[transform.childCount-1];
        for (int i = 0; i < targetChildren.Length; i++)
        {
            targetChildren[i] = transform.GetChild(i+1).gameObject;
        }
        
    }
    
    public void SetOrder()
    {
        targetChildren[orderFlag].GetComponent<Renderer>().material.color = Color.blue;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
