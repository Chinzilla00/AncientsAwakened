using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Akuma
{
    public class CrucibleScale : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crucible Scale");
            Tooltip.SetDefault("The fury of the draconian sun eminates from this scale");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 4));
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Akuma;
                }
            }
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.maxStack = 999;
            item.value = Item.buyPrice(0, 1, 0, 0);
            item.rare = 11;
        }

        // The following 2 methods are purely to show off these 2 hooks. Don't use them in your own code.
        

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.OrangeRed.ToVector3() * 0.55f * Main.essScale);
        }
    }
}