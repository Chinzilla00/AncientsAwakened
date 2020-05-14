using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.ID;
using BaseMod;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Terrarium.PostEquinox
{
    public class TerraKrakenOrbiter : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terrarium Vortex");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.width = 46;
            npc.height = 46;
            npc.friendly = false;
            npc.lifeMax = 1300;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.timeLeft = 10;
            npc.alpha = 255;
            npc.knockBackResist = 0f;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }
        private NPC OwnerNpc
        {
            get { return Main.npc[(int)npc.ai[0]]; }
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(body);
                writer.Write(rotValue);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                body = reader.ReadInt();
                rotValue = reader.ReadFloat();
            }
        }

        public int body = -1;
        public float rotValue = -1f;
        public bool canAdd = false;
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                OwnerNpc.ai[2] = npc.ai[2];
            }
        }


        public override void AI()
        {
            if (npc.frameCounter++ > 5)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 46;
                if (npc.frame.Y >= 46 * 4)
                {
                    npc.frame.Y = 0;
                }
            }
            if (npc.alpha > 0)
            {
                npc.alpha -= 4;
            }
            else
            {
                npc.alpha = 0;
            }
            npc.noGravity = true;
            body = (int)npc.ai[0];
            if (body == -1)
            {
                int npcID = BaseAI.GetNPC(npc.Center, mod.NPCType("TerraKraken"), 120f, null);
                if (npcID >= 0) body = npcID;
            }
            if (body == -1) return;

            NPC TerraKraken = Main.npc[body];
            if (TerraKraken == null || TerraKraken.life <= 0 || !TerraKraken.active || TerraKraken.type != mod.NPCType("TerraKraken")) { npc.active = false; return; }

            double deg = (double)npc.ai[1]; 
            double rad = deg * (Math.PI / 180); 
            double dist = 80; 



            npc.position.X = OwnerNpc.Center.X - (int)(Math.Cos(rad) * dist) - npc.width / 2;
            npc.position.Y = OwnerNpc.Center.Y - (int)(Math.Sin(rad) * dist) - npc.height / 2;

            npc.ai[1] += 1f;
        }

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc, npc.GetAlpha(Color.White), true);
            return false;
        }
    }
}