using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class ManaRose : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mana Rose");
            Tooltip.SetDefault("Long and Magical");
            Item.staff[item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
        }

        public override void SetDefaults()
        {
            item.damage = 45;
            item.magic = true;
            item.mana = 6;
            item.width = 68;
            item.height = 60;
            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 5;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 5;
            item.value = 100000;
            item.rare = 4;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("ManaShot");
            item.shootSpeed = 7f;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.Vilethorn, 1);
                recipe.AddIngredient(null, "MagicFlower", 1);
                recipe.AddIngredient(ItemID.MagicMissile, 1);
                recipe.AddIngredient(ItemID.FlowerofFire, 1);
                recipe.AddTile(TileID.DemonAltar);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.CrimsonRod, 1);
                recipe.AddIngredient(null, "MagicFlower", 1);
                recipe.AddIngredient(ItemID.MagicMissile, 1);
                recipe.AddIngredient(ItemID.FlowerofFire, 1);
                recipe.AddTile(TileID.DemonAltar);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}