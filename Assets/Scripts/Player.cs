using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    [Header("Player details")]
    private string playerName;
    private string dateOfBirth;

    [Header("Pomodoro details")]
    private float focusTimeTotal;

    public void AddToFocusTimeTotal()
    {
        focusTimeTotal += Pomodoro.instance.GetFocusLength();
        Debug.Log("Total focus time to date: " + focusTimeTotal + " mins");
    }
}
