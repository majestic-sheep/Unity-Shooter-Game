using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployedForceField : DeployedGadget
{
    [SerializeField] private GameObject _graphics;

    private Transform _playerTransform;
    private Health _playerHealth;
    private void Awake()
    {
        GameObject playerGameObject = PlayerMovement.Instance.gameObject;
        _playerHealth = playerGameObject.GetComponent<Health>();
        _playerTransform = playerGameObject.transform;
    }
    public override void Update()
    {
        transform.position = _playerTransform.position;
        base.Update();
    }
    public override void Use()
    {
        _graphics.SetActive(true);
        _playerHealth.InvincibilityFactors += 1;

        base.Use();

        Invoke(nameof(Deactivate), 8f);
    }
    public override void Deactivate()
    {
        RemoveInvincibilityFactor();
        base.Deactivate();
    }
    private void RemoveInvincibilityFactor()
    {
        if (Gadget.GadgetState == GadgetState.Active)
            _playerHealth.InvincibilityFactors -= 1;
        _graphics.SetActive(false);
    }
    public override void Undeploy()
    {
        RemoveInvincibilityFactor();
        base.Undeploy();
    }
}
