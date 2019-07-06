using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class CrystalBow : BaseAAItem
    {

        public override void SetDefaults()
        {

            item.damage = 50;
            item.noMelee = true;
            item.ranged = true;
            item.width = 34;
            item.height = 60;
            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = 5;
            item.shoot = 3;
            item.useAmmo = AmmoID.Arrow;
            item.knockBack = 5;
            item.value = Terraria.Item.sellPrice(0, 1, 0, 0);
            item.rare = 7;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shootSpeed = 22f;

        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Crystal Bow");
      Tooltip.SetDefault("");
    }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PixieDust, 14);   
			recipe.AddIngredient(ItemID.CrystalShard, 18);
			recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
