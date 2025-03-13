using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFade : MonoBehaviour
{
    public static SceneFade Instance { get; private set; }

    private const float MAGNITUDE = 2709.64f;
    [SerializeField] private Image _image;
    [SerializeField] private float _fadeLerpRate;
    [SerializeField] private float _startTimePad;
    [SerializeField] private float _endTimePad;
    private float _targetY = -MAGNITUDE;

    private string _toSceneName;
    private float _startTime;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _startTime = Time.time;
    }
    void FixedUpdate()
    {
        if (Time.time <= _startTime + _startTimePad) return;
        _image.transform.localPosition = new Vector3(0, 
            Mathf.Lerp(
                _image.transform.localPosition.y, 
                _targetY,
                _fadeLerpRate),
            0);
    }
    public void SwitchToScene(string sceneName)
    {
        _image.transform.localPosition = new Vector3(0, MAGNITUDE, 0);
        _targetY = 0;
        _toSceneName = sceneName;
        Invoke(nameof(ExecuteSceneTransfer), _endTimePad);
    }
    private void ExecuteSceneTransfer()
    {
        SceneManager.LoadScene(_toSceneName);
    }
}