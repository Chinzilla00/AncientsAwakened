using System;
using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.IO;

namespace AAMod.NPCs.Bosses.GripsShen
{
    public abstract class BaseShenGrips : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grip of Chaos");
            Main.npcFrameCount[npc.type] = 14;
        }

        public int damage = 0;

        public override void SetDefaults()
        {
            npc.width = 66;
            npc.height = 60;			
            npc.aiStyle = -1;
			npc.knockBackResist = 0f;	
            npc.value = Item.sellPrice(0, 4, 50, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.netAlways = true;
            npc.scale *= 1.4f;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Shen");
        }
        
        public override void FindFrame(int frameHeight)
        {
            if (!NPC.AnyNPCs(mod.NPCType("ShenDoragon")))
            {
                npc.life = 0;
            }
            npc.frameCounter++;
            if (npc.frameCounter > 9)
            {
                npc.frame.Y += frameHeight;
                npc.frameCounter = 0;
                if (npc.ai[0] == 2 || npc.ai[0] == 3 || npc.ai[0] == 4)
                {
                    if (npc.frame.Y < 4 * frameHeight || npc.frame.Y > 7 * frameHeight)
                    {
                        npc.frame.Y = 4 * frameHeight;
                    }

                }
                else if (npc.ai[0] == 5)
                {
                    npc.frame.Y = frameHeight * 8;
                    if (internalAI[0] > 8)
                    {npc.frame.Y = frameHeight * 9;}
                    if (internalAI[0] > 16)
                    {npc.frame.Y = frameHeight * 10;}
                    if (internalAI[0] > 24)
                    { npc.frame.Y = frameHeight * 11; }
                    if (internalAI[0] > 32)
                    { npc.frame.Y = frameHeight * 12; }
                    if (internalAI[0] > 40)
                    { npc.frame.Y = frameHeight * 13 ;}
                }
                else
                {
                    if (npc.frame.Y > 3 * frameHeight)
                    {
                        npc.frame.Y = 0;
                    }
                }
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.8f);
        }

		public override void BossHeadRotation(ref float rotation)
		{
			rotation = npc.rotation;
		}
		public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
		{
			spriteEffects = npc.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
		}

		public Vector2 offsetBasePoint = Vector2.Zero;
		public float moveSpeed = 14f;
        public int MinionTimer = 0;

        public float[] internalAI = new float[1];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == 2 || Main.dedServ)
            {
                writer.Write(internalAI[0]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadFloat();
            }
        }

        public override void AI()
		{
            if (Main.expertMode)
            {
                damage = npc.damage / 4;
            }
            else
            {
                damage = npc.damage / 2;
            }
            npc.TargetClosest();
			Player targetPlayer = Main.player[npc.target];

            if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
            {
                npc.TargetClosest(false);
                DespawnHandler();
                return;
            }

            bool forceChange = false;
			if(Main.netMode != 1 && npc.ai[0] != 2 && npc.ai[0] != 3)
			{
				int stopValue = 100;
				npc.ai[3]++;
				if(npc.ai[3] > stopValue) npc.ai[3] = stopValue;
				forceChange = npc.ai[3] >= stopValue;
			}
			if(npc.ai[0] == 1) //move to starting charge position
			{
				moveSpeed = 15;
				Vector2 point = targetPlayer.Center + offsetBasePoint + new Vector2(0f, -250f);
				MoveToPoint(point);
				if(Main.netMode != 1 && (Vector2.Distance(npc.Center, point) < 10f || forceChange))
				{
                    npc.ai[0] = 5;
                    npc.ai[1] = 0;
                    npc.ai[2] = 0;
					npc.ai[3] = 0;
					npc.netUpdate = true;
				}
				BaseAI.LookAt(targetPlayer.Center, npc, 0, 0f, 0.1f, false);			
			}else
			if(npc.ai[0] == 2) //dive down
			{
				moveSpeed = 30f;
				Vector2 targetCenter = new Vector2(npc.ai[1], npc.ai[2]);
				Vector2 point = targetCenter - offsetBasePoint + new Vector2(0f, 250f);
				MoveToPoint(point);
				if(Main.netMode != 1 && Vector2.Distance(npc.Center, point) < 10f)
				{
                    npc.ai[0] = 3;
                    npc.ai[1] = targetPlayer.Center.X;
                    npc.ai[2] = targetPlayer.Center.Y;
                    npc.ai[3] = 0;				
					npc.netUpdate = true;
				}
				BaseAI.Look(npc, 0, 0f, 0.1f, false);				
			}else
			if(npc.ai[0] == 3) //dive up
			{
				moveSpeed = 30f;
				Vector2 targetCenter = new Vector2(npc.ai[1], npc.ai[2]);				
				Vector2 point = targetCenter + offsetBasePoint + new Vector2(0f, -250f);
				MoveToPoint(point);
				if(Main.netMode != 1 && Vector2.Distance(npc.Center, point) < 10f)
				{
                    bool TripleDive = npc.life < npc.lifeMax / 2;
                    npc.ai[0] = TripleDive ? 4 : 0;
                    npc.ai[1] = TripleDive ? targetPlayer.Center.X : 0;
                    npc.ai[2] = TripleDive ? targetPlayer.Center.Y : 0;
                    npc.ai[3] = 0;					
					npc.netUpdate = true;
				}
				BaseAI.Look(npc, 0, 0f, 0.1f, false);				
			}else
            if (npc.ai[0] == 4) //dive back down
            {
                moveSpeed = 30f;
                Vector2 targetCenter = new Vector2(npc.ai[1], npc.ai[2]);
                Vector2 point = targetCenter - offsetBasePoint + new Vector2(0f, 250f);
                MoveToPoint(point);
                if (Main.netMode != 1 && Vector2.Distance(npc.Center, point) < 10f)
                {
                    npc.ai[0] = 0;
                    npc.ai[1] = 0;
                    npc.ai[2] = 0;
                    npc.ai[3] = 0;
                    npc.netUpdate = true;
                }
                BaseAI.Look(npc, 0, 0f, 0.1f, false);
            }
            else
            if (npc.ai[0] == 5) //Fire Projectile (Shen Grips)
            {
                moveSpeed = 15f;
                Vector2 point = targetPlayer.Center + offsetBasePoint + new Vector2(0f, -250f);
                MoveToPoint(point);
                bool BlazeGrip = npc.type == mod.NPCType<BlazeGrip>();
                if (Main.netMode != 1 && (Vector2.Distance(npc.Center, point) < 10f || forceChange))
                {
                    internalAI[0]++;
                    if (internalAI[0] == 40)
                    {
                        BaseAI.FireProjectile(targetPlayer.Center, npc.Center, BlazeGrip ? mod.ProjectileType<BlazeBomb>() : mod.ProjectileType<AbyssalBomb>(), damage, 2, 9f, -1, Main.myPlayer);
                    }
                    if (internalAI[0] > 50)
                    {
                        npc.ai[0] = 2;
                        npc.ai[1] = targetPlayer.Center.X;
                        npc.ai[2] = targetPlayer.Center.Y;
                        npc.ai[3] = 0;
                        internalAI[0] = 0;
                        npc.netUpdate = true;
                    }
                }
                BaseAI.LookAt(targetPlayer.Center, npc, 0, 0f, 0.1f, false);
            }
            else //standard movement
			{
                MinionTimer++;
                if (MinionTimer == 120)
                {
                    if (npc.type == mod.NPCType<BlazeGrip>() && NPC.AnyNPCs(mod.NPCType<BlazeClawM>()))
                    {
                        for (int Loops = 0; Loops < (Main.expertMode ? 6 : 4); Loops++)
                        {
                            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<BlazeClawM>());
                        }
                    }
                    if (npc.type == mod.NPCType<AbyssGrip>())
                    {
                        for (int Loops = 0; Loops < (Main.expertMode ? 6 : 4); Loops++)
                        {
                            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<AbyssClawM>());
                        }
                    }
                    MinionTimer = 0;
                }
				moveSpeed = 16f;
				Vector2 point = targetPlayer.Center + offsetBasePoint;
				MoveToPoint(point);
				if(Main.netMode != 1)
				{
					npc.ai[1]++;
					if(npc.ai[1] > 150)
					{
						npc.ai[0] = 1;
						npc.ai[1] = 0;	
						npc.ai[2] = 0;
						npc.ai[3] = 0;					
						npc.netUpdate = true;				
					}
				}		
				BaseAI.LookAt(targetPlayer.Center, npc, 0, 0f, 0.1f, false);			
			}
            if (npc.ai[0] == 0)
            {
                if (npc.alpha >= 50)
                {
                    npc.defense = 500;
                }
            }
            else
            {
                npc.alpha -= 5;
                if (npc.alpha <= 0)
                {
                    if (npc.type == mod.NPCType<BlazeGrip>())
                    {
                        npc.defense = 110;
                    }
                    if (npc.type == mod.NPCType<AbyssGrip>())
                    {
                        npc.defense = 90;
                    }
                }
            }
        }

      

        public void MoveToPoint(Vector2 point, bool goUpFirst = false)
		{
			if(moveSpeed == 0f || npc.Center == point) return; //don't move if you have no move speed
			float velMultiplier = 1f;
			Vector2 dist = point - npc.Center;
			float length = dist == Vector2.Zero ? 0f : dist.Length();
			if(length < moveSpeed)
			{
				velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
			}
			if(length < 200f)
			{
				moveSpeed *= 0.5f;
			}
			if(length < 100f)
			{
				moveSpeed *= 0.5f;
			}	
			if(length < 50f)
			{
				moveSpeed *= 0.5f;
			}
			npc.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
			npc.velocity *= moveSpeed;
			npc.velocity *= velMultiplier;
		}

        private void DespawnHandler()
        {
            Player player = Main.player[npc.target];
            npc.TargetClosest(false);
            player = Main.player[npc.target];
            if (!player.active || player.dead || Main.dayTime)        // If the player is dead and not active, the npc flies off-screen and despawns
            {
                npc.velocity.X = 0;
                npc.velocity.Y -= 1;
            }
        }
    }
}
