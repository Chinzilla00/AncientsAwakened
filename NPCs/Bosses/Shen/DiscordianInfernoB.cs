using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen
{
    public class DiscordianInfernoB : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discordian Inferno");
        }

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.aiStyle = 1;
            projectile.alpha = 250;
            projectile.penetrate = -1;
            projectile.friendly = false;
            projectile.hostile = true;
			projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            int dustType = mod.DustType<Dusts.AkumaADust>();
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = 1f;
                Main.PlaySound(SoundID.DD2_BetsyFireballShot, projectile.Center);
            }
            if (projectile.ai[0] >= 2f)
            {
                projectile.alpha -= 30;
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
            }
			if(projectile.alpha < 50 && Main.rand.Next(3) == 0)
			{
				for(int m = 0; m < 3; m++)
				{
					int dustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[dustID].velocity = -projectile.velocity * 0.5f;
					Main.dust[dustID].noLight = false;
					Main.dust[dustID].noGravity = true;
				}
				int dustID2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, 0f, 0f, 100, Color.Purple, 2f);
				Main.dust[dustID2].velocity = -projectile.velocity * 0.5f;
				Main.dust[dustID2].noLight = false;
				Main.dust[dustID2].noGravity = true;
			}
        }
        public override void Kill(int timeLeft)
        {
            int dustType = mod.DustType<Dusts.AkumaADust>();
            int pieCut = 20;
			for(int m = 0; m < pieCut; m++)
			{
				int dustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, 0f, 0f, 100, Color.White, 1.6f);
				Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), ((float)m / (float)pieCut) * 6.28f);
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
			for(int m = 0; m < pieCut; m++)
			{
				int dustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, 0f, 0f, 100, Color.White, 2f);
				Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default(Vector2), new Vector2(9f, 0f), ((float)m / (float)pieCut) * 6.28f);
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
			for(int m = 0; m < 15; m++)
			{
				int dustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, 0f, 0f, 100, Color.White, 1.2f);
				Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default(Vector2), new Vector2(8f + Main.rand.Next(6), 0f), MathHelper.Lerp((float)Main.rand.NextDouble(), 0f, 6.28f));
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
            Main.PlaySound(SoundID.Item62, (int)projectile.position.X, (int)projectile.position.Y);
        }

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 50);
		}		
    }
}