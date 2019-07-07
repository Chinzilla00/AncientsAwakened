using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Summoning.Minions
{
    public class CrowMinion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crow");
            Main.projFrames[projectile.type] = 8;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
        }
        public override void SetDefaults()
        {
			projectile.CloneDefaults(317);
			projectile.aiStyle = 54;
			aiType = 317;
            projectile.width = 40;
            projectile.height = 32;
            projectile.timeLeft = 18000;
            projectile.timeLeft *= 5;
            projectile.minionSlots = 1f;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.minion = true;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            player.AddBuff(mod.BuffType("CrowMinion"), 3600);
            if (player.dead)
			{
				modPlayer.CrowMinion = false;
			}
            if (modPlayer.CrowMinion)
			{
				projectile.timeLeft = 2;
			}
            
            if (projectile.frameCounter > 5)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 7)
                {
                    projectile.frame = 0;
                }
            }
        }
    }
}