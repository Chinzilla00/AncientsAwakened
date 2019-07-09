using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Melee
{
    public class MushMace : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Mushmace");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(0, 0, 20, 0);
            item.rare = 1;
            item.noMelee = true;
            item.useStyle = 5;
            item.useAnimation = 30;
            item.useTime = 30;
            item.knockBack = 4f;
            item.damage = 19;
            item.noUseGraphic = true;
            item.shoot = mod.ProjectileType("MushMace");
            item.shootSpeed = 9;
            item.UseSound = SoundID.Item1;
            item.melee = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Mushroom, 5);
            recipe.AddIngredient(null, "MushiumBar", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}