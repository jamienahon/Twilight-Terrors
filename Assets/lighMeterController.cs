using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class lighMeterController : MonoBehaviour
{
    public Slider slider;
    public float batteryPercentage;
    public float batteryReductionScaler;
    public GameObject flashLight;
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

        flashLight.GetComponent<Light2D>().pointLightOuterRadius = 5 + (batteryPercentage * 0.07f) ;
    }
}
