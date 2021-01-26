using System;
using System.Drawing;

namespace LessRoomyMoreShooty.Component.Sprites.Item.SpecialItem
{
    class ShotgunUpgrade : Item
    {
        public ShotgunUpgrade() : base("Shotgun Mode", "I own more shotguns than I need. But less shotguns than I want.")
        {
            Texture = ContentManager.ButtonTexture;
            Size = new Size(50, 50);
        }

        public override void OnPickup(Player player)
        {
            if (player.Spread < 50) player.Spread = 50;
            player.ShootAll = true;
            player.RangeInSeconds = Math.Min(player.RangeInSeconds, 0.8f);
            player.ReloadTimeInSeconds = Math.Max(player.ReloadTimeInSeconds, 2.1);
        }
    }
}
