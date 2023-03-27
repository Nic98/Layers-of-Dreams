using UnityEngine;
using UnityEngine.SceneManagement;

public class InventorySave : MonoBehaviour
{   
    private int[] ItemQuantity = {0, 0, 0, 0, 0};

    public int[] getItemQuantity()
    {
        return ItemQuantity;
    }

    public void setItemQuantity(int[] updatedQuantity)
    {   
        if (ItemQuantity.Length != updatedQuantity.Length) { return; }
        for (int i = 0 ; i < updatedQuantity.Length ; i++)
        {   
            ItemQuantity[i] = updatedQuantity[i];
        }
    }
}