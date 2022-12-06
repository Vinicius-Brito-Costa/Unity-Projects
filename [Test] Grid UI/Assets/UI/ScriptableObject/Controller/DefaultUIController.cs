using UnityEngine;

[CreateAssetMenu(fileName = "DefaultUIController", menuName = "Inventory/UIController/DefaultUIController", order = 50)]
public class DefaultUIController : SO_BaseUIController{
    public override UIControlEnum GetButtonPressed(){
        UIControlEnum buttonPressed = UIControlEnum.NOT_PRESSED;
        if (Input.GetKeyDown("w"))
        {
            buttonPressed = UIControlEnum.UP;
        }
        if (Input.GetKeyDown("s"))
        {
            buttonPressed = UIControlEnum.DOWN;
        }
        if (Input.GetKeyDown("a"))
        {
            buttonPressed = UIControlEnum.LEFT;
        }
        if (Input.GetKeyDown("d"))
        {
            buttonPressed = UIControlEnum.RIGHT;
        }
        if(Input.GetKeyDown("space")){
            buttonPressed = UIControlEnum.ACTION;
        }
        if(Input.GetKeyDown("r")){
            buttonPressed = UIControlEnum.RETURN;
        }

        return buttonPressed;
    }
}