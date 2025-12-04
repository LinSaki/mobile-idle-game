using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    [Header("Player details")]
    private string playerName;
    private string dateOfBirth;

    [Header("Pomodoro details")]
    private float focusTimeTotal;
    private string dateKey;
    private float goalTotal;
    private readonly string dateFormat = "yyyy-MM-dd";

    private void Awake()
    {
        LoadPlayerData();
    }

    private void LoadPlayerData()
    {
        ResetFocusTimeDaily();
    }

    public void AddToFocusTimeTotal()
    {
        focusTimeTotal += Pomodoro.instance.GetFocusLength();
        Debug.Log("Total focus time today: " + focusTimeTotal + " mins");
        PlayerPrefs.SetFloat("focusTimeTotal", focusTimeTotal);
        PlayerPrefs.Save();
    }

    public float GetFocusTimeTotal()
    {
        return focusTimeTotal;
    }

    private void ResetFocusTimeDaily()
    {
        string lastReset = PlayerPrefs.GetString(dateKey, "");

        if (string.IsNullOrEmpty(lastReset))
        {
            PlayerPrefs.SetString(dateKey, DateTime.Now.ToString(dateFormat));
            PlayerPrefs.Save();
            return;
        }
        

        DateTime lastResetDate = DateTime.Parse(lastReset);
        DateTime today = DateTime.Today;

        Debug.Log("todays date: " + today);
        Debug.Log("last reset date: " + lastResetDate);

        if (today > lastResetDate)
        {
            PlayerPrefs.SetFloat("focusTimeTotal", 0f); //reset
            PlayerPrefs.SetString(dateKey, today.ToString(dateFormat)); // update date
            PlayerPrefs.Save();
            Debug.Log("Daily float reset!");
        }
        else
            Debug.Log("Same day, NOT resetting.");

    }
}
