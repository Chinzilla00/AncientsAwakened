using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Truffle
{
    public class TLaser : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tesla Laser");
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.aiStyle = 79;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 240;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 200);
        }
        public override void AI()
        {
            bool flag32 = true;
            int num737 = (int)projectile.ai[0] - 1;
            NPC nPC7 = Main.npc[num737];
            if (projectile.ai[0] == 0f || !nPC7.active || nPC7.type != mod.NPCType<TechnoTruffle>() || !((TechnoTruffle)nPC7.modNPC).ShotLaser)
            {
                flag32 = false;
            }
            if (!flag32)
            {
                projectile.Kill();
                return;
            }
            float num738 = nPC7.Center.Y + 46f;
            int num739 = (int)nPC7.Center.X / 16;
            int num740 = (int)num738 / 16;
            int num741 = 0;
            Tile tileHit = Main.tile[num739, num740];
            bool flag33 = tileHit.nactive() && Main.tileSolid[tileHit.type] && !Main.tileSolidTop[tileHit.type];
            if (flag33)
            {
                num741 = 1;
            }
            else
            {
                while (num741 < 150 && num740 + num741 < Main.maxTilesY)
                {
                    int num742 = num740 + num741;
                    bool flag34 = Main.tile[num739, num742].nactive() && Main.tileSolid[Main.tile[num739, num742].type] && !Main.tileSolidTop[Main.tile[num739, num742].type];
                    if (flag34)
                    {
                        num741--;
                        break;
                    }
                    num741++;
                }
            }
            projectile.position.X = nPC7.Center.X - (projectile.width / 2);
            projectile.position.Y = num738;
            projectile.height = (num741 + 1) * 16;
            int num743 = (int)projectile.position.Y + projectile.height;
            if (Main.tile[num739, num743 / 16].nactive() && Main.tileSolid[Main.tile[num739, num743 / 16].type] && !Main.tileSolidTop[Main.tile[num739, num743 / 16].type])
            {
                int num744 = num743 % 16;
                projectile.height -= num744 - 2;
            }
            for (int num745 = 0; num745 < 2; num745++)
            {
                int num746 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + projectile.height - 16f), projectile.width, 16, mod.DustType<Dusts.FulguriteDust>(), 0f, 0f, 0, default, 1f);
                Main.dust[num746].noGravity = true;
                Main.dust[num746].velocity *= 0.5f;
                Dust expr_1E976_cp_0 = Main.dust[num746];
                expr_1E976_cp_0.velocity.X -= (num745 - nPC7.velocity.X * 2f / 3f);
                Main.dust[num746].scale = 2.8f;
            }
            if (Main.rand.Next(5) == 0)
            {
                int num747 = Dust.NewDust(new Vector2(projectile.position.X + projectile.width / 2 - projectile.width / 2 * Math.Sign(nPC7.velocity.X) - 4f, projectile.position.Y + projectile.height - 16f), 4, 16, 31, 0f, 0f, 100, default, 1.5f);
                Main.dust[num747].velocity *= 0.5f;
                Dust expr_1EA8C_cp_0 = Main.dust[num747];
                expr_1EA8C_cp_0.velocity.X -= nPC7.velocity.X / 2f;
                Main.dust[num747].velocity.Y = -Math.Abs(Main.dust[num747].velocity.Y);
            }
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 4)
                {
                    projectile.frame = 0;
                    return;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            lightColor = projectile.GetAlpha(lightColor);
            Texture2D texture2D16 = Main.projectileTexture[projectile.type];
            Texture2D texture2D17 = mod.GetTexture("NPCs/Bosses/Truffle/TLaser_Head");
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            int num220 = texture2D16.Height / Main.projFrames[projectile.type];
            int y9 = num220 * projectile.frame;
            int num221 = texture2D17.Height / Main.projFrames[projectile.type];
            int num222 = num221 * projectile.frame;
            Rectangle value20 = new Rectangle(0, num222, texture2D17.Width, num221);
            Vector2 vector28 = projectile.position + new Vector2(projectile.width, 0f) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
            Main.spriteBatch.Draw(texture2D17, vector28, new Rectangle?(value20), lightColor, projectile.rotation, new Vector2(texture2D17.Width / 2, 0f), projectile.scale, spriteEffects, 0f);
            int num223 = projectile.height - num220 - 14;
            if (num223 < 0)
            {
                num223 = 0;
            }
            if (num223 > 0)
            {
                if (num222 == num221 * 3)
                {
                    num222 = num221 * 2;
                }
                Main.spriteBatch.Draw(texture2D17, vector28 + Vector2.UnitY * (num221 - 1), new Rectangle?(new Rectangle(0, num222 + num221 - 1, texture2D17.Width, 1)), lightColor, projectile.rotation, new Vector2(texture2D17.Width / 2, 0f), new Vector2(1f, num223), spriteEffects, 0f);
            }
            value20.Width = texture2D16.Width;
            value20.Y = y9;
            Main.spriteBatch.Draw(texture2D16, vector28 + Vector2.UnitY * (num221 - 1 + num223), new Rectangle?(value20), lightColor, projectile.rotation, new Vector2(texture2D16.Width / 2f, 0f), projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}