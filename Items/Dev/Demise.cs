using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Dev
{
    public class Demise : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Demise");
			Tooltip.SetDefault(@"A legendary sword that was once wielded by the demon king");
		}
		public override void SetDefaults()
		{
			item.damage = 70;
			item.melee = true;
			item.width = 58;
			item.height = 58;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 1;
			item.knockBack = 5;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 9;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Demise");
            item.shootSpeed = 9f;
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(77, 20, 102);
                }
            }
        }
	}
}
