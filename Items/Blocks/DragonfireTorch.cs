using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
	public class DragonfireTorch : BaseAAItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dragonfire Torch");
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
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createTile = mod.TileType("DragonfireTorch");
			item.flame = true;
            item.rare = ItemRarityID.Orange;
            item.value = Item.sellPrice(0, 0, 1, 0);
		}

		public override void HoldItem(Player player)
		{
			if (Main.rand.Next(player.itemAnimation > 0 ? 40 : 80) == 0)
			{
				Dust.NewDust(new Vector2(player.itemLocation.X + 16f * player.direction, player.itemLocation.Y - 14f * player.gravDir), 4, 4, ModContent.DustType<Dusts.DragonflameDust>());
			}
			Vector2 position = player.RotatedRelativePoint(new Vector2(player.itemLocation.X + 12f * player.direction + player.velocity.X, player.itemLocation.Y - 14f + player.velocity.Y), true);
			Lighting.AddLight(position, Globals.AAColor.DragonFire.R / 255, Globals.AAColor.DragonFire.G / 255, Globals.AAColor.DragonFire.B / 255);
		}

		public override void PostUpdate()
		{
			if (!item.wet)
			{
				Lighting.AddLight((int)((item.position.X + item.width / 2) / 16f), (int)((item.position.Y + item.height / 2) / 16f), Globals.AAColor.DragonFire.R / 255, Globals.AAColor.DragonFire.G / 255, Globals.AAColor.DragonFire.B / 255);
			}
		}

		public override void AutoLightSelect(ref bool dryTorch, ref bool wetTorch, ref bool glowstick)
		{
			dryTorch = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Torch, 33);
			recipe.AddIngredient(null, "DragonFire");
			recipe.SetResult(this, 33);
			recipe.AddRecipe();
		}
	}
}