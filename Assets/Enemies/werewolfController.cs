using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class werewolfController : MonoBehaviour
{
    public float defaultMovementSpeed;
    float movementSpeed;
    public GameObject player;
    bool lockedOn = false;
    public int health = 100;

    public Slider slider;
    public float attackCooldown;
    float attackTimer;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = defaultMovementSpeed;
        player = GameObject.Find("PlayerChild");
    }
    public GameObject bloodParticles;
    // Update is called once per frame
    void Update()
    {
        if (health < 0){
            Instantiate(bloodParticles, gameObject.transform.position, quaternion.identity);
            Destroy(gameObject);
        }
        if (lockedOn && (Mathf.Abs(transform.position.x - player.transform.position.x) > 1 || Mathf.Abs(transform.position.y - player.transform.position.y) > 2)){
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, defaultMovementSpeed * Time.deltaTime);
        }

        if (attackTimer > attackCooldown && (Mathf.Abs(transform.position.x - player.transform.position.x) < 1.1 && Mathf.Abs(transform.position.y - player.transform.position.y) < 2.1)){
            player.GetComponent<PlayerController>().playerHealth -= 10;
            attackTimer = 0;
        }

        if (Mathf.Abs(transform.position.x - player.transform.position.x) < 10.1 && Mathf.Abs(transform.position.y - player.transform.position.y) < 5.7){
            lockedOn = true;
        }
        slider.value = health;
        attackTimer += Time.deltaTime;
        
    }
}
