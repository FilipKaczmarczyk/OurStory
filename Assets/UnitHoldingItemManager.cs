using UnityEngine;

public class UnitHoldingItemManager : MonoBehaviour
{
    [SerializeField] private Transform slotTransform;

    public void ChangeHoldingItem(Item.ItemType itemType)
    {
        foreach (Transform child in slotTransform)
        {
            child.gameObject.SetActive(itemType == child.GetComponent<Item>().GetItemType());
        }
    }
}
