using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class EnchantedWarhammer : BaseAAItem
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
            item.hammer = 100;
        }

        public override void SetStaticDefaults()
        {
          DisplayName.SetDefault("Enchanted Warhammer");
          Tooltip.SetDefault("Now with hammer power because gibs wouldn't shut up about it.");
        }

 
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.EnchantedSword, 1);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}
