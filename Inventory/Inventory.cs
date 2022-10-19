using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{


    [System.Serializable]
    public class InventoryItem
    {
        public string inventoryId = "";

        public InteractableItem interactableItem;

        
        [Title("Activate & Deactivate Events")]
        [HideLabel]
        public OnOffToggleEvent activateDeactivateEvents = new OnOffToggleEvent
        {
            eventName = "Activate & Deactivate Events",
            onEvent = new FrameCoreEvent
            {
                eventName = "Activate"
            },
            offEvent = new FrameCoreEvent
            {
                eventName = "Deactivate"
            }
        };

    }

    [FoldoutGroup("Active Interactable Item")]
    [HideLabel]
    public InventoryItem activeItem;

    [FoldoutGroup("Item To Switch Interactable Item")]
    [HideLabel]
    public InventoryItem itemToSwitchItem;




    [Title("Inventory Items")]
    public List<InventoryItem> inventoryItems;


    [FoldoutGroup("Inventory Events")]
    [Title("No Inventory Item Event")]
    [HideLabel]
    public FrameCoreEvent noInventoryItemEvent = new FrameCoreEvent
    {
        eventName = "No Inventory Item"
    };


    [FoldoutGroup("Inventory Events")]
    [Title("Switch Item Event")]
    [HideLabel]
    public FrameCoreEvent switchItemEvent = new FrameCoreEvent
    {
        eventName = "Switch Item"
    };



    [FoldoutGroup("Ammo Pouch")]
    [HideLabel]
    [InlineEditor(InlineEditorObjectFieldModes.Foldout)]
    public AmmoPouch ammoPouch;


    [FoldoutGroup("System")]
    public bool switching = false;

    public void Init()
    {

    }

    bool CanInteractWithItem()
    {
        return !switching;
    }

    public void TriggerPrimaryAction(float triggerValue)
    {
        if (CanInteractWithItem())
        {
            activeItem.interactableItem.Trigger(triggerValue);
        };
    }
    public void UnTriggerPrimaryAction()
    {
        activeItem.interactableItem.UnTrigger();
    }



    public void TriggerSecondaryAction(float triggerValue)
    {
        if (CanInteractWithItem())
        {
            activeItem.interactableItem.TriggerSecondary(triggerValue);
        };
    }

    public void UnTriggerSecondaryAction()
    {
        activeItem.interactableItem.UnTriggerSecondary();
    }


    public InventoryItem GetInventoryItem(string inventoryId)
    {

        InventoryItem inventoryItem = null;

        if (inventoryId != "None")
        {
            inventoryItems.ForEach(item =>
            {
                if (item.inventoryId == inventoryId)
                {
                    inventoryItem = item;
                };
            });
        };

        return inventoryItem;
    }

    public void SelectInventoryItem(Vector2 input)
    {

        string inventoryId = "None";

        if (input.x == 1)
        {
            inventoryId = "Right";
        }
        else if (input.x == -1)
        {
            inventoryId = "Left";
        }
        else if (input.y == 1)
        {
            inventoryId = "Up";
        }
        else if (input.y == -1)
        {
            inventoryId = "Down";
        };

        //Debug.Log("Selecting Inventory Item => " + inventoryId);



        SwitchItem(inventoryId);
    }




    void SwitchItem(string inventoryId)
    {
        if (inventoryId == "None")
        {
            //Debug.Log("SwitchItem() => noInventoryItemEvent");
            noInventoryItemEvent.Activate();
            return;
        };


        itemToSwitchItem = GetInventoryItem(inventoryId);



        if (itemToSwitchItem.interactableItem)
        {
            //Debug.Log("SwitchItem() => " + itemToSwitchItem.interactableItem);


            if (!activeItem.interactableItem)
            {
                //  CHECK IF ALREADY ACTIVE
                //Debug.Log("SwitchItem() => No active items exist ->> ActivateItem()");
                ActivateItem();
            }
            else if (itemToSwitchItem == activeItem)
            {
                //  CHECK IF ALREADY ACTIVE
                //Debug.Log("SwitchItem() => Item Already Set To Active Roll");
                // ACTIVATE CURRENT ITEM
                if (!activeItem.activateDeactivateEvents.toggleState)
                {
                    //Debug.Log("SwitchItem() => Activating Item");
                    ActivateItem();
                };

                return;
            }
            else
            {
                //Debug.Log("SwitchItem() => Switching Item");
                //  SWITCH ITEM
                switching = true;
                switchItemEvent.Activate();
                DeactivateActiveItem();
            };
        }
        else {
            noInventoryItemEvent.Activate();
        };
    }

    public void ItemDeactivatedCallback()
    {
        if (switching)
        {
            ActivateItem();
        };
    }


    public void ActivateItem()
    {
        //Debug.Log("ActivateItem() => " + itemToSwitchItem);

        switching = false;
        itemToSwitchItem.activateDeactivateEvents.ToggleEvent(true);
        activeItem = itemToSwitchItem;
        
        activeItem.interactableItem.Pickup();

    }

    public void DeactivateActiveItem()
    {
        //Debug.Log("DeactivateActiveItem() => " + activeItem);
        activeItem.interactableItem.Drop();

        activeItem.activateDeactivateEvents.ToggleEvent(false);
    }




}
