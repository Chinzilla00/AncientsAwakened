using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.CC
{
	[AutoloadEquip(EquipType.Body)]
	internal class CCRobe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dread Cultist Robe");
			Tooltip.SetDefault(@"The hood of a crazy lizard enthusiast
'Great for impersonating Ancients Awakened Developers!'");
		}
		public override void SetDefaults() 
		{
			item.width = 18;
			item.height = 14;
			item.rare = 1;
			item.vanity = true;
		}

		public override void SetMatch(bool male, ref int equipSlot, ref bool robes) 
		{
			robes = true;
			equipSlot = mod.GetEquipSlot("CCRobe_Legs", EquipType.Legs);
		}

		public override void DrawHands(ref bool drawHands, ref bool drawArms) 
		{
			drawHands = true;
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine line2 in list)
			{
				if (line2.mod == "Terraria" && line2.Name == "ItemName")
				{
					line2.overrideColor = new Color(92, 101, 150);
				}
			}
		}
	}
}
