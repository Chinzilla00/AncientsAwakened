using System;
using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.GripsShen;
using AAMod.NPCs.Bosses.Shen;


namespace AAMod.NPCs.Bosses.Grips
{
    public abstract class BaseGripOfChaos : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grip of Chaos");
            Main.npcFrameCount[npc.type] = 8;
        }

        public override void SetDefaults()
        {
            npc.width = 66;
            npc.height = 60;			
            npc.aiStyle = -1;
			npc.knockBackResist = 0f;	
            npc.value = Item.buyPrice(0, 4, 50, 0);
            npc.npcSlots = 1f;
            npc.boss = true;  
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.netAlways = true;
            bossBag = mod.ItemType("GripBag");
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter > 6)
            {
				npc.frameCounter = 0;
                npc.frame.Y += frameHeight;
                if (npc.ai[0] == 2 || npc.ai[0] == 3)
                {
                    if (npc.frame.Y < 4 * frameHeight || npc.frame.Y < 7 * frameHeight)
                    {
                        npc.frame.Y = 4 * frameHeight;
                    }
                }
                else
                {
                    if (npc.frame.Y > 4 * frameHeight)
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
            if (shenGrips)
            {
                potionType = ItemID.SuperHealingPotion;
            }
            else
            {
                potionType = ItemID.LesserHealingPotion;
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 0.8f);  //boss damage increase in expermode
        }

		public override void BossHeadRotation(ref float rotation)
		{
			rotation = npc.rotation;
		}
		public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
		{
			spriteEffects = (npc.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
		}

		public Vector2 offsetBasePoint = Vector2.Zero;
		public float moveSpeed = 6f;
        public bool shenGrips = false;
        public int MinionTimer = 0;

		public override void AI()
		{
			npc.TargetClosest();
			Player targetPlayer = Main.player[npc.target];

            if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
            {
                npc.TargetClosest(false);
                DespawnHandler();
                return;
            }

            if (Config.GripsMusic && (npc.type != mod.NPCType<AbyssGripS>() || npc.type != mod.NPCType<BlazeGrip>()))
            {
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/GripsTheme");
            }
            else
            {
                music = MusicID.Boss4;
            }

            bool forceChange = false;
			if(Main.netMode != 1 && npc.ai[0] != 2 && npc.ai[0] != 3)
			{
				int stopValue = (shenGrips ? 100 : 250);
				npc.ai[3]++;
				if(npc.ai[3] > stopValue) npc.ai[3] = stopValue;
				forceChange = npc.ai[3] >= stopValue;
			}
			if(npc.ai[0] == 1) //move to starting charge position
			{
				moveSpeed = (shenGrips ? 15f : 7f);
				Vector2 point = targetPlayer.Center + offsetBasePoint + new Vector2(0f, -250f);
				MoveToPoint(point);
				if(Main.netMode != 1 && (Vector2.Distance(npc.Center, point) < 10f || forceChange))
				{
					npc.ai[0] = 2;
					npc.ai[1] = targetPlayer.Center.X;
					npc.ai[2] = targetPlayer.Center.Y;
					npc.ai[3] = 0;
					npc.netUpdate = true;
				}
				BaseAI.LookAt(targetPlayer.Center, npc, 0, 0f, 0.1f, false);			
			}else
			if(npc.ai[0] == 2) //dive down
			{
				moveSpeed = (shenGrips ? 20f : 9f);
				Vector2 targetCenter = new Vector2(npc.ai[1], npc.ai[2]);
				Vector2 point = targetCenter - offsetBasePoint + new Vector2(0f, 250f);
				MoveToPoint(point);
				if(Main.netMode != 1 && Vector2.Distance(npc.Center, point) < 10f)
				{
					bool doubleDive = (npc.life < npc.lifeMax / 2);
					if(doubleDive)
					{
						npc.ai[0] = 3;
						npc.ai[1] = targetPlayer.Center.X;
						npc.ai[2] = targetPlayer.Center.Y;	
					}else
					{
						npc.ai[0] = 0;	
						npc.ai[1] = 0;
						npc.ai[2] = 0;							
					}
					npc.ai[3] = 0;				
					npc.netUpdate = true;
				}
				BaseAI.Look(npc, 0, 0f, 0.1f, false);				
			}else
			if(npc.ai[0] == 3) //dive up
			{
				moveSpeed = (shenGrips ? 20f : 9f);
				Vector2 targetCenter = new Vector2(npc.ai[1], npc.ai[2]);				
				Vector2 point = targetCenter + offsetBasePoint + new Vector2(0f, -250f);
				MoveToPoint(point);
				if(Main.netMode != 1 && Vector2.Distance(npc.Center, point) < 10f)
				{
					npc.ai[0] = 0;
					npc.ai[1] = 0;	
					npc.ai[2] = 0;					
					npc.ai[3] = 0;					
					npc.netUpdate = true;
				}
				BaseAI.Look(npc, 0, 0f, 0.1f, false);				
			}else //standard movement
			{
                MinionTimer++;
                if (MinionTimer == (shenGrips ? 100 : 160))
                {
                    if (npc.type == mod.NPCType<BlazeGrip>())
                    {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<BlazeClawM>());
                    }
                    if (npc.type == mod.NPCType<AbyssGrip>())
                    {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<AbyssClawM>());
                    }
                    if (npc.type == mod.NPCType<GripOfChaosRed>())
                    {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<DragonClawM>());
                    }
                    if (npc.type == mod.NPCType<GripOfChaosBlue>())
                    {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<HydraClawM>());
                    }
                    MinionTimer = 0;
                }
				moveSpeed = (shenGrips ? 12f : 5f);
				Vector2 point = targetPlayer.Center + offsetBasePoint;
				MoveToPoint(point);
				if(Main.netMode != 1 && (Vector2.Distance(npc.Center, point) < 50f || forceChange))
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
                npc.alpha += 5;
                if (npc.alpha >= 50)
                {
                    if (shenGrips == true)
                    {
                        npc.defense = 1000;
                    }
                    else
                    {
                        npc.defense = 40;
                    }
                    npc.alpha = 50;
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
                    if (npc.type == mod.NPCType<GripOfChaosRed>())
                    {
                        npc.defense = 20;
                    }
                    if (npc.type == mod.NPCType<GripOfChaosBlue>())
                    {
                        npc.defense = 12;
                    }
                    npc.alpha = 0;
                }
            }
        }

      

        public void MoveToPoint(Vector2 point, bool goUpFirst = false)
		{
			if(moveSpeed == 0f || npc.Center == point) return; //don't move if you have no move speed
			float velMultiplier = 1f;
			Vector2 dist = point - npc.Center;
			float length = (dist == Vector2.Zero ? 0f : dist.Length());
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
			npc.velocity = (length == 0f ? Vector2.Zero : Vector2.Normalize(dist));
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
