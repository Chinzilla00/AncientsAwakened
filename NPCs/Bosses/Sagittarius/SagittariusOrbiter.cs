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


        public float[] MovementType = new float[1];
        public float[] InternalAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write(InternalAI[0]);
                writer.Write(InternalAI[1]);
                writer.Write(InternalAI[2]);
                writer.Write(InternalAI[3]);
                writer.Write(MovementType[0]);
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
                InternalAI[3] = reader.ReadFloat();
                MovementType[0] = reader.ReadFloat();
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

            Player player = Main.player[sagittarius.target];

            pos = sagittarius.Center;

            npc.ai[1]++;
            if (npc.ai[1] > 200)
            {
                npc.ai[1] = 0;
                MovementType[0] = MovementType[0] == 1 ? 0 : 1;
                npc.netUpdate = true;
            }

            if (MovementType[0] == 0)
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
                MoveToPoint(BaseUtility.RotateVector(sagittarius.Center, sagittarius.Center + new Vector2(140, 0f), rotValue));
                int aiTimerFire = npc.whoAmI % 3 == 0 ? 50 : npc.whoAmI % 2 == 0 ? 150 : 100;
                BaseAI.ShootPeriodic(npc, player.position, player.width, player.height, mod.ProjType("DoomLaser"), ref shootAI[0], aiTimerFire, (int)(npc.damage * (Main.expertMode ? 0.25f : 0.5f)), 10f, true, new Vector2(20f, 15f));
            }
            else if (MovementType[0] == 1)
            {
                BaseAI.AIEye(npc, ref InternalAI, false, true, 0.2f, 0.2f, 5f, 5f, 1f, 1f);
            }
		}

        public override void NPCLoot()
        {
            if (NPC.AnyNPCs(mod.NPCType<Sagittarius>()))
            {
                Item.NewItem(npc.Center, mod.ItemType<Items.Materials.VoidEnergy>(), Main.rand.Next(2));
            }
        }

        public void MoveToPoint(Vector2 point, bool goUpFirst = false)
        {
            if (npc.Center == point) return; //don't move if you have no move speed
            float velMultiplier = 1f;
            Vector2 dist = point - npc.Center;
            float moveSpeed = 18f ;
            float length = (dist == Vector2.Zero ? 0f : dist.Length());
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            if (length < 200f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 100f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 50f)
            {
                moveSpeed *= 0.5f;
            }
            npc.velocity = (length == 0f ? Vector2.Zero : Vector2.Normalize(dist));
            npc.velocity *= moveSpeed;
            npc.velocity *= velMultiplier;
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
            BaseDrawing.DrawChain(sb, chainTex, 0, endPoint, pos, 0, AAColor.Oblivion);
			BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc, lightColor);
            BaseDrawing.DrawTexture(sb, mod.GetTexture("Glowmasks/SagittariusOrbiter_Glow"), 0, npc, AAColor.ZeroShield);
            BaseDrawing.DrawAfterimage(sb, mod.GetTexture("Glowmasks/SagittariusOrbiter_Glow"), 0, npc, 2f, 0.9f, 4, false, 0f, 0f, AAColor.ZeroShield);
            return false;
		}		
	}
}