using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Create_Arrays : MonoBehaviour
{
    public static Create_Arrays instance = null;
    public Button setArray;
    public Button resetArray;
    public bool setPressed = false;
    public bool resetPressed = false;

    public int counterFlag;
    public GameObject TogglesParent;
    public GameObject ShapesParent;
    public GameObject[] selectedShapes;
    public GameObject[] toggles;
    public Toggle[] onToggles;
    public int nbOn = 0;

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

    public void CreateToggleArray(GameObject togglesParent)
    {
        //Ciclo que cria o array com os toggles selecionados
        toggles = new GameObject [togglesParent.transform.childCount];
        for (int j = 0; j < toggles.Length; j++)
        {
            toggles[j] = togglesParent.transform.GetChild(j).gameObject;
        }
        
        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].GetComponent<Toggle>().isOn)
            {
                nbOn++;
            }           
        }
        onToggles = new Toggle[nbOn];
        counterFlag = 0;
        for (int l = 0; l < toggles.Length; l++)
        {
            if (toggles[l].GetComponent<Toggle>().isOn)
            {
                counterFlag++;
                onToggles[counterFlag-1] = toggles[l].GetComponent<Toggle>();                
            }
            
        }                  
    }

    public void CreateShapeArray (GameObject shapesParent)
    {
        selectedShapes = new GameObject[nbOn];
        for (int j = 0; j < onToggles.Length; j++)
        {
            if (onToggles[j].name == "Square toggle")
            {
                selectedShapes[j] = shapesParent.transform.GetChild(0).gameObject;
            }
            else if (onToggles[j].name == "Triangle toggle")
            {
                selectedShapes[j] = shapesParent.transform.GetChild(1).gameObject;
            }
            else if (onToggles[j].name == "Circle toggle")
            {
                selectedShapes[j] = shapesParent.transform.GetChild(2).gameObject;
            }
            else if (onToggles[j].name == "Diamond toggle")
            {
                selectedShapes[j] = shapesParent.transform.GetChild(3).gameObject;
            }
            else if (onToggles[j].name == "Trapeze toggle")
            {
                selectedShapes[j] = shapesParent.transform.GetChild(4).gameObject;
            }
            else if (onToggles[j].name == "Star toggle")
            {
                selectedShapes[j] = shapesParent.transform.GetChild(5).gameObject;
            }
        }
        setPressed = true;
    }

    public void ResetButtonPressed()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].GetComponent<Toggle>().isOn = false;
        }
        toggles = new GameObject[0];
        selectedShapes = new GameObject[0];
        onToggles = new Toggle [0];
        nbOn = 0;
        resetPressed = true;
    }
}
