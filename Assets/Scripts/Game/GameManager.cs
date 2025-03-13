using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private float _timeToWaitBeforeExit;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void OnPlayerDied()
    {
        PersistentDataManager.Instance.CollectLeaderstats();
        Invoke(nameof(EndGame), _timeToWaitBeforeExit);
    }
    private void EndGame()
    {
        SceneFade.Instance.SwitchToScene("Main Menu");
    }
}
