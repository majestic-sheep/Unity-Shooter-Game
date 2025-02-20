using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlotGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private Inventory _inventory;
    private List<GameObject> _slots = new();

    [SerializeField] private float _rootY;
    [SerializeField] private float _slotSpacing;
    public void Update()
    {
        CheckSlotIcons();

        InformActiveSlot();
    }
    private void CheckSlotIcons()
    {
        if (SlotsCorrect()) return;
        ClearSlots();
        CreateAllSlots();
    }
    private bool SlotsCorrect()
    {
        for (int i = 0; i < Mathf.Max(_inventory.Items.Count, _slots.Count); i++)
        {
            if (i >= _slots.Count) return false;
            Item slotItem = _slots[i].GetComponent<SlotUI>().Item;
            if (i >= _inventory.Items.Count) return false;
            Item inventoryItem = _inventory.Items[i];

            if (!slotItem.Equals(inventoryItem)) return false;
        }
        return true;
    }
    private void ClearSlots()
    {
        int count = _slots.Count;
        for (int i = 0; i < count; i++)
        {
            Destroy(_slots[0]);
            _slots.RemoveAt(0);
        }
    }
    private void CreateAllSlots()
    {
        for (int i = 0; i < _inventory.Items.Count; i++)
        {
            Vector2 pos = new Vector2(_slotSpacing * i - 0.5f * _slotSpacing * (_inventory.Items.Count-1), _rootY);
            CreateSlot(_inventory.Items[i], pos);
        }
    }
    private void CreateSlot(Item item, Vector2 position)
    {
        GameObject slot = Instantiate(_slotPrefab, transform);
        slot.transform.localPosition = position;
        slot.GetComponent<SlotUI>().SetItem(item.Clone());
        _slots.Add(slot);
    }
    private void InformActiveSlot()
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            bool active = i == _inventory.CurrentIndex;
            _slots[i].GetComponent<SlotUI>().Active = active;
        }
    }
}