using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Kraken
{
    public class Kraken : ModNPC
    {
        public NPC Tentacle1;
        public NPC Tentacle2;
        public NPC Tentacle3;
        public NPC Tentacle4;
        public NPC Tentacle5;
        public NPC Tentacle6;


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

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Kraken");
            
        }
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 120;
            npc.alpha = 255;
            npc.damage = 80;
            npc.defense = 40;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Kraken");
            npc.lifeMax = 120000;
            npc.dontTakeDamage = true;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.timeLeft = 10;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public bool TentaclesSpawned = false;
        private int testime = 60;
        public bool Reseting = false;

        public override void AI()
        {
            Player player = Main.player[npc.target];
            if (player.Center.X > npc.Center.X) // so it faces the player
            {
                npc.spriteDirection = -1;
            }
            else
            {
                npc.spriteDirection = 1;
            }

            if (testime > 0)
            {
                testime--;
            }

            BaseAI.AIElemental(npc, ref customAI, false, 0, false, false, 800f, 600f, 60, 2.5f);

            if (!TentaclesSpawned)
            {
                if (Main.netMode != 1)
                {
                    if (Main.netMode != 1)
                    {
                        int latestNPC = npc.whoAmI;
                        int handType = 0;
                        latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("Tentacle1"), 0, npc.whoAmI);
                        Main.npc[latestNPC].ai[0] = npc.whoAmI;
                        Main.npc[latestNPC].ai[1] = handType;
                        handType++;
                        Tentacle1 = Main.npc[latestNPC];
                        latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("Tentacle1"), 0, npc.whoAmI);
                        Main.npc[latestNPC].ai[0] = npc.whoAmI;
                        Main.npc[latestNPC].ai[1] = handType;
                        handType++;
                        Tentacle2 = Main.npc[latestNPC];
                        latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("Tentacle1"), 0, npc.whoAmI);
                        Main.npc[latestNPC].ai[0] = npc.whoAmI;
                        Main.npc[latestNPC].ai[1] = handType;
                        handType++;
                        Tentacle3 = Main.npc[latestNPC];
                        latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("Tentacle2"), 0, npc.whoAmI);
                        Main.npc[latestNPC].ai[0] = npc.whoAmI;
                        Main.npc[latestNPC].ai[1] = handType;
                        handType++;
                        Tentacle4 = Main.npc[latestNPC];
                        latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("Tentacle2"), 0, npc.whoAmI);
                        Main.npc[latestNPC].ai[0] = npc.whoAmI;
                        Main.npc[latestNPC].ai[1] = handType;
                        handType++;
                        Tentacle5 = Main.npc[latestNPC];
                        latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("Tentacle2"), 0, npc.whoAmI);
                        Main.npc[latestNPC].ai[0] = npc.whoAmI;
                        Main.npc[latestNPC].ai[1] = handType;
                        Tentacle6 = Main.npc[latestNPC];
                    }
                }
                TentaclesSpawned = true;
            }
            if (testime == 0 && (Tentacle1 == null || Tentacle2 == null || Tentacle3 == null || Tentacle4 == null || Tentacle5 == null || Tentacle6 == null || !Tentacle1.active || !Tentacle2.active || !Tentacle3.active || !Tentacle4.active || !Tentacle5.active || !Tentacle6.active))
            {
                Reseting = true;
                testime = 60;
            }
            if ((Tentacle1 == null || !Tentacle1.active) && (Tentacle2 == null || !Tentacle2.active) && (Tentacle3 == null || !Tentacle3.active) && (Tentacle4 == null || !Tentacle4.active) && (Tentacle5 == null || !Tentacle5.active) && (Tentacle6 == null || !Tentacle6.active))
            {
                TentaclesSpawned = false;
            }
            for (int m = npc.oldPos.Length - 1; m > 0; m--)
            {
                npc.oldPos[m] = npc.oldPos[m - 1];
            }
            npc.oldPos[0] = npc.position;

        }
    }
}