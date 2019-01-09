using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Dusts
{
    public class SnowDustLight : ModDust
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
            return false;
        }

        public override bool MidUpdate(Dust dust)
        {
            if (!dust.noLight)
            {
                float strength = dust.scale * 1.4f;
                if (strength > 1f)
                {
                    strength = 1f;
                }
                Lighting.AddLight(dust.position, 0f * strength, 0.3f * strength, 0.3f * strength);
            }
            return false;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return new Color(lightColor.R, lightColor.G, lightColor.B, 25);
        }
    }
}