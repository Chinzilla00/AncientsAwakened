using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Summoning.Minions
{
    public class XiaoDoragon : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Xiao Doragon");
			Main.projFrames[projectile.type] = 5;
            ProjectileID.Sets.Homing[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.width = 96;
            projectile.height = 70;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.minionSlots = 1f;
            projectile.timeLeft = 18000;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft *= 5;
            projectile.minion = true;
        }

        public int FrameTimer = 0;
        bool hasTarget = false;

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            player.AddBuff(mod.BuffType("XiaoDoragon"), 3600);

            if (player.dead)
            {
                modPlayer.Xiao = false;
            }
            if (modPlayer.Xiao)
            {
                projectile.timeLeft = 2;
            }

            float minRange = 700f;
            float Range = 800f;
            float MaxRange = 1200f;
            float MaxOwnerDist = 150f;
            float IdleSpeed = 0.05f;
            for (int num638 = 0; num638 < 1000; num638++)
            {
                bool flag23 = Main.projectile[num638].type == mod.ProjectileType("XiaoDoragon");
                if (num638 != projectile.whoAmI && Main.projectile[num638].active && Main.projectile[num638].owner == projectile.owner && flag23 && Math.Abs(projectile.position.X - Main.projectile[num638].position.X) + Math.Abs(projectile.position.Y - Main.projectile[num638].position.Y) < projectile.width)
                {
                    if (projectile.position.X < Main.projectile[num638].position.X)
                    {
                        projectile.velocity.X = projectile.velocity.X - IdleSpeed;
                    }
                    else
                    {
                        projectile.velocity.X = projectile.velocity.X + IdleSpeed;
                    }
                    if (projectile.position.Y < Main.projectile[num638].position.Y)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - IdleSpeed;
                    }
                    else
                    {
                        projectile.velocity.Y = projectile.velocity.Y + IdleSpeed;
                    }
                }
            }
            Vector2 TargetCenter = projectile.position;
            hasTarget = false;
            if (projectile.ai[0] != 1f)
            {
                projectile.tileCollide = false;
            }
            if (projectile.tileCollide && WorldGen.SolidTile(Framing.GetTileSafely((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16)))
            {
                projectile.tileCollide = false;
            }
            if (player.HasMinionAttackTargetNPC)
			{
				NPC target = Main.npc[player.MinionAttackTargetNPC];
                if (target.CanBeChasedBy(projectile, false))
                {
                    float Distance = Vector2.Distance(target.Center, projectile.Center);
                    if (((Vector2.Distance(projectile.Center, TargetCenter) > Distance && Distance < minRange) || !hasTarget) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, target.position, target.width, target.height))
                    {
                        minRange = Distance;
                        TargetCenter = target.Center;
                        hasTarget = true;
                    }
                }
			}
			else
			{
				for (int targetID = 0; targetID < 200; targetID++)
                {
                    NPC target = Main.npc[targetID];
                    if (target.CanBeChasedBy(projectile, false))
                    {
                        float Distance = Vector2.Distance(target.Center, projectile.Center);
                        if (((Vector2.Distance(projectile.Center, TargetCenter) > Distance && Distance < minRange) || !hasTarget) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, target.position, target.width, target.height))
                        {
                            minRange = Distance;
                            TargetCenter = target.Center;
                            hasTarget = true;
                        }
                    }
                }
			}
            float inRange = Range;
            if (hasTarget)
            {
                inRange = MaxRange;
            }
            if (Vector2.Distance(player.Center, projectile.Center) > inRange)
            {
                projectile.ai[0] = 1f;
                projectile.tileCollide = false;
                projectile.netUpdate = true;
            }
            if (hasTarget && projectile.ai[0] == 0f)
            {
                Vector2 TargetPos = TargetCenter - projectile.Center;
                float TargetDistance = TargetPos.Length();
                TargetPos.Normalize();
                if (TargetDistance > 200f)
                {
                    float FastSpeed = 10f;
                    TargetPos *= FastSpeed;
                    projectile.velocity = (projectile.velocity * 40f + TargetPos) / 41f;
                }
                else
                {
                    float Speed = 6f;
                    TargetPos *= -Speed;
                    projectile.velocity = (projectile.velocity * 40f + TargetPos) / 41f;
                }
            }
            else
            {
                bool isIdle = false;
                if (!isIdle)
                {
                    isIdle = projectile.ai[0] == 1f;
                }
                float Speed = 6f;
                if (isIdle)
                {
                    Speed = 15f;
                }
                Vector2 Center = projectile.Center;
                Vector2 IdlePos = player.Center - Center + new Vector2(0f, -60f);
                float OwnerDistance = IdlePos.Length();
                if (OwnerDistance > 200f && Speed < 8f)
                {
                    Speed = 8f;
                }
                if (OwnerDistance < MaxOwnerDist && isIdle && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
                {
                    projectile.ai[0] = 0f;
                    projectile.netUpdate = true;
                }
                if (OwnerDistance > 2000f)
                {
                    projectile.position.X = Main.player[projectile.owner].Center.X - projectile.width / 2;
                    projectile.position.Y = Main.player[projectile.owner].Center.Y - projectile.height / 2;
                    projectile.netUpdate = true;
                }
                if (OwnerDistance > 70f)
                {
                    IdlePos.Normalize();
                    IdlePos *= Speed;
                    projectile.velocity = (projectile.velocity * 40f + IdlePos) / 41f;
                }
                else if (projectile.velocity.X == 0f && projectile.velocity.Y == 0f)
                {
                    projectile.velocity.X = -0.15f;
                    projectile.velocity.Y = -0.05f;
                }
            }
            
            if(hasTarget)
            {
                projectile.spriteDirection = ((TargetCenter - projectile.Center).X > 0? -1: 1);
            }
            else
            {
                projectile.spriteDirection =(projectile.velocity.X > 0? -1: 1);
            }
            

            if (projectile.ai[1] > 0f)
            {
                projectile.ai[1] += Main.rand.Next(1, 4);
            }
            if (projectile.ai[1] > 45f)
            {
                projectile.ai[1] = 0f;
                projectile.netUpdate = true;
            }
            if (projectile.ai[0] == 0f)
            {
                float ShootSpeed = 8f;
                int proj = ModContent.ProjectileType<XiaoFireball>();
                if (hasTarget && projectile.ai[1] == 0f)
                {
                    projectile.ai[1] += 1f;
                    if (Main.myPlayer == projectile.owner && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, TargetCenter, 0, 0))
                    {
                        Vector2 value19 = TargetCenter - projectile.Center;
                        value19.Normalize();
                        value19 *= ShootSpeed;
                        int num659 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, value19.X, value19.Y, proj, projectile.damage, 0f, Main.myPlayer, 0f, 0f);
						Main.projectile[num659].penetrate = 2;
                        Main.projectile[num659].timeLeft = 300;
						Main.projectile[num659].usesLocalNPCImmunity = true;
						Main.projectile[num659].localNPCHitCooldown = -1;
                        projectile.netUpdate = true;
                    }
                }
            }
        }

        public override void PostAI()
        {
            for (int m = projectile.oldPos.Length - 1; m > 0; m--)
            {
                projectile.oldPos[m] = projectile.oldPos[m - 1];
            }
            projectile.oldPos[0] = projectile.position;

            projectile.frameCounter++;
            if (projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
            }
            if (projectile.frame > 4)
            {
                projectile.frame = 0;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];

            Rectangle frame = BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / 5, 0, 0);


            if (projectile.spriteDirection == 1)
            {
                tex = mod.GetTexture("Items/Summoning/Minions/XiaoDoragonBlue");
            }

            if (hasTarget)
            {
                tex = mod.GetTexture("Items/Summoning/Minions/XiaoDoragonA");
                BaseDrawing.DrawAfterimage(spriteBatch, tex, 0, projectile.position, projectile.width, projectile.height, projectile.oldPos, 1f, projectile.rotation, projectile.spriteDirection, 5, frame, 1, 1, 5, true);
            }


            BaseDrawing.DrawTexture(spriteBatch, tex, 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.spriteDirection, 5, frame, lightColor, true);

            if (hasTarget)
            {
                Texture2D g = mod.GetTexture("Glowmasks/XiaoDoragon_Glow");
                BaseDrawing.DrawTexture(spriteBatch, g, 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.spriteDirection, 5, frame, Globals.AAColor.Shen2, true);
                BaseDrawing.DrawAfterimage(spriteBatch, g, 0, projectile.position, projectile.width, projectile.height, projectile.oldPos, 1f, projectile.rotation, projectile.spriteDirection, 5, frame, 1, 1, 5, true, 0, 0, Globals.AAColor.Shen2);
            }
            return false;
        }
    }
}