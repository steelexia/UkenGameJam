using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
    [TaskCategory("Basic/Rigidbody")]
    [TaskDescription("Applies a torque to the rigidbody relative to its coordinate system. Returns Success.")]
    public class AddRelativeTorque : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        [Tooltip("The amount of torque to apply")]
        public SharedVector3 torque;
        [Tooltip("The type of torque")]
        public ForceMode forceMode = ForceMode.Force;

        // cache the rigidbody component
        private Rigidbody targetRigidbody;

        public override void OnStart()
        {
            targetRigidbody = GetDefaultGameObject(targetGameObject.Value).GetComponent<Rigidbody>();
        }

        public override TaskStatus OnUpdate()
        {
            targetRigidbody.AddRelativeTorque(torque.Value, forceMode);

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            targetGameObject = null;
            torque = Vector3.zero;
            forceMode = ForceMode.Force;
        }
    }
}