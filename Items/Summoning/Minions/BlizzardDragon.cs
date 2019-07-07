using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Summoning.Minions
{
    public class BlizzardDragon : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 80;
            projectile.height = 74;
            projectile.timeLeft = Projectile.SentryLifeTime;
            projectile.ignoreWater = true;
            projectile.sentry = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blizzard Dragon");
			Main.projFrames[projectile.type] = 10;

        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Snow;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			fallThrough = false;
			return true;
		}
		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.width = 80;
			projectile.velocity.Y = 0f;
			return false;
		}
		
        public override void AI()
        {
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[1] = 1f;
                projectile.localAI[0] = 1f;
                projectile.ai[0] = 120f;
                int num501 = 80;
                Main.PlaySound(SoundID.Item46, projectile.position);
                for (int num502 = 0; num502 < num501; num502++)
                {
                    int num503 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 16f), projectile.width, projectile.height - 16, 172, 0f, 0f, 0);
                    Main.dust[num503].velocity *= 2f;
                    Main.dust[num503].noGravity = true;
                    Main.dust[num503].scale *= 1.15f;
                }
            }
            projectile.velocity.X = 0f;
            projectile.velocity.Y = projectile.velocity.Y + 0.2f;
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }
            bool flag18 = false;
            float num506 = projectile.Center.X;
            float num507 = projectile.Center.Y;
            float num508 = 1000f;
            NPC ownerMinionAttackTargetNPC = projectile.OwnerMinionAttackTargetNPC;
            if (ownerMinionAttackTargetNPC != null && ownerMinionAttackTargetNPC.CanBeChasedBy(this, false))
            {
                float num509 = ownerMinionAttackTargetNPC.position.X + ownerMinionAttackTargetNPC.width / 2;
                float num510 = ownerMinionAttackTargetNPC.position.Y + ownerMinionAttackTargetNPC.height / 2;
                float num511 = Math.Abs(projectile.position.X + projectile.width / 2 - num509) + Math.Abs(projectile.position.Y + projectile.height / 2 - num510);
                if (num511 < num508 && Collision.CanHit(projectile.position, projectile.width, projectile.height, ownerMinionAttackTargetNPC.position, ownerMinionAttackTargetNPC.width, ownerMinionAttackTargetNPC.height))
                {
                    num508 = num511;
                    num506 = num509;
                    num507 = num510;
                    flag18 = true;
                }
            }
            if (!flag18)
            {
                for (int num512 = 0; num512 < 200; num512++)
                {
                    if (Main.npc[num512].CanBeChasedBy(this, false))
                    {
                        float num513 = Main.npc[num512].position.X + Main.npc[num512].width / 2;
                        float num514 = Main.npc[num512].position.Y + Main.npc[num512].height / 2;
                        float num515 = Math.Abs(projectile.position.X + projectile.width / 2 - num513) + Math.Abs(projectile.position.Y + projectile.height / 2 - num514);
                        if (num515 < num508 && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[num512].position, Main.npc[num512].width, Main.npc[num512].height))
                        {
                            num508 = num515;
                            num506 = num513;
                            num507 = num514;
                            flag18 = true;
                        }
                    }
                }
            }
            if (flag18)
            {
                float num516 = num506;
                float num517 = num507;
                num506 -= projectile.Center.X;
                num507 -= projectile.Center.Y;
                int num518 = 0;
                if (projectile.frameCounter > 0)
                {
                    projectile.frameCounter--;
                }
                if (projectile.frameCounter <= 0)
                {
                    int num519 = projectile.spriteDirection;
                    if (num506 < 0f)
                    {
                        projectile.spriteDirection = -1;
                    }
                    else
                    {
                        projectile.spriteDirection = 1;
                    }
                    if (num507 > 0f)
                    {
                        num518 = 0;
                    }
                    else if (Math.Abs(num507) > Math.Abs(num506) * 3f)
                    {
                        num518 = 4;
                    }
                    else if (Math.Abs(num507) > Math.Abs(num506) * 2f)
                    {
                        num518 = 3;
                    }
                    else if (Math.Abs(num506) > Math.Abs(num507) * 3f)
                    {
                        num518 = 0;
                    }
                    else if (Math.Abs(num506) > Math.Abs(num507) * 2f)
                    {
                        num518 = 1;
                    }
                    else
                    {
                        num518 = 2;
                    }
                    int num520 = projectile.frame;
                    projectile.frame = num518 * 2;
                    projectile.frame++;
                    if (num520 != projectile.frame || num519 != projectile.spriteDirection)
                    {
                        projectile.frameCounter = 8;
                        if (projectile.ai[0] <= 0f)
                        {
                            projectile.frameCounter = 4;
                        }
                    }
                }
                if (projectile.ai[0] <= 0f)
                {
                    projectile.localAI[1] = 0f;
                    projectile.ai[0] = 60f;
                    if (Main.myPlayer == projectile.owner)
                    {
                        float num521 = 6f;
                        int num522 = mod.ProjectileType("IceBall");
                        Vector2 vector37 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                        if (num518 == 0)
                        {
                            vector37.Y += 12f;
                            vector37.X += 24 * projectile.spriteDirection;
                        }
                        else if (num518 == 1)
                        {
                            vector37.Y += 0f;
                            vector37.X += 24 * projectile.spriteDirection;
                        }
                        else if (num518 == 2)
                        {
                            vector37.Y -= 2f;
                            vector37.X += 24 * projectile.spriteDirection;
                        }
                        else if (num518 == 3)
                        {
                            vector37.Y -= 6f;
                            vector37.X += 14 * projectile.spriteDirection;
                        }
                        else if (num518 == 4)
                        {
                            vector37.Y -= 14f;
                            vector37.X += 2 * projectile.spriteDirection;
                        }
                        if (projectile.spriteDirection < 0)
                        {
                            vector37.X += 10f;
                        }
                        float num523 = num516 - vector37.X;
                        float num524 = num517 - vector37.Y;
                        float num525 = (float)Math.Sqrt(num523 * num523 + num524 * num524);
                        num525 = num521 / num525;
                        num523 *= num525;
                        num524 *= num525;
                        int num526 = projectile.damage;
                        Projectile.NewProjectile(vector37.X, vector37.Y, num523*2f, num524*2f, num522, num526, projectile.knockBack, Main.myPlayer, 0f, 0f);
                    }
                }
            }
            else if (projectile.ai[0] <= 60f && (projectile.frame == 1 || projectile.frame == 3 || projectile.frame == 5 || projectile.frame == 7 || projectile.frame == 9))
            {
                projectile.frame--;
            }
            if (projectile.ai[0] > 0f)
            {
                projectile.ai[0] -= 1f;
                return;
            }
        }
    }
}