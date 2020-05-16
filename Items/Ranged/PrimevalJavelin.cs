using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class PrimevalJavelin : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Primeval Javelin");
            Tooltip.SetDefault("If stuck in an enemy and that enemy dies, releases 4 homing bolts of Dyna-Energy");
        }

        public override void SetDefaults()
        {
            item.shoot = mod.ProjectileType("PrimevalJavelin");
            item.shootSpeed = 12f;
            item.damage = 70;
            item.knockBack = 5f;
            item.ranged = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item1;
            item.useAnimation = 22;
            item.useTime = 22;
            item.width = 30;
            item.height = 30;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.value = 100000;
            item.rare = ItemRarityID.Yellow;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DynaskullJavelin");
            recipe.AddIngredient(null, "HeroShards");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
