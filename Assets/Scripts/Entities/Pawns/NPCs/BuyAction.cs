using Game.Entities.Pawns.NPCs;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Buy", story: "[Buyer] [interact] with [Target]", category: "Action", id: "ccba9a048f76adfbb116ade194adce3c")]
public partial class BuyAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Buyer;
    [SerializeReference] public BlackboardVariable<Buyer> Interact;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    protected override Status OnStart()
    {
        return Interact.Value.TryBuy(Target.Value.transform) ? Status.Success : Status.Failure;
    }
}

