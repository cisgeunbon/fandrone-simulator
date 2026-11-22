using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mainmenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private string firstStageScene = "Level0";
    [SerializeField] private Button startButton;

    void Awake()
    {
        Debug.Log("Mainmenu Awake", this);
        if (startButton == null)
        {
            Debug.LogError("Start Button이 연결되지 않았어.", this);
            return;
        }

        startButton.onClick.AddListener(StartClick);
        Debug.Log($"클릭 연결 완료: {startButton.name}", startButton);
    }

    private void StartClick()
    {
        Debug.Log("startgame");
        StartGame();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(firstStageScene);
    }
}