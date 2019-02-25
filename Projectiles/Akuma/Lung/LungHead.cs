using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using AAMod;
using Terraria.Graphics.Shaders;

namespace AAMod.Projectiles.Akuma.Lung
{
    public class LungHead : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.netImportant = true;
            projectile.tileCollide = false;
            projectile.minion = true;

            projectile.penetrate = -1;
            projectile.timeLeft = 18000;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            projectile.timeLeft *= 5;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Lung");
            Main.projFrames[projectile.type] = 2;
        }
        public override void AI()
        {
            // vanilla code
            Player player10 = Main.player[projectile.owner];
            if ((int)Main.time % 120 == 0)
            {
                projectile.netUpdate = true;
            }
            if (!player10.active)
            {
                projectile.active = false;
                return;
            }
            int num1049 = 10;
            // head
            Vector2 center14 = player10.Center;
            float maxTargetDist = 700f;
            float num1052 = 1000f;
            int num1053 = -1;
            if (projectile.Distance(center14) > 2000f)
            {
                projectile.Center = center14;
                projectile.netUpdate = true;
            }
            bool flag67 = true;
            if (flag67)
            {
                NPC ownerMinionAttackTargetNPC5 = projectile.OwnerMinionAttackTargetNPC;
                if (ownerMinionAttackTargetNPC5 != null && ownerMinionAttackTargetNPC5.CanBeChasedBy(projectile, false))
                {
                    float num1054 = projectile.Distance(ownerMinionAttackTargetNPC5.Center);
                    if (num1054 < maxTargetDist * 2f)
                    {
                        num1053 = ownerMinionAttackTargetNPC5.whoAmI;
                        if (ownerMinionAttackTargetNPC5.boss)
                        {
                            int whoAmI = ownerMinionAttackTargetNPC5.whoAmI;
                        }
                        else
                        {
                            int whoAmI2 = ownerMinionAttackTargetNPC5.whoAmI;
                        }
                    }
                }
                if (num1053 < 0)
                {
                    for (int i = 0; i < Main.npc.Length; i++)
                    {
                        NPC target = Main.npc[i];
                        if (target.CanBeChasedBy(projectile, false) && player10.Distance(target.Center) < num1052)
                        {
                            float num1056 = projectile.Distance(target.Center);
                            if (num1056 < maxTargetDist)
                            {
                                num1053 = i;
                            }
                        }
                    }
                }
            }
            if (num1053 != -1)
            {
                NPC nPC15 = Main.npc[num1053];
                Vector2 vector148 = nPC15.Center - projectile.Center;
                float num1057 = (float)(vector148.X > 0f).ToDirectionInt();
                float num1058 = (float)(vector148.Y > 0f).ToDirectionInt();
                float scaleFactor15 = 0.4f;
                if (vector148.Length() < 600f)
                {
                    scaleFactor15 = 0.6f;
                }
                if (vector148.Length() < 300f)
                {
                    scaleFactor15 = 0.8f;
                }
                if (vector148.Length() > nPC15.Size.Length() * 0.75f)
                {
                    projectile.velocity += Vector2.Normalize(vector148) * scaleFactor15 * 1.5f;
                    if (Vector2.Dot(projectile.velocity, vector148) < 0.25f)
                    {
                        projectile.velocity *= 0.8f;
                    }
                }
                float num1059 = 30f;
                if (projectile.velocity.Length() > num1059)
                {
                    projectile.velocity = Vector2.Normalize(projectile.velocity) * num1059;
                }
            }
            else
            {
                float num1060 = 0.2f;
                Vector2 vector149 = center14 - projectile.Center;
                if (vector149.Length() < 200f)
                {
                    num1060 = 0.12f;
                }
                if (vector149.Length() < 140f)
                {
                    num1060 = 0.06f;
                }
                if (vector149.Length() > 100f)
                {
                    if (Math.Abs(center14.X - projectile.Center.X) > 20f)
                    {
                        projectile.velocity.X = projectile.velocity.X + num1060 * (float)Math.Sign(center14.X - projectile.Center.X);
                    }
                    if (Math.Abs(center14.Y - projectile.Center.Y) > 10f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y + num1060 * (float)Math.Sign(center14.Y - projectile.Center.Y);
                    }
                }
                else if (projectile.velocity.Length() > 2f)
                {
                    projectile.velocity *= 0.96f;
                }
                if (Math.Abs(projectile.velocity.Y) < 1f)
                {
                    projectile.velocity.Y = projectile.velocity.Y - 0.1f;
                }
                float num1061 = 15f;
                if (projectile.velocity.Length() > num1061)
                {
                    projectile.velocity = Vector2.Normalize(projectile.velocity) * num1061;
                }
            }
            projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            int direction = projectile.direction;
            projectile.direction = (projectile.spriteDirection = ((projectile.velocity.X > 0f) ? 1 : -1));
            if (direction != projectile.direction)
            {
                projectile.netUpdate = true;
            }
            float num1062 = MathHelper.Clamp(projectile.localAI[0], 0f, 50f);
            projectile.position = projectile.Center;
            projectile.scale = 1f + num1062 * 0.01f;
            projectile.width = (projectile.height = (int)((float)num1049 * projectile.scale));
            projectile.Center = projectile.position;
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 42;
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                    return;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[projectile.type];
            Texture2D glowTex = mod.GetTexture("Glowmasks/Shenling_Glow");
            Texture2D glowTex2 = mod.GetTexture("Glowmasks/ShenlingBody_Glow1");
            Texture2D glowTex3 = mod.GetTexture("Glowmasks/ShenlingBody_Glow2");
            Texture2D glowTex4 = mod.GetTexture("Glowmasks/ShenlingBody_Glow3");
            Texture2D glowTex5 = mod.GetTexture("Glowmasks/ShenlingTail_Glow");

            int shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingOceanDye);


            Texture2D myGlowTex = (projectile.type == mod.ProjectileType<LungHead>() ? glowTex : projectile.type == mod.ProjectileType<LungBody>() ? glowTex2 : projectile.type == mod.ProjectileType<LungTail>() ? glowTex2 : glowTex5);
            var effects = projectile.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            BaseDrawing.DrawTexture(spriteBatch, texture, 0, projectile, projectile.GetAlpha(Color.White), true);
            BaseDrawing.DrawTexture(spriteBatch, myGlowTex, shader, projectile, projectile.GetAlpha(Color.White), true);
            if (projectile.type == mod.ProjectileType<LungBody>())
            {
                BaseDrawing.DrawTexture(spriteBatch, glowTex3, shader, projectile, drawColor, true);
                BaseDrawing.DrawTexture(spriteBatch, glowTex4, shader, projectile, drawColor, true);
            }
            return false;
        }

        public class LungBody : ModProjectile
        {
            public override void SetDefaults()
            {
                projectile.width = 24;
                projectile.height = 24;

                projectile.friendly = true;
                projectile.ignoreWater = true;
                projectile.netImportant = true;
                projectile.tileCollide = false;
                projectile.minion = true;

                projectile.penetrate = -1;
                projectile.timeLeft = 18000;
                ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
                projectile.timeLeft *= 5;
                projectile.minionSlots = 1f;
            }

            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("Ancient Lung");
                Main.projFrames[projectile.type] = 1;
            }
            public override void AI()
            {
                Player player10 = Main.player[projectile.owner];
                if (!player10.active)
                {
                    projectile.active = false;
                    return;
                }

                int num1049 = 10;
                Vector2 parCenter = Vector2.Zero;
                float parRot = 0f;
                float scaleFactor16 = 0f;
                float scaleFactor17 = 1f;
                if (projectile.ai[1] == 1f)
                {
                    projectile.ai[1] = 0f;
                    projectile.netUpdate = true;
                }
                Projectile parent = Main.projectile[(int)projectile.ai[0]];
                if ((int)projectile.ai[0] >= 0 && parent.active)
                {
                    parCenter = parent.Center;
                    parRot = parent.rotation;
                    scaleFactor17 = MathHelper.Clamp(parent.scale, 0f, 50f);
                    scaleFactor16 = 16f;
                    parent.localAI[0] = projectile.localAI[0] + 1f;
                }
                else
                {
                    return;
                }
                if (projectile.alpha > 0)
                {
                    int num3;
                    for (int num1068 = 0; num1068 < 2; num1068 = num3 + 1)
                    {
                        int num1069 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 135, 0f, 0f, 100, default(Color), 2f);
                        Main.dust[num1069].noGravity = true;
                        Main.dust[num1069].noLight = true;
                        num3 = num1068;
                    }
                }
                projectile.alpha -= 42;
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
                projectile.velocity = Vector2.Zero;
                Vector2 vector151 = parCenter - projectile.Center;
                if (parRot != projectile.rotation)
                {
                    float num1070 = MathHelper.WrapAngle(parRot - projectile.rotation);
                    vector151 = vector151.RotatedBy((double)(num1070 * 0.1f), default(Vector2));
                }
                projectile.rotation = vector151.ToRotation() + 1.57079637f;
                projectile.position = projectile.Center;
                projectile.scale = scaleFactor17;
                projectile.width = (projectile.height = (int)((float)num1049 * projectile.scale));
                projectile.Center = projectile.position;
                if (vector151 != Vector2.Zero)
                {
                    projectile.Center = parCenter - Vector2.Normalize(vector151) * scaleFactor16 * scaleFactor17;
                }
                projectile.spriteDirection = ((vector151.X > 0f) ? 1 : -1);
                return;
            }
        }
        public class LungTail : ModProjectile
        {
            public override void SetDefaults()
            {
                projectile.width = 24;
                projectile.height = 24;

                projectile.friendly = true;
                projectile.ignoreWater = true;
                projectile.netImportant = true;
                projectile.tileCollide = false;
                projectile.minion = true;

                projectile.penetrate = -1;
                projectile.timeLeft = 18000;
                ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
                projectile.timeLeft *= 5;
            }

            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("Ancient Lung");
                Main.projFrames[projectile.type] = 1;
            }
            public override void AI()
            {
                Player player10 = Main.player[projectile.owner];
                if (!player10.active)
                {
                    projectile.active = false;
                    return;
                }

                int num1049 = 10;
                Vector2 parCenter = Vector2.Zero;
                float parRot = 0f;
                float scaleFactor16 = 0f;
                float scaleFactor17 = 1f;
                if (projectile.ai[1] == 1f)
                {
                    projectile.ai[1] = 0f;
                    projectile.netUpdate = true;
                }
                Projectile parent = Main.projectile[(int)projectile.ai[0]];
                if ((int)projectile.ai[0] >= 0 && parent.active)
                {
                    parCenter = parent.Center;
                    parRot = parent.rotation;
                    scaleFactor17 = MathHelper.Clamp(parent.scale, 0f, 50f);
                    scaleFactor16 = 16f;
                    parent.localAI[0] = projectile.localAI[0] + 1f;
                }
                else
                {
                    return;
                }
                if (projectile.alpha > 0)
                {
                    int num3;
                    for (int num1068 = 0; num1068 < 2; num1068 = num3 + 1)
                    {
                        int num1069 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 135, 0f, 0f, 100, default(Color), 2f);
                        Main.dust[num1069].noGravity = true;
                        Main.dust[num1069].noLight = true;
                        num3 = num1068;
                    }
                }
                projectile.alpha -= 42;
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
                projectile.velocity = Vector2.Zero;
                Vector2 vector151 = parCenter - projectile.Center;
                if (parRot != projectile.rotation)
                {
                    float num1070 = MathHelper.WrapAngle(parRot - projectile.rotation);
                    vector151 = vector151.RotatedBy((double)(num1070 * 0.1f), default(Vector2));
                }
                projectile.rotation = vector151.ToRotation() + 1.57079637f;
                projectile.position = projectile.Center;
                projectile.scale = scaleFactor17;
                projectile.width = (projectile.height = (int)((float)num1049 * projectile.scale));
                projectile.Center = projectile.position;
                if (vector151 != Vector2.Zero)
                {
                    projectile.Center = parCenter - Vector2.Normalize(vector151) * scaleFactor16 * scaleFactor17;
                }
                projectile.spriteDirection = ((vector151.X > 0f) ? 1 : -1);
                return;
            }
        }
    }
}