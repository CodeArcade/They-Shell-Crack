namespace LessRoomyMoreShooty.Component.Sprites.Item
{
    public class AttackSpeedUpItem : Item
    {
        public AttackSpeedUpItem() : base("Attack Speed Up", "Attack Speed Up | Increased Spray")
        {
            Texture = ContentManager.ButtonTexture;
        }

        public override void OnPickup(Player player)
        {
            player.AttackSpeedInSeconds -= 0.1;
            player.Spread += 5;

            if (player.AttackSpeedInSeconds <= 0.05f) player.AttackSpeedInSeconds = 0.05f;
        }
    }
}
