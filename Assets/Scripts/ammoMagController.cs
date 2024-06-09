using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoMagController : MonoBehaviour
{
    public GameObject player;
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
            player.GetComponentInChildren<PlayerController>().totalBullets += 15;
            Destroy(gameObject);
        }
    }
}
