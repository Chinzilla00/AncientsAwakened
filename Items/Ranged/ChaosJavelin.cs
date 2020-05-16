using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class ChaosJavelin : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Javelin");
            Tooltip.SetDefault("Explodes on contact");
        }

        public override void SetDefaults()
        {
            item.shoot = mod.ProjectileType("ChaosJavelin");
            item.shootSpeed = 15f;
            item.damage = 105;
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
            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = ItemRarityID.Yellow;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "PrimevalJavelin");
            recipe.AddIngredient(null, "ChaosCrystal");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
