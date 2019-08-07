using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class UraniumAxe : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.useTurn = true;
            item.autoReuse = true;
            item.useStyle = 1;
            item.useAnimation = 28;
            item.useTime = 5;
            item.knockBack = 7f;
            item.width = 20;
            item.height = 12;
            item.damage = 70;
            item.axe = 23;
            item.UseSound = SoundID.Item1;
            item.rare = 7;
            item.value = 216000;
            item.melee = true;
            item.scale = 1.15f;
            item.tileBoost++;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Uranium Logger");
        }

        public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "UraniumBar", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
