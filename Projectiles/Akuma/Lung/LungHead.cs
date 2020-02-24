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
            projectile.width = 24;
            projectile.height = 24;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.netImportant = true;
            projectile.tileCollide = false;
            projectile.minion = true;

            projectile.penetrate = -1;
            projectile.timeLeft = 18000;
            projectile.timeLeft *= 5;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 5;
            projectile.GetGlobalProjectile<AAGlobalProjectile>().LongMinion = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Lung");
            Main.projFrames[projectile.type] = 2;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture2D13 = Main.projectileTexture[projectile.type];
            int num214 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            if(flaming) projectile.frame = 1;
            else projectile.frame = 0;
            int y6 = num214 * projectile.frame;
            Main.spriteBatch.Draw(texture2D13, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle(0, y6, texture2D13.Width, num214),
                projectile.GetAlpha(Color.White), projectile.rotation, new Vector2(texture2D13.Width / 2f, num214 / 2f), projectile.scale,
                projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }

        public bool flaming = false;

        public override void OnHitNPC(NPC npc, int damage, float knockback, bool crit)
        {
            npc.immune[projectile.owner] = 6;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();

            if ((int)Main.time % 120 == 0) projectile.netUpdate = true;
            if (!player.active)
            {
                projectile.active = false;
                return;
            }

            int num1038 = 10;
            if (player.dead) modPlayer.LungMinion = false;
            if (modPlayer.LungMinion) projectile.timeLeft = 2;
            num1038 = 30;

            Vector2 center = player.Center;
            float num1040 = 700f;
            float num1041 = 1000f;
            int num1042 = -1;
            if (projectile.Distance(center) > 2000f)
            {
                projectile.Center = center;
                projectile.netUpdate = true;
            }

            bool flag66 = true;
            if (flag66)
            {
                NPC ownerMinionAttackTargetNPC5 = projectile.OwnerMinionAttackTargetNPC;
                if (ownerMinionAttackTargetNPC5 != null && ownerMinionAttackTargetNPC5.CanBeChasedBy(projectile, false))
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
                    for (int num1044 = 0; num1044 < 200; num1044++)
                    {
                        NPC nPC13 = Main.npc[num1044];
                        if (nPC13.CanBeChasedBy(projectile, false) && player.Distance(nPC13.Center) < num1041)
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

            if (num1042 != -1)
            {
                NPC nPC14 = Main.npc[num1042];
                Vector2 vector132 = nPC14.Center - projectile.Center;
                (vector132.X > 0f).ToDirectionInt();
                (vector132.Y > 0f).ToDirectionInt();
                float scaleFactor15 = 0.4f;
                if (vector132.Length() < 600f) scaleFactor15 = 0.6f;
                if (vector132.Length() < 500f && vector132.Length() >= 100f && projectile.velocity.X / vector132.X > 0)
                {
                    flaming = true;
                    Vector2 shootspeed = Vector2.Normalize(projectile.velocity) * 20f;
                    Vector2 shootpos = Vector2.Normalize(projectile.velocity).RotatedBy((float)Math.PI / 2 * projectile.direction) * projectile.height / 2;
                    int fire = Projectile.NewProjectile(projectile.position.X + projectile.velocity.X + shootpos.X, projectile.position.Y + projectile.velocity.Y + shootpos.Y, shootspeed.X, shootspeed.Y, mod.ProjectileType("DragonfireProj"), (int)(projectile.damage / 1.5), 0, projectile.owner);
                    Main.projectile[fire].minion = true;
                    Main.projectile[fire].minionSlots = 0f;
                }
                else
                {
                    flaming = false;
                }
                if (vector132.Length() < 300f) scaleFactor15 = 0.8f;
                if (vector132.Length() > (nPC14.Size.Length() * 0.75f + 100f))
                {
                    projectile.velocity += Vector2.Normalize(vector132) * scaleFactor15 * 1.5f;
                    if (Vector2.Dot(projectile.velocity, vector132) < 0.25f) projectile.velocity *= 0.8f;
                }

                float num1046 = 30f;
                if (projectile.velocity.Length() > num1046) projectile.velocity = Vector2.Normalize(projectile.velocity) * num1046;
            }
            else
            {
                float num1047 = 0.2f;
                Vector2 vector133 = center - projectile.Center;
                if (vector133.Length() < 200f) num1047 = 0.12f;
                if (vector133.Length() < 140f) num1047 = 0.06f;
                if (vector133.Length() > 100f)
                {
                    if (Math.Abs(center.X - projectile.Center.X) > 20f) projectile.velocity.X = projectile.velocity.X + num1047 * Math.Sign(center.X - projectile.Center.X);
                    if (Math.Abs(center.Y - projectile.Center.Y) > 10f) projectile.velocity.Y = projectile.velocity.Y + num1047 * Math.Sign(center.Y - projectile.Center.Y);
                }
                else if (projectile.velocity.Length() > 2f)
                {
                    projectile.velocity *= 0.96f;
                }

                if (Math.Abs(projectile.velocity.Y) < 1f) projectile.velocity.Y = projectile.velocity.Y - 0.1f;
                float num1048 = 15f;
                if (projectile.velocity.Length() > num1048) projectile.velocity = Vector2.Normalize(projectile.velocity) * num1048;
            }

            projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            int direction = projectile.direction;
            projectile.direction = projectile.spriteDirection = projectile.velocity.X > 0f ? 1 : -1;
            if (direction != projectile.direction) projectile.netUpdate = true;
            float num1049 = MathHelper.Clamp(projectile.localAI[0], 0f, 50f);
            projectile.position = projectile.Center;
            projectile.scale = 1f + num1049 * 0.01f;
            projectile.width = projectile.height = (int)(num1038 * projectile.scale);
            projectile.Center = projectile.position;
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 42;
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
            }

            float DamageBoost = Main.player[projectile.owner].minionDamage + Main.player[projectile.owner].allDamage - 1f;
            projectile.damage = (int)(DamageBoost > 0f? (projectile.localAI[0] * 60 * DamageBoost) : 1);
        }
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
            projectile.minionSlots = .5f;
            projectile.GetGlobalProjectile<AAGlobalProjectile>().LongMinion = true;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Lung");
        }
        public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles,
            List<int> drawCacheProjsOverWiresUI)
        {
            drawCacheProjsBehindProjectiles.Add(index);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture2D13 = Main.projectileTexture[projectile.type];
            int num214 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int y6 = num214 * projectile.frame;
            Main.spriteBatch.Draw(texture2D13, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle(0, y6, texture2D13.Width, num214),
                projectile.GetAlpha(Color.White), projectile.rotation, new Vector2(texture2D13.Width / 2f, num214 / 2f), projectile.scale,
                projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }

        public override void OnHitNPC(NPC npc, int damage, float knockback, bool crit)
        {
            npc.immune[projectile.owner] = 6;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();

            if ((int)Main.time % 120 == 0) projectile.netUpdate = true;
            if (!player.active)
            {
                projectile.active = false;
                return;
            }

            int num1038 = 10;
            if (player.dead) modPlayer.LungMinion = false;
            if (modPlayer.LungMinion) projectile.timeLeft = 2;
            num1038 = 30;

            //D U S T
            /*if (Main.rand.Next(30) == 0)
            {
                int num1039 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 135, 0f, 0f, 0, default, 2f);
                Main.dust[num1039].noGravity = true;
                Main.dust[num1039].fadeIn = 2f;
                Point point4 = Main.dust[num1039].position.ToTileCoordinates();
                if (WorldGen.InWorld(point4.X, point4.Y, 5) && WorldGen.SolidTile(point4.X, point4.Y))
                {
                    Main.dust[num1039].noLight = true;
                }
            }*/

            bool flag67 = false;
            Vector2 value67 = Vector2.Zero;
            Vector2 arg_2D865_0 = Vector2.Zero;
            float num1052 = 0f;
            float scaleFactor16 = 0f;
            float scaleFactor17 = 1f;
            if (projectile.ai[1] == 1f)
            {
                projectile.ai[1] = 0f;
                projectile.netUpdate = true;
            }

            int byUUID = Projectile.GetByUUID(projectile.owner, (int)projectile.ai[0]);
            if (byUUID >= 0 && Main.projectile[byUUID].active)
            {
                flag67 = true;
                value67 = Main.projectile[byUUID].Center;
                Vector2 arg_2D957_0 = Main.projectile[byUUID].velocity;
                num1052 = Main.projectile[byUUID].rotation;
                float num1053 = MathHelper.Clamp(Main.projectile[byUUID].scale, 0f, 50f);
                scaleFactor17 = num1053;
                scaleFactor16 = 16f;
                int arg_2D9AD_0 = Main.projectile[byUUID].alpha;
                Main.projectile[byUUID].localAI[0] = projectile.localAI[0] + 1f;
                if (Main.projectile[byUUID].type != mod.ProjectileType("LungHead")) Main.projectile[byUUID].localAI[1] = projectile.whoAmI;
            }

            if (!flag67) return;
            if (projectile.alpha > 0)
                for (int num1054 = 0; num1054 < 2; num1054++)
                {
                    int num1055 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 135, 0f, 0f, 100, default, 2f);
                    Main.dust[num1055].noGravity = true;
                    Main.dust[num1055].noLight = true;
                }

            projectile.alpha -= 42;
            if (projectile.alpha < 0) projectile.alpha = 0;
            projectile.velocity = Vector2.Zero;
            Vector2 vector134 = value67 - projectile.Center;
            if (num1052 != projectile.rotation)
            {
                float num1056 = MathHelper.WrapAngle(num1052 - projectile.rotation);
                vector134 = vector134.RotatedBy(num1056 * 0.1f, default);
            }

            projectile.rotation = vector134.ToRotation() + 1.57079637f;
            projectile.position = projectile.Center;
            projectile.scale = scaleFactor17;
            projectile.width = projectile.height = (int)(num1038 * projectile.scale);
            projectile.Center = projectile.position;
            if (vector134 != Vector2.Zero) projectile.Center = value67 - Vector2.Normalize(vector134) * scaleFactor16 * scaleFactor17;
            projectile.spriteDirection = vector134.X > 0f ? 1 : -1;

            projectile.damage = Main.projectile[byUUID].damage;
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
            projectile.GetGlobalProjectile<AAGlobalProjectile>().LongMinion = true;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Lung");
        }
        public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles,
           List<int> drawCacheProjsOverWiresUI)
        {
            drawCacheProjsBehindProjectiles.Add(index);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture2D13 = Main.projectileTexture[projectile.type];
            int num214 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int y6 = num214 * projectile.frame;
            Main.spriteBatch.Draw(texture2D13, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle(0, y6, texture2D13.Width, num214),
                projectile.GetAlpha(Color.White), projectile.rotation, new Vector2(texture2D13.Width / 2f, num214 / 2f), projectile.scale,
                projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }

        public override void OnHitNPC(NPC npc, int damage, float knockback, bool crit)
        {
            npc.immune[projectile.owner] = 6;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();

            if ((int)Main.time % 120 == 0) projectile.netUpdate = true;
            if (!player.active)
            {
                projectile.active = false;
                return;
            }


            int num1038 = 10;
            if (player.dead) modPlayer.LungMinion = false;
            if (modPlayer.LungMinion) projectile.timeLeft = 2;
            num1038 = 30;

            //D U S T
            /*if (Main.rand.Next(30) == 0)
            {
                int num1039 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 135, 0f, 0f, 0, default, 2f);
                Main.dust[num1039].noGravity = true;
                Main.dust[num1039].fadeIn = 2f;
                Point point4 = Main.dust[num1039].position.ToTileCoordinates();
                if (WorldGen.InWorld(point4.X, point4.Y, 5) && WorldGen.SolidTile(point4.X, point4.Y))
                {
                    Main.dust[num1039].noLight = true;
                }
            }*/

            bool flag67 = false;
            Vector2 value67 = Vector2.Zero;
            Vector2 arg_2D865_0 = Vector2.Zero;
            float num1052 = 0f;
            float scaleFactor16 = 0f;
            float scaleFactor17 = 1f;
            if (projectile.ai[1] == 1f)
            {
                projectile.ai[1] = 0f;
                projectile.netUpdate = true;
            }

            int byUUID = Projectile.GetByUUID(projectile.owner, (int)projectile.ai[0]);
            if (byUUID >= 0 && Main.projectile[byUUID].active)
            {
                flag67 = true;
                value67 = Main.projectile[byUUID].Center;
                Vector2 arg_2D957_0 = Main.projectile[byUUID].velocity;
                num1052 = Main.projectile[byUUID].rotation;
                float num1053 = MathHelper.Clamp(Main.projectile[byUUID].scale, 0f, 50f);
                scaleFactor17 = num1053;
                scaleFactor16 = 16f;
                int arg_2D9AD_0 = Main.projectile[byUUID].alpha;
                Main.projectile[byUUID].localAI[0] = projectile.localAI[0] + 1f;
                if (Main.projectile[byUUID].type != mod.ProjectileType("LungHead")) Main.projectile[byUUID].localAI[1] = projectile.whoAmI;
                if (projectile.owner == player.whoAmI && Main.projectile[byUUID].type == mod.ProjectileType("LungHead"))
                {
                    Main.projectile[byUUID].Kill();
                    projectile.Kill();
                    return;
                }
            }

            if (!flag67) return;
            if (projectile.alpha > 0)
                for (int num1054 = 0; num1054 < 2; num1054++)
                {
                    int num1055 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 135, 0f, 0f, 100, default, 2f);
                    Main.dust[num1055].noGravity = true;
                    Main.dust[num1055].noLight = true;
                }

            projectile.alpha -= 42;
            if (projectile.alpha < 0) projectile.alpha = 0;
            projectile.velocity = Vector2.Zero;
            Vector2 vector134 = value67 - projectile.Center;
            if (num1052 != projectile.rotation)
            {
                float num1056 = MathHelper.WrapAngle(num1052 - projectile.rotation);
                vector134 = vector134.RotatedBy(num1056 * 0.1f, default);
            }

            projectile.rotation = vector134.ToRotation() + 1.57079637f;
            projectile.position = projectile.Center;
            projectile.scale = scaleFactor17;
            projectile.width = projectile.height = (int)(num1038 * projectile.scale);
            projectile.Center = projectile.position;
            if (vector134 != Vector2.Zero) projectile.Center = value67 - Vector2.Normalize(vector134) * scaleFactor16 * scaleFactor17;
            projectile.spriteDirection = vector134.X > 0f ? 1 : -1;

            projectile.damage = Main.projectile[byUUID].damage;
        }
    }
}