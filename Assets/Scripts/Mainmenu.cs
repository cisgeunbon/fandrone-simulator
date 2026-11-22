using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mainmenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private string firstStageScene = "Level0";

    private IDisposable startInput;

    private void OnEnable()
    {
        startInput = InputSystem.onAnyButtonPress.CallOnce(OnStartPressed);
    }

    private void OnDisable()
    {
        startInput?.Dispose();
    }

    private void OnStartPressed(InputControl button)
    {
        SceneManager.LoadScene(firstStageScene);
    }
}