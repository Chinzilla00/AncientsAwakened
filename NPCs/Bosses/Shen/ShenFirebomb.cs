using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen
{
    public class ShenFirebomb : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discordian Firebomb");
            Main.projFrames[projectile.type] = 4;			
        }

        public override void SetDefaults()
        {
            projectile.width = 60;
            projectile.height = 60;
            projectile.hostile = true;
			projectile.aiStyle = 1;
            projectile.scale = 1f;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            cooldownSlot = 1;
        }

		public int frameCounter = 0;
        public override void AI()
        {
			frameCounter++;
			if(frameCounter > 5)
			{
				frameCounter = 0;
				projectile.frame++;
				if(projectile.frame >= 4) projectile.frame = 0;
			}
			projectile.velocity.Y += 0.01f;
			if(projectile.velocity.Y > 12) projectile.velocity.Y = 12f;
        }
		
        public override void Kill(int timeLeft)
        {
			int dustType = Main.rand.Next(3);
			dustType = (dustType == 0 ? mod.DustType<Dusts.DiscordLight>() : dustType == 1 ? mod.DustType<Dusts.AkumaDustLight>() : mod.DustType<Dusts.YamataDustLight>());	
			int pieCut = 20;
			for(int m = 0; m < pieCut; m++)
			{
				dustType = Main.rand.Next(3);
				dustType = (dustType == 0 ? mod.DustType<Dusts.DiscordLight>() : dustType == 1 ? mod.DustType<Dusts.AkumaDustLight>() : mod.DustType<Dusts.YamataDustLight>());	
				int dustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, 0f, 0f, 100, Color.White, 1.6f);
				Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default(Vector2), new Vector2(8f + Main.rand.Next(6), 0f), MathHelper.Lerp((float)Main.rand.NextDouble(), 0f, 6.28f));
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
			for(int m = 0; m < pieCut; m++)
			{
				dustType = Main.rand.Next(3);
				dustType = (dustType == 0 ? mod.DustType<Dusts.DiscordLight>() : dustType == 1 ? mod.DustType<Dusts.AkumaDustLight>() : mod.DustType<Dusts.YamataDustLight>());	
				int dustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, 0f, 0f, 100, Color.White, 2f);
				Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default(Vector2), new Vector2(8f + Main.rand.Next(6), 0f), MathHelper.Lerp((float)Main.rand.NextDouble(), 0f, 6.28f));
				Main.dust[dustID].velocity += (projectile.velocity * -0.5f);
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
			for(int m = 0; m < 15; m++)
			{
				dustType = Main.rand.Next(3);
				dustType = (dustType == 0 ? mod.DustType<Dusts.DiscordLight>() : dustType == 1 ? mod.DustType<Dusts.AkumaDustLight>() : mod.DustType<Dusts.YamataDustLight>());	
				int dustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, 0f, 0f, 100, Color.White, 1.2f);
				Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default(Vector2), new Vector2(8f + Main.rand.Next(6), 0f), MathHelper.Lerp((float)Main.rand.NextDouble(), 0f, 6.28f));
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
            Main.PlaySound(SoundID.Item62, (int)projectile.position.X, (int)projectile.position.Y);
        }		
		
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 150);
		}		
    }
}