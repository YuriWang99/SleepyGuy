using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Countdown : MonoBehaviour
{
    //Data
    public float timeStart;
    public Text textBox;
    // Start is called before the first frame update
    void Start()
    {
        textBox.text = timeStart.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timeStart -= Time.deltaTime;
        textBox.text = timeStart.ToString("0.00");
    }
}
