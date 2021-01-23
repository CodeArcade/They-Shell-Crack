namespace LessRoomyMoreShooty.Component.Sprites.Item
{
    public abstract class Item : Sprite
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Item(string name, string description = "")
        {
            Name = name;
            Description =  description;
        }

        public abstract void OnPickup(Player player);
    }
}
