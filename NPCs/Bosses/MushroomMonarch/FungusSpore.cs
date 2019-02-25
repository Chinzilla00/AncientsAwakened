using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ReLogic.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Bosses.MushroomMonarch
{
    public class FungusSpore : ModNPC
    {
        public float[] internalAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write((float)internalAI[0]);
                writer.Write((float)internalAI[1]);
                writer.Write((float)internalAI[2]);
                writer.Write((float)internalAI[3]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mushling");
        }

        public override void SetDefaults()
        {
            npc.width = 14;
            npc.height = 14;
            npc.value = BaseMod.BaseUtility.CalcValue(0, 0, 0, 0);
            npc.npcSlots = 1;
            npc.aiStyle = -1;
            npc.lifeMax = 1;
            npc.defense = 0;
            npc.damage = 15;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = null;
            npc.knockBackResist = 0f;
            NPCID.Sets.NeedsExpertScaling[npc.type] = false;
        }

        public override void AI()
        {
            if (npc.ai[0] == 0 && npc.ai[1] == 0)
            {
                npc.velocity.X = 5;
            }
            else if (npc.ai[0] == 1 && npc.ai[1] == 0)
            {
                npc.velocity.X = -5;
            }
            else if (npc.ai[0] == 2 && npc.ai[1] == 0)
            {
                npc.velocity.X = 4;
                npc.velocity.Y = 2.5F;

            }
            else if (npc.ai[0] == 3 && npc.ai[1] == 0)
            {
                npc.velocity.X = -4;
                npc.velocity.Y = 2.5f;
            }
            npc.ai[1] = 1;
            
            BaseAI.AISpore(npc, ref internalAI, 0.1f, 0.02f, 5f, 1f);
            
        }
    }
}


