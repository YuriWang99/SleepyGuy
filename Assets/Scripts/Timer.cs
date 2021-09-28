using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class Timer : MonoBehaviour
{
    // Time Slider
    public Slider timerSlider;
    public float timeStart;//seconds 720s
    public float MaxTime = 720;
    public int Hour;
    public int Minute;
    public Text textBox;

    //Start Button
    public GameObject StartBtn;
    public Text startBtnText;
    //public bool timerActive = false;
    public Flowchart FungusFlowchart;
    void Start()
    {
        textBox.text = timeStart.ToString("00:00");
        timerSlider.maxValue = timeStart;
        timerSlider.value = timeStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (FungusFlowchart.GetBooleanVariable("TimerActive"))
        {
            //720 24h »»Ëã 30s = 1h 30s = 60min 1s = 2 min
            timeStart += Time.deltaTime;
            Hour = (int)timeStart /30;
            Minute = (int)(timeStart - Hour*30)*2;
            if(Hour+8>24)
            {
                textBox.text = (Hour + 8 - 24).ToString("00") + ":" + Minute.ToString("00");// Timer font 00:00
            }
            else
            {
                textBox.text = (Hour + 8).ToString("00") + ":" + Minute.ToString("00");// Timer font 00:00
            }
            
        }

        timerSlider.value = MaxTime - timeStart;

        //Timer finished
        if (timeStart >= MaxTime)
        {
            timeStart = 0;
            FungusFlowchart.SetBooleanVariable("TimerActive", false);
        }
    }

    public void timerButton()
    {
        FungusFlowchart.SetBooleanVariable("TimerActive", !FungusFlowchart.GetBooleanVariable("TimerActive"));
        startBtnText.text = FungusFlowchart.GetBooleanVariable("TimerActive") ? "Pause" : "Start";
        // reset

    }
}
