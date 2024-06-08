using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class pickupSpawner : MonoBehaviour
{
    public GameObject ammoObject;
    public int maxAmmo;
    public GameObject vaccumTubesObject;
    public int maxVaccumTubes;
    public GameObject batteriesObject;
    public int maxBatteries;
    public float respawnDelay;

    private int currentAmmoCount = 0;
    private int currentVaccumTubeCount = 0;
    private int currentBatteriesCount = 0;

    private float ammoTimer = 0;
    private float vaccumTubeTimer = 0;
    private float batteriesTimer = 0;

    public float xMax;
    public float xMin;
    public float yMax;
    public float yMin;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ammoTimer += Time.deltaTime;
        if (currentAmmoCount < maxAmmo && ammoTimer > respawnDelay){
            currentAmmoCount += 1;
            ammoTimer = 0;
            Instantiate(ammoObject, new Vector3(UnityEngine.Random.Range(xMin, xMax) * (UnityEngine.Random.Range(0,2)*2-1), UnityEngine.Random.Range(yMin, yMax) * (UnityEngine.Random.Range(0,2)*2-1), 0), Quaternion.identity);
        }

        vaccumTubeTimer += Time.deltaTime;
        if (currentVaccumTubeCount < maxVaccumTubes && vaccumTubeTimer > respawnDelay){
            currentVaccumTubeCount += 1;
            vaccumTubeTimer = 0;
            Instantiate(vaccumTubesObject, new Vector3(UnityEngine.Random.Range(xMin, xMax) * (UnityEngine.Random.Range(0,2)*2-1), UnityEngine.Random.Range(yMin, yMax) * (UnityEngine.Random.Range(0,2)*2-1), 0), Quaternion.identity);
        }

        batteriesTimer += Time.deltaTime;
        if (currentBatteriesCount < maxBatteries && batteriesTimer > respawnDelay){
            currentBatteriesCount += 1;
            batteriesTimer = 0;
            Instantiate(batteriesObject, new Vector3(UnityEngine.Random.Range(xMin, xMax) * (UnityEngine.Random.Range(0,2)*2-1), UnityEngine.Random.Range(yMin, yMax) * (UnityEngine.Random.Range(0,2)*2-1), 0), Quaternion.identity);
        }
    }
}
