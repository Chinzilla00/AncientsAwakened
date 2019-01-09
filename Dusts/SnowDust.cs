using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Dusts
{
    public class SnowDust : ModDust
	{
        public override void OnSpawn(Dust dust)
        {
            dust.alpha = 1;
            dust.scale = 1.2f;
            dust.velocity *= 0.4f;
            dust.noGravity = true;
            dust.noLight = true;
        }

        public override bool Update(Dust dust)
        {
            dust.velocity.Y -= 0.05f;
            dust.rotation += dust.velocity.X / 3f;
            dust.position += dust.velocity;
            int oldAlpha = dust.alpha;
            dust.alpha = (int)(dust.alpha * 1.2);
            if (dust.alpha == oldAlpha)
            {
                dust.alpha++;
            }
            if (dust.alpha >= 255)
            {
                dust.alpha = 255;
                dust.active = false;
            }
            bool flag5 = WorldGen.SolidTile(Framing.GetTileSafely((int)dust.position.X / 16, (int)dust.position.Y / 16));
            if (flag5)
            {
                dust.noLight = true;
            }
            return false;
        }

        public override bool MidUpdate(Dust dust)
        {
            dust.rotation += dust.velocity.X / 3f;
            if (!dust.noLight)
            {
                float strength = dust.scale * 1.4f;
                if (strength > 1f)
                {
                    strength = 1f;
                }
                Lighting.AddLight(dust.position, 0f * strength, 0.3f * strength, 0.3f * strength);
            }
            if (Collision.SolidCollision(dust.position - Vector2.One * 5f, 10, 10) && dust.fadeIn == 0f)
            {
                dust.scale *= 0.9f;
                dust.velocity *= 0.1f;
            }
            return false;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return new Color(lightColor.R, lightColor.G, lightColor.B, 25);
        }
    }
}