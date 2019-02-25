using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Beg
{
	public class Pony : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Weird Horse Doll");
			Tooltip.SetDefault("Neigh.");
		}

		public override void SetDefaults()
		{
			item.width = 38;
			item.height = 42;
			item.accessory = true;
			item.rare = 10;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			AAPlayer p = player.GetModPlayer<AAPlayer>();
			p.BegAccessory = true;
			if (hideVisual)
			{
				p.BegHideVanity = true;
			}
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(0, 130, 150);
                }
            }
        }
    }


    public class Pony_Head : EquipTexture
	{
		public override bool DrawHead()
		{
			return false;
		}
	}

	public class Pony_Body : EquipTexture
	{
		public override bool DrawBody()
		{
			return false;
		}
	}

	public class Pony_Legs : EquipTexture
	{
		public override bool DrawLegs()
		{
			return false;
		}
	}
}