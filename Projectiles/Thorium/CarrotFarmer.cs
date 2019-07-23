using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Thorium
{
    public class CarrotFarmer : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Carrot Farmer");
        }

        public override void SetDefaults()
        {
            projectile.width = 160;
            projectile.height = 156;
            projectile.aiStyle = 0;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ownerHitCheck = true;
            projectile.ignoreWater = true;
            projectile.timeLeft = 26;
            aiType = ProjectileID.Bullet;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            projHitbox.Width += 16;
            projHitbox.Height += 16;

            return projHitbox.Intersects(targetHitbox);
        }

        public override void OnHitNPC(NPC npc, int damage, float knockback, bool crit)
        {
            npc.immune[projectile.owner] = 8;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[projectile.owner];
            if (target.Center.X < player.Center.X)
            {
                hitDirection = -1;
            }
            else
            {
                hitDirection = 1;
            }
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            if (player.dead)
            {
                projectile.Kill();
            }

            if (player.direction > 0)
            {
                projectile.rotation += 0.35f;
                projectile.spriteDirection = 1;
            }
            else
            {
                projectile.rotation -= 0.35f;
                projectile.spriteDirection = -1;
            }

            player.heldProj = projectile.whoAmI;
            projectile.position.X = player.Center.X - 80;
            projectile.position.Y = player.Center.Y - 73;

            Projectile.NewProjectile(projectile.Center.X + 20, projectile.Center.Y, -15f, 0f, mod.ProjectileType("CarrotFarmerDamage"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X - 20, projectile.Center.Y, 15f, 0f, mod.ProjectileType("CarrotFarmerDamage"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);

            if (projectile.timeLeft == 13)
            {
                Projectile.NewProjectile(projectile.Center.X + 20, projectile.Center.Y, -15f, 0f, mod.ProjectileType("CarrotFarmerDamage2"), (int)(projectile.damage * .35), projectile.knockBack, projectile.owner, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X - 20, projectile.Center.Y, 15f, 0f, mod.ProjectileType("CarrotFarmerDamage2"), (int)(projectile.damage * .35), projectile.knockBack, projectile.owner, 0f, 0f);
            }

            if (projectile.timeLeft < 8)
            {
                projectile.alpha -= 28;
            }

            projectile.ai[1]++;
            if (projectile.ai[1] >= 16)
            {
                for (int u = 0; u < 10; u++)
                {
                    int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType<Dusts.CarrotDust>(), Main.rand.Next((int)-5f, (int)5f), Main.rand.Next((int)-5f, (int)5f), 0);
                    Main.dust[dust].noGravity = true;
                }
                float spread = 12f * 0.0174f;
                double startAngle = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - spread / 2;
                double deltaAngle = spread / 30f;
                double offsetAngle;
                int i;
                if (projectile.owner == Main.myPlayer)
                {
                    for (i = 0; i < 30; i++)
                    {
                        offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
                        if (Main.rand.Next(15) == 0)
                        {
                            int ProjID = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(Math.Sin(offsetAngle) * 6f), (float)(Math.Cos(offsetAngle) * 6f), mod.ProjectileType("Carrot"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                        }
                        if (Main.rand.Next(15) == 0)
                        {
                            int ProjID = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(-Math.Sin(offsetAngle) * 6f), (float)(-Math.Cos(offsetAngle) * 6f), mod.ProjectileType("Carrot"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                        }
                    }
                }
                projectile.ai[1] = -0;
            }
        }
    }

    public class CarrotFarmerDamage : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetDefaults()
        {
            projectile.width = 160;
            projectile.height = 156;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ownerHitCheck = true;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 8;
            aiType = ProjectileID.Bullet;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[projectile.owner];
            if (Main.rand.Next(100) <= ((ModSupportPlayer)player.GetModPlayer(mod, "ModSupportPlayer")).Thorium_radiantCrit)
            {
                crit = true;
            }
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            projectile.position.X = player.Center.X - (projectile.width / 2f);
            projectile.position.Y = player.Center.Y - (projectile.height / 2f);
        }
    }

    public class CarrotFarmerDamage2 : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetDefaults()
        {
            projectile.width = 130;
            projectile.height = 128;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ownerHitCheck = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 4;
            aiType = ProjectileID.Bullet;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[projectile.owner];
            if (Main.rand.Next(100) <= ((ModSupportPlayer)player.GetModPlayer(mod, "ModSupportPlayer")).Thorium_radiantCrit)
            {
                crit = true;
            }
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            projectile.position.X = player.Center.X - (projectile.width / 2f);
            projectile.position.Y = player.Center.Y - (projectile.height / 2f);
        }
    }


    public class CarrotFarmerEffect : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.aiStyle = -1;
            projectile.tileCollide = false;
            projectile.ownerHitCheck = true;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 24;
        }

        public static Vector2 RotateVector(Vector2 origin, Vector2 vecToRot, float rot)
        {
            float newPosX = (float)(Math.Cos(rot) * (vecToRot.X - origin.X) - Math.Sin(rot) * (vecToRot.Y - origin.Y) + origin.X);
            float newPosY = (float)(Math.Sin(rot) * (vecToRot.X - origin.X) + Math.Cos(rot) * (vecToRot.Y - origin.Y) + origin.Y);
            return new Vector2(newPosX, newPosY);
        }

        public Vector2 rotVec = new Vector2(0, 65);
        public float rot = 0f;

        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            if (player.direction > 0)
            {
                rot += 0.20f;
            }
            else
            {
                rot -= 0.20f;
            }

            projectile.Center = player.Center + new Vector2(-8f, -8f) + RotateVector(default, rotVec, rot + (projectile.ai[0] * (6.28f / 2)));

            for (int m = 0; m < 5; m++)
            {
                float velX = projectile.velocity.X / 3f * m;
                float velY = projectile.velocity.Y / 3f * m;
                int dustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType<Dusts.CarrotDust>(), 0, 0, 0);
                Main.dust[dustID].position.X = projectile.Center.X - velX;
                Main.dust[dustID].position.Y = projectile.Center.Y - velY;
                Main.dust[dustID].velocity *= 0f;
                Main.dust[dustID].alpha = 180;
                Main.dust[dustID].noGravity = true;
                Main.dust[dustID].scale = 0.8f;
            }
        }
    }
}