using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Dev
{
    public class Placeholder : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Placeholder");
			Tooltip.SetDefault(@"'They will die SoonTM'
-Fargo");
		}
		public override void SetDefaults()
		{
			item.damage = 220;
            item.useStyle = 1;
			item.magic = true;
            item.mana = 5;
            item.useAnimation = 27;
            item.useTime = 27;
            item.knockBack = 7f;
            item.width = 60;
            item.height = 56;
            item.scale = 1.15f;
            item.UseSound = SoundID.Item1;
            item.rare = 11;
            item.shootSpeed = 9f;
            item.value = 500000;
            item.autoReuse = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("SoonTM");
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(189, 76, 15);
                }
            }
        }
	}
}
