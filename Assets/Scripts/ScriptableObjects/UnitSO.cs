using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Unit", menuName = "ScriptableObjects/New Unit")]
    public class UnitSO : ScriptableObject
    {
        public string type;
        public string firstName;
        public Sprite sprite;
        public int damage;
        public int armour;
        public int healthPoint;
    }
}
