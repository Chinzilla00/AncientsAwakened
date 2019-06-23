using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class DragonPickaxe : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 10;
            item.melee = true;
            item.width = 54;
            item.height = 52;

            item.useTime = 12;
            item.useAnimation = 24;
            item.pick = 130;    //pickaxe power
            item.useStyle = 1;
            item.knockBack = 0;
            item.value = 10;
            item.rare = 5;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Dragon Pickaxe");
      Tooltip.SetDefault("");
    }

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DragonSpirit", 18);
            recipe.AddTile(TileID.MythrilAnvil);   //at work bench
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
