namespace LessRoomyMoreShooty.Component.Sprites.Item
{
    public class DamageUpItem : Item
    {
        public DamageUpItem() : base("Damage Up", "Damage +1 | Range -0.25")
        {
            Texture = ContentManager.ButtonTexture;
        }

        public override void OnPickup(Player player)
        {
            player.Damage += 1;
            player.RangeInSeconds -= 0.25f;

            if (player.RangeInSeconds <= 0.25f) player.RangeInSeconds = 0.25f;
        }
    }
}
