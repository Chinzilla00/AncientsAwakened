using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class DragonSpine : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon's Spine");
            Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {

            item.shoot = mod.ProjectileType("DragonSpine");
            item.shootSpeed = 9f;
            item.damage = 18;
            item.knockBack = 4f;
            item.ranged = true;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;
            item.useAnimation = 28;
            item.useTime = 28;
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.consumable = true;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.value = 40;
            item.rare = 1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "IncineriteBar");
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }
    }
}
