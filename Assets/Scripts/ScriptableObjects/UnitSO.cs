using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Unit", menuName = "ScriptableObjects/New Unit")]
    public class UnitSO : ObjectSO
    {
        public string firstName;
        public int damage;
        public int armour;
        public int healthPoint;
    }
}
