using Terraria;
using Terraria.ID;
using System;
using Terraria.ModLoader;

namespace AAMod.NPCs.TownNPCs
{
    public class StanMjolnir : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 38;
            projectile.aiStyle = 2;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
        }

		public override void SetStaticDefaults()
		{
		    DisplayName.SetDefault("Mjolnir");
		}
		
		public override void AI()
        {
            projectile.rotation += (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) * 0.03f * projectile.direction;
            if (projectile.ai[0] == 0f)
            {
                Main.PlaySound(SoundID.Item1, projectile.position);
            }
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 60f)
            {
                projectile.velocity.Y = projectile.velocity.Y + 0.2f;
                projectile.velocity.X = projectile.velocity.X * 0.99f;
            }
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }
        }

        public override void ModifyHitNPC (NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
            target.AddBuff(mod.BuffType<Buffs.Electrified>(), 200);
		}
    }
}
