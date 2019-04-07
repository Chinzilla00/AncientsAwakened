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
	public class VoidReturn : ModNPC
	{				
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Void Return");
		}

        public override void SetDefaults()
        {
            npc.width = 10;
            npc.height = 10;
            npc.aiStyle = -1;
            npc.lifeMax = 1;
            npc.noGravity = true;
            npc.immortal = true;
            npc.damage = 0;
        }

        public float[] internalAI = new float[1];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write(internalAI[0]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadFloat();
            }
        }

        public override void AI()
        {
            Player player = Main.player[npc.target];

            AAPlayer modplayer = player.GetModPlayer<AAPlayer>(mod);

            if (internalAI[0] == 0)
            {
                BaseAI.AIEye(npc, ref npc.ai, false, true, 0.2f, 0.2f, 6f, 6f, 1f, 1f);
            }
            else if (internalAI[0] == 1)
            {
                modplayer.RingLocation = npc.Center;
                int VoidHeight = 140;
                Vector2 point = new Vector2((Main.maxTilesX / 15 * 14) + (Main.maxTilesX / 15 / 2) - 100, VoidHeight);
                MoveToPoint(point);
                float dist = npc.Distance(point);
                if (dist < 500 && !Collision.SolidCollision(npc.position, npc.width, npc.height) && modplayer.ZoneVoid)
                {
                    internalAI[0] = 2;
                }
            }
            else
            {
                npc.scale *= .9f;
                if (npc.scale < .1f)
                {
                    npc.active = false;
                }
            }
        }

        public void MoveToPoint(Vector2 point)
        {
            if (npc.Center == point) return;
            float velMultiplier = 1f;
            Vector2 dist = point - npc.Center;
            float moveSpeed = 12;
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

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("Abducted"), 2);
            internalAI[0] = 1;
        }

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            if (internalAI[0] == 1)
            {
                BaseDrawing.DrawTexture(sb, mod.GetTexture("NPCs/Bosses/Sagittarius/SagittariusShield"), 0, npc, AAColor.ZeroShield);
            }
            BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseDrawing.DrawTexture(sb, mod.GetTexture("Glowmasks/SagitarriusFreeRing_Glow"), 0, npc, AAColor.ZeroShield);
            return false;
		}		
	}
}