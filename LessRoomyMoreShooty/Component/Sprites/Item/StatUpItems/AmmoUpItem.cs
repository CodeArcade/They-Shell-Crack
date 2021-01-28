using System.Drawing;

namespace LessRoomyMoreShooty.Component.Sprites.Item
{
    public class AmmoUpItem : Item
    {
        public AmmoUpItem() : base("Ammo Up", "Ammo Up | Reloading takes longer")
        {
            Texture = ContentManager.AmmoUpTexture;
            Size = new Size(50, 50);
        }

        public override void OnPickup(Player player)
        {
            player.MaxAmmo += 3;
            player.CurrentAmmo = player.MaxAmmo;
            player.ReloadTimeInSeconds += 0.20f;
        }
    }
}
