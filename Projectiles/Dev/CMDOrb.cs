using AAMod.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace AAMod.Projectiles.Dev
{
    public class CMDOrb : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Healing Soul");
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return ColorUtils.COLOR_GLOWPULSE;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, ColorUtils.COLOR_GLOWPULSE.ToVector3() * 0.55f * Main.essScale);
        }

        public override void GrabRange(Player player, ref int grabRange)
        {
            grabRange += 100;
        }

        public override bool OnPickup(Player player)
        {
            Main.PlaySound(7, (int)player.position.X, (int)player.position.Y, 1, 1f, 0f);

            player.statLife += player.statLifeMax2 / 12;

            item.TurnToAir();
            return true;
        }
    }
}