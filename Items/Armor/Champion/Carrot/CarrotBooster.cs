using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace AAMod.Items.Armor.Champion.Carrot
{
    public class CarrotBooster : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Carrot Booster");
            Tooltip.SetDefault("Etheral, but crunchy.");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.AnimatesAsSoul[item.type] = true;
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofSight);
            item.width = refItem.width;
            item.height = refItem.height;
            item.maxStack = 999;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Main.DiscoColor;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Main.DiscoColor.ToVector3() * 0.55f * Main.essScale);
        }

        public override void GrabRange(Player player, ref int grabRange)
        {
            grabRange += 100;
        }

        public override bool OnPickup(Player player)
        {
            Main.PlaySound(7, (int)player.position.X, (int)player.position.Y, 1, 1f, 0f);
            if (Main.netMode != 1)
            {
                player.GetModPlayer<AAPlayer>().CarrotLevelup();
            }
            item.TurnToAir();
            return true;
        }
    }
}