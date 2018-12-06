using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class DeathDaggers : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Death Daggers");
            Tooltip.SetDefault("Throw life stealing daggers that inflict Hydratoxin");
        }

        public override void SetDefaults()
		{
            item.autoReuse = true;
            item.useStyle = 1;
            item.shootSpeed = 8f;
            item.shoot = mod.ProjectileType<Projectiles.DeathDagger>();
            item.damage = 29;
            item.width = 54;
            item.height = 54;
            item.scale *= 0.5f;
            item.UseSound = SoundID.Item39;
            item.useAnimation = 25;
            item.useTime = 25;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.value = Item.sellPrice(1, 0, 0, 0);
            item.knockBack = 1f;
            item.melee = true;
            item.rare = 4;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float spread = 25f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            for (int i = 0; i < 3; i++)
            {
                offsetAngle = startAngle + (deltaAngle * i);
                Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), item.shoot, damage, knockBack, item.owner);
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AbyssiumBar", 10);
            recipe.AddIngredient(null, "HydraToxin", 10);
		    recipe.AddTile(TileID.Mythril);
            recipe.SetResult(this);
            recipe.AddRecipe();
		}
    }
}
