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
	public class SagittariusOrbiter : ModNPC
	{				
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Orbiter");
		}

        public override void SetDefaults()
        {
            npc.width = 38;
            npc.height = 38;
            npc.value = BaseUtility.CalcValue(0, 0, 0, 0);
            npc.npcSlots = 1;
            npc.aiStyle = -1;
            npc.lifeMax = 800;
            npc.defense = 20;
            npc.damage = 20;
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


        public float[] shootAI = new float[1];

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
            }
        }

        private Color Glow = GenericUtils.COLOR_GLOWPULSE;

        public override void AI()
		{
            if (!NPC.AnyNPCs(mod.NPCType<SagittariusOrbiter>()))
            {
                npc.life = 0;
            }

            npc.noGravity = true;

            if (npc.alpha > 0)
            {
                npc.dontTakeDamage = true;
                npc.chaseable = false;
            }
            else
            {
                npc.dontTakeDamage = false;
                npc.chaseable = true;
            }

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



            for (int m = npc.oldPos.Length - 1; m > 0; m--)
            {
                npc.oldPos[m] = npc.oldPos[m - 1];
            }
            npc.oldPos[0] = npc.position;

            int probeNumber = ((Sagittarius)sagittarius.modNPC).ProbeCount;
            if (rotValue == -1f) rotValue = (npc.ai[0] % probeNumber) * ((float)Math.PI * 2f / probeNumber);
            rotValue += 0.04f;
            while (rotValue > (float)Math.PI * 2f) rotValue -= (float)Math.PI * 2f;

            npc.alpha = (int)Sagittarius.MovementType[1];

            int aiTimerFire = 0;

            shootAI[0]++;

            if (npc.ai[0] == 1 || npc.ai[0] == 4 || npc.ai[0] == 7 || npc.ai[0] == 10)
            {
                aiTimerFire = 50;
                if (shootAI[0] < 50)
                {
                    Glow = AAColor.ZeroShield;
                }
                else
                {
                    Glow = GenericUtils.COLOR_GLOWPULSE;
                }
            }
            if (npc.ai[0] == 2 || npc.ai[0] == 5 || npc.ai[0] == 8 || npc.ai[0] == 11)
            {
                aiTimerFire = 100;
                if (shootAI[0] < 100 && shootAI[0] > 50)
                {
                    Glow = AAColor.ZeroShield;
                }
                else
                {
                    Glow = GenericUtils.COLOR_GLOWPULSE;
                }
            }
            if (npc.ai[0] == 3 || npc.ai[0] == 6 || npc.ai[0] == 9 || npc.ai[0] == 12)
            {
                aiTimerFire = 150;
                if (shootAI[0] > 100)
                {
                    Glow = AAColor.ZeroShield;
                }
                else
                {
                    Glow = GenericUtils.COLOR_GLOWPULSE;
                }
            }


            if (shootAI[0] >= 150)
            {
                shootAI[0] = 0;
            }

            if (Sagittarius.MovementType[0] == 0)
            {
                for (int m = npc.oldPos.Length - 1; m > 0; m--)
                {
                    npc.oldPos[m] = npc.oldPos[m - 1];
                }
                npc.oldPos[0] = npc.position;

                npc.Center = BaseUtility.RotateVector(sagittarius.Center, sagittarius.Center + new Vector2(140, 0f), rotValue);

                if (shootAI[0] == aiTimerFire)
                {
                    if (Collision.CanHit(npc.position, npc.width, npc.height, player.Center, player.width, player.height))
                    {
                        Vector2 fireTarget = npc.Center;
                        float rot = BaseUtility.RotationTo(npc.Center, player.Center);
                        fireTarget = BaseUtility.RotateVector(npc.Center, fireTarget, rot);
                        BaseAI.FireProjectile(player.Center, fireTarget, mod.ProjType("DoomLaser"), npc.damage / 2, 0f, 8f);
                    }
                }
            }
            if (Sagittarius.MovementType[0] == 1)
            {
                BaseAI.AIEye(npc, ref npc.ai, false, true, .2f, .1f, 6, 6, 0, 0);
            }
            else if (Sagittarius.MovementType[0] == 2)
            {
                for (int m = npc.oldPos.Length - 1; m > 0; m--)
                {
                    npc.oldPos[m] = npc.oldPos[m - 1];
                }
                npc.oldPos[0] = npc.position;
                
                npc.Center = BaseUtility.RotateVector(sagittarius.Center, sagittarius.Center + new Vector2(140, 0f), rotValue);
            }
            else if (Sagittarius.MovementType[0] == 3)
            {
                npc.Center = BaseUtility.RotateVector(player.Center, player.Center + new Vector2(260, 0f), rotValue);
                if (shootAI[0] == aiTimerFire)
                {
                    if (Collision.CanHit(npc.position, npc.width, npc.height, player.Center, player.width, player.height))
                    {
                        Vector2 fireTarget = npc.Center;
                        float rot = BaseUtility.RotationTo(npc.Center, player.Center);
                        fireTarget = BaseUtility.RotateVector(npc.Center, fireTarget, rot);
                        BaseAI.FireProjectile(player.Center, fireTarget, mod.ProjType("DoomLaser"), npc.damage / 2, 0f, 8f);
                    }
                }
            }
            else if (Sagittarius.MovementType[0] == 4)
            {
                for (int m = npc.oldPos.Length - 1; m > 0; m--)
                {
                    npc.oldPos[m] = npc.oldPos[m - 1];
                }
                npc.oldPos[0] = npc.position;
                
                npc.Center = BaseUtility.RotateVector(sagittarius.Center, sagittarius.Center + new Vector2(140, 0f), rotValue);
            }
            else if (Sagittarius.MovementType[0] == 5)
            {
                npc.velocity *= .8f;
            }
            if (Sagittarius.MovementType[0] != 1)
            {
                npc.rotation = 0;
            }
        }

        public override void NPCLoot()
        {
            if (NPC.AnyNPCs(mod.NPCType<Sagittarius>()))
            {
                Item.NewItem(npc.Center, mod.ItemType<Items.Materials.Doomite>(), Main.rand.Next(2));
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color dColor)
		{
			BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc, npc.GetAlpha(dColor));
            if(npc.alpha < 0)
            {
                BaseDrawing.DrawTexture(sb, mod.GetTexture("Glowmasks/SagittariusOrbiter_Glow"), 0, npc, GenericUtils.COLOR_GLOWPULSE);
            }
            return false;
		}		
	}
}