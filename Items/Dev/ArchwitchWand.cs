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
            DisplayName.SetDefault("Archwitch's Galactic Scepter");
            Tooltip.SetDefault(@"An old wand. It seems to have not been used recently.");
        }

        public override void SetDefaults()
        {
            item.damage = 100;
            item.magic = true;
            item.width = 42;
            item.height = 46;
            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = 5;
            item.knockBack = 6f;
            item.value = Item.buyPrice(0, 40, 0, 0);
            item.rare = 11;                  
            item.shoot = mod.ProjectileType("ArchwitchSorm");
            item.noUseGraphic = false;
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