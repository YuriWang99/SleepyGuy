using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopWatch : MonoBehaviour
{
    // Start is called before the first frame update
    
    // Time Slider
    public Slider timerSlider;
    public float timeStart=0;//seconds
    public int Minute;
    public float Second;
    public Text textBox;
    //Set Panel
    public GameObject SetPanel;
    public int InputM=0, InputS=0;
    public InputField InputFieldM, InputFieldS;

    //Start Button
    public GameObject StartBtn;
    public Text startBtnText;


    bool timerActive = false;
    void Start()
    {
        textBox.text = timeStart.ToString("00:00.00");
        timerSlider.maxValue = timeStart;
        timerSlider.value = timeStart;

        //set SetPanel

    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            timeStart -= Time.deltaTime;
            Minute = (int)timeStart / 60;
            Second = timeStart - Minute*60;
            textBox.text = Minute.ToString("00")+":"+Second.ToString("00.00");// Timer font 00:00.00
        }

        timerSlider.value = timeStart;

        //Timer finished
        if(timeStart <= 0)
        {
            StartBtn.SetActive(false);
            SetPanel.SetActive(true);
            timeStart = 0;
            timerActive = false;
        }
        else
        {

        }
        Debug.Log(InputFieldM + " " + InputFieldM);

    }
    public void SetButton()
    {
        InputM = int.Parse(InputFieldM.text);
        InputS = int.Parse(InputFieldS.text);

        timeStart = InputM * 60 + InputS;
        if (timeStart>0)
        {
            StartBtn.SetActive(true);
            Minute = (int)timeStart / 60;
            Second = timeStart - Minute * 60;
            textBox.text = Minute.ToString("00") + ":" + Second.ToString("00.00");

            timerSlider.maxValue = timeStart;
            timerSlider.value = timeStart;
        }

    }
public void timerButton()
    {
        timerActive = !timerActive;
        startBtnText.text = timerActive ? "Pause" : "Start";
        // reset
        if(!timerActive)
        {
            SetPanel.SetActive(true);
        }
        else
        {
            SetPanel.SetActive(false);
        }

    }
}
