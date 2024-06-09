using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class tutorialText : MonoBehaviour
{
    public string[] textInputs;
    public float textSpeed;
    public int count = 0;
    private int charachterCount;
    private float timer;
    public int textNumber = 1;

    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = textInputs[textNumber];
        text.maxVisibleCharacters = count;
        timer += Time.deltaTime;
        if (timer > textSpeed && count < 1000){
            count += 1;
            timer = 0;
        }

    }
}
