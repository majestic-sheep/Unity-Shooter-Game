using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GadgetDisplay : MonoBehaviour
{
    public Gadget Gadget;

    [SerializeField] private UnityEngine.UI.Image _chargeBarForeground;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _valueText;
    [SerializeField] private GameObject _altUseButtonGameObject;
    [SerializeField] private GameObject _swapToButtonGameObject;
    [SerializeField] private Transform _transform;

    public Vector2 TargetPosition;
    public Vector2 DisplayedPosition;
    [SerializeField] private float _positionLerpRate;
    private void FixedUpdate()
    {
        switch (Gadget.GadgetState)
        {
            case GadgetState.Waiting:
                _chargeBarForeground.color = Color.gray;
                _chargeBarForeground.fillAmount = 1;
                _swapToButtonGameObject.SetActive(GadgetManager.Instance.IndexOf(this) != 0);
                break;
            case GadgetState.Charging:
                _chargeBarForeground.color = Color.gray;
                _chargeBarForeground.fillAmount = Gadget.Charge;
                _swapToButtonGameObject.SetActive(false);
                break;
            case GadgetState.Ready:
                _chargeBarForeground.color = Color.white;
                _chargeBarForeground.fillAmount = Gadget.Charge;
                _swapToButtonGameObject.SetActive(GadgetManager.Instance.IndexOf(this) != 0);
                break;
            case GadgetState.Active:
                _chargeBarForeground.color = Color.red;
                _chargeBarForeground.fillAmount = 1;
                _swapToButtonGameObject.SetActive(false);
                break;
        }
        MatchDisplayToGadget();
        _transform.localPosition = DisplayedPosition;

        DisplayedPosition = Vector2.Lerp(DisplayedPosition, TargetPosition, _positionLerpRate);
    }

    public void SetGadget(Gadget gadget)
    {
        _altUseButtonGameObject.SetActive(gadget.HasAltUse);
        Gadget = gadget;
        MatchDisplayToGadget();
    }
    public void MatchDisplayToGadget()
    {
        _nameText.text = Gadget.GadgetName;
        _nameText.color = Gadget.GadgetNameColor;
        if (Gadget.Value == -1)
            _valueText.text = "";
        else
            _valueText.text = Gadget.Value.ToString();
    }
    public void AltUseButtonPressed()
    {
        GadgetManager.Instance.AltUseGadget(Gadget);
    }
    public void SwapToButtonPressed()
    {
        GadgetManager.Instance.SetGadgetToPrimary(this);
    }
    public void UndeployButtonPressed()
    {
        GadgetManager.Instance.RemoveGadget(Gadget, this);
    }
    public void Undeploy()
    {
        StartCoroutine(UndeployCoroutine());
    }
    private IEnumerator UndeployCoroutine()
    {
        TargetPosition += Vector2.right * GadgetManager.Instance.InitialDisplayXOffset;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
