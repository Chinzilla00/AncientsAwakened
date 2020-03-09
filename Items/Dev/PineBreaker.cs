using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class PineBreaker : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Pine Breaker");
            Tooltip.SetDefault(@"'I don't like egg'
-Planterror");
			Item.staff[item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
		}

		public override void SetDefaults()
		{
			item.damage = 80;
			item.magic = true;
			item.mana = 7;
			item.width = 66;
			item.height = 64;
			item.useTime = 32;
			item.useAnimation = 32;
			item.useStyle = 5;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 4;
			item.rare = 9;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Pine");
			item.shootSpeed = 9f;
            item.expert = true; 
            item.expertOnly = true;
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(91, 149, 91);
                }
            }
        }
    }
}