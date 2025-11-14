using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    [Header("Player details")]
    private string playerName;
    private string dateOfBirth;

    [Header("Movement details")]
    [SerializeField] float movementSpeed = 1.0f;

    [Header("Pomodoro details")]
    private float focusTimeTotal;
    private float completedNumOfPomodoro;

    void Awake()
    {
        instance = this;
    }
}
