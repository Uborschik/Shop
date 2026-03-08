using Game.Entities.Tools;
using System;
using UnityEngine;

namespace Game.Entities.Pawns.Player
{
    [Serializable]
    public class ToolHolder
    {
        [SerializeField] private Tool tool;

        public Tool Tool => tool;

        public bool IsEmpty() => tool == null;
        public void SetTool(Tool tool) => this.tool = tool;
    }
}