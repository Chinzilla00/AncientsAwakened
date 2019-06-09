using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Enemies.BiomeGuardians
{
	public class Dynasaur : ModNPC
	{
		public override void SetStaticDefaults()
		{
            Main.npcFrameCount[npc.type] = 8;
		}		
		
        public override void SetDefaults()
        {
            npc.width = 42;
            npc.height = 38;
            npc.value = BaseMod.BaseUtility.CalcValue(0, 0, 39, 50);
            npc.npcSlots = 1;
            npc.aiStyle = -1;
            npc.lifeMax = 180;
            npc.defense = 20;
            npc.damage = 30;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.7f;	
        }

		public Color smokeColor = new Color(145, 55, 26);

		public static int[] velocitiesX = new int[] { -6, -3, 0, 3, 6, 3, 0, -3 };
		public static int[] velocitiesY = new int[] { 0, 3, 6, 3, 0, -3, -6, -3 };

		public static Texture2D bodyTex;

		public override void NPCLoot()
		{
			if (Main.netMode != 1)
			{
				for (int m = 0; m < 8; m++)
				{
					BaseMod.BaseAI.FireProjectile(npc.Center + new Vector2(velocitiesX[m], velocitiesY[m]), npc.Center, mod.ProjType("BugAcidShot"), 0, 0f, 5f);
				}
			}
			BaseMod.BaseAI.DropItem(npc, mod.ItemType("AcidSac"), 1 + Main.rand.Next(2) + (Main.expertMode ? 2 : 0), 2, 65, true);
			if(ModSupport.calamity != null)
			{
				BaseMod.BaseAI.DropItem(npc, ModSupport.calamity.ItemType("BeetleJuice"), 1, 1, 65, true);	
				BaseMod.BaseAI.DropItem(npc, ModSupport.calamity.ItemType("EssenceofCinder"), 1, 1, (Main.expertMode ? 20 : 15), true);
			}
		}

        public float moveSpeed = 14f;
        public Vector2 MovePoint;
        public bool SelectPoint = false;

        public override void AI()
        {
            Player player = Main.player[npc.target];
            npc.noGravity = true;
            if (Main.netMode != 1)
            {
                npc.ai[3]++;
            }
            if (npc.ai[3] > 240)
            {
                if (SelectPoint && Main.netMode != 1)
                {
                    float Point = 500 * npc.direction;
                    MovePoint = player.Center + new Vector2(Point, 500f);
                    SelectPoint = false;
                    npc.netUpdate = true;
                }
                MoveToPoint(MovePoint);
                if (npc.ai[3] > 300 && Main.netMode != 1)
                {
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
            }
            else
            {
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
                npc.TargetClosest(true);
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
                if (npc.ai[1] > 200f)
                {
                    if (!Main.player[npc.target].wet && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                    {
                        npc.ai[1] = 0f;
                    }
                    float num205 = 0.2f;
                    float num206 = 0.1f;
                    float num207 = 4f;
                    float num208 = 1.5f;
                    if (npc.ai[1] > 1000f)
                    {
                        npc.ai[1] = 0f;
                    }
                    npc.ai[2] += 1f;
                    if (npc.ai[2] > 0f)
                    {
                        if (npc.velocity.Y < num208)
                        {
                            npc.velocity.Y = npc.velocity.Y + num206;
                        }
                    }
                    else if (npc.velocity.Y > -num208)
                    {
                        npc.velocity.Y = npc.velocity.Y - num206;
                    }
                    if (npc.ai[2] < -150f || npc.ai[2] > 150f)
                    {
                        if (npc.velocity.X < num207)
                        {
                            npc.velocity.X = npc.velocity.X + num205;
                        }
                    }
                    else if (npc.velocity.X > -num207)
                    {
                        npc.velocity.X = npc.velocity.X - num205;
                    }
                    if (npc.ai[2] > 300f)
                    {
                        npc.ai[2] = -300f;
                    }
                }
                if (Main.netMode != 1)
                {
                    npc.ai[0] += 1f;
                    if (npc.ai[0] == 20f || npc.ai[0] == 40f || npc.ai[0] == 60f || npc.ai[0] == 80f || npc.ai[0] == 100f)
                    {
                        if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                        {
                            float num223 = 0.2f;
                            Vector2 value2 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                            float num224 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - value2.X + (float)Main.rand.Next(-50, 51);
                            float num225 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - value2.Y + (float)Main.rand.Next(-50, 51);
                            float num226 = (float)Math.Sqrt((double)(num224 * num224 + num225 * num225));
                            num226 = num223 / num226;
                            num224 *= num226;
                            num225 *= num226;
                            int num227 = 80;
                            value2 += npc.velocity * 5f;
                            int num229 = Projectile.NewProjectile(value2.X + num224 * 100f, value2.Y + num225 * 100f, num224, num225, mod.ProjectileType<DynaBlast>(), num227, 0f, Main.myPlayer, 0f, 0f);
                            Main.projectile[num229].timeLeft = 300;
                            return;
                        }
                    }
                    else if (npc.ai[0] >= (float)(250 + Main.rand.Next(250)))
                    {
                        npc.ai[0] = 0f;
                        return;
                    }
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (npc.frameCounter++ > 8)
            {
                npc.frame.Y += frameHeight;
                npc.frameCounter = 0;
                if (npc.ai[3] < 240 && npc.frame.Y > frameHeight * 3)
                {
                    npc.frame.Y = 0;
                }
                else if (npc.ai[3] >= 240 && (npc.frame.Y < frameHeight * 4 || npc.frame.Y >= frameHeight * 8))
                {
                    npc.frame.Y = frameHeight * 4;
                }
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color dColor)
		{
			BaseDrawing.DrawAfterimage(sb, bodyTex, 0, npc, 2.5f, 0.9F, 3, true, 0f, 0f, dColor);
			BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseDrawing.DrawTexture(sb, mod.GetTexture("Glowmasks/Dynasaur_Glow"), 0, npc, Color.White);
            return false;
		}

        public void MoveToPoint(Vector2 point, bool goUpFirst = false)
        {
            if (moveSpeed == 0f || npc.Center == point) return; //don't move if you have no move speed
            float velMultiplier = 1f;
            Vector2 dist = point - npc.Center;
            float length = (dist == Vector2.Zero ? 0f : dist.Length());
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            if (length < 200f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 100f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 50f)
            {
                moveSpeed *= 0.5f;
            }
            npc.velocity = (length == 0f ? Vector2.Zero : Vector2.Normalize(dist));
            npc.velocity *= moveSpeed;
            npc.velocity *= velMultiplier;
        }
    }
}