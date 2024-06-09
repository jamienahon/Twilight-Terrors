using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthSlider : MonoBehaviour
{
    public Slider slider;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = player.GetComponent<PlayerController>().playerHealth;
    }
}
