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
        gameObject.transform.rotation = quaternion.Euler(0, 0, player.rotationZ);
        
    }
}
