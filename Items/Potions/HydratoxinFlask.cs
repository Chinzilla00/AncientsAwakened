using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Potions
{
    public class HydratoxinFlask : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flask of Hydratoxin");
			Tooltip.SetDefault("Melee attacks inflict hydratoxin");
		}    

		public override void SetDefaults()
		{
			item.UseSound = SoundID.Item3;
			item.useStyle = ItemUseStyleID.EatingUsing;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 15;
			item.maxStack = 30;
			item.consumable = true;
			item.width = 22;
			item.height = 28;
			item.value = Item.sellPrice(0, 5, 0, 0);
			item.rare = ItemRarityID.Orange;
			item.buffType = mod.BuffType("HydratoxinFlaskBuff");
			item.buffTime = 52000;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddIngredient(mod.ItemType("HydraToxin"), 2);
			recipe.AddTile(TileID.ImbuingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
