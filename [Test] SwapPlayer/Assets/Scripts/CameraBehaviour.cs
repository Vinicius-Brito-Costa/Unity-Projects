using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField]
    private Controller target;
    [SerializeField]
    private float duration = 2;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector3 defaultDistance;
    private Transform cameraTransform;
    private Transform targetTransform;
    private Vector3 newPosition;
    private Vector3 reference = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        this.cameraTransform = this.GetComponent<Transform>();
        newPosition = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        if (this.target != null)
        {
            if (this.targetTransform == null || this.target.ControlledObjectChanged())
            {
                this.targetTransform = this.target.GetControlledObject().transform;
                if (this.target.ControlledObjectChanged())
                {
                    this.target.SetControlledObjectChanged(false);
                }

            }
            Vector3 currentPos = this.cameraTransform.localPosition;
            Vector3 desiredPos = this.targetTransform.localPosition + this.defaultDistance;

            this.newPosition = Vector3.SmoothDamp(currentPos, desiredPos, ref reference, duration / 100);
            this.cameraTransform.localPosition = newPosition;
        }
    }
}
