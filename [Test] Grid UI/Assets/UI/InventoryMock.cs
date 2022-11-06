using System.Collections;
using UnityEngine;

public class InventoryMock : MonoBehaviour
{
    [SerializeField]
    private Item replica;
    [SerializeField]
    private ArrayInventory inventory;
    [SerializeField]
    private float _delayTime = 2;
    void Update()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move(){
        if (Input.GetKeyDown("w"))
        {
            StartCoroutine(MoveUP());
        }
        if (Input.GetKeyDown("s"))
        {
            StartCoroutine(MoveDOWN());
        }
        if (Input.GetKeyDown("a"))
        {
            StartCoroutine(MoveLEFT());
        }
        if (Input.GetKeyDown("d"))
        {
            StartCoroutine(MoveRIGHT());
        }
        if(Input.GetKeyDown("space")){
            inventory.AddItem(replica);
            Debug.Log("Item inserted");
        }
        if(Input.GetKeyDown("r")){
            inventory.RemoveItem();
            Debug.Log("Item Removed");
        }
        if(Input.GetKeyDown("e")){
            inventory.AddItem(replica);
            Debug.Log("Item inserted");
        }
        if(Input.GetKeyDown("z")){
            inventory.AddSlot();
            Debug.Log("Slot added");
        }
        if(Input.GetKeyDown("f")){
            Debug.Log("Slot SubMenu");
            inventory.OpenSlotMenu();
        }
        yield return new WaitForSeconds(_delayTime);
    }
    IEnumerator MoveUP(){
        inventory.SelectSlot(UIMovementEnum.UP);
        yield return new WaitForSeconds(_delayTime);
    }
    IEnumerator MoveDOWN(){
        inventory.SelectSlot(UIMovementEnum.DOWN);
        yield return new WaitForSeconds(_delayTime);
    }
    IEnumerator MoveLEFT(){
        inventory.SelectSlot(UIMovementEnum.LEFT);
        yield return new WaitForSeconds(_delayTime);
    }
    IEnumerator MoveRIGHT(){
        inventory.SelectSlot(UIMovementEnum.RIGHT);
        yield return new WaitForSeconds(_delayTime);
    }
}
