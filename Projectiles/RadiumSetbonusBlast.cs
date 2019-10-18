using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class RadiumSetbonusBlast : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.width = projectile.height = 4;
            projectile.usesLocalNPCImmunity = true;
            projectile.timeLeft = 3;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.minion = true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 0;
            projectile.localNPCImmunity[target.whoAmI] = -1;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return (targetHitbox.Center() - projHitbox.Center()).Length() < projectile.ai[0] + Math.Min(targetHitbox.Width, targetHitbox.Height);
        }
    }
}
