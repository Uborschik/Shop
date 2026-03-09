using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        public int PrefabID { get; set; }
    }

    [CreateAssetMenu(fileName = "DataBase", menuName = "Game/DataBase")]
    public class DataBase : ScriptableObject
    {
        [SerializeField] private List<Entity> prefabs;

        [ContextMenu("GenerateID")]
        private void GenerateID()
        {
            for (int i = 1; i < prefabs.Count; i++)
            {
                prefabs[i].PrefabID = i;
            }
        }
    }
}