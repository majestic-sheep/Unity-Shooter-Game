using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _timeToWaitBeforeExit;
    [SerializeField] private SceneFade _sceneFade;
    public void OnPlayerDied()
    {
        PersistentDataManager.Instance.CollectLeaderstats();
        Invoke(nameof(EndGame), _timeToWaitBeforeExit);
    }
    private void EndGame()
    {
        _sceneFade.SwitchToScene("Main Menu");
    }
}
