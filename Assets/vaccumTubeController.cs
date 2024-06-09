using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vaccumTubeController : MonoBehaviour
{
    public GameObject player;
    public GameObject flashLight;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

        void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player")){
            
            Destroy(gameObject);
        }
    }
}
