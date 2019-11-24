using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Zero.Protocol
{
    public class GlitchBoom : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("b00m");     
            Main.projFrames[projectile.type] = 7;     
        }

        public override void SetDefaults()
        {
            projectile.width = 98;
            projectile.height = 98;
            projectile.penetrate = -1;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 600;
        }


        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (Main.rand.Next(3) == 0)
            {
                target.AddBuff(ModContent.BuffType<Buffs.Unstable>(), 360);
            }
        }

        public override void AI()
        {
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 6)
                {
                    projectile.Kill();

                }
            }
            projectile.velocity.X *= 0.00f;
            projectile.velocity.Y *= 0.00f;

        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Glitch"), (int)projectile.Center.X, (int)projectile.Center.Y);
            projectile.timeLeft = 0;
        }

    }
}
