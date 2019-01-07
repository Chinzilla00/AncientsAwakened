using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class BlazePike : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mana Rose");
            Tooltip.SetDefault("Long and Magical");
            Item.staff[item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
        }

        public override void SetDefaults()
        {
            item.damage = 20;
            item.magic = true;
            item.mana = 3;
            item.width = 56;
            item.height = 56;
            item.useTime = 27;
            item.useAnimation = 27;
            item.useStyle = 5;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 5;
            item.value = 10000;
            item.rare = 3;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = 664;
            item.shootSpeed = 6f;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "IncineriteBar", 10);
                recipe.AddIngredient(null, "BroodScale", 10);
                recipe.AddTile(TileID.Anvils);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}