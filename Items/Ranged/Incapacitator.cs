using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class Incapacitator : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Incapacitator");
            Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            item.shoot = mod.ProjectileType("Incapacitator");
            item.shootSpeed = 11f;
            item.damage = 21;
            item.knockBack = 5f;
            item.ranged = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item1;
            item.useAnimation = 25;
            item.useTime = 25;
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.consumable = true;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.value = 60;
            item.rare = ItemRarityID.Orange;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Doomite");
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }
    }
}
