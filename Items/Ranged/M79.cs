using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class M79 : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("M79");
			Tooltip.SetDefault("Uses M79 Rounds as ammo\n33% chance not to consume ammo");
        }

		public override void SetDefaults()
		{
			item.useStyle = 5;
			item.autoReuse = false;
			item.useAnimation = 75;
			item.useTime = 75;
			item.width = 68;
			item.height = 24;
			item.shoot = mod.ProjectileType("M79P");
			item.UseSound = SoundID.Item61;
			item.damage = 150;
			item.shootSpeed = 11f;
			item.noMelee = true;
			item.value = 50000;
			item.knockBack = 6f;
			item.rare = 3;
			item.ranged = true;
			item.useAmmo = mod.ItemType("M79Round");
		}
		
		public override bool ConsumeAmmo(Player player)
		{
		return Main.rand.NextFloat() >= .33;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-18, 0);
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("M79Parts"));
			recipe.AddIngredient(ItemID.IllegalGunParts);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
