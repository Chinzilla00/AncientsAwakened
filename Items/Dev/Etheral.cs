using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class Etheral : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Etheral");
			Tooltip.SetDefault(" \"If in the wrong hands, it can cause devastating damage, so don't give it to me\" \n-TheRedstoneBro");
		}


        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(159, 207, 190);
                }
            }
        }

        public override void SetDefaults()
		{
            item.useStyle = 5;
            item.useAnimation = 7;
            item.useTime = 7;
            item.mana = 10;
            item.shootSpeed = 16f;
            item.knockBack = 0f;
            item.width = 122;
            item.reuseDelay = 5;
            item.height = 32;
            item.damage = 270;
            item.UseSound = SoundID.Item13;
            item.channel = true;
            item.shoot = mod.ProjectileType("Etheral");
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.noMelee = true;
            item.magic = true;
            item.autoReuse = true;
			item.noUseGraphic = true;
            
		}
	}
}
