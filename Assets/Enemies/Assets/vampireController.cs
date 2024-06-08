using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;

public class vampireController : MonoBehaviour
{
    public float defaultMovementSpeed;
    public GameObject player;
    bool lockedOn = false;
    public int health = 100;

    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lockedOn){
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, defaultMovementSpeed * Time.deltaTime);
        }

        if (Mathf.Abs(transform.position.x - player.transform.position.x) < 10.1 && Mathf.Abs(transform.position.y - player.transform.position.y) < 5.7){
            lockedOn = true;
        }
        slider.value = health / 10;
        
    }
}
