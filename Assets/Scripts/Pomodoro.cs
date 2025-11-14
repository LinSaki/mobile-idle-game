using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pomodoro : MonoBehaviour
{
    [Header("Timer Controls")]
    private bool isTimerPaused = false;
    private float remainingTime;
    Coroutine timerRoutine = null;

    [Header("Timer UI")]
    [SerializeField] private TextMeshProUGUI timerText = null;
    [SerializeField] private TextMeshProUGUI calculationsText = null;
    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI pauseText;

    [Header("User Input")]
    [SerializeField] private TMP_InputField userTimerInput;
    [SerializeField] private TMP_InputField userBreakInput;
    [SerializeField] private TMP_InputField userBreakAmountInput;
    private float focusLengths;
    private float numOfSessions;
    private float sessionCount = 0f;

    private void Start()
    {
        timerRoutine = StartCoroutine(CountdownCoroutine());
    }

    public void StartToFocus()
    {
        Level.instance.SetPanelAfterInput();
        PomodoroTime();
    }

    IEnumerator CountdownCoroutine()
    {
        while (remainingTime > 0)
        {
            if (!isTimerPaused)
            {
                remainingTime = Mathf.Max(remainingTime - Time.deltaTime, 0f); //time never goes below 0
                UpdateTimerText();
            }
            yield return null;  // wait 1 frame
        }
    }

    private float ConvertTexttoFloat(TMP_InputField inputField) 
    {
        string inputText = inputField.text;

        if (float.TryParse(inputText, out float value))
            return value;
        else
        {
            Debug.Log("Conversion failed. Input is not a number: " + inputText);
            return 0;
        }
    }

    private float ConvertFloatToSeconds(float value)
    {
        return value * 60; //convert to seconds
    }

    private void PomodoroTime()
    {
        float userTimer = ConvertFloatToSeconds(focusLengths);
        if (userTimer <= 0)
            userTimer = 900.0f; //automate 15min
        remainingTime = userTimer;
        UpdateTimerText();
        timerRoutine = StartCoroutine(CountdownCoroutine());
    }

    private void BreakTime()
    {
        float userBreak = ConvertTexttoFloat(userBreakInput);
        userBreak = ConvertFloatToSeconds(userBreak);
        if (userBreak <= 0)
            userBreak = 300.0f; //automate 5min
        remainingTime = userBreak;
        UpdateTimerText();
        timerRoutine = StartCoroutine(CountdownCoroutine());
    }

    string FormatTime(float timeLength)
    {
        int minutes = Mathf.FloorToInt(timeLength / 60);
        int seconds = Mathf.FloorToInt(timeLength % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

   
    private void UpdateTimerText()
    {
        timerText.text = FormatTime(remainingTime);
    }

     private void UpdatePomodoroCalculationText(string value)
    {
        calculationsText.text = value;
    }

    public void PomodoroCalculations()
    {

        float focusValue = ConvertTexttoFloat(userTimerInput);
        float breakValue = ConvertTexttoFloat(userBreakInput);
        numOfSessions = ConvertTexttoFloat(userBreakAmountInput);

        //math for focus length of each session
        focusLengths = (focusValue - (breakValue * numOfSessions)) / numOfSessions;

        string calcText = numOfSessions + " focus session(s) that are " + focusLengths + " min long \r\nwith \r\n"+ numOfSessions + " break session(s) that are "+ breakValue + " min long";
        UpdatePomodoroCalculationText(calcText);
    }

    private void TimerFinished()
    {
        StopCoroutine(timerRoutine);
        if (Level.instance.GetActivePanel().activeInHierarchy == true)
        {
            Level.instance.SetBreakPanel();
            BreakTime();
        }
        else
        {
            Level.instance.SetPomodoroBackground();
            PomodoroTime();
        }
    }

    public void pausePlayTime()
    {
        isTimerPaused = !isTimerPaused;
        if (isTimerPaused)
            pauseText.text = "|>";
        else
            pauseText.text = "||";
    }

    public void StartPomodoro()
    {
        if (sessionCount < numOfSessions)
        {
            if (remainingTime > 0)
                Debug.Log("Focus time started earlier.");
            TimerFinished();
        }
        else
            FinishedPomodoro();
    }
    public void StartBreak() 
    {
        if (remainingTime > 0)
            Debug.Log("Break time started earlier.");
        if (remainingTime == 0)
        {
            sessionCount++;
            Debug.Log("Session count: " + sessionCount);
        }
        TimerFinished();
    }

    private void FinishedPomodoro()
    {
        Debug.Log("Finished a complete Pomodoro!");
        sessionCount = 0;
        Level.instance.StartUpPanel();
    }
}
