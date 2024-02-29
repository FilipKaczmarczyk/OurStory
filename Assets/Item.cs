using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Axe,
        Wood
    }

    [SerializeField] private ItemType itemType;

    public ItemType GetItemType()
    {
        return itemType;
    }
}
