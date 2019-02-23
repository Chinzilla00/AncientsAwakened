using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Tools
{

    public class DarkmatterJackhammerPro : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.width = 22;
            projectile.height = 52;
            projectile.aiStyle = 20;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.hide = true;
            projectile.ownerHitCheck = true;
            projectile.melee = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Darkmatter Jackhammer");
        }


        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Electrified"), 500);
        }
    }
}