using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class DynaskullJavelin : ModItem
    {

        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dynaskull Javelin");
            Tooltip.SetDefault("If stuck in an enemy and that enemy dies, releases a homing bolt of Dyna-Energy");
        }

        public override void SetDefaults()
        {
            item.shoot = mod.ProjectileType("DynaskullJavelin");
            item.shootSpeed = 12f;
            item.damage = 40;
            item.knockBack = 5f;
            item.ranged = true;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;
            item.useAnimation = 24;
            item.useTime = 24;
            item.width = 30;
            item.height = 30;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.value = 50000;
            item.rare = 4;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BoneJavelin, 500);
            recipe.AddIngredient(null, "DragonSpine", 500);
            recipe.AddIngredient(null, "Winterbreak", 500);
            recipe.AddIngredient(null, "Incapacitator", 500);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
