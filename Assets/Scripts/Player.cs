using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    [Header("Player details")]
    private string playerName;
    private string dateOfBirth;

    [Header("Pomodoro details")]
    private float focusTimeTotal;
    private float goalTotal;

    private void Awake()
    {
        LoadPlayerData();
    }

    private void LoadPlayerData()
    {
        focusTimeTotal = PlayerPrefs.GetFloat("focusTimeTotal", 0f);
    }

    public void AddToFocusTimeTotal()
    {
        focusTimeTotal += Pomodoro.instance.GetFocusLength();
        Debug.Log("Total focus time to date: " + focusTimeTotal + " mins");
        PlayerPrefs.SetFloat("focusTimeTotal", focusTimeTotal);
        PlayerPrefs.Save();
    }

    public float GetFocusTimeTotal()
    {
        return focusTimeTotal;
    }
}
