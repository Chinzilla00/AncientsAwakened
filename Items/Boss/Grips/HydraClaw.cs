using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Grips
{
    public class HydraClaw : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hydra Claw");
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

        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            player.twinsMinion = false;
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