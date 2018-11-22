using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Yamata
{
    public class DreadScale : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dread Scale");
            Tooltip.SetDefault("The power of the dread moon is in your hands");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 9));
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(45, 46, 70);
                }
            }
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 34;
            item.maxStack = 999;
            item.value = 10000;
            item.rare = 11;
        }

        // The following 2 methods are purely to show off these 2 hooks. Don't use them in your own code.
        

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.Indigo.ToVector3() * 0.55f * Main.essScale);
        }
    }
}