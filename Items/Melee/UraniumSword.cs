using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class UraniumSword : ModItem
    {
        public override void SetDefaults()
        {

            item.useStyle = 1;
            item.useAnimation = 30;
            item.useTime = 30;
            item.knockBack = 6f;
            item.width = 52;
            item.height = 52;
            item.damage = 44;
            item.scale = 1.15f;
            item.UseSound = SoundID.Item1;
            item.rare = 4;
            item.value = 103500;
            item.melee = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Amnesia");
            Tooltip.SetDefault("");
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
			recipe.AddIngredient(mod, "UraniumBar", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
