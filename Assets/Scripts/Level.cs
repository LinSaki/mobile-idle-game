using UnityEngine;

public class Level : MonoBehaviour
{
    public static Level instance; //set instance on Awake()
    [Space]
    [Header("Level UI")]
    [SerializeField] private GameObject timerPanels;
    [SerializeField] private GameObject pomodorPanel;
    [SerializeField] private GameObject breakPanel;
    [SerializeField] private GameObject userInputPanel;
    [SerializeField] private GameObject sunsetBackground;
    [SerializeField] private GameObject starlightBackground;

    public void Awake()
    {
        instance = this;
    }
    public void setBreakPanel()
    {
        pomodorPanel.SetActive(false);
        sunsetBackground.SetActive(false);
        breakPanel.SetActive(true);
        starlightBackground.SetActive(true);
    }

    public void setPomodoroBackground()
    {
        pomodorPanel.SetActive(true);
        sunsetBackground.SetActive(true);
        breakPanel.SetActive(false);
        starlightBackground.SetActive(false);
    }

    public GameObject getActivePanel()
    {
        return pomodorPanel;
    }

    public void setPanelAfterInput()
    {
        userInputPanel.SetActive(false);
        timerPanels.SetActive(true);
        setPomodoroBackground();
    }
}
