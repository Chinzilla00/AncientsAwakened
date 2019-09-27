using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class GibsFemur : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Angry Femur");
            Main.projFrames[projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.aiStyle = 1;
            projectile.scale = 1f;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = 5;
            projectile.extraUpdates = 1;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 16;
            height = 16;
            return true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            bool flag8 = false;
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = oldVelocity.X * -0.75f;
                flag8 = true;
            }
            if ((projectile.velocity.Y != oldVelocity.Y && oldVelocity.Y > 2f) || projectile.velocity.Y == 0f)
            {
                projectile.velocity.Y = oldVelocity.Y * -0.75f;
                flag8 = true;
            }
            if (flag8)
            {
                float num10 = oldVelocity.Length() / projectile.velocity.Length();
                if (num10 == 0f)
                {
                    num10 = 1f;
                }
                projectile.velocity /= num10;
                projectile.penetrate--;
                Collision.HitTiles(projectile.position, oldVelocity, projectile.width, projectile.height);
            }
            return false;
        }

        int a = 0;
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (a++ > 4)
            {
                CombatText.NewText(projectile.getRect(), Color.IndianRed, "A", true);
                a = 0;
            }
            projectile.ai[0] += 2f;
            if (projectile.ai[0] >= 15f)
            {
                projectile.ai[0] = 15f;
                projectile.velocity.Y = projectile.velocity.Y + 0.1f;
            }
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }

            if (projectile.penetrate < 2)
            {
                projectile.penetrate = 2;
                projectile.tileCollide = false;
                float distPlayerX = player.Center.X - projectile.Center.X;
                float distPlayerY = player.Center.Y - projectile.Center.Y;
                float distPlayer = (float)Math.Sqrt(distPlayerX * distPlayerX + distPlayerY * distPlayerY);
                if (distPlayer > 3000f)
                {
                    projectile.Kill();
                }
                projectile.velocity = BaseMod.BaseUtility.RotateVector(default, new Vector2(.7f, 0f), BaseMod.BaseUtility.RotationTo(projectile.Center, player.Center));
                if (Main.myPlayer == projectile.owner)
                {
                    Rectangle rectangle = projectile.Hitbox;
                    Rectangle value = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
                    if (rectangle.Intersects(value)) { projectile.Kill(); }
                }
            }
            projectile.rotation += 0.2f + Math.Abs(projectile.velocity.X) * 0.1f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Bleeding, 600);
        }

        public override void Kill(int timeleft)
        {
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1, 1f, 0f);
            for (int num604 = 0; num604 < 10; num604++)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 26, 0f, 0f, 0, default, 0.8f);
            }
        }
    }
}
