using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Sagittarius
{
    public class SagProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.aiStyle = 0;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.timeLeft = 180;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
        }

		public override void SetStaticDefaults()
		{
		    DisplayName.SetDefault("Nova Star");
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.position.X = projectile.position.X + projectile.velocity.X;
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.position.Y = projectile.position.Y + projectile.velocity.Y;
                projectile.velocity.Y = -oldVelocity.Y;
            }
            return false; // return false because we are handling collision
        }

        public override void AI()
        {
            projectile.rotation += .1f;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height, 0, 2);
            DrawAfterimage(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.oldPos, 1, projectile.rotation, projectile.direction, 1, frame, 0.8f, 1f, 7, true, 0, 0, true, new Color(AAColor.ZeroShield.R, AAColor.ZeroShield.G, AAColor.ZeroShield.B, 150));
            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.direction, 24, frame, new Color(AAColor.ZeroShield.R, AAColor.ZeroShield.G, AAColor.ZeroShield.B, 150), true);
            return false;
        }

        public static void DrawAfterimage(object sb, Texture2D texture, int shader, Vector2 position, int width, int height, Vector2[] oldPoints, float scale = 1f, float rotation = 0f, int direction = 0, int framecount = 1, Rectangle frame = default, float distanceScalar = 1.0F, float sizeScalar = 1f, int imageCount = 7, bool useOldPos = true, float offsetX = 0f, float offsetY = 0f, bool drawCentered = false, Color? overrideColor = null)
        {
            Vector2 origin = new Vector2(texture.Width / 2, texture.Height / framecount / 2);
            Color lightColor = overrideColor != null ? (Color)overrideColor : BaseDrawing.GetLightColor(position + new Vector2(width * 0.5f, height * 0.5f));
            Vector2 velAddon = default;
            Vector2 originalpos = position;
            Vector2 offset = new Vector2(offsetX, offsetY);
            for (int m = 1; m <= imageCount; m++)
            {
                scale *= sizeScalar;
                Color newLightColor = lightColor;
                newLightColor.R = (byte)(newLightColor.R * (imageCount + 3 - m) / (imageCount + 9));
                newLightColor.G = (byte)(newLightColor.G * (imageCount + 3 - m) / (imageCount + 9));
                newLightColor.B = (byte)(newLightColor.B * (imageCount + 3 - m) / (imageCount + 9));
                newLightColor.A = (byte)(newLightColor.A * (imageCount + 3 - m) / (imageCount + 9));
                if (useOldPos)
                {
                    position = Vector2.Lerp(originalpos, m - 1 >= oldPoints.Length ? oldPoints[oldPoints.Length - 1] : oldPoints[m - 1], distanceScalar);
                    BaseDrawing.DrawTexture(sb, texture, shader, position + offset, width, height, scale, rotation, direction, framecount, frame, newLightColor, drawCentered ? true : false);
                }
                else
                {
                    Vector2 velocity = m - 1 >= oldPoints.Length ? oldPoints[oldPoints.Length - 1] : oldPoints[m - 1];
                    velAddon += velocity * distanceScalar;
                    BaseDrawing.DrawTexture(sb, texture, shader, position + offset - velAddon, width, height, scale, rotation, direction, framecount, frame, newLightColor, drawCentered ? true : false);
                }
            }
        }

    }
}
