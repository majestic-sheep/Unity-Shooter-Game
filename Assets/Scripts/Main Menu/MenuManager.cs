using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private SceneFade _sceneFade;
    [SerializeField] private GameObject[] canvases;
    public void Play()
    {
        _sceneFade.SwitchToScene("Game");
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void ToCanvas(string name)
    {
        foreach (GameObject canvas in canvases)
            canvas.SetActive(canvas.name == name);
    }
}
