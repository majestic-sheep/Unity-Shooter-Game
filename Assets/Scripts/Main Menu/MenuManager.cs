using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] private GameObject[] canvases;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void Play()
    {
        SceneFade.Instance.SwitchToScene("Game");
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
