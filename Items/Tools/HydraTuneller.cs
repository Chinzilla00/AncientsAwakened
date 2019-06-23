using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    //ported from my tAPI mod because I don't want to make more artwork
    public class HydraTuneller : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Hydra Tuneller");
            Tooltip.SetDefault("Okay, this is getting rediculous. Hydras don't use drills.");
		}

		public override void SetDefaults()
		{
			item.damage = 6;
			item.melee = true;
			item.width = 50;
			item.height = 18;
			item.useTime = 10;
			item.useAnimation = 15;
			item.channel = true;
			item.noUseGraphic = true;
			item.noMelee = true;
			item.pick = 65;
			item.useStyle = 5;
			item.knockBack = 0;
			item.value = Item.sellPrice(0, 0, 30, 0);
			item.rare = 1;
			item.UseSound = SoundID.Item23;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("HydraTuneller");
			item.shootSpeed = 40f;
		}

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "AbyssiumBar", 12);
                recipe.AddIngredient(null, "HydraHide", 6);
                recipe.AddTile(TileID.Anvils);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}