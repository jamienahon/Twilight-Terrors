using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightBatteryController : MonoBehaviour
{
    
    public GameObject player;
    private GameObject lightMeter;
    public float batteryIncreaseAmount;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        lightMeter = GameObject.Find("Light Meter");
        

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player")){
            lightMeter.GetComponent<lighMeterController>().batteryPercentage += batteryIncreaseAmount;
            Destroy(gameObject);
        }
    }
}
