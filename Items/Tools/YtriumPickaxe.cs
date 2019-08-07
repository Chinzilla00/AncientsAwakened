using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class YtriumPickaxe : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 9;
            item.melee = true;
            item.width = 38;
            item.height = 38;

            item.useTime = 16;
            item.useAnimation = 20;
            item.pick = 100;
            item.useStyle = 1;
            item.knockBack = 1;
            item.value = BaseMod.BaseUtility.CalcValue(0, 5, 0, 0);
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yttrium Pickaxe");
        }

        public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "YtriumBar", 15);
            recipe.AddTile(TileID.Anvils);   
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
