using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropCollectable : MonoBehaviour
{
    [SerializeField] private GameObject _collectablePrefab;
    [SerializeField] private LootTable _lootTable;

    private float _dropYOffset;
    private void Start()
    {
        _dropYOffset = GetComponent<Transform>().localScale.y * GetComponent<SpriteRenderer>().size.y * -0.4f;
    }
    public void DropCollectable()
    {
        Item item = _lootTable.ModifiedBy(LootTableManager.Instance.LootTable).GetItemToDrop();
        if (item != null)
            CreateCollectable(item);
    }
    public void CreateCollectable(Item item)
    {
        GameObject _collectable = Instantiate(_collectablePrefab, transform.position, Quaternion.identity);
        _collectable.transform.position = new Vector2(
            _collectable.transform.position.x,
            _collectable.transform.position.y + _dropYOffset);
        _collectable.GetComponent<CollectableBehavior>().SetItem(item);
    }
}
