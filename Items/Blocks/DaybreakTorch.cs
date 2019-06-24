using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
	public class DaybreakTorch : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Daybreak Torch");
		}

		public override void SetDefaults()
		{
			item.width = 10;
			item.height = 12;
			item.maxStack = 99;
			item.holdStyle = 1;
			item.noWet = true;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.createTile = mod.TileType("DaybreakTorch");
			item.flame = true;
            AARarity = 13;
            item.value = Item.sellPrice(0, 0, 25, 0);
        }

		public override void HoldItem(Player player)
		{
			if (Main.rand.Next(player.itemAnimation > 0 ? 40 : 80) == 0)
			{
				Dust.NewDust(new Vector2(player.itemLocation.X + 16f * player.direction, player.itemLocation.Y - 14f * player.gravDir), 4, 4, mod.DustType("AkumaADust"));
			}
			Vector2 position = player.RotatedRelativePoint(new Vector2(player.itemLocation.X + 12f * player.direction + player.velocity.X, player.itemLocation.Y - 14f + player.velocity.Y), true);
			Lighting.AddLight(position, AAColor.AkumaA.R / 255, AAColor.AkumaA.G / 255, AAColor.AkumaA.B / 255);
		}

		public override void PostUpdate()
		{
			if (!item.wet)
			{
				Lighting.AddLight((int)((item.position.X + item.width / 2) / 16f), (int)((item.position.Y + item.height / 2) / 16f), AAColor.AkumaA.R / 255, AAColor.AkumaA.G / 255, AAColor.AkumaA.B / 255);
			}
		}

		public override void AutoLightSelect(ref bool dryTorch, ref bool wetTorch, ref bool glowstick)
		{
			dryTorch = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Torch, 50);
			recipe.AddIngredient(null, "DaybreakIncinerite");
			recipe.SetResult(this, 50);
			recipe.AddRecipe();
		}
	}
}