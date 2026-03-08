using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/Open")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "Open", message: "Open", category: "Events", id: "ad8c457a792d9f7bdddbe5e776ed5d33")]
public sealed partial class Discovery : EventChannel { }