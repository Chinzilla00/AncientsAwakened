using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using System;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Dev
{
    public class SkullshotEX : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Supreme Hellshot");
            Tooltip.SetDefault(@"fires a massive spread of bullets at your foes
Right click to fire a hellskull at your foe
Uses Bullets & Bones as ammo
Super Skullshot EX");
        }

        public override void SetDefaults()
        {
            item.autoReuse = true;
            item.knockBack = 7f;
            item.useStyle = 5;
            item.useAnimation = 20;
            item.useTime = 20;
            item.width = 70;
            item.height = 32;
            item.shoot = 10;
            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item36;
            item.damage = 90;
            item.shootSpeed = 9f;
            item.noMelee = true;
            item.value = 100000;
            item.rare = 9;
            item.ranged = true;
            item.expert = true;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Glowmasks/" + GetType().Name);
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useAmmo = AAMod.BoneAmmo;
                item.damage = 300;
                item.useAnimation = 30;
                item.useTime = 30;
                item.shoot = mod.ProjectileType<Projectiles.Hellshot>();
            }
            else
            {
                item.useAmmo = AmmoID.Bullet;
                item.damage = 90;
                item.useAnimation = 20;
                item.useTime = 20;
                item.shoot = 10;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse != 2)
            {
                float spread = Main.rand.Next(20, 31) * 0.0174f;
                float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
                double startAngle = Math.Atan2(speedX, speedY) - .1d;
                double deltaAngle = spread / 6f;
                double offsetAngle;
                
                for (int i = 0; i < Main.rand.Next(6, 13); i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        offsetAngle = startAngle + (deltaAngle * i);
                        Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockBack, item.owner);
                    }
                }
            }
            else
            {
                int proj = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType<Projectiles.Hellshot>(), damage, knockBack, Main.myPlayer, 0f, 0f);
                Main.projectile[proj].ranged = true;
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Skullshot", 1);
            recipe.AddIngredient(null, "EXSoul", 1);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}