using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Yamata
{
    public class MidnightWrath : ModItem
	{
		public override void SetDefaults()
		{

            item.damage = 130;            
            item.thrown = true;
            item.width = 20;
            item.height = 20;
			item.useTime = 8;
			item.useAnimation = 8;
            item.noUseGraphic = true;
            item.useStyle = 1;
			item.knockBack = 0;
			item.value = Item.buyPrice(1, 0, 0, 0);
			item.shootSpeed = 20f;
			item.shoot = mod.ProjectileType ("MidnightWrath");
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Midnight's Wrath");
            Tooltip.SetDefault("Non-consumable");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EventideAbyssium", 5);
            recipe.AddIngredient(null, "DreadScale", 5);
            recipe.AddIngredient(null, "DarkmatterKunai", 999);
		    recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
		}
    }
}
