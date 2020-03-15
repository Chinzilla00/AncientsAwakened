using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
	public class Toxifang : BaseAAItem
	{
		public override void SetDefaults()
		{
			item.damage = 30;                        
			item.magic = true;
			item.width = 28;
			item.height = 30;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = Item.sellPrice(0, 3, 0, 0);
			item.rare = 4;
			item.mana = 10;
			item.UseSound = SoundID.Item21;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Toxifang");
			item.shootSpeed = 8f;
		}   

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Toxifang");
			Tooltip.SetDefault("Shoots Toxic Fangs");
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 perturbedSpeed = new Vector2(speedX*2, speedY*2).RotatedByRandom(MathHelper.ToRadians(8));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
		}
		
		public override void AddRecipes()  
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SpellTome);
			recipe.AddIngredient(ItemID.SpellTome);
			recipe.AddIngredient(mod.ItemType("Hydratoxin"), 20);
			recipe.AddIngredient(mod.ItemType("SoulOfSpite"), 15);
			recipe.AddTile(TileID.Bookcases);
			recipe.SetResult(this);  
			recipe.AddRecipe();
		}
	}
}
