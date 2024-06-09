using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class pickupSpawner : MonoBehaviour
{
    public GameObject ammoObject;
    public GameObject player;
    public int maxAmmo;
    public GameObject vaccumTubesObject;
    public int maxVaccumTubes;
    public GameObject batteriesObject;
    public int maxBatteries;

    private int currentAmmoCount = 0;
    private int currentVaccumTubeCount = 0;
    private int currentBatteriesCount = 0;

    private float ammoTimer = 0;
    private float vaccumTubeTimer = 0;
    private float batteriesTimer = 0;

    public float ammoDelay;
    public float vaccumTubeDelay;
    public float batteriesDelay;


    float x;
    float y;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x = UnityEngine.Random.Range(-48f, 48f);
        y = UnityEngine.Random.Range(-48f, 48f);
        if (x > player.gameObject.transform.position.x - 10 && x < player.gameObject.transform.position.x + 10 && y > player.gameObject.transform.position.y - 6 && y < player.gameObject.transform.position.y + 6){
            x += 20;
            y += 12;
        }

        ammoTimer += Time.deltaTime;
        if (currentAmmoCount < maxAmmo && ammoTimer > ammoDelay){
            currentAmmoCount += 1;
            ammoTimer = 0;
            Instantiate(ammoObject, new Vector3(x, y, 0), Quaternion.identity);
        }
        x = UnityEngine.Random.Range(-48f, 48f);
        y = UnityEngine.Random.Range(-48f, 48f);
        if (x > player.gameObject.transform.position.x - 10 && x < player.gameObject.transform.position.x + 10 && y > player.gameObject.transform.position.y - 6 && y < player.gameObject.transform.position.y + 6){
            x += 20;
            y += 12;
        }

        vaccumTubeTimer += Time.deltaTime;
        if (currentVaccumTubeCount < maxVaccumTubes && vaccumTubeTimer > vaccumTubeDelay){
            currentVaccumTubeCount += 1;
            vaccumTubeTimer = 0;
            Instantiate(vaccumTubesObject, new Vector3(x, y, 0), Quaternion.identity);
        }
        x = UnityEngine.Random.Range(-48f, 48f);
        y = UnityEngine.Random.Range(-48f, 48f);
        if (x > player.gameObject.transform.position.x - 10 && x < player.gameObject.transform.position.x + 10 && y > player.gameObject.transform.position.y - 6 && y < player.gameObject.transform.position.y + 6){
            x += 20;
            y += 12;
        }

        batteriesTimer += Time.deltaTime;
        if (currentBatteriesCount < maxBatteries && batteriesTimer > batteriesDelay){
            currentBatteriesCount += 1;
            batteriesTimer = 0;
            Instantiate(batteriesObject, new Vector3(x, y, 0), Quaternion.identity);
        }
    }
}
