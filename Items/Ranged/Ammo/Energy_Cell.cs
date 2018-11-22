using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Ranged.Ammo
{
	public class Energy_Cell : ModItem
	{
		public override void SetDefaults()
		{
			item.damage = 5;
			item.width = 8;
			item.height = 16;
			item.maxStack = 999;
			item.value = Item.sellPrice(0, 0, 1, 0);
			item.rare = 5;
			item.consumable = true;
			item.shoot = mod.ProjectileType("Energy_Cell_Pro");
			item.ammo = item.type;
			item.glowMask = customGlowMask;
		}
		
		public static short customGlowMask = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Energy Cell");
			if (Main.netMode != 2)
			{
				Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					glowMasks[i] = Main.glowMaskTexture[i];
				}
				glowMasks[glowMasks.Length - 1] = mod.GetTexture("Items/Ranged/Ammo/" + this.GetType().Name + "_Glowmask");
				customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
		}
	}
}
