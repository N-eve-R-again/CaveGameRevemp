using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RiverUiManager : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update


    // Update is called once per frame
    public void UpdateSlider(float value)
    {
        slider.value = value;
    }
}
