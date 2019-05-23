using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class Stardust : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            DisplayName.SetDefault("Radiant Photon");
            Tooltip.SetDefault("A shard of the heavenly cosmos");
        }
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 48;
            item.maxStack = 99;
            item.rare = 11;
            item.value = 10000;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Glow;
        }
    }
}
