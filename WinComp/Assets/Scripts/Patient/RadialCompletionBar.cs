using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RadialCompletionBar : MonoBehaviour
{

    public Image completion;
    public Text percentage;
    public Exercise_Flexion amount_flexion;
    public DDA_Exercise_Grid amount_grid;

    [Range (0,100)]
    public float Amount;

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
            Amount = amount_flexion.completion;
            completion.fillAmount = (Amount / 100);

            percentage.text = string.Format("{0} %", Amount);
        }
        else if (amount_grid.isActiveAndEnabled)
        {
            Amount = amount_grid.completion;
            completion.fillAmount = (Amount / 100);

            percentage.text = string.Format("{0} %", Amount);
        }

    }

}
