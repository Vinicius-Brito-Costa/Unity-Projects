using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playable : IPlayable
{
    private Controller controller;

    [SerializeField]
    private float speed = 1;

    [SerializeField]
    private float waitTimeToChange = 0.5f;
    private float timeSinceControlledChanged = 0;

    public override void Control()
    {
        if (Input.GetKey("w"))
        {
            Move(new Vector3(0, 0, 1));
        }
        if (Input.GetKey("s"))
        {
            Move(new Vector3(0, 0, -1));
        }
        if (Input.GetKey("a"))
        {
            Move(new Vector3(-1, 0, 0));
        }
        if (Input.GetKey("d"))
        {
            Move(new Vector3(1, 0, 0));
        }

    }

    public override void Move(Vector3 vec)
    {
        Vector3 position = ((vec * Time.deltaTime) * speed) + this.transform.localPosition;
        this.transform.localPosition = position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other != null && controller != null)
        {
            if (other.gameObject != null)
            {
                controller.ChangeControlledObject(other);
            }
        }
    }

    public override void SetControlled(Controller controller)
    {
        if (controller != null)
        {
            this.controller = controller;
            Rigidbody rigidbody = this.gameObject.AddComponent<Rigidbody>();
            rigidbody.useGravity = false;
            this.gameObject.GetComponent<Collider>().isTrigger = true;
        }
    }

    public override bool ReadyToChangeObject()
    {
        return timeSinceControlledChanged > waitTimeToChange;
    }

    public override void AddToChangeTimer(float time)
    {
        timeSinceControlledChanged += time;
    }
    public override void ResetChangeTimer()
    {
        timeSinceControlledChanged = 0;
    }
    public override void ChangeControlledObject()
    {

    }
    public override void SetUncontrolled()
    {
        this.controller = null;
        Destroy(this.gameObject.GetComponent<Rigidbody>());
        this.gameObject.GetComponent<Collider>().isTrigger = false;
    }

    public override GameObject GetGameObject()
    {
        return this.gameObject;
    }
}
