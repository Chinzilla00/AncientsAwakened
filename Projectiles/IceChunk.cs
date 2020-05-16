using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class IceChunk : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Chunk");
        }
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.melee = true;
            projectile.penetrate = 1;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.timeLeft = 200;
        }

        public override void AI()
        {
            projectile.localAI[0]++;
            projectile.rotation += 0.06f;
            projectile.velocity.Y += 0.2f;

            if (projectile.localAI[0] > 130f) //projectile time left before disappears
            {
                projectile.Kill();
            }
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            target.AddBuff(BuffID.Frostburn, 90);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(projectile.position, oldVelocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item, (int)projectile.position.X, (int)projectile.position.Y, 50);
            return true;
        }
    }
}