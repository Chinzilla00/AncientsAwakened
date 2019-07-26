using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class MagicFlower : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Magic Flower");
            Tooltip.SetDefault("Pretty");
		}

		public override void SetDefaults()
		{
			item.damage = 17;
			item.magic = true;
			item.mana = 6;
			item.width = 32;
			item.height = 32;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 5;
			item.value = 1000;
			item.rare = 2;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("ManaShot");
			item.shootSpeed = 5f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.JungleSpores, 10);
            recipe.AddIngredient(ItemID.NaturesGift, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}