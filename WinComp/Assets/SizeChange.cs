using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeChange : MonoBehaviour
{
    public void Update()
    {
        
    }
    public void AdjustSize (Slider S)
    {
        Instantiate_target.instance.ObInstance.transform.localScale = new Vector3(S.value, S.value, S.value);
        Debug.Log("funcionou");
    }
}
