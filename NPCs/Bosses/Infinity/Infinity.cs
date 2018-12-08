using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using AAMod.Sounds;

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
            npc.width = 422;
            npc.height = 342;
            npc.npcSlots = 100;
            npc.scale = 1f;
			npc.defense = 180;
			npc.lifeMax = 2500000;
			npc.knockBackResist = 0f;
			npc.aiStyle = -1;
            aiType = -1;
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
            npc.behindTiles = true;
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/IZ");
			npc.HitSound = SoundID.NPCHit44;
			npc.DeathSound = SoundID.NPCDeath46;
			bossBag = mod.ItemType("IZBag");
		}

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((short)npc.localAI[5]);
            writer.Write((short)npc.localAI[6]);
        }

        public int roartimer = 0;

        public override void AI()
		{
            int Life = npc.life;
            npc.localAI[5] = Life;
            npc.localAI[6] = Life / 2;
            int ThreeQuartersHealth = npc.life * (int).75f;
            int HalfHealth = npc.life * (int).5f;
            int QuarterHealth = npc.life * (int).25f;
            
            bool isRoaring1 = true;
            bool isRoaring2 = true;
            bool isRoaring3 = true;

            --roartimer;
            if (npc.life <= ThreeQuartersHealth && isRoaring1)
            {
                roartimer = 200;
            }
            if (npc.life <= HalfHealth && isRoaring2)
            {
                roartimer = 200;
            }
            if (npc.life <= QuarterHealth && isRoaring3)
            {
                roartimer = 200;
            }
            if (roartimer == 180)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/IZRoar"), (int)npc.Center.X, (int)npc.Center.Y);
            }

            if (!ZerosSpawned)
            {
                if (Main.netMode != 1)
                {
                    int latestNPC = npc.whoAmI;
                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("IZHand1"), 0, npc.whoAmI);
                    Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                    Main.npc[(int)latestNPC].ai[0] = npc.whoAmI;
                    Zero1 = Main.npc[latestNPC];
                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("IZHand1"), 0, npc.whoAmI);
                    Main.npc[(int)latestNPC].ai[0] = npc.whoAmI;
                    Zero2 = Main.npc[latestNPC];
                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("IZHand1"), 0, npc.whoAmI);
                    Main.npc[(int)latestNPC].ai[0] = npc.whoAmI;
                    Zero3 = Main.npc[latestNPC];
                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("IZHand2"), 0, npc.whoAmI);
                    Main.npc[(int)latestNPC].ai[0] = npc.whoAmI;
                    Zero4 = Main.npc[latestNPC];
                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("IZHand2"), 0, npc.whoAmI);
                    Main.npc[(int)latestNPC].ai[0] = npc.whoAmI;
                    Zero5 = Main.npc[latestNPC];
                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("IZHand2"), 0, npc.whoAmI);
                    Main.npc[(int)latestNPC].ai[0] = npc.whoAmI;
                    Zero6 = Main.npc[latestNPC];
                }
                ZerosSpawned = true;
            }
            BaseAI.AIElemental(npc, ref npc.ai, false, 0, false, false, 800f, 600f, 60, 1f);
        }

        /*public override void NPCLoot()
		{
			
			
		}*/

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (roartimer > 0 && roartimer < 180)
            {
                npc.frame.Y = 1 * frameHeight;
            }
            else
            {
                npc.frameCounter = 0;
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.SuperHealingPotion;
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
			cooldownSlot = 1;
			return true;
		}
		
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.8f);
		}
		
		public override void HitEffect(int hitDirection, double damage)
		{
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

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            string ZeroTex = ("NPCs/Bosses/Infinity/IZHand");
            BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc.position + new Vector2(0f, npc.gfxOffY) + topVisualOffset, npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, Main.npcFrameCount[npc.type], npc.frame, dColor, false);
            DrawZero(sb, ZeroTex, ZeroTex + "_Glow", Zero1, dColor);
            DrawZero(sb, ZeroTex, ZeroTex + "_Glow", Zero2, dColor);
            DrawZero(sb, ZeroTex, ZeroTex + "_Glow", Zero3, dColor);
            DrawZero(sb, ZeroTex, ZeroTex + "_Glow", Zero4, dColor);
            DrawZero(sb, ZeroTex, ZeroTex + "_Glow", Zero5, dColor);
            DrawZero(sb, ZeroTex, ZeroTex + "_Glow", Zero6, dColor);
            return false;
        }

        public Vector2[] GetVectorChain(NPC Zero)
        {
            Vector2 bodyVec = (npc.Center - new Vector2(0f, 50f));
            float offsetY = 50f;
            return new Vector2[] { bodyVec, Vector2.Lerp(bodyVec, Zero.Center, 0.25f) - new Vector2(0f, offsetY * 0.5f), Vector2.Lerp(bodyVec, Zero.Center, 0.5f) - new Vector2(0f, offsetY), Vector2.Lerp(bodyVec, Zero.Center, 0.75f) - new Vector2(0f, offsetY * 0.5f), Zero.Center };
        }


        public void DrawZero(SpriteBatch spriteBatch, string ZeroTexture, string glowMaskTexture, NPC Zero, Color drawColor)
        {
            if (Zero != null && Zero.active)
            {
                string ArmTex = ("NPCs/Bosses/Infinity/IZArm");
                Texture2D ArmTex2D = mod.GetTexture(ArmTex);
                Vector2 ArmOrigin = new Vector2(npc.Center.X, npc.Center.Y - 40);
                Vector2 connector = Zero.Center;
                BaseDrawing.DrawChain(spriteBatch, new Texture2D[] { ArmTex2D, ArmTex2D, ArmTex2D }, 0, ArmOrigin, connector, ArmTex2D.Height - 10f, null, 1f, false, null);
            }
        }
    }
	
}