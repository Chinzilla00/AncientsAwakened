using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class The_Plague : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Plague");
			ItemID.Sets.Yoyo[item.type] = true;
			ItemID.Sets.GamepadExtraRange[item.type] = 15;
			ItemID.Sets.GamepadSmartQuickReach[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.useStyle = 5;
			item.width = 30;
			item.height = 26;
			item.useAnimation = 25;
			item.useTime = 25;
			item.shootSpeed = 16f;
			item.knockBack = 2.5f;
			item.damage = 400;
			item.rare = 10;
			item.melee = true;
			item.channel = true;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.UseSound = SoundID.Item1;
			item.value = Item.sellPrice(silver: 1);
			item.shoot = mod.ProjectileType("The_Plague_Pro");
		}

		/*public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.WoodYoyo);
            recipe.AddIngredient(ItemID.Rally);
            recipe.AddIngredient(ItemID.CorruptYoyo);
            recipe.AddIngredient(ItemID.CrimsonYoyo);
            recipe.AddIngredient(ItemID.JungleYoyo);
            recipe.AddIngredient(ItemID.Code1);
            recipe.AddIngredient(ItemID.Valor);
            recipe.AddIngredient(ItemID.Cascade);
            recipe.AddIngredient(ItemID.FormatC);
            recipe.AddIngredient(ItemID.Gradient);
            recipe.AddIngredient(ItemID.Chik);
            recipe.AddIngredient(ItemID.HelFire);
            recipe.AddIngredient(ItemID.Amarok);
            recipe.AddIngredient(ItemID.Code2);
            recipe.AddIngredient(ItemID.Yelets);
            recipe.AddIngredient(ItemID.RedsYoyo);
            recipe.AddIngredient(ItemID.ValkyrieYoyo);
            recipe.AddIngredient(ItemID.Kraken);
            recipe.AddIngredient(ItemID.TheEyeOfCthulhu);
            recipe.AddIngredient(ItemID.Terrarian);
            recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}*/
	}
}
