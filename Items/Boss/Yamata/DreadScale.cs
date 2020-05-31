using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Boss.Yamata
{
    public class DreadScale : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dread Scale");
            Tooltip.SetDefault("The power of the dread moon is in your hands");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 9));
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 34;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = ItemRarityID.Cyan; AARarity = 13;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Globals.AAColor.Rarity13;
                }
            }
        }


        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.Indigo.ToVector3() * 0.55f * Main.essScale);
        }
    }
}