using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class TitanAxe : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Titan Axe");
            Tooltip.SetDefault("Right clicking throws the axe. \n" + "Left clicking swings the axe. \n" + "'Oof this isn't google' \n'" + "-Welox");
		}

		public override void SetDefaults()
		{
			item.damage = 200;
			item.melee = true;
			item.width = 72;
			item.height = 72;
			item.useTime = 26;
			item.useAnimation = 26;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 100000;
			item.rare = 11;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(40, 255, 40);
                }
            }
        }

        public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{

            if (player.altFunctionUse == 2)
            {
                item.useStyle = 1;
                item.useTime = 26;
                item.useAnimation = 26;
                item.melee = true;
                item.damage = 200;
                item.shoot = mod.ProjectileType("TitanAxe");
                item.shootSpeed = 12f;
                item.noMelee = true;
                item.noUseGraphic = true;
            }
            else
            {
                item.useStyle = 1;
                item.useTime = 26;
                item.useAnimation = 26;
                item.melee = true;
                item.thrown = false;
                item.shoot = 0;
                item.damage = 200;
                item.noMelee = false;
                item.noUseGraphic = false;
            }
            return base.CanUseItem(player);
		}
	}
}