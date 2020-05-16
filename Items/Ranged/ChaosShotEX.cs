using Terraria;
using System;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AAMod.Items.Ranged
{
    public class ChaosShotEX : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Perfect Chaos Bustershot");
            Tooltip.SetDefault(@"Fires a piercing dualblast as well as a spread of 10 bullets
Chaos Bustershot EX");
        }

        public override void SetDefaults()
        {

            item.damage = 300;
            item.noMelee = true;
            item.ranged = true;
            item.width = 50;
            item.height = 20;
            item.useTime = 45;
            item.useAnimation = 45;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.shoot = ProjectileID.PurificationPowder;
            item.useAmmo = AmmoID.Bullet;
            item.knockBack = 0;
            item.value = Item.sellPrice(5, 0, 0, 0);
            item.rare = ItemRarityID.Yellow;
            item.UseSound = SoundID.Item14;
            item.shootSpeed = 12f;
            item.expert = true; item.expertOnly = true;
            item.autoReuse = true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, -2);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
		    float spread = 20f * 0.0174f;
		    float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
		    double deltaAngle = spread / 6f;
		    double offsetAngle;
		    for (int i = 0; i < 10; i++)
		    {
		    	offsetAngle = startAngle + (deltaAngle * i);
		    	Projectile.NewProjectile(position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), type, damage, knockBack, Main.myPlayer);
            }
            for (int m = 0; m < 2; m++)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX * 1f, speedY * 1f, m == 0 ? mod.ProjectileType("ChaosShot2") : mod.ProjectileType("ChaosShot3"), damage, knockBack, player.whoAmI, 0, 1);
            }

            Projectile.NewProjectile(position.X, position.Y, speedX * 1f, speedY * 1f, mod.ProjectileType("ChaosShot1"), damage, knockBack, player.whoAmI, 0, 1);
            return false;
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ChaosShot", 1);
            recipe.AddIngredient(null, "EXSoul", 1);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
