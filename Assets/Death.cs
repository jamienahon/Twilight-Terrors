using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public GameObject black;
    public PlayerController player;

    void Update()
    {
        if (player.playerHealth <= 0)
            black.SetActive(true);
    }
}
