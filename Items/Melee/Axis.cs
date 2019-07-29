using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class Axis : BaseAAItem
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Axis");
			Tooltip.SetDefault("Enemies struck by this spear will be surrounded by snowflakes\nNorth Pole EX");
		}
		
        public override void SetDefaults()
        {
            item.damage = 250;
            item.melee = true;
            item.width = 64;
            item.height = 64;
            item.shoot = mod.ProjectileType("Axis");
            item.useStyle = 5;
            item.useAnimation = 30;
			item.useTime = 30;
			item.shootSpeed = 4.75f;
            item.knockBack = 5f;
            item.UseSound = SoundID.Item1;
            item.useTurn = true;
			item.autoReuse = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 7;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.NorthPole);
            recipe.AddIngredient(mod.ItemType("EXSoul"));
			recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] < 1;
        }
        
    }
}
