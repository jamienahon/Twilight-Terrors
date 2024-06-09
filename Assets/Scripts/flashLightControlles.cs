using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class flashLightControlles : MonoBehaviour
{
    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, player.GetComponent<PlayerController>().rotationZ - 90);
        
        
    }
}
