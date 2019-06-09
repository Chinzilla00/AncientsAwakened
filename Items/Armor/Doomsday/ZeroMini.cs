using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Doomsday
{
    public class ZeroMini : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zero Construct");
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.netImportant = true;
            projectile.width = 62;
            projectile.height = 62;
            projectile.friendly = true;
            projectile.minion = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = (AAPlayer)player.GetModPlayer(mod, "AAPlayer");
            if (player.dead)
            {
                modPlayer.MiniZero = false;
            }
            if (modPlayer.MiniZero)
            {
                projectile.timeLeft = 2;
            }
            float num619 = 0f;
            float num620 = 0f;
            float num621 = 0f;
            float num622 = 0f;
            float num623 = 0.05f;
            for (int num624 = 0; num624 < 1000; num624++)
            {
                if (num624 != projectile.whoAmI && Main.projectile[num624].active && Main.projectile[num624].owner == projectile.owner && Math.Abs(projectile.position.X - Main.projectile[num624].position.X) + Math.Abs(projectile.position.Y - Main.projectile[num624].position.Y) < (float)projectile.width)
                {
                    if (projectile.position.X < Main.projectile[num624].position.X)
                    {
                        projectile.velocity.X = projectile.velocity.X - num623;
                    }
                    else
                    {
                        projectile.velocity.X = projectile.velocity.X + num623;
                    }
                    if (projectile.position.Y < Main.projectile[num624].position.Y)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - num623;
                    }
                    else
                    {
                        projectile.velocity.Y = projectile.velocity.Y + num623;
                    }
                }
            }
            Lighting.AddLight(projectile.Center, 0.8f, 0.3f, 0.1f);
            bool flag23 = false;
            if (projectile.ai[0] >= 3f && projectile.ai[0] <= 5f)
            {
                int num625 = 2;
                flag23 = true;
                projectile.velocity *= 0.9f;
                projectile.ai[1] += 1f;
                int num626 = (int)projectile.ai[1] / num625 + (int)(projectile.ai[0] - 3f) * 8;
                if (num626 < 4)
                {
                    projectile.frame = 17 + num626;
                }
                else if (num626 < 5)
                {
                    projectile.frame = 0;
                }
                else if (num626 < 8)
                {
                    projectile.frame = 1 + num626 - 5;
                }
                else if (num626 < 11)
                {
                    projectile.frame = 11 - num626;
                }
                else if (num626 < 12)
                {
                    projectile.frame = 0;
                }
                else if (num626 < 16)
                {
                    projectile.frame = num626 - 2;
                }
                else if (num626 < 20)
                {
                    projectile.frame = 29 - num626;
                }
                else if (num626 < 21)
                {
                    projectile.frame = 0;
                }
                else
                {
                    projectile.frame = num626 - 4;
                }
                if (projectile.ai[1] > (float)(num625 * 8))
                {
                    projectile.ai[0] -= 3f;
                    projectile.ai[1] = 0f;
                }
            }
            if (projectile.ai[0] >= 6f && projectile.ai[0] <= 8f)
            {
                projectile.ai[1] += 1f;
                projectile.MaxUpdates = 2;
                if (projectile.ai[0] == 7f)
                {
                    projectile.rotation = projectile.velocity.ToRotation() + 3.14159274f;
                }
                else
                {
                    projectile.rotation += 0.5235988f;
                }
                int num627 = 0;
                switch ((int)projectile.ai[0])
                {
                    case 6:
                        projectile.frame = 5;
                        num627 = 40;
                        break;
                    case 7:
                        projectile.frame = 13;
                        num627 = 30;
                        break;
                    case 8:
                        projectile.frame = 17;
                        num627 = 30;
                        break;
                }
                if (projectile.ai[1] > (float)num627)
                {
                    projectile.ai[1] = 1f;
                    projectile.ai[0] -= 6f;
                    projectile.localAI[0] += 1f;
                    projectile.extraUpdates = 0;
                    projectile.numUpdates = 0;
                    projectile.netUpdate = true;
                }
                else
                {
                    flag23 = true;
                }
                if (projectile.ai[0] == 8f)
                {
                    for (int num628 = 0; num628 < 4; num628++)
                    {
                        int num629 = Utils.SelectRandom<int>(Main.rand, new int[]
                        {
                                                                    226,
                                                                    228,
                                                                    75
                        });
                        int num630 = Dust.NewDust(projectile.Center, 0, 0, num629, 0f, 0f, 0, default(Color), 1f);
                        Dust dust2 = Main.dust[num630];
                        Vector2 value17 = Vector2.One.RotatedBy((double)((float)num628 * 1.57079637f), default(Vector2)).RotatedBy((double)projectile.rotation, default(Vector2));
                        dust2.position = projectile.Center + value17 * 10f;
                        dust2.velocity = value17 * 1f;
                        dust2.scale = 0.6f + Main.rand.NextFloat() * 0.5f;
                        dust2.noGravity = true;
                    }
                }
            }
            if (flag23)
            {
                return;
            }
            Vector2 vector44 = projectile.position;
            bool flag24 = false;
            if (projectile.ai[0] < 9f)
            {
                projectile.tileCollide = true;
            }
            if (projectile.tileCollide && WorldGen.SolidTile(Framing.GetTileSafely((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16)))
            {
                projectile.tileCollide = false;
            }
            NPC ownerMinionAttackTargetNPC3 = projectile.OwnerMinionAttackTargetNPC;
            if (ownerMinionAttackTargetNPC3 != null && ownerMinionAttackTargetNPC3.CanBeChasedBy(this, false))
            {
                float num631 = Vector2.Distance(ownerMinionAttackTargetNPC3.Center, projectile.Center);
                if (((Vector2.Distance(projectile.Center, vector44) > num631 && num631 < num619) || !flag24) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, ownerMinionAttackTargetNPC3.position, ownerMinionAttackTargetNPC3.width, ownerMinionAttackTargetNPC3.height))
                {
                    num619 = num631;
                    vector44 = ownerMinionAttackTargetNPC3.Center;
                    flag24 = true;
                }
            }
            if (!flag24)
            {
                for (int num632 = 0; num632 < 200; num632++)
                {
                    NPC nPC2 = Main.npc[num632];
                    if (nPC2.CanBeChasedBy(this, false))
                    {
                        float num633 = Vector2.Distance(nPC2.Center, projectile.Center);
                        if (((Vector2.Distance(projectile.Center, vector44) > num633 && num633 < num619) || !flag24) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, nPC2.position, nPC2.width, nPC2.height))
                        {
                            num619 = num633;
                            vector44 = nPC2.Center;
                            flag24 = true;
                        }
                    }
                }
            }
            float num634 = num620;
            if (flag24)
            {
                num634 = num621;
            }
            if (Vector2.Distance(player.Center, projectile.Center) > num634)
            {
                projectile.ai[0] += (float)(3 * (3 - (int)(projectile.ai[0] / 3f)));
                projectile.tileCollide = false;
                projectile.netUpdate = true;
            }
            bool flag25 = projectile.ai[0] >= 9f;
            float num637 = 12f;
            if (flag25)
            {
                num637 = 15f;
            }
            Vector2 center2 = projectile.Center;
            Vector2 vector46 = player.Center - center2 + new Vector2(0f, -60f);
            float num638 = vector46.Length();
            if (num638 > 200f && num637 < 8f)
            {
                num637 = 8f;
            }
            if (num638 < num622 && flag25 && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
            {
                projectile.ai[0] -= 9f;
                projectile.netUpdate = true;
            }
            if (num638 > 2000f)
            {
                projectile.position.X = Main.player[projectile.owner].Center.X - (float)(projectile.width / 2);
                projectile.position.Y = Main.player[projectile.owner].Center.Y - (float)(projectile.height / 2);
                projectile.netUpdate = true;
            }
            if (num638 > 70f)
            {
                vector46.Normalize();
                vector46 *= num637;
                projectile.velocity = (projectile.velocity * 40f + vector46) / 41f;
            }
            else if (projectile.velocity.X == 0f && projectile.velocity.Y == 0f)
            {
                projectile.velocity.X = -0.15f;
                projectile.velocity.Y = -0.05f;
            }
            if (projectile.ai[0] < 3f || projectile.ai[0] >= 9f)
            {
                projectile.rotation += projectile.velocity.X * 0.04f;
            }
            if (projectile.ai[1] > 0f)
            {
                projectile.ai[1] += 1f;
                int num644 = 10;
                if (projectile.ai[1] > (float)num644)
                {
                    projectile.ai[1] = 0f;
                    projectile.netUpdate = true;
                }
            }
            else if (projectile.ai[0] < 3f)
            {
                int num647 = 0;
                switch ((int)projectile.ai[0])
                {
                    case 0:
                    case 3:
                    case 6:
                        num647 = 400;
                        break;
                    case 1:
                    case 4:
                    case 7:
                        num647 = 400;
                        break;
                    case 2:
                    case 5:
                    case 8:
                        num647 = 600;
                        break;
                }
                if (projectile.ai[1] == 0f && flag24 && num619 < (float)num647)
                {
                    projectile.ai[1] += 1f;
                    if (Main.myPlayer == projectile.owner)
                    {
                        if (projectile.localAI[0] >= 3f)
                        {
                            projectile.ai[0] += 4f;
                            if (projectile.ai[0] == 6f)
                            {
                                projectile.ai[0] = 3f;
                            }
                            projectile.localAI[0] = 0f;
                            return;
                        }
                        projectile.ai[0] += 6f;
                        Vector2 value21 = vector44 - projectile.Center;
                        value21.Normalize();
                        float scaleFactor4 = (projectile.ai[0] == 8f) ? 12f : 10f;
                        projectile.velocity = value21 * scaleFactor4;
                        projectile.netUpdate = true;
                        return;
                    }
                }
            }


            projectile.frameCounter++;
            if (projectile.frameCounter >= 8)
            {
                projectile.frameCounter = 0;
                projectile.frame += 1;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
        }
    }
}