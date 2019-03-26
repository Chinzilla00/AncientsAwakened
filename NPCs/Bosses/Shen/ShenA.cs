using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using AAMod;

namespace AAMod.NPCs.Bosses.Shen
{
    [AutoloadBossHead]
    public class ShenA : ShenDoragon
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shen Doragon Awakened; Unyieldng Chaos Incarnate");
            Main.npcFrameCount[npc.type] = 2;
        }

        public float[] InternalAI = new float[3];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write((short)InternalAI[0]);
                writer.Write((short)InternalAI[1]);
                writer.Write((short)InternalAI[2]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                InternalAI[0] = reader.ReadFloat();
                InternalAI[1] = reader.ReadFloat();
                InternalAI[2] = reader.ReadFloat();
            }
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 230;
            npc.defense = 230;
            npc.lifeMax = 500000;
            npc.value = Item.buyPrice(40, 0, 0, 0);
            bossBag = mod.ItemType("ShenCache");
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/ShenA");
            musicPriority = (MusicPriority)11;
            isAwakened = true;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = npc.lifeMax;
            npc.defense = (int)(npc.defense * 1.2f);
            npc.damage = (int)(npc.damage * 1.2f);
            damageDiscordianInferno = (int)(damageDiscordianInferno * 1.2f);
        }

        public override void PostAI()
        {
            Player player = Main.player[npc.target];
            InternalAI[0]++;
            if (InternalAI[0] > 240)
            {
                if (InternalAI[2] == 0)
                {
                    InternalAI[1] = Main.rand.Next(3);
                    InternalAI[2] = 1;
                }
                if (InternalAI[1] == 0)
                {
                    if (InternalAI[0] == 280 || InternalAI[0] == 320 || InternalAI[0] == 360 || InternalAI[0] == 400)
                    {
                        int Fireballs = Main.expertMode ? 10 : 7;
                        for (int Loops = 0; Loops < Fireballs; Loops++)
                        {
                            ShenAttacks.Dragonfire(npc, mod);
                        }
                    }
                }
                if (InternalAI[1] == 1)
                {
                    int Fireballs = Main.expertMode ? 12 : 14;
                    if (InternalAI[0] == 280 || InternalAI[0] == 340 || InternalAI[0] == 400)
                    {
                        for (int Loops = 0; Loops < Fireballs; Loops++)
                        {
                            ShenAttacks.Eruption(npc, mod);
                        }
                    }
                }
                if (InternalAI[1] == 2)
                {
                    if (InternalAI[0] == 330)
                    {
                        if (NPC.CountNPCS(mod.NPCType<Shenling>()) < (Main.expertMode ? 3 : 4))
                        {
                            ShenAttacks.SpawnLung(player, mod);
                        }
                    }
                }
                if (InternalAI[0] > 400)
                {
                    InternalAI[0] = 0;
                    InternalAI[2] = 0;
                }
            }
        }

        public bool Health9 = false;
        public bool Health8 = false;
        public bool Health7 = false;
        public bool Health6 = false;
        public bool Health4 = false;
        public bool Health3 = false;
        public bool Health2 = false;
        public bool Health1 = false;
        public bool HealthOneHalf = false;

        public override void HitEffect(int hitDirection, double damage)
        {
            base.HitEffect(hitDirection, damage);			
            if (npc.life <= npc.lifeMax * 0.9f && !Health9)
            {
                if (Main.netMode != 1) BaseUtility.Chat("Face it, child! You’ll never defeat the living embodiment of disarray itself!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                Health9 = true;
                npc.netUpdate = true;
            }
            if (npc.life <= npc.lifeMax * 0.8f && !Health8)
            {
                if (Main.netMode != 1) BaseUtility.Chat("You’re still going? How amusing...", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                Health8 = true;
                npc.netUpdate = true;
            }
            if (npc.life <= npc.lifeMax * 0.7f && !Health7)
            {
                if (Main.netMode != 1) BaseUtility.Chat("Putting up a fight when you know Death is inevitable...", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                Health7 = true;
                npc.netUpdate = true;
            }
            if (npc.life <= npc.lifeMax * 0.6f && !Health6)
            {
                if (Main.netMode != 1) BaseUtility.Chat("Now stop making this hard! Stand still and take it like a man!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                Health6 = true;
                npc.netUpdate = true;
            }
            if (npc.life <= npc.lifeMax * 0.4f && !Health4)
            {
                if (Main.netMode != 1) BaseUtility.Chat("DIE ALREADY YOU INSIGNIFICANT LITTLE WORM!!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                Health4 = true;
                npc.netUpdate = true;
            }
            if (npc.life <= npc.lifeMax * 0.3f && !Health3)
            {
                if (Main.netMode != 1) BaseUtility.Chat("WHAT?! HOW HAVE YOU- ENOUGH! YOU WILL KNOW WHAT IT MEANS TO FEEL UNYIELDING CHAOS!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                Health3 = true;
                npc.netUpdate = true;
            }
            if (npc.life <= npc.lifeMax * 0.2f && !Health2)
            {
                if (Main.netMode != 1) BaseUtility.Chat("NO! I WILL NOT LOSE! NOT TO YOU!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                Health2 = true;
                npc.netUpdate = true;
            }
            if (npc.life <= npc.lifeMax * 0.1f && !Health1)
            {
                if (Main.netMode != 1) BaseUtility.Chat("I AM SHEN DORAGON! EMPEROR OF DEATH AND DISASTER, AND I WILL NOT BE OUTDONE BY A HAIRLESS APE!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                Health1 = true;
                npc.netUpdate = true;
            }
            if (npc.life <= npc.lifeMax * 0.05f && !HealthOneHalf)
            {
                if (Main.netMode != 1) BaseUtility.Chat("GRAAAAAAAAAH!!!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                HealthOneHalf = true;
                npc.netUpdate = true;
            }
            if (Health3)
            {
                music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/LastStand");
            }			
        }

        public override bool PreDraw(SpriteBatch sb, Color drawColor)
        {
			Texture2D currentTex = Main.npcTexture[npc.type];
			Texture2D currentWingTex = mod.GetTexture("NPCs/Bosses/Shen/ShenAWings");
            Texture2D glowTex = mod.GetTexture("NPCs/Bosses/Shen/ShenA_Glow");

			//offset
			npc.position.Y += 130f;

			//draw body/charge afterimage
			if(Charging)
			{
				BaseDrawing.DrawAfterimage(sb, currentTex, 0, npc, 1.5f, 1f, 3, false, 0f, 0f, new Color(drawColor.R, drawColor.G, drawColor.B, (byte)150));	
			}
			BaseDrawing.DrawTexture(sb, currentTex, 0, npc, drawColor);
			
			//draw glow/glow afterimage
            BaseDrawing.DrawTexture(sb, glowTex, 0, npc, AAColor.Shen3);
			BaseDrawing.DrawAfterimage(sb, glowTex, 0, npc, 0.8f, 1f, 4, false, 0f, 0f, AAColor.Shen3);	
			
			//draw wings
			BaseDrawing.DrawTexture(sb, currentWingTex, 0, npc.position + new Vector2(0, npc.gfxOffY), npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, 5, wingFrame, drawColor);

			//deoffset
			npc.position.Y -= 130f; // offsetVec;			

            return false;
        }		
    }
    
}
