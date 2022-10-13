using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField]
    private IPlayable controlledObject;
    [SerializeField]
    private bool touchSwitch = false;

    private bool changed = false;

    // Start is called before the first frame update
    void Start()
    {

        if (this.controlledObject != null)
        {
            this.controlledObject.SetControlled(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        controlledObject.AddToChangeTimer(Time.deltaTime);
        controlledObject.Control();
    }

    public void ChangeControlledObject(Collider other)
    {
        if (touchSwitch && controlledObject.ReadyToChangeObject())
        {
            IPlayable playable = other.gameObject.GetComponent<IPlayable>();
            if (playable != null && playable != this.controlledObject)
            {
                Debug.Log("Controlled Object: " + this.controlledObject);
                this.controlledObject.SetUncontrolled();
                this.controlledObject = playable;
                this.controlledObject.SetControlled(this);
                Debug.Log("New Controlled Object: " + this.controlledObject);
                this.changed = true;
                controlledObject.ResetChangeTimer();
            }
        }
    }
    public GameObject GetControlledObject()
    {
        return this.controlledObject.GetGameObject();
    }

    public bool ControlledObjectChanged()
    {
        return this.changed;
    }

    public void SetControlledObjectChanged(bool state)
    {
        this.changed = state;
    }
}
