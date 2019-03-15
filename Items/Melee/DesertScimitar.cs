using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAmod.Items.Melee
{
    public class DesertScimitar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desert Scimitar");
        }
        public override void SetDefaults()
        {
            item.damage = 25;
            item.melee = true;
            item.width = 38;
            item.height = 38;
            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 1;
            item.knockBack = 4;
            item.value = Item.buyPrice(0, 0, 0, 52);
            item.rare = 5;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("SandPro");
            item.shootSpeed = 12f;
        }

        public override void AddRecipes() //the Recipe of the item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SandScimitar");
            recipe.AddIngredient(ItemID.Sandstone, 50);
            recipe.AddIngredient(ItemID.SandBlock, 100);
            recipe.AddIngredient(null, "DesertMana", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
            float scale = 1f - (Main.rand.NextFloat() * .3f);
            perturbedSpeed = perturbedSpeed * scale;
            Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            return false;
        }
    }
}
