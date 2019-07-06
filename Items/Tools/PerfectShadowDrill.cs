using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class PerfectShadowDrill : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Perfect Shadow Drill");
            Tooltip.SetDefault("Now that's more like it.");
		}

		public override void SetDefaults()
		{
			item.damage = 10;
			item.melee = true;
			item.width = 50;
			item.height = 18;
			item.useTime = 6;
			item.useAnimation = 15;
			item.channel = true;
			item.noUseGraphic = true;
			item.noMelee = true;
			item.pick = 205;
			item.useStyle = 5;
			item.knockBack = 0;
            item.value = Item.sellPrice(0, 10);
            item.rare = 7;
			item.UseSound = SoundID.Item23;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("PShadowDrill");
			item.shootSpeed = 40f;
		}

        public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "ShadowDrill");
            recipe.AddIngredient(mod, "MireCrystal");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}