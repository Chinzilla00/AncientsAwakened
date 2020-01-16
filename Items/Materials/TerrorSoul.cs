using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class TerrorSoul : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terror Soul");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 3));
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }



        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity12;
                }
            }
        }


        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.maxStack = 999;
            item.value = 1000;
            item.rare = 10;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.PaleVioletRed.ToVector3() * 0.55f * Main.essScale);
        }
    }
}