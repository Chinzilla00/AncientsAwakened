using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using AAMod.Buffs;

namespace AAMod.NPCs.Bosses.Infinity
{
	[AutoloadBossHead]
	public class Infinity : ModNPC
	{
        public NPC Zero1;
        public NPC Zero2;
        public NPC Zero3;
        public NPC Zero4;
        public NPC Zero5;
        public NPC Zero6;
        public NPC Core;
        public bool ZerosSpawned = false;
        public bool Reseting = false;
        public Vector2 topVisualOffset = default(Vector2);
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Infinity Zero; Mechanical Malice");
			Main.npcFrameCount[npc.type] = 4;
		}
		public override void SetDefaults()
		{
			npc.damage = 0;
            npc.width = 420; 			
            npc.height = 342;
            npc.npcSlots = 100;
            npc.scale = 1f;
            npc.dontTakeDamage = true;
			npc.lifeMax = 2500000;
			npc.knockBackResist = 0f;
			npc.aiStyle = -1;
			npc.value = Item.buyPrice(30, 0, 0, 0);
			npc.boss = true;
			for (int k = 0; k < npc.buffImmune.Length; k++)
			{
				npc.buffImmune[k] = true;
			}
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.netAlways = true;
			npc.chaseable = true;
			music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/IZ");
			npc.HitSound = SoundID.NPCHit44;
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/IZRoar");
            npc.scale *= 1.4f;
            bossBag = mod.ItemType("IZCache");
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.damage = (int)(npc.damage * 1.1f); 
        }

        public float[] customAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write((short)customAI[0]);
                writer.Write((short)customAI[1]);
                writer.Write((short)customAI[2]);
                writer.Write((short)customAI[3]);				
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {				
                customAI[0] = reader.ReadFloat();
                customAI[1] = reader.ReadFloat();
                customAI[2] = reader.ReadFloat();
                customAI[3] = reader.ReadFloat();				
            }
        }
        public int roarTimer = 200;
		public bool[] roared = new bool[3];
        private int testime = 60;
        private int StormTimer = 0;
        public override void AI()
		{
            npc.timeLeft = 200;
            if (testime > 0)
            {
                testime--;
            }

            StormTimer++;
            if (StormTimer >= 750)
            {
                StormTimer = 0;
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, npc.velocity.X * 2f, npc.velocity.Y * 2f, mod.ProjectileType("InfinityStorm"), npc.damage, 0, Main.myPlayer);
            }

            if (Main.netMode != 2)
			{
				int ThreeQuartersHealth = npc.lifeMax * (int).75f;
				int HalfHealth = npc.lifeMax * (int).5f;
				int QuarterHealth = npc.lifeMax * (int).25f;
				
				if(roarTimer > -1) roarTimer--;
				if (npc.life <= ThreeQuartersHealth && !roared[0])
				{
					roared[0] = true;
					roarTimer = 200;
				}
				if (npc.life <= HalfHealth && !roared[1])
				{
					roared[1] = true;
					roarTimer = 200;
				}
				if (npc.life <= QuarterHealth && !roared[2])
				{
					roared[2] = true;
					roarTimer = 200;
				}
				if (roarTimer == 180)
				{
					Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/IZRoar"), npc.Center);
				}
			}

            Player player = Main.player[npc.target];
            if (player != null)
            {
                float dist = npc.Distance(player.Center);
                if (dist > 1200) //trigger teleporting stuff
                {
                    npc.dontTakeDamage = true;
                    npc.alpha += 10;
                    if (npc.alpha >= 255) //teleport, you're invisible!
                    {
                        npc.alpha = 254; //don't let it hit 255 or it will despawn!
                        Vector2 tele = new Vector2(player.Center.X, player.Center.Y);
                        npc.Center = tele;
                        npc.dontTakeDamage = false;
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/IZRoar"), npc.Center);
                    }
                }
                else //you're close to the player, so make sure you're visible!
                {
                    npc.dontTakeDamage = false; //to prevent you from being indestructible if the teleport is interrupted
                    npc.alpha -= 25;
                    if (npc.alpha <= 0)
                    {
                        npc.alpha = 0;
                    }
                }
            }

            float movementMax = 1.5f;
			if(npc.target > -1)
			{
				Player targetPlayer = Main.player[npc.target];
				if(!targetPlayer.dead) //speed changes depending on how far the player is
				{
                    npc.alpha -= 10;
                    if (npc.alpha <= 0)
                    {
                        npc.alpha = 0;
                    }
                    movementMax = MathHelper.Lerp(1f, 4f, Math.Min(1f, Math.Max(0f, (Vector2.Distance(npc.Center, targetPlayer.Center) / 1000f))));
				}
                if (targetPlayer.dead) //speed changes depending on how far the player is
                {
                    npc.alpha += 10;
                    if (npc.alpha >= 255)
                    {
                        npc.active = false;
                    }
                }
            }
			//customAI is used here because the original ai and localAI are both used elsewhere. It is synced above.
            BaseAI.AIElemental(npc, ref customAI, false, 0, false, false, 800f, 600f, 60, movementMax);
            if (!ZerosSpawned)
            {
                if (Main.netMode != 1)
                {
                    int latestNPC = npc.whoAmI;
					int handType = 0;
                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("IZHand1"), 0, npc.whoAmI);
                    Main.npc[latestNPC].ai[0] = npc.whoAmI;
					Main.npc[latestNPC].ai[1] = handType;
					handType++;
                    Zero1 = Main.npc[latestNPC];
                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("IZHand1"), 0, npc.whoAmI);
                    Main.npc[latestNPC].ai[0] = npc.whoAmI;
					Main.npc[latestNPC].ai[1] = handType;
					handType++;
                    Zero2 = Main.npc[latestNPC];
                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("IZHand1"), 0, npc.whoAmI);
                    Main.npc[latestNPC].ai[0] = npc.whoAmI;
					Main.npc[latestNPC].ai[1] = handType;
					handType++;
                    Zero3 = Main.npc[latestNPC];
                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("IZHand2"), 0, npc.whoAmI);
                    Main.npc[latestNPC].ai[0] = npc.whoAmI;
					Main.npc[latestNPC].ai[1] = handType;
					handType++;
                    Zero4 = Main.npc[latestNPC];
                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("IZHand2"), 0, npc.whoAmI);
                    Main.npc[latestNPC].ai[0] = npc.whoAmI;
					Main.npc[latestNPC].ai[1] = handType;
					handType++;
                    Zero5 = Main.npc[latestNPC];
                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("IZHand2"), 0, npc.whoAmI);
                    Main.npc[latestNPC].ai[0] = npc.whoAmI;
					Main.npc[latestNPC].ai[1] = handType;
                    Zero6 = Main.npc[latestNPC];
                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("InfinityCore"), 0, npc.whoAmI);
                    Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                    Main.npc[latestNPC].ai[0] = npc.whoAmI;
                    Core = Main.npc[latestNPC];
                }
                ZerosSpawned = true;
            }
            if (testime == 0 && (Zero1 == null || Zero2 == null || Zero3 == null || Zero4 == null || Zero5 == null || Zero6 == null || !Zero1.active || !Zero2.active || !Zero3.active || !Zero4.active || !Zero5.active || !Zero6.active))
            {
                Reseting = true;
                testime = 60;
            }
            if ((Zero1 == null || !Zero1.active) && (Zero2 == null || !Zero2.active) && (Zero3 == null || !Zero3.active) && (Zero4 == null || !Zero4.active) && (Zero5 == null || !Zero5.active) && (Zero6 == null || !Zero6.active))
            {
                ZerosSpawned = false;
            }
            for (int m = npc.oldPos.Length - 1; m > 0; m--)
            {
                npc.oldPos[m] = npc.oldPos[m - 1];
            }
            npc.oldPos[0] = npc.position;		
        }

        public bool Dead = false;

        public override void NPCLoot()
		{
            Dead = true;
            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<Oblivion>(), 0, 0);
            AAPlayer.ZeroKills += 1;
            AAWorld.downedIZ = true;
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                npc.DropLoot(mod.ItemType("Infinitium"), 25, 35);
                string[] lootTable =
                {
                    "Genocide",
                    "Nova",
                    "Sagittarius",
                    "TotalDestruction",
                    "Annihilator",
                    "InfinityBlade"
                };
                int loot = Main.rand.Next(lootTable.Length);
                npc.DropLoot(mod.ItemType(lootTable[loot]));
                npc.DropLoot(Items.Boss.EXSoul.type);
            }
        }
        

        public override void FindFrame(int frameHeight)
        {
            if (roarTimer > -1)
            {
                npc.frame.Y = 2 * frameHeight;
            } else
            {
                npc.frame.Y = 0;
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = mod.ItemType("GrandHealingPotion");
        }
		
		private void ModifyHit(ref int damage)
		{
            damage = (int)(damage * 0.6f);
            if (damage >= 800)
            {
                damage = 800;
            }
        }
		
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 3f;
			return null;
		}
		
		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}
		

        public bool quarterHealth = false;
        public bool threeQuarterHealth = false;
        public bool HalfHealth = false;
        public bool fifthHealth = false;
        public bool OpenCore = false;
        public bool FirstCoreLine = false;
        public int CoreTimer = 600;

        public override void HitEffect(int hitDirection, double damage)
		{
            if (npc.life <= ((npc.lifeMax / 4) * 3) && threeQuarterHealth == false)
            {
                if (Main.netMode != 1) BaseUtility.Chat("WARNING. Systems have reached 75% efficiency.", new Color(158, 3, 32));
                threeQuarterHealth = true;
                roarTimer = 200;
            }
            if (npc.life <= npc.lifeMax / 2 && HalfHealth == false)
            {
                if (Main.netMode != 1) BaseUtility.Chat("Redirecting resources to offensive systems.", new Color(158, 3, 32));
                HalfHealth = true;
                npc.defense = 225;
                IZHand1.damageIdle = 250;
                IZHand1.damageCharging = 350;
                roarTimer = 200;
            }
            if (npc.life <= npc.lifeMax / 4 && quarterHealth == false)
            {
                if (Main.netMode != 1) BaseUtility.Chat("CRITICAL WARNING. Systems have reached 25% efficiency. Failure imminent.", new Color(158, 3, 32));
                quarterHealth = true;
                roarTimer = 200;
            }
            if (npc.life <= npc.lifeMax / 6 && !fifthHealth)
            {
                fifthHealth = true;
                if (Main.netMode != 1) BaseUtility.Chat("Terrarian, you will not win this. Rerouting all resources to offensive systems.", new Color(158, 3, 32));
                npc.defense = 175;
                IZHand1.damageIdle = 350;
                IZHand1.damageCharging = 500;
                roarTimer = 200;
            }
            if (npc.ai[3] == 6)
            {
                CoreTimer--;
                OpenCore = true;
                if (Main.netMode != 1 && !FirstCoreLine)
                {
                    FirstCoreLine = true;
                    BaseUtility.Chat("Zero Units in critical condition. Rerouting resources to repair systems. Core defense temporarily disabled.", new Color(158, 3, 32));
                }
                if (CoreTimer <= 0)
                {
                    BaseUtility.Chat("Zero Units sufficiently repaired. Reengaging Core defense system.", new Color(158, 3, 32));
                    npc.ai[3] = 0;
                    OpenCore = false;
                    CoreTimer = 600;
                    IZHand1.RepairMode = false;
                    IZHand2.RepairMode = false;
                }

            }
            if (npc.life <= npc.lifeMax / 6)
            {
                music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/LastStand");
            }
			if (npc.life <= 0)
			{
				float randomSpread = (Main.rand.Next(-50, 50) / 100);
				Gore.NewGore(npc.Center, npc.velocity * randomSpread * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/IZGore1"), 1.4f);
				Gore.NewGore(npc.Center, npc.velocity * randomSpread * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/IZGore2"), 1.4f);
				Gore.NewGore(npc.Center, npc.velocity * randomSpread * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/IZGore3"), 1.4f);
				Gore.NewGore(npc.Center, npc.velocity * randomSpread * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/IZGore4"), 1.4f);
				Gore.NewGore(npc.Center, npc.velocity * randomSpread * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/IZGore5"), 1.4f);
				npc.position.X = npc.position.X + (npc.width / 2);
				npc.position.Y = npc.position.Y + (npc.height / 2);
				npc.width = 400;
				npc.height = 350;
				npc.position.X = npc.position.X - npc.width / 2;
				npc.position.Y = npc.position.Y - npc.height / 2;
				for (int num621 = 0; num621 < 60; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType<Dusts.VoidDust>(), 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 90; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType<Dusts.VoidDust>(), 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 244, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}

        public void DrawCore(SpriteBatch spriteBatch, string coreTex, NPC core, Color drawColor, bool DrawUnder)
        {
            if (core != null && core.active)
            {
                BaseDrawing.DrawTexture(spriteBatch, mod.GetTexture(coreTex), 0, npc.Center, core.width, core.height, core.scale, core.rotation, core.spriteDirection, Main.npcFrameCount[core.type], core.frame, drawColor, false);
            }
        }

        public static Color infinityGlowRed = new Color(233, 53, 53);
        public static Color GetGlowAlpha(bool aura)
        {
            return (aura ? infinityGlowRed : Color.White) * (Main.mouseTextColor / 255f);
        }

        public Color GetRedAlpha()
        {
            return new Color(233, 53, 53) * (Main.mouseTextColor / 255f);
        }

        public static Texture2D glowTex = null;
        public float auraPercent = 0f;
        public bool auraDirection = true;
        public bool saythelinezero = false;
		
		public Vector2 GetConnectionPoint(int handType)
		{
			float offsetX = 0, offsetY = 0;
			switch(handType)
			{
				case 0: offsetX = -62; offsetY = -80; break;
				case 1: offsetX = -32; offsetY = -44; break;
				case 2: offsetX = -46; offsetY = -20; break;
				case 3: offsetX = 62; offsetY = -80; break;
				case 4: offsetX = 32; offsetY = -44; break;
				case 5: offsetX = 46; offsetY = -20; break;		
				default: break;
			}
			offsetX *= 2f;
			offsetY *= 2f;
			return new Vector2(offsetX, offsetY);
		}		

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            if (glowTex == null)
            {
                glowTex = mod.GetTexture("NPCs/Bosses/Infinity/Infinity_Glow");
            }
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            DrawCore(sb, "NPCs/Bosses/Infinity/InfinityCore", Core, AAColor.Oblivion, false);
            if (fifthHealth)
            {
                BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc, dColor);
                BaseDrawing.DrawAura(sb, glowTex, 0, npc, auraPercent, 1f, 0f, 0f, GetRedAlpha());
                BaseDrawing.DrawTexture(sb, glowTex, 0, npc, GetRedAlpha());
            }
            else
            {
                BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc, BaseUtility.ColorClamp(BaseDrawing.GetNPCColor(npc, npc.Center + new Vector2(0, -30), true, 0f), GetGlowAlpha(true)));
                BaseDrawing.DrawAura(sb, glowTex, 0, npc, auraPercent, 1f, 0f, 0f, GetGlowAlpha(true));
                BaseDrawing.DrawTexture(sb, glowTex, 0, npc, GetGlowAlpha(false));
            }


            string ZeroTex = ("NPCs/Bosses/Infinity/IZHand1");

            //bottom arms
            DrawZero(sb, ZeroTex, ZeroTex + "_Glow", Zero6, dColor);
	        DrawZero(sb, ZeroTex, ZeroTex + "_Glow", Zero3, dColor);	
            //middle arms
	        DrawZero(sb, ZeroTex, ZeroTex + "_Glow", Zero5, dColor);		
            DrawZero(sb, ZeroTex, ZeroTex + "_Glow", Zero2, dColor);
			//top arms
			DrawZero(sb, ZeroTex, ZeroTex + "_Glow", Zero4, dColor);		
			DrawZero(sb, ZeroTex, ZeroTex + "_Glow", Zero1, dColor);			

		
            return false;
        }

        public void DrawZero(SpriteBatch spriteBatch, string zeroTexture, string glowMaskTexture, NPC Zero, Color drawColor)
        {
            if (Zero != null && Zero.active && Zero.modNPC != null && (Zero.modNPC is IZHand1 || Zero.modNPC is IZHand2))
            {
				IZHand1 handNPC = (IZHand1)Zero.modNPC;
                string ArmTex = ("NPCs/Bosses/Infinity/IZArm");
                Texture2D ArmTex2D = mod.GetTexture(ArmTex);
				Texture2D zeroTex = mod.GetTexture(zeroTexture);
                Texture2D glowTex = mod.GetTexture(glowMaskTexture);				
                Vector2 ArmOrigin = new Vector2(npc.Center.X, npc.Center.Y) + GetConnectionPoint(handNPC.handType);
                Vector2 connector = Zero.Center;
                BaseDrawing.DrawChain(spriteBatch, new Texture2D[] { ArmTex2D, ArmTex2D, ArmTex2D }, 0, ArmOrigin, connector, ArmTex2D.Height - 10f, null, 1f, false, null);
				BaseDrawing.DrawTexture(spriteBatch, zeroTex, 0, Zero, BaseUtility.ColorClamp(BaseDrawing.GetNPCColor(Zero), GetGlowAlpha(true)));
                if (fifthHealth)
                {
                    BaseDrawing.DrawAura(spriteBatch, glowTex, 0, Zero, auraPercent, 1f, 0f, 0f, GetGlowAlpha(true));
                    BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, Zero, GetRedAlpha());
                }
                else
                {
                    BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, Zero, GetGlowAlpha(false));
                }
            }
        }
    }
	
}
