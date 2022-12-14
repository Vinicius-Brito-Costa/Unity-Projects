using UnityEngine;
public class Submenu : MonoBehaviour, ISubmenu
{
    public void Action(ISlot slot)
    {
        Debug.Log("Action!");
    }

    public void Move(UIControlEnum move)
    {
        Debug.Log("Move! " + move);
    }
}