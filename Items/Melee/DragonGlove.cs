using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class DragonGlove : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Glove");
        }

        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 9;
            item.useTime = 9;
            item.width = 28;
            item.height = 24;
            item.damage = 21;
            item.knockBack = 4f;
            item.UseSound = SoundID.Item1;
            item.scale = 1.35f;
            item.melee = true;
            item.rare = 3;
            item.value = 50000;
            item.melee = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "IncineriteBar", 10);
            recipe.AddIngredient(mod, "DragonClaw", 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}