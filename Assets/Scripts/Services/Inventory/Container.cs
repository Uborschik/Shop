using Game.Entities.Items;

namespace Game.Services.Inventory
{
    public class Container
    {
        public Item[] Inventory { get; private set; }

        public Container(int count)
        {
            Inventory = new Item[count];
        }

        public bool TryGetPushIndex(out int index)
        {
            if (Inventory != null)
            {
                for (int i = 0; i < Inventory.Length; i++)
                {
                    if (Inventory[i] != null) continue;

                    index = i;
                    return true;
                }
            }

            index = -1;
            return false;
        }

        public bool TryGetPullIndex(out int index)
        {
            if (Inventory != null)
            {
                for (int i = Inventory.Length - 1; i >= 0; i--)
                {
                    if (Inventory[i] == null) continue;

                    index = i;
                    return true;
                }
            }

            index = -1;
            return false;
        }

        public void Push(int index, Item item)
        {
            if (item == null) return;

            Inventory[index] = item;
        }

        public Item Pull(int index)
        {
            if (Inventory == null) return null;

            var item = Inventory[index];
            Inventory[index] = null;
            return item;
        }
    }
}
