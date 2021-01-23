namespace LessRoomyMoreShooty.Component.Sprites.Item
{
    public class AmmoUpItem : Item
    {
        public AmmoUpItem() : base("Ammo Up", "Ammo +1 | Reload Speed +0.25")
        {
            Texture =  ContentManager.ButtonTexture;
        }

        public override void OnPickup(Player player)
        {
            player.MaxAmmo += 1;
            player.CurrentAmmo = player.MaxAmmo;
            player.ReloadTimeInSeconds += 0.25f;
        }
    }
}
