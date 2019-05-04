using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RadialCompletionBar : MonoBehaviour
{

    public Image completion;
    public Text percentage;
    public Exercise amount;

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

        Amount = amount.completion;
        completion.fillAmount = (Amount/100);

        percentage.text = string.Format("{0} %", Amount);
    }

}
