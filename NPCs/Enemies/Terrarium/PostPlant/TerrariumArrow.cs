using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Terrarium.PostPlant
{
    public class TerrariumArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.WoodenArrowHostile);
            projectile.hostile = true;
            projectile.friendly = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deadshot");
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType<Buffs.Terrablaze>(), 300);
        }
    }
}
