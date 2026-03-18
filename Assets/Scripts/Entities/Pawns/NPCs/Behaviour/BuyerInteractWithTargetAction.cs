//using Game.Entities;
//using Game.Entities.Objects;
//using Game.Entities.Pawns.NPCs;
//using System;
//using Unity.Behavior;
//using Unity.Properties;
//using UnityEngine;
//using Action = Unity.Behavior.Action;

//[Serializable, GeneratePropertyBag]
//[NodeDescription(name: "Buyer interact with target", story: "[Buyer] interact with [Target]", category: "Action", id: "d009a1261aa91d5f5e6536c9a2993b12")]
//public partial class BuyerInteractWithTargetAction : Action
//{
//    [SerializeReference] public BlackboardVariable<Buyer> Buyer;
//    [SerializeReference] public BlackboardVariable<InteractableObject> Target;
//    protected override Status OnStart()
//    {
//        var result = Buyer.Value.InteractWith(Target.Value.transform);

//        return result == InteractionResult.Success ? Status.Running : Status.Success;
//    }
//}

