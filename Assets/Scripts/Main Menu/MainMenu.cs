using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    [SerializeField] private SceneFade _sceneFade;
    public void Play()
    {
        _sceneFade.SwitchToScene("Game");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
