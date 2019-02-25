using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class Stardust : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 3));
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            DisplayName.SetDefault("Stardust");
            Tooltip.SetDefault("Fragments from the celestial sky");
        }
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 48;
            item.maxStack = 99;
            item.rare = 11;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Glow;
        }
    }
}
