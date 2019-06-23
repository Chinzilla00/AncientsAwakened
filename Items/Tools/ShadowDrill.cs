using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class ShadowDrill : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Shadow Drill");
            Tooltip.SetDefault("Mines things with a spinning...green thing I guess?");
		}

		public override void SetDefaults()
		{
			item.damage = 10;
			item.melee = true;
			item.width = 50;
			item.height = 18;
			item.useTime = 8;
			item.useAnimation = 15;
			item.channel = true;
			item.noUseGraphic = true;
			item.noMelee = true;
			item.pick = 110;
			item.useStyle = 5;
			item.knockBack = 0;
			item.value = Item.sellPrice(0, 1, 8, 0);
			item.rare = 4;
			item.UseSound = SoundID.Item23;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("ShadowDrill");
			item.shootSpeed = 40f;
		}

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "HydraTuneller");
            recipe.AddIngredient(mod, "OceanPick");
            recipe.AddIngredient(mod, "Icepick");
            recipe.AddIngredient(mod, "DoomiteMiningLaser");
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}