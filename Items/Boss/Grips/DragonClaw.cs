using AAMod.Items.Summoning.Minions;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Grips
{
    public class DragonClaw : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Claw");
            Main.projFrames[projectile.type] = 5;
        }
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Spazmamini);
            projectile.width = 28;
            projectile.height = 24;
            projectile.damage = 15;
            projectile.timeLeft *= 5;
            projectile.minionSlots = 1f;
            projectile.aiStyle = 66;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            player.twinsMinion = false;
            projectile.frameCounter++;
            if (projectile.frameCounter > 4)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 4)
                {
                    projectile.frame = 0;
                }
            }
        }

        public void CheckActive()
        {
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = (AAPlayer)player.GetModPlayer(mod, "AAPlayer");
            if (player.dead)
            {
                modPlayer.GripMinion = false;
            }
            if (modPlayer.GripMinion)
            {
                projectile.timeLeft = 2;
            }
        }

        

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            target.AddBuff(BuffID.OnFire, 100);
        }
    }
}