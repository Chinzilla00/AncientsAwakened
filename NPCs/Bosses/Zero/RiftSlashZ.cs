using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Zero
{
    // to investigate: Projectile.Damage, (8843)
    class RiftSlashZ : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 50;
            projectile.height = 50;
            projectile.aiStyle = 27;
            projectile.hostile = true;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 240;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
            projectile.alpha = 50;
        }

        public override void AI()
        {
            Lighting.AddLight((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16, 0.3f * 1, 0.4f * 0, 1f * .2f);
        }

        public override bool PreDraw(SpriteBatch spritebatch, Color lightColor)
        {
            BaseDrawing.DrawAfterimage(spritebatch, Main.npcTexture[projectile.type], 0, projectile, 1.5f, 1f, 5, false, 0f, 0f, projectile.GetAlpha(AAColor.ZeroShield));
            return true;
        }
    }
}
