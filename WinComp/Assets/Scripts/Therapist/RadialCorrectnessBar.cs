using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialCorrectnessBar : MonoBehaviour
{
    public Image correctness;
    public Text percentage;
    public Exercise_Flexion amount_flexion;
    public DDA_Exercise_Grid amount_grid;

    [Range(0, 100)]
    public float Correct;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnValidate()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (amount_flexion.isActiveAndEnabled)
        {
            Correct = amount_flexion.icorrectness;
            correctness.fillAmount = (Correct / 100);

            percentage.text = string.Format("{0} %", Correct);
        }
        else if (amount_grid.isActiveAndEnabled)
        {
            Correct = amount_grid.icorrectness;
            correctness.fillAmount = (Correct / 100);

            percentage.text = string.Format("{0} %", Correct);
        }
        
    }
}
