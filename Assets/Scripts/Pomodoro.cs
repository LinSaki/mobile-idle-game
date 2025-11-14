using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pomodoro : MonoBehaviour
{
    [Header("Timer Controls")]
    [SerializeField] private float countdownSpeed = 1f;
    private bool isTimerPaused = false;

    [Header("Timer UI")]
    [SerializeField] private TextMeshProUGUI timerText = null;
    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI pauseText;

    [Header("User Input")]
    [SerializeField] private TMP_InputField userTimerInput;
    [SerializeField] private TMP_InputField userBreakInput;
    [SerializeField] private TMP_InputField userRoundsInput;

    Coroutine timerRoutine = null;

    private float remainingTime;

    private void Start()
    {
        timerRoutine = StartCoroutine(CountdownCoroutine());
    }
    public void StartToFocus()
    {
        Level.instance.setPanelAfterInput();
        PomodoroTime();
    }

    IEnumerator CountdownCoroutine()
    {
        while(remainingTime > 0)
        {
            yield return new WaitForSeconds(countdownSpeed);
            remainingTime -= countdownSpeed;
            UpdateTimerText();
        }
    }

    private float ConvertTexttoFloat(TMP_InputField inputField) 
    {
        string inputText = inputField.text;

        if (float.TryParse(inputText, out float value))
        {
            return value;
        }
        else
        {
            Debug.Log("Conversion failed. Input is not a number: " + inputText);
            return 0;
        }
    }

    private void PomodoroTime()
    {
        float userTimer = ConvertTexttoFloat(userTimerInput);
        if (userTimer <= 0)
            userTimer = 1500.0f;
        remainingTime = userTimer;
        UpdateTimerText();
        timerRoutine = StartCoroutine(CountdownCoroutine());
    }

    private void BreakTime()
    {
        float userBreak = ConvertTexttoFloat(userBreakInput);
        if (userBreak <= 0)
            userBreak = 300.0f;
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

    private void TimerFinished()
    {
        Debug.Log("Timer finished!");
        StopCoroutine(timerRoutine);
        if (Level.instance.getActivePanel().activeInHierarchy == true)
        {
            Level.instance.setBreakPanel();
            BreakTime();
        }
        else
        {
            Level.instance.setPomodoroBackground();
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

    public void StartPomodoro() => TimerFinished();

    public void StartBreak() => TimerFinished();
}
