using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Shen
{
    public class DiscordianInfernoF : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discordian Inferno");
        }

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.aiStyle = -1;
            projectile.penetrate = 1;
            projectile.friendly = true;
            projectile.hostile = false;
			projectile.extraUpdates = 1;
        }


        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            int dustType = mod.DustType<Dusts.Discord>();
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = 1f;
                Main.PlaySound(SoundID.DD2_BetsyFireballShot, projectile.Center);
            }
			if(Main.rand.Next(3) == 0)
			{
				int dustID2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, 0f, 0f, 100, Color.Magenta, 2f);
				Main.dust[dustID2].velocity = -projectile.velocity * 0.5f;
				Main.dust[dustID2].noLight = false;
				Main.dust[dustID2].noGravity = true;
			}
        }
        public override void Kill(int timeLeft)
        {
            int dustType =  mod.DustType<Dusts.Discord>();
            int pieCut = 20;
			for(int m = 0; m < pieCut; m++)
			{
				int dustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, 0f, 0f, 100, Color.Magenta, 1.6f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), ((float)m / (float)pieCut) * 6.28f);
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
			for(int m = 0; m < pieCut; m++)
			{
				int dustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, 0f, 0f, 100, Color.Magenta, 2f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(9f, 0f), ((float)m / (float)pieCut) * 6.28f);
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
			for(int m = 0; m < 15; m++)
			{
				int dustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, 0f, 0f, 100, Color.Magenta, 1.2f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f + Main.rand.Next(6), 0f), MathHelper.Lerp((float)Main.rand.NextDouble(), 0f, 6.28f));
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
            Main.PlaySound(SoundID.Item62, (int)projectile.position.X, (int)projectile.position.Y);
        }


        public override Color? GetAlpha(Color lightColor)
        {
            Color color = Color.Magenta;
            return new Color(color.R, color.G, color.B, 200);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("DiscordInferno"), 300);
        }
    }
}