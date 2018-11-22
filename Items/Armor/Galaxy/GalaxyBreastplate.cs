using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Armor.Galaxy
{
	[AutoloadEquip(EquipType.Body)]
	public class GalaxyBreastplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Galactic Breastplate");
			Tooltip.SetDefault("+24% thrown damage"
				+ "\n+30% thrown velocity");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 20;
			item.value = Item.sellPrice(0, 4, 0, 0);
			item.rare = 10;
			item.defense = 16;
		}

		public override void UpdateEquip(Player player)
		{
			player.thrownDamage += 0.24f;
			player.thrownVelocity += 0.30f;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "Galaxy_Fragment", 20);
			recipe.AddIngredient(ItemID.LunarBar, 16);
			recipe.SetResult(this);
			recipe.AddTile(null, "Soul_Forge_Placed");
			recipe.AddRecipe();
		}
	}
}

		/*public override void DrawArmorColor(Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor)
		{
			Texture2D texture = mod.GetTexture("Glowmasks/MechBody_Body_Glowmask");
			{
				color = drawPlayer.GetImmuneAlphaPure(Color.White, shadow);
			}
		}*/