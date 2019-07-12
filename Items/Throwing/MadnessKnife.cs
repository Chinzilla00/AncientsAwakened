using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Throwing
{
	public class MadnessKnife : BaseAAItem
	{
		public override void SetDefaults()
		{
			item.damage = 13;
			item.ranged = true;
			item.width = 14;
			item.height = 32;
			item.noUseGraphic = true;
			item.maxStack = 999;
			item.consumable = true;
			item.useTime = 15;
			item.useAnimation = 15;
			item.shoot = mod.ProjectileType("MadnessKnifeP");
			item.shootSpeed = 12f;
			item.useStyle = 1;
			item.knockBack = 2;
			item.value = Item.sellPrice(0, 0, 0, 25);
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.noMelee = true;
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Madness Knife");
			Tooltip.SetDefault("");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("MadnessFragment"));
			recipe.SetResult(this, 75);
			recipe.AddTile(TileID.Anvils);
			recipe.AddRecipe();
		}
	}
}
