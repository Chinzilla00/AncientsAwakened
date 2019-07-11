using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Ryusei : ModProjectile
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
            BaseMod.BaseAI.AIFlail(projectile, ref projectile.ai, false, 250);
            projectile.direction = projectile.spriteDirection = Main.player[projectile.owner].direction;
            if ((Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) / 2f > 0.52f)
            {
                rot += (float)Math.PI / 16f;
            }
            else { rot *= 0.9f; if (rot < (float)Math.PI / 20f) { rot = 0f; } }
            projectile.rotation += rot;
        }

        public override bool OnTileCollide(Vector2 value2)
        {
            BaseMod.BaseAI.TileCollideFlail(projectile, ref value2);
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color dColor)
        {
            Texture2D chainTex = mod.GetTexture("Projectiles/Ryusei_Chain");
            if (Main.instance.IsActive)
                for (int m = 0; m < 2; m++)
                    BaseMod.BaseDrawing.DrawChain(spriteBatch, chainTex, 0, projectile.Center, Main.player[projectile.owner].Center);

            Rectangle frame = BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height, 0, 2);
            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 1, frame, dColor, true);
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
            target.AddBuff(mod.BuffType<Buffs.DragonFire>(), 300);
		}
    }
}
