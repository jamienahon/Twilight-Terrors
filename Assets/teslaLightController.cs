using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class teslaLightController : MonoBehaviour
{
    public GameObject mainLight;
    public GameObject brightLight;
    public GameObject lightningEffect;
    public float lightPercentage;
    public float lightDropScaler;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mainLight.GetComponent<Light2D>().pointLightOuterRadius = lightPercentage * 0.35f;
        brightLight.GetComponent<Light2D>().intensity =  lightPercentage * 0.1f;
        lightningEffect.transform.localScale = new Vector3(lightPercentage / 100, lightPercentage / 100, 1);

        if (lightPercentage > 0){
            lightPercentage -= Time.deltaTime * lightDropScaler;
        } 
        if (lightPercentage > 100){
            lightPercentage = 100;
        }
        
    }
}
