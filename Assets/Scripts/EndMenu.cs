using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private string mainscene = "MainScene";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private IDisposable restartInput;
    // Update is called once per frame
    private void OnEnable()
    {
        restartInput = InputSystem.onAnyButtonPress.CallOnce(OnRestartPressed);
    }

    private void OnDisable()
    {
        restartInput?.Dispose();
    }

    private void OnRestartPressed(InputControl button)
    {
        SceneManager.LoadScene(mainscene);
    }

}
