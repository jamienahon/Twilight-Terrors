using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class teslaLightController : MonoBehaviour
{
    public GameObject mainLight;
    public GameObject brightLight;
    public GameObject lightningEffect;
    public float lightPercentage;
    public float lightDropScaler;

    public GameObject player;

    public float vaccumTubePoints;

    public Slider slider;
    public TextMeshProUGUI countText;
    public int vaccumTubesInInventory = 0;
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
        
        slider.value = lightPercentage;
        countText.text = vaccumTubesInInventory.ToString();

        if (player.gameObject.transform.position.x > -2.3 && player.gameObject.transform.position.x < 2.3 && player.gameObject.transform.position.y > -2.3 && player.gameObject.transform.position.y < 2.3){
            lightPercentage += vaccumTubesInInventory * vaccumTubePoints;
            
            vaccumTubesInInventory = 0;
            
        }

        if (lightPercentage > 0){
            lightPercentage -= Time.deltaTime * lightDropScaler;
        } 
        if (lightPercentage > 100){
            lightPercentage = 100;
        }

        
        
    }
}
