using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPlayable : MonoBehaviour, IMovable
{
    public abstract void SetControlled(Controller defaultMovement);
    public abstract void SetUncontrolled();
    public abstract GameObject GetGameObject();
    public abstract void Control();
    public abstract void Move(Vector3 vec);
    public abstract bool ReadyToChangeObject();
    public abstract void AddToChangeTimer(float time);
    public abstract void ResetChangeTimer();
    public abstract void ChangeControlledObject();
}
