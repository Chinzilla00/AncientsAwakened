using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class UraniumPick : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.useAnimation = 23;
            item.useTime = 6;
            item.knockBack = 5f;
            item.useTurn = true;
            item.autoReuse = true;
            item.width = 20;
            item.height = 12;
            item.damage = 40;
            item.pick = 200;
            item.UseSound = SoundID.Item1;
            item.rare = 8;
            item.value = 216000;
            item.melee = true;
            item.scale = 1.15f;
            item.tileBoost++;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Uranium Pickaxe");
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
