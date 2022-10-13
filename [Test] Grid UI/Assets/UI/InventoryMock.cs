using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMock : MonoBehaviour
{
    [SerializeField]
    private Item replica;
    [SerializeField]
    private Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            inventory.SelectSlot(UIMovementEnum.UP);
        }
        if (Input.GetKeyDown("s"))
        {
            inventory.SelectSlot(UIMovementEnum.DOWN);
        }
        if (Input.GetKeyDown("a"))
        {
            inventory.SelectSlot(UIMovementEnum.LEFT);
        }
        if (Input.GetKeyDown("d"))
        {
            inventory.SelectSlot(UIMovementEnum.RIGHT);
        }
        if(Input.GetKeyDown("space")){
            bool res = inventory.AddItem(replica);
            Debug.Log("Item inserted? " + res);
        }
    }
}
