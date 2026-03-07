using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "In range", story: "[Self] in range [Target]", category: "Conditions", id: "b3c25163feb451748f111bf668c1a487")]
public partial class InRangeCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    public override bool IsTrue()
    {
        return Vector3.Distance(Self.Value.transform.position, Target.Value.transform.position) <= 0;
    }
}
