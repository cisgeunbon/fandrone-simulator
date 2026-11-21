using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private string firstStageScene = "Stage1";
    public void StartGame()
    {
        SceneManager.LoadScene()
    }
