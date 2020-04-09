using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace AAMod.Items.Dev
{
    public class ArchwitchWand : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Wand");
            Tooltip.SetDefault(@"An old wand. It seems to have not been used recently.");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 100;
            item.magic = true;
            item.width = 42;
            item.height = 46;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.knockBack = 6f;
            item.value = Item.buyPrice(0, 20, 0, 0);
            item.rare = 11;                  
            item.shoot = mod.ProjectileType("ArchwitchSorm");
            item.shootSpeed = 10;
            item.noMelee = true;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(121, 21, 214);
                }
            }
        }
    }
}