using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        menuPanel.SetActive(false);
        pauseButton.onClick.AddListener(PauseClick);
        resumeButton.onClick.AddListener(ResumeClick);
        returnButton.onClick.AddListener(ReturnClick);

    }

    private void PauseClick()
    {
        Debug.Log("Pause");
        menuPanel.SetActive(true);
        pausePanel.SetActive(false);
        PauseGame();
    }

    private void ResumeClick()
    {
        menuPanel.SetActive(false);
        pausePanel.SetActive(true);
        ResumeGame();
        return;
    }

    private void ReturnClick()
    {
        SceneManager.LoadScene("MainScene");
        return;
    }

    void PauseGame()
    {
        Time.timeScale = 0f;


    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
