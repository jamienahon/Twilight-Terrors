using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class enemySpawner : MonoBehaviour
{
    public GameObject ghost;
    public GameObject frank;
    public GameObject vampire;
    public GameObject werewolf;
    public int enemyCount;

    public GameObject player;

    public float enemyCountIncreaseDelay;
    float enemyCountIncreaseTimer;

    int maxEnemyCount = 10;


    public float spawnDelay;
    float spawnDelayTimer;

    int enemyType = 0;

    float x;
    float y;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){

        spawnDelayTimer += Time.deltaTime;
        enemyCountIncreaseTimer += Time.deltaTime;

        if (enemyCountIncreaseTimer > enemyCountIncreaseDelay){
            enemyCountIncreaseTimer = 0;
            maxEnemyCount += 1;
        }

        if (spawnDelayTimer > spawnDelay && enemyCount < maxEnemyCount){
            spawnDelayTimer = 0;

            x = UnityEngine.Random.Range(-48f, 48f);
            y = UnityEngine.Random.Range(-48f, 48f);
            if (x > player.gameObject.transform.position.x - 10 && x < player.gameObject.transform.position.x + 10 && y > player.gameObject.transform.position.y - 6 && y < player.gameObject.transform.position.y + 6){
                x += 20;
                y += 12;
            }

            
        

            if (enemyType == 0){
                enemyType = 1;
                Instantiate(vampire, new Vector3(x, y, 0), Quaternion.identity);
                enemyCount += 1;
            }
            else if (enemyType == 1){
                enemyType = 2;
                Instantiate(ghost, new Vector3(x, y, 0), Quaternion.identity);
                enemyCount += 1;
            }
            else if (enemyType == 2){
                enemyType = 3;
                Instantiate(frank, new Vector3(x, y, 0), Quaternion.identity);
                enemyCount += 1;
            }
            else if (enemyType == 3){
                enemyType = 0;
                Instantiate(werewolf, new Vector3(x, y, 0), Quaternion.identity);
                enemyCount += 1;
            }
        }
    }
}
