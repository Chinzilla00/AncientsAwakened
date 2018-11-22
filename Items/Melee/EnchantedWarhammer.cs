using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class EnchantedWarhammer : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 113;
            item.melee = true;
            item.width = 48;
            item.height = 48;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 1;
            item.knockBack = 26;
            item.value = 100;
            item.rare = 6;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
 
 
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Enchanted Warhammer");
      Tooltip.SetDefault("Cannot be used as a hammer!");
    }

 
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.EnchantedSword, 1);
            recipe.AddIngredient(ItemID.HallowedBar, 15);  //in this example you see how to add your custom item to the crafting recipe
            recipe.AddTile(TileID.MythrilAnvil);     //in this example you see how to add your custom craftingbench
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}