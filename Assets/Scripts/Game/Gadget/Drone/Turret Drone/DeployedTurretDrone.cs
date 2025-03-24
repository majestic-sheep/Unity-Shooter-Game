using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeployedTurretDrone : DeployedGadget
{
    [Header("Turret Drone References")]
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObjectTargeter _gameObjectTargeter;
    [SerializeField] private Targeter _targeterLadder;
    [SerializeField] private ShootTarget _shootTarget;
    [SerializeField] private GameObject _gadgetCrosshairPrefab;
    private GameObject _gadgetCrosshair;
    private void Awake()
    {
        _camera = FindAnyObjectByType<Camera>();
        _gadgetCrosshair = Instantiate(_gadgetCrosshairPrefab);
        _gadgetCrosshair.GetComponent<GadgetCrosshair>().Targeter = _targeterLadder;
        _gadgetCrosshair.GetComponent<GadgetCrosshair>().ShootTarget = _shootTarget;
        _shootTarget.DeployedTurretDrone = this;
    }
    public override void Use()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPos = _camera.ScreenToWorldPoint(mouseScreenPos);

        float lowestDistance = float.MaxValue;
        GameObject currentEnemy = null;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            float distance = Vector2.Distance(mouseWorldPos, enemy.transform.position);
            if (distance < lowestDistance)
            {
                lowestDistance = distance;
                currentEnemy = enemy;
            }
        }

        if (currentEnemy != null)
            _gameObjectTargeter.Target = currentEnemy;

        _shootTarget.StartFiring();

        base.Use();
    }
    public void AssignTarget(Collider2D collider)
    {
        _gameObjectTargeter.Target = collider.gameObject;
    }
    public override void OnGadgetSwitchedToState(GadgetState state)
    {
        if (state == GadgetState.Ready)
            _shootTarget.RefillAmmo();
        base.OnGadgetSwitchedToState(state);
    }
    public new void Update()
    {
        base.Update();
        Gadget.Value = _shootTarget.Weapon.Ammo;
    }
    public override void Undeploy()
    {
        Destroy(_gadgetCrosshair);
        base.Undeploy();
    }
}
