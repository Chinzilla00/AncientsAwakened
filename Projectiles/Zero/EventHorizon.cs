using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Enums;

namespace AAMod.Projectiles.Zero
{
    public class EventHorizon : ModProjectile
    {
        public short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
            projectile.glowMask = customGlowMask;
        }
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = 75;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.hide = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 6;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            float num = 1.57079637f;
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            projectile.alpha = 0;
            if (projectile.localAI[1] > 0f)
            {
                projectile.localAI[1] -= 1f;
            }
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = projectile.velocity.ToRotation();
            }
            float num32 = (projectile.localAI[0].ToRotationVector2().X >= 0f) ? 1 : -1;
            if (projectile.ai[1] <= 0f)
            {
                num32 *= -1f;
            }
            Vector2 vector17 = (num32 * ((projectile.ai[0] / 30f * 6.28318548f) - 1.57079637f)).ToRotationVector2();
            vector17.Y *= (float)Math.Sin(projectile.ai[1]);
            if (projectile.ai[1] <= 0f)
            {
                vector17.Y *= -1f;
            }
            vector17 = vector17.RotatedBy(projectile.localAI[0], default);
            projectile.ai[0] += 1f;
            if (projectile.ai[0] < 30f)
            {
                projectile.velocity += 48f * vector17;
            }
            else
            {
                projectile.Kill();
            }
            projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - (projectile.Size / 2f);
            projectile.rotation = projectile.velocity.ToRotation() + num;
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2(projectile.velocity.Y * projectile.direction, projectile.velocity.X * projectile.direction);
            Vector2 vector24 = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
            if (player.direction != 1)
            {
                vector24.X = player.bodyFrame.Width - vector24.X;
            }
            if (player.gravDir != 1f)
            {
                vector24.Y = player.bodyFrame.Height - vector24.Y;
            }
            vector24 -= new Vector2(player.bodyFrame.Width - player.width, player.bodyFrame.Height - 42) / 2f;
            projectile.Center = player.RotatedRelativePoint(player.position + vector24, true) - projectile.velocity;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
            Color color25 = Lighting.GetColor((int)(projectile.position.X + (projectile.width * 0.5)) / 16, (int)((projectile.position.Y + (projectile.height * 0.5)) / 16.0));
            if (projectile.hide && !ProjectileID.Sets.DontAttachHideToAlpha[projectile.type])
            {
                color25 = Lighting.GetColor((int)mountedCenter.X / 16, (int)(mountedCenter.Y / 16f));
            }
            Texture2D texture2D22 = Main.projectileTexture[projectile.type];
            Color alpha3 = projectile.GetAlpha(color25);
            if (projectile.velocity == Vector2.Zero)
            {
                return false;
            }
            float num230 = projectile.velocity.Length() + 16f;
            bool flag24 = num230 < 100f;
            Vector2 value28 = Vector2.Normalize(projectile.velocity);
            Rectangle rectangle8 = new Rectangle(0, 0, texture2D22.Width, 36); //2 and 40
            Vector2 value29 = new Vector2(0f, Main.player[projectile.owner].gfxOffY);
            float rotation24 = projectile.rotation + 3.14159274f;
            Main.spriteBatch.Draw(texture2D22, projectile.Center.Floor() - Main.screenPosition + value29, new Microsoft.Xna.Framework.Rectangle?(rectangle8), alpha3, rotation24, (rectangle8.Size() / 2f) - (Vector2.UnitY * 4f), projectile.scale, SpriteEffects.None, 0f);
            num230 -= 40f * projectile.scale;
            Vector2 vector31 = projectile.Center.Floor();
            vector31 += value28 * projectile.scale * 24f;
            rectangle8 = new Rectangle(0, 62, texture2D22.Width, 18); //68 and 18
            if (num230 > 0f)
            {
                float num231 = 0f;
                while (num231 + 1f < num230)
                {
                    if (num230 - num231 < rectangle8.Height)
                    {
                        rectangle8.Height = (int)(num230 - num231);
                    }
                    Main.spriteBatch.Draw(texture2D22, vector31 - Main.screenPosition + value29, new Microsoft.Xna.Framework.Rectangle?(rectangle8), alpha3, rotation24, new Vector2(rectangle8.Width / 2, 0f), projectile.scale, SpriteEffects.None, 0f);
                    num231 += rectangle8.Height * projectile.scale;
                    vector31 += value28 * rectangle8.Height * projectile.scale;
                }
            }
            Vector2 value30 = vector31;
            vector31 = projectile.Center.Floor();
            vector31 += value28 * projectile.scale * 24f;
            rectangle8 = new Rectangle(0, 40, texture2D22.Width, 20); //46 and 18
            int num232 = 18;
            if (flag24)
            {
                num232 = 9;
            }
            float num233 = num230;
            if (num230 > 0f)
            {
                float num234 = 0f;
                float num235 = num233 / num232;
                num234 += num235 * 0.25f;
                vector31 += value28 * num235 * 0.25f;
                for (int num236 = 0; num236 < num232; num236++)
                {
                    float num237 = num235;
                    if (num236 == 0)
                    {
                        num237 *= 0.75f;
                    }
                    Main.spriteBatch.Draw(texture2D22, vector31 - Main.screenPosition + value29, new Microsoft.Xna.Framework.Rectangle?(rectangle8), alpha3, rotation24, new Vector2(rectangle8.Width / 2, 0f), projectile.scale, SpriteEffects.None, 0f);
                    num234 += num237;
                    vector31 += value28 * num237;
                }
            }
            rectangle8 = new Rectangle(0, 84, texture2D22.Width, 56); //90 and 48
            Main.spriteBatch.Draw(texture2D22, value30 - Main.screenPosition + value29, new Microsoft.Xna.Framework.Rectangle?(rectangle8), alpha3, rotation24, texture2D22.Frame(1, 1, 0, 0).Top(), projectile.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override void CutTiles()
        {
            DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
            Vector2 unit = projectile.velocity;
            Utils.PlotTileLine(projectile.Center, projectile.Center + (unit * projectile.localAI[1]), projectile.width * projectile.scale, new Utils.PerLinePoint(DelegateMethods.CutTiles));
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projHitbox.Intersects(targetHitbox))
            {
                return true;
            }
            float num8 = 0f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center, projectile.Center + projectile.velocity, 16f * projectile.scale, ref num8))
            {
                return true;
            }
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center, Vector2.Zero, ModContent.ProjectileType<Horizon>(), projectile.damage*2, 0, projectile.owner, 0, 0);
        }
    }
}