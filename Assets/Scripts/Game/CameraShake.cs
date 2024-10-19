using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Camera cam;
    public float ShakeMagnitude;
    [SerializeField] private float _shakeDecreaseFactor;
    void Start()
    {
        cam = GetComponent<Camera>();
    }
    void FixedUpdate()
    {
        Vector2 random = Random.insideUnitCircle * ShakeMagnitude;
        cam.transform.position = new Vector3(random.x, random.y, -10);
        ShakeMagnitude *= _shakeDecreaseFactor;
    }
}
