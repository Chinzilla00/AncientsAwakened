using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class YtriumPickaxe : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 9;
            item.melee = true;
            item.width = 38;
            item.height = 38;

            item.useTime = 8;
            item.useAnimation = 12;
            item.pick = 110;    //pickaxe power
            item.useStyle = 1;
            item.knockBack = 1;
            item.value = 10;
            item.rare = 9;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Yttrium Pickaxe");
      Tooltip.SetDefault("");
    }

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "YtriumBar", 15);
            recipe.AddTile(TileID.MythrilAnvil);   //at work bench
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
