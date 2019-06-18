using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
	public class Fireblast : ModItem
	{
		public override void SetDefaults()
		{
			item.damage = 42;                        
			item.magic = true;
			item.width = 28;
			item.height = 30;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = Item.sellPrice(0, 3, 0, 0);
			item.rare = 4;
			item.mana = 10;
			item.UseSound = SoundID.Item21;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("FireblastP");
			item.shootSpeed = 8f;
		}   

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fireblast");
			Tooltip.SetDefault("Shoots an explosive bolt of dragonflame");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SpellTome);
			recipe.AddIngredient(mod.ItemType("DragonFire"), 20);
			recipe.AddIngredient(null, "SoulOfSmite", 15);
			recipe.AddTile(TileID.Bookcases);
			recipe.SetResult(this);  
			recipe.AddRecipe();
		}
	}
}
