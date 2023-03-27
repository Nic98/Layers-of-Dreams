using System.Collections.Generic;
using UnityEngine;

public class playerInventory : MonoBehaviour
{   
    private playerAttribute playerAttr;
    private InventorySave InventorySave = null;
    /****************************************************************************/
    // due to time constraint,
    // our team has decided to implement a limited item inventory
    // so this is kind of a constant var
    private static int buffTypeCount = 5;
    private List<Buff> ItemList; // item is originally buffs...
    private int[] ItemQuantity; // dependent on global options
    public enum Item { BuffRecoverHealth1 = 0,
                       BuffFasterAttack = 1,
                       BuffSpeedBoost = 2,
                       BuffPowerWithin = 3,
                       BuffRecoveryBlessing = 4
                      }
    public Buff RecoverHealth1;
    public Buff FasterAttack;
    public Buff SpeedBoost;
    public Buff PowerWithin;
    public Buff RecoveryBlessing;
    /****************************************************************************/

    void Start() {
        playerAttr = this.GetComponent<playerAttribute>();

        ItemList = new List<Buff>(buffTypeCount);
        ItemQuantity = new int[] {0, 0, 0, 0 ,0};
        
        getInventorySaveFromGlobalOption();
        if (InventorySave != null)
        {
            GameEvents.current.onPortalTriggeredEnter += saveInventoryToGlobalOption;
        }
        
        ItemList.Add(RecoverHealth1);
        ItemList.Add(FasterAttack);
        ItemList.Add(SpeedBoost);
        ItemList.Add(PowerWithin);
        ItemList.Add(RecoveryBlessing);
    }
    
    void Update() {

        if (Input.GetKeyDown(KeyCode.Alpha1)) { UseItem((int)Item.BuffRecoverHealth1); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { UseItem((int)Item.BuffFasterAttack); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { UseItem((int)Item.BuffSpeedBoost); }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { UseItem((int)Item.BuffPowerWithin); }
        if (Input.GetKeyDown(KeyCode.Alpha5)) { UseItem((int)Item.BuffRecoveryBlessing); }

        // printItemList(); // for debug
    }

    public void addToInventory(Buff buff)
    {   
        string buffName = buff.GetType().Name;
        
        for (int i = 0; i < ItemList.Count ; i++)
        {
            string name = ItemList[i].GetType().Name;
            if (name.CompareTo(buffName) == 0)
            {   
                if (ItemQuantity[i] < 99)
                {
                    ItemQuantity[i] += 1;
                }
                break;
            }
        }
    }

    void UseItem(int index)
    {
        if (ItemQuantity[index] > 0)
        {   
            ItemQuantity[index] -= 1;
            playerAttr.applyItemBuffEffect(ItemList[index]);
        }
    }

    public int[] getItemQuantities()
    {
        return ItemQuantity;
    }

    void printItemList()
    {
        string output = "|  ";
        for (int i = 0; i < ItemList.Count ; i++)
        {
            output += ItemList[i].GetType().Name;
            output += ": ";
            output += ItemQuantity[i];
            output += "  |  ";
        }

        print(output);
    }

    void getInventorySaveFromGlobalOption()
    {   
        int[] save;
        GameObject[] global = GameObject.FindGameObjectsWithTag("GlobalOption");

        if (global.Length > 0)
        {
            InventorySave = global[0].GetComponent<InventorySave>();
        }

        if (InventorySave != null)
        {
            save = InventorySave.getItemQuantity();
            if (save.Length != ItemQuantity.Length) { return; }
            for (int i = 0 ; i < ItemQuantity.Length ; i++)
            {   
                ItemQuantity[i] = save[i];
            }
        }
    }

    void saveInventoryToGlobalOption()
    {
        if (InventorySave != null)
        {
            InventorySave.setItemQuantity(ItemQuantity);
        }
    }
}

