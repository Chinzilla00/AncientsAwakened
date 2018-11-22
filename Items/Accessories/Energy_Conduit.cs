using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AAMod.Items.Accessories
{
	public class Energy_Conduit : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 24;
			item.value = Item.sellPrice(0, 6, 0, 0);
			item.rare = 8;
			item.accessory = true;
			item.glowMask = customGlowMask;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
            player.moveSpeed *= 1.5f;
		}
		
		public static short customGlowMask = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Energy Conduit");
			Tooltip.SetDefault("50% increased movement speed");
			if (Main.netMode != 2)
			{
				Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					glowMasks[i] = Main.glowMaskTexture[i];
				}
				glowMasks[glowMasks.Length - 1] = mod.GetTexture("Items/Accessories/" + this.GetType().Name + "_Glowmask");
				customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
		}
	}
}
