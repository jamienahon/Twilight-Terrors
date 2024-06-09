using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lighMeterController : MonoBehaviour
{
    public Slider slider;
    public float batteryPercentage;
    public float batteryReductionScaler;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        batteryPercentage -= Time.deltaTime * batteryReductionScaler;
        if (batteryPercentage > 100){
            batteryPercentage = 100;
        }
        slider.value = batteryPercentage;
    }
}
