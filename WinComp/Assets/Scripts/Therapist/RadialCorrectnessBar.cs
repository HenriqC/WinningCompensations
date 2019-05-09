using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialCorrectnessBar : MonoBehaviour
{
    public Image correctness;
    public Text percentage;
    public Exercise_Flexion amount;

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
        Correct = amount.icorrectness;
        correctness.fillAmount = (Correct / 100);

        percentage.text = string.Format("{0} %", Correct);
    }
}
