/*using AAMod.Buffs;
using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Infinity
{
    public class Scanner : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infinity Scanner");     //The English name of the projectile
        }

        public override void SetDefaults()
        {
            projectile.width = 200;
            projectile.height = 2500;
            projectile.penetrate = -1;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Player target = Main.player[projectile.owner];
            NPC npc = Main.npc[(int)projectile.ai[0]];
            if (target == null || !target.active || target.dead)
            {
                projectile.Kill();
            }
            if (npc == null || !npc.active)
            {
                projectile.Kill();
            }
            if (target.Center.Y > npc.Top.Y - 400f)
            {
                projectile.Kill();
            }
        }

        public override void Kill(int timeLeft)
        {
            projectile.timeLeft = 0;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType<InfinityScorch>(), 240);
        }
    }
}*/