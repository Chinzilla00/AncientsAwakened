using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Shen
{
    public class ChaosSoul : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Soul");
            Tooltip.SetDefault("Solid discord, symbolizing unrest and Anarchy itself");
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 4));
            ItemID.Sets.ItemNoGravity[item.type] = true;

        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item refItem = new Item();
            item.width = 22;
            item.height = 24;
            item.maxStack = 999;
            item.value = Item.sellPrice(1, 0, 0, 0);
            item.rare = 11;
            item.expert = true;
            item.alpha = 25;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Shen2;
                }
            }
        }
    }
}