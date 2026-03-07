using Game.Entities.Items;

namespace Game.Services.Inventory
{
    public class Container
    {
        private readonly Item[] inventory;

        public Container(int count)
        {
            inventory = new Item[count];
        }

        public bool TryGetPushIndex(out int index)
        {
            if (inventory != null)
            {
                for (int i = 0; i < inventory.Length; i++)
                {
                    if (inventory[i] == null)
                    {
                        index = i;
                        return true;
                    }
                }
            }

            index = -1;
            return false;
        }

        public bool TryGetPullIndex(out int index)
        {
            if (inventory != null)
            {
                for (int i = inventory.Length - 1; i >= 0; i--)
                {
                    if (inventory[i] != null)
                    {
                        index = i;
                        return true;
                    }
                }
            }

            index = -1;
            return false;
        }

        public void Push(int index, Item item)
        {
            if (item == null) return;

            inventory[index] = item;
        }

        public Item Pull(int index)
        {
            if (inventory == null) return null;

            var item = inventory[index];
            inventory[index] = null;
            return item;
        }
    }
}
