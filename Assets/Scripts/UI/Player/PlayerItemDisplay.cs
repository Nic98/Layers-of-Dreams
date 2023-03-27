using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerItemDisplay : MonoBehaviour
{
    public playerInventory playerInventory;
    private List<Text> ItemQuantityDisplay;

    void Start() {
        if (playerInventory == null)
        {
            Destroy(this.gameObject);
        }

        ItemQuantityDisplay = new List<Text>();
        foreach ( Transform child in this.transform )
        {
            ItemQuantityDisplay.Add(child.GetComponentInChildren<Text>());
        }
    }

    void Update() {

        int[] update = playerInventory.getItemQuantities();
        int length = ItemQuantityDisplay.Count;

        if (update.Length < length) {
            length = update.Length;
        }

        for (int i = 0 ; i < length ; i++)
        {
            ItemQuantityDisplay[i].text = update[i].ToString();
        }
    }
}