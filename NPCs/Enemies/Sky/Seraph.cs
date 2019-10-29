using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using AAMod.NPCs.Bosses.Athena;
using AAMod.NPCs.Bosses.Athena.Olympian;

namespace AAMod.NPCs.Enemies.Sky
{
	public class Seraph : ModNPC
	{
        public override void SetStaticDefaults()
		{
            Main.npcFrameCount[npc.type] = 4;		
		}			
		
        public override void SetDefaults()
        {
            npc.width = 60;
            npc.height = 40;
            npc.value = BaseUtility.CalcValue(0, 0, 10, 0);
            npc.npcSlots = 1;
			npc.aiStyle = -1;
            npc.lifeMax = 500;
            npc.defense = 20;
            npc.damage = 55;
            npc.knockBackResist = 0.3f;
			npc.noGravity = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.noTileCollide = true;
            if (npc.type == ModContent.NPCType<SeraphA>())
            {
                npc.alpha = 255;
            }
        }

        public override bool PreAI()
        {
            if (npc.type == ModContent.NPCType<SeraphA>() && !(NPC.AnyNPCs(ModContent.NPCType<Athena>()) || NPC.AnyNPCs(ModContent.NPCType<AthenaA>())))
            {
                npc.velocity.Y -= .2f;
                npc.velocity.X *= .95f;
                if (npc.position.Y + npc.velocity.Y <= 0f && Main.netMode != 1) { BaseAI.KillNPC(npc); npc.netUpdate = true; }
                return false;
            }
            return true;
        }

        public bool Friendly = true;

		public override void AI()
		{
            if (Friendly)
            {
                FriendlyAI();
                Rectangle rectangle = new Rectangle((int)Main.player[npc.target].position.X, (int)Main.player[npc.target].position.Y, Main.player[npc.target].width, Main.player[npc.target].height);
                Rectangle rectangle2 = new Rectangle((int)npc.position.X - 100, (int)npc.position.Y - 100, npc.width + 200, npc.height + 200);
                if (rectangle2.Intersects(rectangle) || npc.life < npc.lifeMax)
                {
                    npc.TargetClosest(true);
                    npc.ai[0] = 0;
                    npc.ai[1] = 0;
                    npc.ai[2] = 0;
                    npc.ai[3] = 0;
                    npc.localAI[0] = 0;
                    npc.localAI[1] = 0;
                    npc.localAI[2] = 0;
                    npc.localAI[3] = 0;
                    Friendly = false;
                    npc.netUpdate = true;
                }
            }
            else
			{
                BaseAI.AIFlier(npc, ref npc.ai, true, 0.15f, 0.08f, 8f, 7f, false, 300);

                if (npc.alpha > 0)
                {
                    npc.alpha -= 4;
                }
                else
                {
                    npc.alpha = 0;
                }

                Player player = Main.player[npc.target];

                if (npc.ai[3]++ > 30 && Main.netMode != 1)
                {
                    int projType = ModContent.ProjectileType<SeraphFeather>();
                    float spread = 30f * 0.0174f;
                    Vector2 dir = Vector2.Normalize(player.Center - npc.Center);
                    dir *= 14f;
                    float baseSpeed = (float)Math.Sqrt((dir.X * dir.X) + (dir.Y * dir.Y));
                    double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
                    double deltaAngle = spread / 6f;
                    for (int i = 0; i < 3; i++)
                    {
                        double offsetAngle = startAngle + (deltaAngle * i);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), projType, npc.damage / 4, 2, Main.myPlayer);
                    }
                    npc.ai[3] = 0;
                    npc.netUpdate = true;
                }

                npc.spriteDirection = npc.direction;
                npc.rotation = npc.velocity.X * 0.05f;
            }
		}

        private void FriendlyAI()
        {
            npc.TargetClosest(true);
            npc.noGravity = true;
            if (npc.collideX)
            {
                npc.velocity.X = npc.oldVelocity.X * -0.5f;
                if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 2f)
                {
                    npc.velocity.X = 2f;
                }
                if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -2f)
                {
                    npc.velocity.X = -2f;
                }
            }
            if (npc.collideY)
            {
                npc.velocity.Y = npc.oldVelocity.Y * -0.5f;
                if (npc.velocity.Y > 0f && npc.velocity.Y < 1f)
                {
                    npc.velocity.Y = 1f;
                }
                if (npc.velocity.Y < 0f && npc.velocity.Y > -1f)
                {
                    npc.velocity.Y = -1f;
                }
            }
            if (npc.direction == -1 && npc.velocity.X > -4f)
            {
                npc.velocity.X = npc.velocity.X - 0.1f;
                if (npc.velocity.X > 4f)
                {
                    npc.velocity.X = npc.velocity.X - 0.1f;
                }
                else if (npc.velocity.X > 0f)
                {
                    npc.velocity.X = npc.velocity.X + 0.05f;
                }
                if (npc.velocity.X < -4f)
                {
                    npc.velocity.X = -4f;
                }
            }
            else if (npc.direction == 1 && npc.velocity.X < 4f)
            {
                npc.velocity.X = npc.velocity.X + 0.1f;
                if (npc.velocity.X < -4f)
                {
                    npc.velocity.X = npc.velocity.X + 0.1f;
                }
                else if (npc.velocity.X < 0f)
                {
                    npc.velocity.X = npc.velocity.X - 0.05f;
                }
                if (npc.velocity.X > 4f)
                {
                    npc.velocity.X = 4f;
                }
            }
            if (npc.directionY == -1 && (double)npc.velocity.Y > -1.5)
            {
                npc.velocity.Y = npc.velocity.Y - 0.04f;
                if ((double)npc.velocity.Y > 1.5)
                {
                    npc.velocity.Y = npc.velocity.Y - 0.05f;
                }
                else if (npc.velocity.Y > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y + 0.03f;
                }
                if ((double)npc.velocity.Y < -1.5)
                {
                    npc.velocity.Y = -1.5f;
                }
            }
            else if (npc.directionY == 1 && (double)npc.velocity.Y < 1.5)
            {
                npc.velocity.Y = npc.velocity.Y + 0.04f;
                if ((double)npc.velocity.Y < -1.5)
                {
                    npc.velocity.Y = npc.velocity.Y + 0.05f;
                }
                else if (npc.velocity.Y < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y - 0.03f;
                }
                if ((double)npc.velocity.Y > 1.5)
                {
                    npc.velocity.Y = 1.5f;
                }
            }
            npc.ai[1] += 1f;
            if (npc.type == 158)
            {
                npc.ai[1] += 1f;
            }
            if (npc.ai[1] > 200f)
            {
                if (!Main.player[npc.target].wet && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                {
                    npc.ai[1] = 0f;
                }
                float num206 = 0.2f;
                float num207 = 0.1f;
                float num208 = 4f;
                float num209 = 1.5f;
                if (npc.ai[1] > 1000f)
                {
                    npc.ai[1] = 0f;
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] > 0f)
                {
                    if (npc.velocity.Y < num209)
                    {
                        npc.velocity.Y = npc.velocity.Y + num207;
                    }
                }
                else if (npc.velocity.Y > -num209)
                {
                    npc.velocity.Y = npc.velocity.Y - num207;
                }
                if (npc.ai[2] < -150f || npc.ai[2] > 150f)
                {
                    if (npc.velocity.X < num208)
                    {
                        npc.velocity.X = npc.velocity.X + num206;
                    }
                }
                else if (npc.velocity.X > -num208)
                {
                    npc.velocity.X = npc.velocity.X - num206;
                }
                if (npc.ai[2] > 300f)
                {
                    npc.ai[2] = -300f;
                }
            }
        }

		public override void FindFrame(int frameHeight)
		{
            if (npc.velocity.X > 0f)
            {
                npc.spriteDirection = 1;
            }
            if (npc.velocity.X < 0f)
            {
                npc.spriteDirection = -1;
            }
            npc.rotation = npc.velocity.X * 0.1f;
            if (npc.type == 210 || npc.type == 211)
            {
                npc.frameCounter += 1.0;
                npc.rotation = npc.velocity.X * 0.2f;
            }
            npc.frameCounter += 1.0;
            if (npc.frameCounter >= 6.0)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.frame.Y >= frameHeight * Main.npcFrameCount[npc.type])
            {
                npc.frame.Y = 0;
            }
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SeraphFeather"));
        }
    }
}