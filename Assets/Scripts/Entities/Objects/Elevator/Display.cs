using TMPro;
using UnityEngine;

namespace Game.Entities.Objects.Elevator
{
    public class Display : MonoBehaviour
    {
        [SerializeField] private TMP_Text target;
        [SerializeField] private TMP_Text current;

        public void SetTarget(int value)
        {
            target.text = value.ToString();
        }

        public void SetCurrent(int value)
        {
            current.text = value.ToString();
        }
    }
}