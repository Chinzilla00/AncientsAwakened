using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Zero
{
    public class NovaRay : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nova Ray");    //The recording mode
		}

		public override void SetDefaults()
		{
            projectile.width = 18;
            projectile.height = 18;
            projectile.aiStyle = 0;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.tileCollide = false;
            projectile.usesLocalNPCImmunity = true;
            
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float n = 0f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center, projectile.Center + projectile.velocity * projectile.localAI[1], 30f * projectile.scale, ref n))
                return true;

            return false;
        }

        public NPC shooter;
        private const float MoveDistance = 70f;
        
        public float Distance;


        public bool runOnce = true;
        public override void AI()
        {


            float rOffset = 0;
            shooter = Main.npc[(int)projectile.ai[0]];
            Vector2 mousePos = Main.MouseWorld;
            Player player = Main.player[projectile.owner];
            if (!shooter.active || shooter.life <= 0)
            {
                projectile.Kill();
            }

            #region Set projectile position


            Vector2 diff = new Vector2((float)Math.Cos(shooter.rotation + rOffset) * 14f, (float)Math.Sin(shooter.rotation + rOffset) * 14f);
            diff.Normalize();
            projectile.velocity = diff;
            projectile.direction = projectile.Center.X > shooter.Center.X ? 1 : -1;
            projectile.netUpdate = true;

            projectile.position = new Vector2(shooter.Center.X, shooter.Center.Y) + projectile.velocity * MoveDistance;
            projectile.timeLeft = 2;
            int dir = projectile.direction;
            #endregion

            Vector2 start = new Vector2(shooter.Center.X, shooter.Center.Y);
            Vector2 unit = projectile.velocity;
            unit *= -1;
            for (Distance = MoveDistance; Distance <= 2200f; Distance += 5f)
            {
                start = new Vector2(shooter.Center.X, shooter.Center.Y) + projectile.velocity * Distance;
                if (!Collision.CanHit(new Vector2(shooter.Center.X, shooter.Center.Y), 1, 1, start, 1, 1))
                {
                    Distance -= 5f;
                    break;
                }
            }



            //Add lights
            DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
            Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * (Distance - MoveDistance), 26,
                DelegateMethods.CastLight);

        }

        public override void Kill(int timeLeft)
        {
            for (int num56 = 0; num56 < 1000; num56++)
            {
                if (Main.projectile[num56].active && Main.projectile[num56].owner == projectile.owner && Main.projectile[num56].type == mod.ProjectileType<NovaRay>())
                {
                    Main.projectile[num56].Kill();
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (projectile.velocity == Vector2.Zero)
                return false;

            Texture2D tex2 = Main.projectileTexture[projectile.type];
            float num210 = projectile.localAI[1];
            Color c_ = new Color(AAColor.ZeroShield.R, AAColor.ZeroShield.G, AAColor.ZeroShield.B, 127);
            Vector2 value20 = projectile.Center.Floor();
            num210 -= projectile.scale * 10.5f;
            Vector2 vector41 = new Vector2(projectile.scale);
            DelegateMethods.f_1 = 1f;
            DelegateMethods.c_1 = c_;
            DelegateMethods.i_1 = 54000 - (int)Main.time / 2;
            Vector2 vector42 = projectile.oldPos[0] + new Vector2((float)projectile.width, (float)projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
            Utils.DrawLaser(Main.spriteBatch, tex2, value20 - Main.screenPosition, value20 + projectile.velocity * num210 - Main.screenPosition, vector41, new Utils.LaserLineFraming(DelegateMethods.TurretLaserDraw));
            DelegateMethods.c_1 = new Color(AAColor.ZeroShield.R, AAColor.ZeroShield.G, AAColor.ZeroShield.B, 127) * 0.75f * projectile.Opacity;
            Utils.DrawLaser(Main.spriteBatch, tex2, value20 - Main.screenPosition, value20 + projectile.velocity * num210 - Main.screenPosition, vector41 / 2f, new Utils.LaserLineFraming(DelegateMethods.TurretLaserDraw));
            return false;
        }

    }
}
