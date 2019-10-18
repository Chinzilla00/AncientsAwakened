using Microsoft.Xna.Framework;
using Terraria;
using BaseMod;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Greed.WKG
{
    public class OreChunk : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 34;
            projectile.aiStyle = 14;
            projectile.friendly = true;
            projectile.penetrate = 6;
            projectile.magic = true;
            projectile.ignoreWater = true;
        }

		public override void SetStaticDefaults()
		{
		    DisplayName.SetDefault("Ore");
            Main.projFrames[projectile.type] = 23;
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if ((projectile.velocity.X != oldVelocity.X && (oldVelocity.X < -3f || oldVelocity.X > 3f)) || (projectile.velocity.Y != oldVelocity.Y && (oldVelocity.Y < -3f || oldVelocity.Y > 3f)))
            {
                Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
                Main.PlaySound(0, (int)projectile.Center.X, (int)projectile.Center.Y, 1, 1f, 0f);
            }
            return false;
        }

        public override void AI()
        {
            projectile.ai[0] += 1f;
            if (projectile.ai[0] > 15f)
            {
                projectile.ai[0] = 15f;
                if (projectile.velocity.Y == 0f && projectile.velocity.X != 0f)
                {
                    projectile.velocity.X = projectile.velocity.X * 0.97f;
                    if (projectile.velocity.X > -0.01 && projectile.velocity.X < 0.01)
                    {
                        projectile.Kill();
                    }
                }
                projectile.velocity.Y = projectile.velocity.Y + 0.2f;
            }
            projectile.rotation += projectile.velocity.X * 0.05f;
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }
        }

        public int Damage()
        {
            switch ((int)projectile.ai[1])
            {
                case 0:
                    return 8;
                case 1:
                    return 9;
                case 2:
                    return 10;
                case 3:
                case 4:
                    return 11;
                case 5:
                    return 12;
                case 6:
                    return 13;
                case 7:
                    return 15;
                case 8:
                    return 21;
                case 9:
                    return 19;
                case 10:
                    return 22;
                case 11:
                    return 14;
                case 12:
                    return 26;
                case 13:
                    return 36;
                case 14:
                    return 39;
                case 15:
                    return 41;
                case 16:
                    return 44;
                case 17:
                    return 47;
                case 18:
                    return 50;
                case 19:
                    return 52;
                case 20:
                    return 57;
                case 21:
                    return 75;
                case 22:
                    return 110;
                default:
                    goto case 0;
            }

        }

        public override void PostAI()
        {
            projectile.frame = (int)projectile.ai[1];
        }

        public override bool PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / 23, 0, 0);
            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 7, frame, lightColor, true);
            return false;
        }

        public override bool PreKill(int timeLeft)
        {
            int pieCut = 20;
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.ShroomDust>(), 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.ShroomDust>(), 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            return true;
        }
    }
}
