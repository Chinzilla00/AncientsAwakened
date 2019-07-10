using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class  Mushbow : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mushroom Bow");
        }

		public override void SetDefaults()
		{
			item.damage = 11;
			item.ranged = true;
			item.width = 20;
			item.height = 40;
			item.useTime = 24;
			item.useAnimation = 24;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 1;
            item.value = Item.sellPrice(0, 0, 10, 50) ;
			item.rare = 1;
			item.UseSound = SoundID.Item5;
			item.autoReuse = false;
			item.shoot = 10;
			item.shootSpeed = 7f;
			item.useAmmo = AmmoID.Arrow;
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Mushroom, 5);
            recipe.AddIngredient(null, "MushiumBar", 3);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
