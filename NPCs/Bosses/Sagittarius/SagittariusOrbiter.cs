using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using System.IO;

namespace AAMod.NPCs.Bosses.Sagittarius
{
	public class SagittariusOrbiter : Sagittarius
	{				
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Orbiter");
		}

        public override void SetDefaults()
        {
            npc.width = 42;
            npc.height = 38;
            npc.value = BaseUtility.CalcValue(0, 0, 0, 0);
            npc.npcSlots = 1;
            npc.aiStyle = -1;
            npc.lifeMax = 500;
            npc.defense = 20;
            npc.damage = 10;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.knockBackResist = 0f;	
			npc.noTileCollide = true;	
			npc.defense = 15;
        }

		public int body = -1;
		public float rotValue = -1f;
		public bool spawnedDust = false;
        public Vector2 pos;
        int ChainFrame = 0;
        int ChainTimer = 0;


        public float[] internalAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
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

        public override void AI()
		{
            if (!NPC.AnyNPCs(mod.NPCType<SagittariusOrbiter>()))
            {
                npc.life = 0;
            }
            npc.noGravity = true;
            ChainTimer++;
            if (ChainTimer > 4)
            {
                ChainFrame += 1;
                if (ChainFrame > 2)
                {
                    ChainFrame = 0;
                }
            }
			if(body == -1)
			{
				int npcID = BaseAI.GetNPC(npc.Center, mod.NPCType("Sagittarius"), 400f, null);	
				if(npcID >= 0) body = npcID;
			}
			if(body == -1) return;				
			NPC sagittarius = Main.npc[body];
			if(sagittarius == null || sagittarius.life <= 0 || !sagittarius.active || sagittarius.type != mod.NPCType("Sagittarius")){ BaseAI.KillNPCWithLoot(npc); return; }

            pos = sagittarius.Center;

            npc.ai[2]++;
            if (npc.ai[2] > 200)
            {
                npc.ai[2] = 0;
                if (npc.ai[1] == 0)
                {
                    npc.ai[1] = 1;
                }
                else
                {
                    npc.ai[1] = 0;
                }
            }

            if (Main.netMode != 2 && !spawnedDust)
			{
				spawnedDust = true;
				for (int m = 0; m < 10; m++)
				{
                    int dustID = Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.VoidDust>());
					Main.dust[dustID].noGravity = true;
					Main.dust[dustID].velocity = new Vector2(MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()), MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()));
					Main.dust[dustID].velocity *= 2f;
				}
			}
            if (npc.ai[1] == 0)
            {
                for (int m = npc.oldPos.Length - 1; m > 0; m--)
                {
                    npc.oldPos[m] = npc.oldPos[m - 1];
                }
                npc.oldPos[0] = npc.position;

                int probeNumber = ((Sagittarius)sagittarius.modNPC).ProbeCount;
                if (rotValue == -1f) rotValue = (npc.ai[0] % probeNumber) * ((float)Math.PI * 2f / probeNumber);
                rotValue += 0.05f;
                while (rotValue > (float)Math.PI * 2f) rotValue -= (float)Math.PI * 2f;
                npc.Center = BaseUtility.RotateVector(sagittarius.Center, sagittarius.Center + new Vector2(140f, 0f), rotValue);
            }
            else
            {
                BaseAI.AIEye(npc, ref internalAI, false, true, 0.2f, 0.2f, 3f, 3f, 1f, 1f);
            }

            if (Main.netMode != 2 && Main.player[Main.myPlayer].miscTimer % 2 == 0)
			{
                Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.VoidDust>());
            }			
		}

        public override void NPCLoot()
        {
            if (NPC.AnyNPCs(mod.NPCType<Sagittarius>()))
            {
                Item.NewItem(npc.Center, mod.ItemType<Items.Materials.VoidEnergy>(), Main.rand.Next(2));
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color dColor)
		{
			Color lightColor = BaseDrawing.GetNPCColor(npc, null);
            Texture2D chainTex = mod.GetTexture("NPCs/Bosses/Sagittarius/OrbiterChain1");
            if (ChainFrame == 0)
            {
                chainTex = mod.GetTexture("NPCs/Bosses/Sagittarius/OrbiterChain1");
            }
            if (ChainFrame == 1)
            {
                chainTex = mod.GetTexture("NPCs/Bosses/Sagittarius/OrbiterChain2");
            }
            else
            {
                chainTex = mod.GetTexture("NPCs/Bosses/Sagittarius/OrbiterChain3");
            }
            Vector2 endPoint = BaseUtility.RotateVector(npc.Center, npc.Center + new Vector2(-2f, 0), npc.rotation + (npc.spriteDirection == -1 ? (float)Math.PI : 0));
            BaseDrawing.DrawChain(sb, new Texture2D[] { null, chainTex, null }, 0, endPoint, (pos* 16) + new Vector2(8f, 8f));
			BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc, lightColor);
            BaseDrawing.DrawTexture(sb, mod.GetTexture("Glowmasks/SagitarriusOrbiter_Glow"), 0, npc, AAColor.ZeroShield);
            BaseDrawing.DrawAfterimage(sb, mod.GetTexture("Glowmasks/SagitarriusOrbiter_Glow"), 0, npc, 2f, 0.9f, 4, true, 0f, 0f, AAColor.ZeroShield);
            return false;
		}		
	}
}