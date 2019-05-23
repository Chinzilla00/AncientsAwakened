using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
namespace AAmod.Items.Dev
{
    public class Etheral : ModItem
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
			item.width = 24;
			item.height = 28;
            item.useTurn = false;
            item.value = Item.sellPrice(0, 1, 0, 0);
			item.damage = 150;
            item.mana = 9;
            item.useStyle = 5;
			item.useTime = 10;
			item.useAnimation = 7;
			item.reuseDelay = 5;
			item.magic = true;
			item.channel = true;
			item.noMelee = true;
			item.shoot = mod.ProjectileType("EtheralLazer");
			item.shootSpeed = 26f;
		}
	}
}
