using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class Apollo : BaseAAItem
    {

        public override void SetDefaults()
        {

            item.damage = 19;
            item.noMelee = true;
            item.ranged = true;
            item.width = 18;
            item.height = 42;
            item.useTime = 19;
            item.useAnimation = 19;
            item.useStyle = 5;
            item.shoot = 3;
            item.useAmmo = AmmoID.Arrow;
            item.knockBack = 2;
            item.value = 10000;
            item.rare = 3;
            item.UseSound = SoundID.Item5;
            item.autoReuse = false;
            item.shootSpeed = 21f;

        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Apollo's Bow");
      Tooltip.SetDefault("");
    }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FallenStar, 15);
            recipe.AddIngredient(ItemID.SunplateBlock, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
