using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMock : MonoBehaviour
{
    [SerializeField]
    private Item replica;
    [SerializeField]
    private ArrayInventory inventory;
    [SerializeField]
    private float _delayTime = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
            bool res = inventory.AddItem(replica);
            Debug.Log("Item inserted? " + res);
        }
        if(Input.GetKeyDown("e")){
            
            Debug.Log("Item inserted? ");
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
