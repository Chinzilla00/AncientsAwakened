using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    public class Sunpowder : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.shoot = mod.ProjectileType("Sunpowder");
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.shootSpeed = 4f;
            item.width = 16;
            item.height = 24;
            item.maxStack = 99;
            item.consumable = true;
            item.UseSound = SoundID.Item1;
            item.useAnimation = 15;
            item.useTime = 15;
            item.noMelee = true;
            item.value = 75;
        }

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault(@"Cleanses the mire");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Hotshroom", 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }
    }
}
