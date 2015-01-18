using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
    [TaskCategory("Basic/SharedVariable")]
    [TaskDescription("Sets the SharedTransformList variable to the specified object. Returns Success.")]
    public class SetSharedTransformList : Action
    {
        [Tooltip("The value to set the SharedTransformList to.")]
        public SharedTransformList targetValue;
        [Tooltip("The SharedTransformList to set")]
        public SharedTransformList targetVariable;

        public override TaskStatus OnUpdate()
        {
            targetVariable.Value = targetValue.Value;

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            if (targetValue != null) {
                targetValue.Value = null;
            }
            if (targetVariable != null) {
                targetVariable.Value = null;
            }
        }
    }
}