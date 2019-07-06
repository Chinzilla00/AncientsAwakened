using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class CursedSickleEX : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tartarus Reaper");
        }

        public override void SetDefaults()
        {
            projectile.width = 120;
            projectile.height = 114;
            projectile.aiStyle = 0;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ownerHitCheck = true;
            projectile.ignoreWater = true;
            projectile.timeLeft = 26;
            aiType = ProjectileID.Bullet;
            projectile.melee = true;
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
            projectile.position.X = player.Center.X - (projectile.width / 2f);
            projectile.position.Y = player.Center.Y - (projectile.height / 2f);

            Projectile.NewProjectile(projectile.Center.X + 20, projectile.Center.Y, -15f, 0f, mod.ProjectileType("CursedSickleEXDamage"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X - 20, projectile.Center.Y, 15f, 0f, mod.ProjectileType("CursedSickleEXDamage"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);

            if (projectile.timeLeft == 13)
            {
                Projectile.NewProjectile(projectile.Center.X + 20, projectile.Center.Y, -15f, 0f, mod.ProjectileType("CursedSickleEXDamage2"), (int)(projectile.damage * .35), projectile.knockBack, projectile.owner, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X - 20, projectile.Center.Y, 15f, 0f, mod.ProjectileType("CursedSickleEXDamage2"), (int)(projectile.damage * .35), projectile.knockBack, projectile.owner, 0f, 0f);
            }

            if (projectile.timeLeft < 8)
            {
                projectile.alpha -= 28;
            }
        }
    }
    public class CursedSickleEXEffect : ModProjectile
    {
        public override string Texture { get { return "AAMod/BlankTex"; } }
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

            projectile.Center = player.Center + new Vector2(-8f, -8f) + RotateVector(default(Vector2), rotVec, rot + (projectile.ai[0] * (6.28f / 2)));

            for (int m = 0; m < 5; m++)
            {
                float velX = projectile.velocity.X / 3f * m;
                float velY = projectile.velocity.Y / 3f * m;
                int dustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, 75, 0, 0, 0, default(Color), 1f);
                Main.dust[dustID].position.X = projectile.Center.X - velX;
                Main.dust[dustID].position.Y = projectile.Center.Y - velY;
                Main.dust[dustID].velocity *= 0f;
                Main.dust[dustID].alpha = 180;
                Main.dust[dustID].noGravity = true;
                Main.dust[dustID].scale = 0.8f;
            }
        }
    }

    public class CursedSickleEXDamage : ModProjectile
    {
        public override string Texture { get { return "AAMod/BlankTex"; } }
        public override void SetDefaults()
        {
            projectile.width = 120;
            projectile.height = 96;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ownerHitCheck = true;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 8;
            aiType = ProjectileID.Bullet;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 7;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            target.AddBuff(mod.BuffType<Buffs.CursedHellfire>(), 210);
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            projectile.position.X = player.Center.X - (projectile.width / 2f);
            projectile.position.Y = player.Center.Y - (projectile.height / 2f);
        }
    }

    public class CursedSickleEXDamage2 : ModProjectile
    {
        public override string Texture { get { return "AAMod/BlankTex"; } }
        public override void SetDefaults()
        {
            projectile.width = 120;
            projectile.height = 96;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ownerHitCheck = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 4;
            aiType = ProjectileID.Bullet;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 7;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            target.AddBuff(mod.BuffType<Buffs.CursedHellfire>(), 210);
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            projectile.position.X = player.Center.X - (projectile.width / 2f);
            projectile.position.Y = player.Center.Y - (projectile.height / 2f);
        }
    }
}