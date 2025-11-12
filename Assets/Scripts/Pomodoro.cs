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

    [Header("Timer UI")]
    [SerializeField] private TextMeshProUGUI timerText = null;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button breakButton;
    [SerializeField] private Button focusButtonn;
    [SerializeField] private GameObject pomodorPanel;
    [SerializeField] private GameObject breakPanel;
    [SerializeField] private GameObject sunsetBackground;
    [SerializeField] private GameObject starlightBackground;

    private float remainingTime;
    private const float baseInterval = 1f; // 1 second intervals

    private void Start()
    {
        PomodoroTime();
    }

    IEnumerator CountdownCoroutine()
    {
        float waitTime = baseInterval / countdownSpeed;

        while(remainingTime > 0)
        {
            yield return new WaitForSeconds(waitTime);
            remainingTime -= baseInterval;
            UpdateTimerText();
        }
        TimerFinished();
    }

    private void PomodoroTime()
    {
        remainingTime = timerStart;
        UpdateTimerText();
        StartCoroutine(CountdownCoroutine());
    }

    private void BreakTime()
    {
        remainingTime = breakTimerStart;
        UpdateTimerText();
        StartCoroutine(CountdownCoroutine());
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
        StopCoroutine(CountdownCoroutine());
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
}
