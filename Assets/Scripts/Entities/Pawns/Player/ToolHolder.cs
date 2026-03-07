using Game.Entities.Tools;
using System;
using UnityEngine;

namespace Game.Entities.Pawns.Player
{
    [Serializable]
    public class ToolHolder
    {
        [SerializeField] private TraderTool tool;

        public TraderTool Tool => tool;

        public bool IsEmpty() => tool == null;
        public void SetTool(TraderTool tool) => this.tool = tool;
    }
}