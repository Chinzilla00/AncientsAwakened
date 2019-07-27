using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class OdinsBlade : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Odin's Iceblade");
            Tooltip.SetDefault(@"Looks like Greed accidentally snagged this at some point from someone");
        }

        public override void SetDefaults()
        {
            item.shoot = mod.ProjectileType("OdinsBlade");
            item.shootSpeed = 10f;
            item.damage = 70;
            item.knockBack = 5f;
            item.ranged = true;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;
            item.useAnimation = 30;
            item.useTime = 30;
            item.width = 20;
            item.height = 20;
            item.consumable = true;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.value = 100000;
            item.rare = 7;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SnowMana");
            recipe.AddIngredient(ItemID.BorealWood, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }
    }
}
