using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pomodoro : MonoBehaviour
{
    [Header("Timer Controls")]
    [SerializeField] private float timerStart =1500.0f;
    [SerializeField] private float breakTimerStart = 300.0f;
    [SerializeField] private float countdownSpeed = 1f;
    private bool isTimerPaused = false;

    [Header("Timer UI")]
    [SerializeField] private TextMeshProUGUI timerText = null;
    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private Button breakButton;
    [SerializeField] private Button focusButtonn;
    [SerializeField] private GameObject pomodorPanel;
    [SerializeField] private GameObject breakPanel;
    [SerializeField] private GameObject sunsetBackground;
    [SerializeField] private GameObject starlightBackground;
    Coroutine timerRoutine = null;

    private float remainingTime;
    private const float baseInterval = 1f; // 1 second intervals

    private void Start()
    {
        PomodoroTime();
    }

    IEnumerator CountdownCoroutine()
    {
        //float waitTime = baseInterval / countdownSpeed;

        while(remainingTime > 0)
        {
            yield return new WaitForSeconds(countdownSpeed);
            remainingTime -= countdownSpeed;
            UpdateTimerText();
        }
        //TimerFinished();
    }

    private void PomodoroTime()
    {
        remainingTime = timerStart;
        UpdateTimerText();
        timerRoutine = StartCoroutine(CountdownCoroutine());
    }

    private void BreakTime()
    {
        remainingTime = breakTimerStart;
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
        if (pomodorPanel.activeInHierarchy == true)
        {
            setBreakPanel();
            BreakTime();
        }
        else
        {
            setPomodoroBackground();
            PomodoroTime();
        }
    }

    private void setBreakPanel()
    {
        pomodorPanel.SetActive(false);
        sunsetBackground.SetActive(false);
        breakPanel.SetActive(true);
        starlightBackground.SetActive(true);
    }

    private void setPomodoroBackground()
    {
        pomodorPanel.SetActive(true);
        sunsetBackground.SetActive(true);
        breakPanel.SetActive(false);
        starlightBackground.SetActive(false);
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
