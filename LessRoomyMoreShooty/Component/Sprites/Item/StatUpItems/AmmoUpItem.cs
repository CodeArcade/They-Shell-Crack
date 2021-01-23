namespace LessRoomyMoreShooty.Component.Sprites.Item
{
    public class AmmoUpItem : Item
    {
        public AmmoUpItem() : base("Ammo Up", "Ammo Up | Reloading takes longer")
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
