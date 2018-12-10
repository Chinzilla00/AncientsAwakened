using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

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
        public bool ZerosSpawned = false;
        public Vector2 topVisualOffset = default(Vector2);
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Infinity Zero; The Purge");
			Main.npcFrameCount[npc.type] = 2;
		}
		public override void SetDefaults()
		{
			npc.npcSlots = 18f;
			npc.damage = 0;
            npc.width = 420; 			
            npc.height = 342;
            npc.npcSlots = 100;
            npc.scale = 1f;
			npc.defense = 180;
			npc.lifeMax = 3000000;
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
        public override void AI()
		{
			if(Main.netMode != 2)
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
	
			float movementMax = 1f;
			if(npc.target > -1)
			{
				Player targetPlayer = Main.player[npc.target];
				if(targetPlayer.active && !targetPlayer.dead) //speed changes depending on how far the player is
				{
					movementMax = MathHelper.Lerp(1f, 4f, Math.Min(1f, Math.Max(0f, (Vector2.Distance(npc.Center, targetPlayer.Center) / 1000f))));
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
                }
                ZerosSpawned = true;
            }
            for (int m = npc.oldPos.Length - 1; m > 0; m--)
            {
                npc.oldPos[m] = npc.oldPos[m - 1];
            }
            npc.oldPos[0] = npc.position;		
        }

        public override void NPCLoot()
		{
            if (!AAWorld.downedIZ)
            {
                Projectile.NewProjectile((new Vector2(npc.Center.X, npc.Center.Y)), (new Vector2(0f, 0f)), mod.ProjectileType<Oblivion>(), 0, 0);
            }
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
                    "Annihilator"
                    //"RiftShredder",
                    //"VoidStar",
                };
                int loot = Main.rand.Next(lootTable.Length);
                npc.DropLoot(mod.ItemType(lootTable[loot]));
            }
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            damage = damage * 0.8f;
            return true;
        }

        public override void FindFrame(int frameHeight)
        {
            if (roarTimer > -1)
            {
                npc.frame.Y = 1 * frameHeight;
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
			if (damage > npc.lifeMax / 8)
			{
				damage = npc.lifeMax / 8;
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
		
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.8f);
		}

        public bool quarterHealth = false;
        public bool threeQuarterHealth = false;
        public bool HalfHealth = false;
        public bool eighthHealth = false;

        public override void HitEffect(int hitDirection, double damage)
		{
            if (npc.life <= ((npc.lifeMax / 4) * 3) && threeQuarterHealth == false)
            {
                if (Main.netMode != 1) BaseUtility.Chat("WARNING. Systems have reached 75% efficiency.", new Color(158, 3, 32));
                threeQuarterHealth = true;
            }
            if (npc.life <= npc.lifeMax / 2 && HalfHealth == false)
            {
                if (Main.netMode != 1) BaseUtility.Chat("Redirecting resources to offensive systems.", new Color(158, 3, 32));
                HalfHealth = true;
                npc.defense = 175;
                IZHand1.damageIdle = 150;
                IZHand1.damageCharging = 250;
            }
            if (npc.life <= npc.lifeMax / 4 && quarterHealth == false)
            {
                if (Main.netMode != 1) BaseUtility.Chat("CRITICAL WARNING. Systems have reached 25% efficiency. Failure imminent.", new Color(158, 3, 32));
                quarterHealth = true;
            }
            if (npc.life <= npc.lifeMax / 8 && !eighthHealth)
            {
                eighthHealth = true;
                if (Main.netMode != 1) BaseUtility.Chat("Terrarian, you will not win this. Rerouting all resources to offensive systems.", new Color(158, 3, 32));
                npc.defense = 0;
                IZHand1.damageIdle = 200;
                IZHand1.damageCharging = 300;
            }
            if (npc.life <= npc.lifeMax / 8)
            {
                music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/LastStand");
            }
            for (int k = 0; k < 15; k++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.VoidDust>(), hitDirection, -1f, 0, default(Color), 1f);
			}
			if (npc.life <= 0)
			{
				float randomSpread = (float)(Main.rand.Next(-50, 50) / 100);
				Gore.NewGore(npc.position, npc.velocity * randomSpread * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/IZGore1"), 1f);
				Gore.NewGore(npc.position, npc.velocity * randomSpread * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/IZGore2"), 1f);
				Gore.NewGore(npc.position, npc.velocity * randomSpread * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/IZGore3"), 1f);
				Gore.NewGore(npc.position, npc.velocity * randomSpread * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/IZGore4"), 1f);
				Gore.NewGore(npc.position, npc.velocity * randomSpread * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/IZGore5"), 1f);
				npc.position.X = npc.position.X + (float)(npc.width / 2);
				npc.position.Y = npc.position.Y + (float)(npc.height / 2);
				npc.width = 400;
				npc.height = 350;
				npc.position.X = npc.position.X - (float)(npc.width / 2);
				npc.position.Y = npc.position.Y - (float)(npc.height / 2);
				for (int num621 = 0; num621 < 60; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType<Dusts.VoidDust>(), 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
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

		public static Color infinityGlowRed = new Color(233, 53, 53);
        public static Color GetGlowAlpha(bool aura)
        {
            return (aura ? infinityGlowRed : Color.White) * (Main.mouseTextColor / 255f);
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
            string ZeroTex = ("NPCs/Bosses/Infinity/IZHand1");
            if (glowTex == null)
            {
                glowTex = mod.GetTexture("NPCs/Bosses/Infinity/Infinity_Glow");
            }
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }

            BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc, BaseUtility.ColorClamp(BaseDrawing.GetNPCColor(npc, npc.Center + new Vector2(0, -30), true, 0f), GetGlowAlpha(true)));
            BaseDrawing.DrawAura(sb, glowTex, 0, npc, auraPercent, 1f, 0f, 0f, GetGlowAlpha(true));
            BaseDrawing.DrawTexture(sb, glowTex, 0, npc, GetGlowAlpha(false));
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
            if (Zero != null && Zero.active && Zero.modNPC != null && Zero.modNPC is IZHand1)
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
				BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, Zero, GetGlowAlpha(false));
            }
        }
    }
	
}