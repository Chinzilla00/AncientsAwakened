using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAmod.Projectiles
{

    public class Drop : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.friendly = false;
            projectile.hostile = false; 
            projectile.magic = true; 
            projectile.tileCollide = true;
            projectile.penetrate = 10;
            projectile.timeLeft = 600;
            projectile.light = 0.25f;
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
            projectile.damage = 10;
            projectile.scale = 1f;
            projectile.usesIDStaticNPCImmunity = false;
            projectile.usesLocalNPCImmunity = true;
        }

        private const int AI_Timer_Slot = 1;

        public float AI_Timer
        {
            get => projectile.ai[AI_Timer_Slot];
            set => projectile.ai[AI_Timer_Slot] = value;
        }

        public override void AI()
        {
            projectile.rotation = ((float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f) + ((float)Math.PI);
            if (Main.rand.Next(12) == 0)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("AbyssDust"), projectile.velocity.X * 0.25f, projectile.velocity.Y * 0.25f, 150, default, 0.7f);
            }

            projectile.velocity.Y = projectile.velocity.Y + 0.08f;
            if (projectile.velocity.Y >= 0)
            {
                projectile.friendly = true;
            }
            else
            {
                projectile.friendly = false;
            }

        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.NPCHit3, projectile.position);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) // Want some Venom?
        {
        //target.AddBuff(BuffID.Venom, 180);
        }

    }
}
