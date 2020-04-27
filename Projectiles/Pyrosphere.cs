using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using System;

namespace AAMod.Projectiles
{
    public class Pyrosphere : ModProjectile
	{
        public float rot = 0;

        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;
            projectile.aiStyle = -1;
            projectile.timeLeft = 3600;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.damage = 1;
            projectile.penetrate = -1;
            projectile.knockBack = 3;
            projectile.melee = true;
        }

        public override void AI()
        {
            BaseAI.AIFlail(projectile, ref projectile.ai, false, 160);
            projectile.direction = projectile.spriteDirection = Main.player[projectile.owner].direction;
            if ((Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) / 2f > 0.52f)
            {
                rot += (float)Math.PI / 16f;
            }
            else { rot *= 0.9f; if (rot < (float)Math.PI / 20f) { rot = 0f; } }
            projectile.rotation += rot;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }

        public override bool OnTileCollide(Vector2 value2)
        {
            BaseAI.TileCollideFlail(projectile, ref value2);
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color dColor)
        {
            Texture2D chainTex = mod.GetTexture("Chains/Pyrosphere_Chain");
            if (Main.instance.IsActive)
                for (int m = 0; m < 2; m++)
                    BaseDrawing.DrawChain(spriteBatch, chainTex, 0, projectile.Center, Main.player[projectile.owner].Center);
            return true;
        }
    }
}