using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class YtriumAxe : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 19;
            item.melee = true;
            item.width = 38;
            item.height = 38;

            item.useTime = 17;
            item.useAnimation = 28;
            item.axe = 12; 
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
          DisplayName.SetDefault("Yttrium Hatchet");
          Tooltip.SetDefault("");
        }

        public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "YtriumBar", 13);
            recipe.AddTile(TileID.Anvils);   
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
