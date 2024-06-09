using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vaccumTubeController : MonoBehaviour
{
    public GameObject player;
    public GameObject teslaLight;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        teslaLight = GameObject.Find("Tesla Coil");
    }

    // Update is called once per frame
    void Update()
    {

    }

        void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player")){
            teslaLight.GetComponent<teslaLightController>().vaccumTubesInInventory += 1;
            Destroy(gameObject);
        }
    }
}
