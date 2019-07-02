using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using System;

namespace AAMod.Items.Dev
{
    public class Skullshot : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Super Skullshot");
            Tooltip.SetDefault(@"fires a massive spread of bullets at your foes
Right click to fire a spinning bone at your foe
Uses Bullets and Bones as ammo
'I have an irrational hatred for gods`
-Gibs");
        }

        public override void SetDefaults()
        {
            item.autoReuse = true;
            item.knockBack = 7f;
            item.useStyle = 5;
            item.useAnimation = 34;
            item.useTime = 34;
            item.width = 50;
            item.height = 14;
            item.shoot = 10;
            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item36;
            item.damage = 30;
            item.shootSpeed = 6f;
            item.noMelee = true;
            item.value = 100000;
            item.rare = 9;
            item.ranged = true;

            glowmaskTexture = "Glowmasks/" + GetType().Name;
			glowmaskDrawType = GLOWMASKTYPE_GUN;
			glowmaskDrawColor = Color.White;
			customNameColor = new Color(255, 128, 0);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useAnimation = 15;
                item.useTime = 5;
                item.reuseDelay = 17;
                item.useAmmo = AAMod.BoneAmmo;
                item.damage = 80;
            }
            else
            {
                item.useAnimation = 34;
                item.useTime = 34;
                item.reuseDelay = 0;
                item.useAmmo = AmmoID.Bullet;
                item.damage = 30;
            }
            return base.CanUseItem(player);
        }

        public override bool ConsumeAmmo(Player player)
        {
            return !(player.itemAnimation < item.useAnimation - 2);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse != 2)
            {
                float spread = Main.rand.Next(20, 51) * 0.0174f;
                float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
                double startAngle = Math.Atan2(speedX, speedY) - .1d;
                double deltaAngle = spread / 6f;
                double offsetAngle;
                for (int i = 0; i < Main.rand.Next(5, 11); i++)
                {
                    offsetAngle = startAngle + (deltaAngle * i);
                    Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockBack, item.owner);
                }
            }
            else
            {
                int proj = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.BoneGloveProj, damage, knockBack, Main.myPlayer, 0f, 0f);
                Main.projectile[proj].ranged = true;
            }
            return false;
        }
    }
}