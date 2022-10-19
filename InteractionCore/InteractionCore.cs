using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum InteractableItemType
{
    Interact,
    Pickup,
    Control,
    Weapon,
    Important,
    Ammo
}




[System.Serializable]
public class InteractableItemData
{
    public InteractableItemType itemType;
    public HighlightType highlightType;
};


public class InteractionCore : MonoBehaviour
{
    public List<InteractableItemData> interactableItems;

    public InteractableItemData GetInteractableItem(InteractableItemType itemType)
    {
        return interactableItems.First(interactableItem => interactableItem.itemType == itemType);
    }

}
