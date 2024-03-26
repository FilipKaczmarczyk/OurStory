using UnityEngine;

public interface ISelectable
{
    void Select();
    void Deselect();
    string GetName();
}