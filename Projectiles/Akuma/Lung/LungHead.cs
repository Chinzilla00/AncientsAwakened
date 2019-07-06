using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;



namespace AAMod.Projectiles.Akuma.Lung
{
    public class LungHead : ModProjectile
    {
        public override void SetDefaults()
        {
            if (projectile.type == mod.ProjectileType<LungBody>() || projectile.type == mod.ProjectileType<LungBody1>())
            {
                projectile.minionSlots = 0.5f;
            }
            projectile.width = 24;
            projectile.height = 24;
            projectile.aiStyle = 121;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.minion = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.hide = true;
            projectile.netImportant = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Lung");
            Main.projFrames[projectile.type] = 2;
            ProjectileID.Sets.DontAttachHideToAlpha[projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
            ProjectileID.Sets.NoLiquidDistortion[projectile.type] = true;
            ProjectileID.Sets.StardustDragon[projectile.type] = true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 vector42 = projectile.position + new Vector2(projectile.width, projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
            Texture2D texture2D33 = Main.projectileTexture[projectile.type];
            Rectangle rectangle15 = texture2D33.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);
            Color alpha5 = projectile.GetAlpha(lightColor);
            Vector2 origin11 = rectangle15.Size() / 2f;
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            alpha5.A /= 2;
            Main.spriteBatch.Draw(texture2D33, vector42, new Rectangle?(rectangle15), alpha5, projectile.rotation, origin11, projectile.scale, spriteEffects, 0f);
            if (Main.player[projectile.owner].ghostFade != 0f)
            {
                float scaleFactor4 = Main.player[projectile.owner].ghostFade * 5f;
                for (float num286 = 0f; num286 < 4f; num286 += 1f)
                {
                    Main.spriteBatch.Draw(texture2D33, vector42 + Vector2.UnitY.RotatedBy(num286 * 6.28318548f / 4f) * scaleFactor4, new Rectangle?(rectangle15), alpha5 * 0.1f, projectile.rotation, origin11, projectile.scale, spriteEffects, 0f);
                }
            }
            return false;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            if ((int)Main.time % 120 == 0)
            {
                projectile.netUpdate = true;
            }
            if (!player.active)
            {
                projectile.active = false;
                return;
            }
            bool flag64 = projectile.type == mod.ProjectileType<LungHead>();
            bool flag65 = projectile.type == mod.ProjectileType<LungHead>() || projectile.type == mod.ProjectileType<LungBody>() || projectile.type == mod.ProjectileType<LungBody1>() || projectile.type == mod.ProjectileType<LungTail>();
            int num1038 = 10;
            if (flag65)
            {
                if (player.dead) modPlayer.LungMinion = false;
                if (modPlayer.LungMinion) projectile.timeLeft = 2;
                num1038 = 30;
                if (Main.rand.Next(30) == 0)
                {
                    int num1039 = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType<Dusts.AkumaDust>(), 0f, 0f, 0, default(Color), 2f);
                    Main.dust[num1039].noGravity = true;
                    Main.dust[num1039].fadeIn = 2f;
                    Point point4 = Main.dust[num1039].position.ToTileCoordinates();
                    if (WorldGen.InWorld(point4.X, point4.Y, 5) && WorldGen.SolidTile(point4.X, point4.Y))
                    {
                        Main.dust[num1039].noLight = true;
                    }
                }
            }
            if (flag64)
            {
                Vector2 center14 = player.Center;
                float num1040 = 700f;
                float num1041 = 1000f;
                int num1042 = -1;
                if (projectile.Distance(center14) > 2000f)
                {
                    projectile.Center = center14;
                    projectile.netUpdate = true;
                }
                bool flag66 = true;
                if (flag66)
                {
                    NPC ownerMinionAttackTargetNPC5 = projectile.OwnerMinionAttackTargetNPC;
                    if (ownerMinionAttackTargetNPC5 != null && ownerMinionAttackTargetNPC5.CanBeChasedBy(this, false))
                    {
                        float num1043 = projectile.Distance(ownerMinionAttackTargetNPC5.Center);
                        if (num1043 < num1040 * 2f)
                        {
                            num1042 = ownerMinionAttackTargetNPC5.whoAmI;
                            if (ownerMinionAttackTargetNPC5.boss)
                            {
                                int arg_2D352_0 = ownerMinionAttackTargetNPC5.whoAmI;
                            }
                            else
                            {
                                int arg_2D35E_0 = ownerMinionAttackTargetNPC5.whoAmI;
                            }
                        }
                    }
                    if (num1042 < 0)
                    {
                        for (int num1044 = 0; num1044 < 200; num1044++)
                        {
                            NPC nPC13 = Main.npc[num1044];
                            if (nPC13.CanBeChasedBy(this, false) && player.Distance(nPC13.Center) < num1041)
                            {
                                float num1045 = projectile.Distance(nPC13.Center);
                                if (num1045 < num1040)
                                {
                                    num1042 = num1044;
                                    bool arg_2D3CE_0 = nPC13.boss;
                                }
                            }
                        }
                    }
                }
                if (num1042 != -1)
                {
                    NPC nPC14 = Main.npc[num1042];
                    Vector2 vector132 = nPC14.Center - projectile.Center;
                    (vector132.X > 0f).ToDirectionInt();
                    (vector132.Y > 0f).ToDirectionInt();
                    float scaleFactor15 = 0.4f;
                    if (vector132.Length() < 600f)
                    {
                        scaleFactor15 = 0.6f;
                    }
                    if (vector132.Length() < 300f)
                    {
                        scaleFactor15 = 0.8f;
                    }
                    if (vector132.Length() > nPC14.Size.Length() * 0.75f)
                    {
                        projectile.velocity += Vector2.Normalize(vector132) * scaleFactor15 * 1.5f;
                        if (Vector2.Dot(projectile.velocity, vector132) < 0.25f)
                        {
                            projectile.velocity *= 0.8f;
                        }
                    }
                    float num1046 = 30f;
                    if (projectile.velocity.Length() > num1046)
                    {
                        projectile.velocity = Vector2.Normalize(projectile.velocity) * num1046;
                    }
                }
                else
                {
                    float num1047 = 0.2f;
                    Vector2 vector133 = center14 - projectile.Center;
                    if (vector133.Length() < 200f)
                    {
                        num1047 = 0.12f;
                    }
                    if (vector133.Length() < 140f)
                    {
                        num1047 = 0.06f;
                    }
                    if (vector133.Length() > 100f)
                    {
                        if (Math.Abs(center14.X - projectile.Center.X) > 20f)
                        {
                            projectile.velocity.X = projectile.velocity.X + num1047 * Math.Sign(center14.X - projectile.Center.X);
                        }
                        if (Math.Abs(center14.Y - projectile.Center.Y) > 10f)
                        {
                            projectile.velocity.Y = projectile.velocity.Y + num1047 * Math.Sign(center14.Y - projectile.Center.Y);
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
                    float num1048 = 15f;
                    if (projectile.velocity.Length() > num1048)
                    {
                        projectile.velocity = Vector2.Normalize(projectile.velocity) * num1048;
                    }
                }
                projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
                int direction = projectile.direction;
                projectile.direction = (projectile.spriteDirection = ((projectile.velocity.X > 0f) ? 1 : -1));
                if (direction != projectile.direction)
                {
                    projectile.netUpdate = true;
                }
                float num1049 = MathHelper.Clamp(projectile.localAI[0], 0f, 50f);
                projectile.position = projectile.Center;
                projectile.scale = 1f + num1049 * 0.01f;
                projectile.width = (projectile.height = (int)(num1038 * projectile.scale));
                projectile.Center = projectile.position;
                if (projectile.alpha > 0)
                {
                    for (int num1050 = 0; num1050 < 2; num1050++)
                    {
                        int num1051 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.AkumaDust>(), 0f, 0f, 100, default(Color), 2f);
                        Main.dust[num1051].noGravity = true;
                        Main.dust[num1051].noLight = true;
                    }
                    projectile.alpha -= 42;
                    if (projectile.alpha < 0)
                    {
                        projectile.alpha = 0;
                        return;
                    }
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 6;
        }

        public override void Kill(int timeLeft)
        {
            for (int num132 = 0; num132 < 6; num132++)
            {
                int num133 = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType<Dusts.AkumaDust>(), 0f, 0f, 100);
                Main.dust[num133].noGravity = true;
                Main.dust[num133].noLight = true;
            }
        }
    }

    public class LungBody : LungHead
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
        
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            if (player.slotsMinions + projectile.minionSlots > player.maxMinions && projectile.owner == Main.myPlayer)
            {
                int byUUID = Projectile.GetByUUID(projectile.owner, projectile.ai[0]);
                if (byUUID != -1)
                {
                    Projectile projectile1 = Main.projectile[byUUID];
                    if (projectile1.type != mod.ProjectileType("LungHead")) projectile1.localAI[1] = projectile.localAI[1];
                    projectile1 = Main.projectile[(int)projectile.localAI[1]];
                    projectile1.ai[0] = projectile.ai[0];
                    projectile1.ai[1] = 1f;
                    projectile1.netUpdate = true;
                }
            }
        }
    }

    public class LungBody1 : LungHead
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            if (player.slotsMinions + projectile.minionSlots > player.maxMinions && projectile.owner == Main.myPlayer)
            {
                int byUUID = Projectile.GetByUUID(projectile.owner, projectile.ai[0]);
                if (byUUID != -1)
                {
                    Projectile projectile1 = Main.projectile[byUUID];
                    if (projectile1.type != mod.ProjectileType("LungHead")) projectile1.localAI[1] = projectile.localAI[1];
                    projectile1 = Main.projectile[(int)projectile.localAI[1]];
                    projectile1.ai[0] = projectile.ai[0];
                    projectile1.ai[1] = 1f;
                    projectile1.netUpdate = true;
                }
            }
        }
    }

    public class LungTail : LungHead
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
    }
}