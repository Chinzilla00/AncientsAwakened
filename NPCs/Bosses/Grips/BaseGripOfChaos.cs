using System;
using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.NPCs.Bosses.Grips
{
    public abstract class BaseGripOfChaos : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grip of Chaos");
            Main.npcFrameCount[npc.type] = 4;
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
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/GripsTheme");	
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
				if(npc.frame.Y > 3 * frameHeight)
				{
					npc.frame.Y = 0;
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
            potionType = ItemID.LesserHealingPotion;
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
            else
            {
                npc.alpha += 12;
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
                npc.alpha -= 10;
                npc.velocity.X = 0;
                npc.velocity.Y = 0;
                if (npc.alpha >= 255)
                {
                    npc.active = false;
                }
            }
        }

        /*public int timer;
        private bool switchMove = false; //Creates a bool for this .cs only
        public void AIOLD()
        {
            if (Main.dayTime)
            {
                npc.position.Y -= 10;  //disappears at night
            }
            Target();
            DespawnHandler();
            if (switchMove)
            {
                Move(new Vector2(240, 0));   //240 is the X axis, so its to the right of the player, -240 will be to the left
            }
            npc.ai[0]++;
            Player P = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
            }
            npc.netUpdate = true;
            if (Main.rand.Next(700) == 0) // The lower the value, the higher chance of a grippy boi spawning
            {
                NPC.NewNPC((int)npc.position.X + 70, (int)npc.position.Y + 70, mod.NPCType("HydraClaw")); //Change name AAAAAAAAAAAAAAAAAAAA
            }
            timer++;                //Makes the int start
            if (timer == 450)          //if the timer has gotten to 7.5 seconds, this happens (60 = 1 second)
            {
                switchMove = true;     //Makes the switch turn on, making the AI change to nothing
                npc.aiStyle = -1;      //So the AI doesnt mix with the flying AI Style
                npc.rotation = 0;      // I think this is the right rotation, if not change it tooooo 180 or something
            }
            if (timer >= 900)          //After 15 seconds this happens
            {
                switchMove = false;     //Turns the switch off so the void Move stuff is disabled
                npc.aiStyle = 5;        //Reverts back to the original Flying AI Style
                timer = 0;              //Sets the timer back to 0 to repeat
            }
            if (switchMove)
            {
                float num4 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2);
                float num5 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2);
                npc.rotation = (float)Math.Atan2((double)num5, (double)num4) - 1.57f;
            }
        }
		private void Move(Vector2 offset)
        {
            if (switchMove)             //If the switchMove is on, all of this happens, if its off, all of this doesnt happen
            {
                if (Main.expertMode)
                {
                    speed = 30f; // Increased movement speed in expert mode (The Keeper only thing, change if you wish)
                }
                else
                {
                    speed = 30f; // Sets the max speed of the npc.
                }
                Vector2 moveTo = player.Center + offset; // Gets the point that the npc will be moving to.
                Vector2 move = moveTo - npc.Center;
                float magnitude = Magnitude(move);
                if (magnitude > speed)
                {
                    move *= speed / magnitude;
                }
                float turnResistance = 35f; // The larger the number the slower the npc will turn.
                move = ((npc.velocity * turnResistance) + move) / (turnResistance + 1f);
                magnitude = Magnitude(move);
                if (magnitude > speed)
                {
                    move *= speed / magnitude;
                }
                npc.velocity = move;
            }
        }

        private void Target()
        {
            player = Main.player[npc.target]; // This will get the player target.
        }
        
        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt((mag.X * mag.X) + (mag.Y * mag.Y));      //No idea, leave this
        }*/
    }
}
