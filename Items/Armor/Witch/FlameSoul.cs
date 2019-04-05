using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Witch
{
    public class FlameSoul : ModProjectile
    {

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flame Soul");
			Main.projFrames[projectile.type] = 4;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }
    	
        public override void SetDefaults()
        {
            projectile.netImportant = true;
            projectile.width = 28;
            projectile.height = 40;
            projectile.aiStyle = 62;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.minion = true;
            projectile.friendly = true;
            projectile.minionSlots = 1f;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.scale = .2f;
        }

        public int FrameTimer = 0;

        public override void AI()
        {
            bool flag64 = projectile.type == mod.ProjectileType("FlameSoul");
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            player.AddBuff(mod.BuffType("FlameSoul"), 3600);
            if (flag64)
            {
                if (player.dead)
                {
                    modPlayer.FlameSoul = false;
                }
                if (modPlayer.FlameSoul)
                {
                    projectile.timeLeft = 2;
                }
            }
            FireDamage(player);

            float num8 = 0.1f;
            float num9 = projectile.width * 2f;
            for (int j = 0; j < 1000; j++)
            {
                if (j != projectile.whoAmI && Main.projectile[j].active && Main.projectile[j].owner == projectile.owner && Main.projectile[j].type == projectile.type && Math.Abs(projectile.position.X - Main.projectile[j].position.X) + Math.Abs(projectile.position.Y - Main.projectile[j].position.Y) < num9)
                {
                    if (projectile.position.X < Main.projectile[j].position.X)
                    {
                        projectile.velocity.X = projectile.velocity.X - num8;
                    }
                    else
                    {
                        projectile.velocity.X = projectile.velocity.X + num8;
                    }
                    if (projectile.position.Y < Main.projectile[j].position.Y)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - num8;
                    }
                    else
                    {
                        projectile.velocity.Y = projectile.velocity.Y + num8;
                    }
                }
            }
            Vector2 vector = projectile.position;
            float num10 = 400f;
            
            bool flag = false;
            int num11 = -1;
            projectile.tileCollide = false;
            if (Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
            {
                projectile.alpha += 20;
                if (projectile.alpha > 150)
                {
                    projectile.alpha = 150;
                }
            }
            else
            {
                projectile.alpha -= 50;
                if (projectile.alpha < 60)
                {
                    projectile.alpha = 60;
                }
            }
            Vector2 center = Main.player[projectile.owner].Center;
            Vector2 value = new Vector2(0.5f);
            if (projectile.type == 423)
            {
                value.Y = 0f;
            }
            NPC ownerMinionAttackTargetNPC = projectile.OwnerMinionAttackTargetNPC;
            if (ownerMinionAttackTargetNPC != null && ownerMinionAttackTargetNPC.CanBeChasedBy(this, false))
            {
                Vector2 vector2 = ownerMinionAttackTargetNPC.position + ownerMinionAttackTargetNPC.Size * value;
                float num12 = Vector2.Distance(vector2, center);
                if (((Vector2.Distance(center, vector) > num12 && num12 < num10) || !flag) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, ownerMinionAttackTargetNPC.position, ownerMinionAttackTargetNPC.width, ownerMinionAttackTargetNPC.height))
                {
                    num10 = num12;
                    vector = vector2;
                    flag = true;
                    num11 = ownerMinionAttackTargetNPC.whoAmI;
                }
            }
            if (!flag)
            {
                for (int k = 0; k < 200; k++)
                {
                    NPC nPC = Main.npc[k];
                    if (nPC.CanBeChasedBy(this, false))
                    {
                        Vector2 vector3 = nPC.position + nPC.Size * value;
                        float num13 = Vector2.Distance(vector3, center);
                        if (((Vector2.Distance(center, vector) > num13 && num13 < num10) || !flag) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, nPC.position, nPC.width, nPC.height))
                        {
                            num10 = num13;
                            vector = vector3;
                            flag = true;
                            num11 = k;
                        }
                    }
                }
            }
            int num16 = 500;
            if (flag)
            {
                num16 = 1200;
            }
            float num17 = Vector2.Distance(player.Center, projectile.Center);
            if (num17 > (float)num16)
            {
                projectile.ai[0] = 1f;
                projectile.netUpdate = true;
            }
            if (projectile.ai[0] == 1f)
            {
                projectile.tileCollide = false;
            }
            if (flag && projectile.ai[0] == 0f)
            {
                Vector2 vector4 = vector - projectile.Center;
                float num18 = vector4.Length();
                vector4.Normalize();
                if (num18 > 400f)
                {
                    float scaleFactor = 2f;
                    vector4 *= scaleFactor;
                    projectile.velocity = (projectile.velocity * 20f + vector4) / 21f;
                }
                else
                {
                    projectile.velocity *= 0.96f;
                }
                if (num18 > 200f)
                {
                    float scaleFactor2 = 6f;
                    vector4 *= scaleFactor2;
                    projectile.velocity.X = (projectile.velocity.X * 40f + vector4.X) / 41f;
                    projectile.velocity.Y = (projectile.velocity.Y * 40f + vector4.Y) / 41f;
                }
                else if (projectile.velocity.Y > -1f)
                {
                    projectile.velocity.Y = projectile.velocity.Y - 0.1f;
                }
            }
            else
            {
                if (!Collision.CanHitLine(projectile.Center, 1, 1, Main.player[projectile.owner].Center, 1, 1))
                {
                    projectile.ai[0] = 1f;
                }
                float num22 = 9f;
                Vector2 center2 = projectile.Center;
                Vector2 vector6 = player.Center - center2 + new Vector2(0f, -60f);
                vector6 += new Vector2(0f, 40f);
                float num24 = vector6.Length();
                if (num24 > 200f && num22 < 9f)
                {
                    num22 = 9f;
                }
                if (num24 < 100f && projectile.ai[0] == 1f && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
                {
                    projectile.ai[0] = 0f;
                    projectile.netUpdate = true;
                }
                if (num24 > 2000f)
                {
                    projectile.position.X = Main.player[projectile.owner].Center.X - (float)(projectile.width / 2);
                    projectile.position.Y = Main.player[projectile.owner].Center.Y - (float)(projectile.width / 2);
                }
                if (Math.Abs(vector6.X) > 40f || Math.Abs(vector6.Y) > 10f)
                {
                    vector6.Normalize();
                    vector6 *= num22;
                    vector6 *= new Vector2(1.25f, 0.65f);
                    projectile.velocity = (projectile.velocity * 20f + vector6) / 21f;
                }
                else
                {
                    if (projectile.velocity.X == 0f && projectile.velocity.Y == 0f)
                    {
                        projectile.velocity.X = -0.15f;
                        projectile.velocity.Y = -0.05f;
                    }
                    projectile.velocity *= 1.01f;
                }
            }
            projectile.rotation = projectile.velocity.X * 0.05f;
            projectile.frameCounter++;
            if (projectile.velocity.X > 0f)
            {
                projectile.spriteDirection = (projectile.direction = -1);
            }
            else if (projectile.velocity.X < 0f)
            {
                projectile.spriteDirection = (projectile.direction = 1);
            }
            if (projectile.ai[1] > 0f)
            {
                projectile.ai[1] += 1f;
                if (Main.rand.Next(3) != 0)
                {
                    projectile.ai[1] += 1f;
                }
            }
            if (projectile.ai[1] > 60f)
            {
                projectile.ai[1] = 0f;
                projectile.netUpdate = true;
            }
            if (projectile.ai[0] == 0f)
            {
                float scaleFactor4 = 7f;
                int num29 = mod.ProjectileType<FlameSoulShot>();
                
                if (flag)
                {
                    if (Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
                    {
                        return;
                    }
                    else if (projectile.ai[1] == 0f)
                    {
                        projectile.ai[1] += 1f;
                        if (Main.myPlayer == projectile.owner)
                        {
                            Vector2 value4 = vector - projectile.Center;
                            value4.Normalize();
                            value4 *= scaleFactor4;
                            int num33 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, value4.X*1.5f, value4.Y*1.5f, num29, projectile.damage, 0f, Main.myPlayer, 0f, 0f);
                            Main.projectile[num33].timeLeft = 300;
                            Main.projectile[num33].netUpdate = true;
                            projectile.netUpdate = true;
                        }
                    }
                }
            }
            projectile.frameCounter++;
            if (projectile.frameCounter >= 15)
            {
                projectile.frame += 1;
                projectile.frameCounter = 0;
            }
            if (projectile.frame >= 3)
            {
                projectile.frame = 0;
            }
        }

        public float glowColorR = 255;
        public float glowColorG = 255;
        public float glowColorB = 255;
        public Color glowColor = new Color(255, 255, 255);
        public Color glowColor2 = Color.OrangeRed;

        public override Color? GetAlpha(Color lightColor)
        {
            return glowColor * ((float)Main.mouseTextColor / 255f);
        }

        public void FireDamage(Player player)
        {
            glowColor = new Color(glowColorR, glowColorG, glowColorB);

            glowColorR = BaseMod.BaseUtility.MultiLerp(player.statLife / player.statLifeMax, glowColor.R, glowColor2.R);
            glowColorG = BaseMod.BaseUtility.MultiLerp(player.statLife / player.statLifeMax, glowColor.G, glowColor2.G);
            glowColorB = BaseMod.BaseUtility.MultiLerp(player.statLife / player.statLifeMax, glowColor.B, glowColor2.B);

            projectile.scale = BaseMod.BaseUtility.MultiLerp(player.statLife / player.statLifeMax, .1f, .2f, .3f, .4f, .5f, .6f, .7f, .8f, .9f, 1f);

            if (player.statLife > (player.statLifeMax * .9f))
            {
                projectile.damage = 60 + 0;
                projectile.scale = .2f;
                return;
            }
            if (player.statLife > (player.statLifeMax * .8f))
            {
                projectile.damage = 60 + 5;
                projectile.scale = .3f;
                return;
            }
            if (player.statLife > (player.statLifeMax * .7f))
            {
                projectile.damage = 60 + 10;
                projectile.scale = .4f;
                return;
            }
            if (player.statLife > (player.statLifeMax * .6f))
            {
                projectile.damage = 60 + 15;
                projectile.scale = .5f;
                return;
            }
            if (player.statLife > (player.statLifeMax * .5f))
            {
                projectile.damage = 60 + 20;
                projectile.scale = .6f;
                return;
            }
            if (player.statLife > (player.statLifeMax * .4f))
            {
                projectile.damage = 60 + 25;
                projectile.scale = .7f;
                return;
            }
            if (player.statLife > (player.statLifeMax * .3f))
            {
                projectile.damage = 60 + 30;
                projectile.scale = .8f;
                return;
            }
            if (player.statLife > (player.statLifeMax * .2f))
            {
                projectile.damage = 60 + 35;
                projectile.scale = .9f;
                return;
            }
            if (player.statLife > (player.statLifeMax * .1f))
            {
                projectile.damage = 60 + 40;
                projectile.scale = .10f;
                return;
            }
            projectile.damage = 60 + 60;
        }
    }
}