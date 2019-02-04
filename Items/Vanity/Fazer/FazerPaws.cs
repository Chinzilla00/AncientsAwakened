using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Fazer
{

    [AutoloadEquip(EquipType.Wings)]
    public class FazerPaws : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Sammy's Paws");
            Tooltip.SetDefault("'Great for impersonating Ancients Awakened Devs!'");
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 24;
			item.value = 500000;
			item.rare = 11;
			item.accessory = true;
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

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.wingTimeMax = 260;
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 0.85f;
			ascentWhenRising = 0.15f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 3f;
			constantAscend = 0.135f;
		}

		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
			speed = 9f;
			acceleration *= 2.5f;
		}
	}
}
