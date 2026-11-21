using System;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public GameObject pausePanel;
    [SerializeField] public Button pauseButton;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button returnButton;


    void Awake()
    {

        if (pauseButton == null)
        {
            Debug.Log("pausebutton not connect");
        }

        pausePanel.SetActive(true);
        pauseButton.onClick.AddListener(PauseClick);
    }

    private void PauseClick()
    {
        Debug.Log("Pause");
        PauseGame();
    }

    void PauseGame()
    {

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
