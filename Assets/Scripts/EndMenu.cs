using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private GameObject resetPanel;
    [SerializeField] private Button resetButton;
    [SerializeField] private string mainscene = "MainScene";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        resetPanel.SetActive(true);
        resetButton.onClick.AddListener(HandleReset);
    }
    // Update is called once per frame
    public void HandleReset()
    {
        ResetGame();
    }

    void ResetGame()
    {
        SceneManager.LoadScene(mainscene);
    }
}
