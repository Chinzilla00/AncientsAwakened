using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using BaseMod;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.NPCs.Bosses.Athena
{
	public class Hurricane : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = -1;
            projectile.penetrate = 1;
            projectile.timeLeft = 900;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.alpha = 255;
            projectile.hostile = true;
        }

        public override void AI()
        {
            Color newColor3 = new Color(255, 255, 255);
            if (projectile.soundDelay == 0)
            {
                projectile.soundDelay = -1;
                Main.PlaySound(SoundID.Item60, projectile.Center);
            }
            if (projectile.localAI[1] < 30f)
            {
                for (int num1132 = 0; num1132 < 1; num1132++)
                {
                    float value79 = -0.5f;
                    float value80 = 0.9f;
                    float amount4 = Main.rand.NextFloat();
                    Vector2 value81 = new Vector2(MathHelper.Lerp(0.1f, 1f, Main.rand.NextFloat()), MathHelper.Lerp(value79, value80, amount4));
                    value81.X *= MathHelper.Lerp(2.2f, 0.6f, amount4);
                    value81.X *= -1f;
                    Vector2 value82 = new Vector2(2f, 10f);
                    Vector2 position4 = projectile.Center + new Vector2(60f, 200f) * value81 * 0.5f + value82;
                    Dust dust35 = Main.dust[Dust.NewDust(position4, 0, 0, 187, 0f, 0f, 0, default, 1f)];
                    dust35.position = position4;
                    dust35.customData = projectile.Center + value82;
                    dust35.fadeIn = 1f;
                    dust35.scale = 0.3f;
                    if (value81.X > -1.2f)
                    {
                        dust35.velocity.X = 1f + Main.rand.NextFloat();
                    }
                    dust35.velocity.Y = Main.rand.NextFloat() * -0.5f - 1f;
                }
            }
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = 0.8f;
                projectile.direction = 1;
                Point point9 = projectile.Center.ToTileCoordinates();
                projectile.Center = new Vector2(point9.X * 16 + 8, point9.Y * 16 + 8);
            }
            projectile.rotation = projectile.localAI[1] / 40f * 6.28318548f * projectile.direction;
            if (projectile.localAI[1] < 33f)
            {
                if (projectile.alpha > 0)
                {
                    projectile.alpha -= 8;
                }
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
            }
            if (projectile.localAI[1] > 103f)
            {
                if (projectile.alpha < 255)
                {
                    projectile.alpha += 16;
                }
                if (projectile.alpha > 255)
                {
                    projectile.alpha = 255;
                }
            }
            if (projectile.alpha == 0)
            {
                Lighting.AddLight(projectile.Center, newColor3.ToVector3() * 0.5f);
            }
            for (int num1133 = 0; num1133 < 2; num1133++)
            {
                if (Main.rand.Next(10) == 0)
                {
                    Vector2 value83 = Vector2.UnitY.RotatedBy(num1133 * 3.14159274f).RotatedBy(projectile.rotation);
                    Dust dust36 = Main.dust[Dust.NewDust(projectile.Center, 0, 0, 187, 0f, 0f, 225, newColor3, 1.5f)];
                    dust36.noGravity = true;
                    dust36.noLight = true;
                    dust36.scale = projectile.Opacity * projectile.localAI[0];
                    dust36.position = projectile.Center;
                    dust36.velocity = value83 * 2.5f;
                }
            }
            for (int num1134 = 0; num1134 < 2; num1134++)
            {
                if (Main.rand.Next(10) == 0)
                {
                    Vector2 value84 = Vector2.UnitY.RotatedBy(num1134 * 3.14159274f);
                    Dust dust37 = Main.dust[Dust.NewDust(projectile.Center, 0, 0, 187, 0f, 0f, 225, newColor3, 1.5f)];
                    dust37.noGravity = true;
                    dust37.noLight = true;
                    dust37.scale = projectile.Opacity * projectile.localAI[0];
                    dust37.position = projectile.Center;
                    dust37.velocity = value84 * 2.5f;
                }
            }
            if (projectile.localAI[1] < 33f || projectile.localAI[1] > 87f)
            {
                projectile.scale = projectile.Opacity / 2f * projectile.localAI[0];
            }
            projectile.velocity = Vector2.Zero;
            projectile.localAI[1] += 1f;
            if (projectile.localAI[1] == 60f && projectile.owner == Main.myPlayer)
            {
                int num1135 = 30;
                if (Main.expertMode)
                {
                    num1135 = 22;
                }
                Projectile.NewProjectile(projectile.Center, Vector2.Zero, 657, num1135, 3f, projectile.owner, 0f, 0f);
            }
            if (projectile.localAI[1] >= 120f)
            {
                projectile.Kill();
                return;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            SpriteEffects spriteEffects = projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Vector2 vector42 = projectile.position + new Vector2(projectile.width, projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
            Texture2D texture2D33 = Main.projectileTexture[projectile.type];
            Rectangle rectangle15 = texture2D33.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);
            Color alpha5 = projectile.GetAlpha(lightColor);
            Vector2 origin11 = rectangle15.Size() / 2f;
            Color color61 = Main.hslToRgb(0.136f, 1f, 0.5f).MultiplyRGBA(new Color(255, 255, 255, 0));
            Main.spriteBatch.Draw(texture2D33, vector42, new Rectangle?(rectangle15), color61, 0f, origin11, new Vector2(1f, 5f) * projectile.scale * 2f, spriteEffects, 0f);
            Main.spriteBatch.Draw(texture2D33, vector42, new Rectangle?(rectangle15), alpha5, projectile.rotation, origin11, projectile.scale, spriteEffects, 0f);
            Main.spriteBatch.Draw(texture2D33, vector42, new Rectangle?(rectangle15), alpha5, 0f, origin11, new Vector2(1f, 8f) * projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}