namespace LessRoomyMoreShooty.Component.Sprites.Item.PickUpItems
{
    public class SmallHealItem : Item
    {
        public SmallHealItem() : base("Small Heal", "Better than nothing.")
        {
            Texture = ContentManager.ButtonTexture;
        }

        public override void OnPickup(Player player)
        {
            if (player.CurrentHealth < player.MaxHealth - 1)
                player.CurrentHealth += 1;
        }
    }
}
