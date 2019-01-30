using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class Fluff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fluff");
            Tooltip.SetDefault(@"Throws hairballs that slow your opponents
'owO'
-Fazer");
        }

        public override void SetDefaults()
        {
            item.damage = 120;
            item.thrown = true;
            item.width = 32;
            item.height = 32;
            item.noUseGraphic = true;
            item.useTime = 25;
            item.useAnimation = 25;
            item.shoot = mod.ProjectileType("Fluff");
            item.shootSpeed = 10f;
            item.useStyle = 1;
            item.knockBack = 0;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(77, 99, 118);
                }
            }
        }
    }
}