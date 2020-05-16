using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class GentlemansRapier : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Gentleman's Rapier");
            Tooltip.SetDefault(@"Shoots spooky dapper top hats
Right clicking thrusts the blade forward
Left clicking swings the blade
'Spoopy'
-Tied");
		}

		public override void SetDefaults()
		{
			item.damage = 200;
			item.melee = true;
			item.width = 64;
			item.height = 66;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 3;
			item.value = 100000;
			item.rare = ItemRarityID.Purple;
            item.shoot = mod.ProjectileType("TopHat");
            item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.shootSpeed = 12f;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(0, 105, 0);
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
                item.useStyle = ItemUseStyleID.Stabbing;
            }
            else
            {
                item.useStyle = ItemUseStyleID.SwingThrow;
            }
            return base.CanUseItem(player);
		}
	}
}