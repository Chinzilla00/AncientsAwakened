using System;
using System.IO;
using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Zero2
{
    [AutoloadBossHead]
    public class RiftShredder2 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gigataser");
            Main.npcFrameCount[npc.type] = 2;
            NPCID.Sets.TechnicallyABoss[npc.type] = true;
        }

        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 70;
            npc.damage = 80;
            npc.defense = 90;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCHit4;
            npc.lifeMax = 37500;
            npc.noGravity = true;
            animationType = NPCID.PrimeSaw;
            npc.noTileCollide = true;
            npc.knockBackResist = 0.0f;
            npc.buffImmune[20] = true;
            npc.buffImmune[24] = true;
            npc.buffImmune[39] = true;
            npc.lavaImmune = true;
            npc.netAlways = true;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(mod.NPCType<Zero2>()))
            {
                return false;
            }
            return true;
        }

        public float[] internalAI = new float[1];

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((short)npc.localAI[0]);
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write(internalAI[0]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            npc.localAI[0] = reader.ReadInt16();
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadFloat();
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (npc.velocity.Y == 0.0)
                npc.spriteDirection = npc.direction;
            ++npc.frameCounter;
            if (npc.frameCounter >= 8.0)
            {
                npc.frameCounter = 0.0;
                npc.frame.Y += frameHeight;
                if (npc.frame.Y / frameHeight >= 2)
                    npc.frame.Y = 0;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            bool flag = (npc.life <= 0 || (!npc.active && NPC.AnyNPCs(mod.NPCType<Zero2>())));
            if (flag && Main.netMode != 1)
            {
                int ind = NPC.NewNPC((int)(npc.position.X + (double)(npc.width / 2)), (int)npc.position.Y + (npc.height / 2), mod.NPCType("TeslaHand2"), npc.whoAmI, -2f, npc.ai[1], 0f, 0f, byte.MaxValue);
                Main.npc[ind].life = 1;
                Main.npc[ind].rotation = npc.rotation;
                Main.npc[ind].velocity = npc.velocity;
                Main.npc[ind].netUpdate = true;
                Main.npc[(int)npc.ai[1]].ai[3]++;
                Main.npc[(int)npc.ai[1]].netUpdate = true;
            }
        }

        public int body = -1;
        public float rotValue = -1f;
        public Vector2 pos;
        public Vector2 DashPoint;
        public float moveSpeed = 14;
        public bool SelectPoint = false;

        public override void AI()
        {
            npc.noGravity = true;

            if (body == -1)
            {
                int npcID = BaseAI.GetNPC(npc.Center, mod.NPCType("Zero2"), -1f, null);
                if (npcID >= 0) body = npcID;
            }
            if (body == -1) return;
            NPC zero = Main.npc[body];
            if (zero == null || zero.life <= 0 || !zero.active || zero.type != mod.NPCType("Zero2")) { BaseAI.KillNPCWithLoot(npc); return; }

            Player player = Main.player[zero.target];

            pos = zero.Center;

            for (int m = npc.oldPos.Length - 1; m > 0; m--)
            {
                npc.oldPos[m] = npc.oldPos[m - 1];
            }
            npc.oldPos[0] = npc.position;

            int probeNumber = ((Zero2)zero.modNPC).WeaponCount;
            if (rotValue == -1f) rotValue = (npc.ai[0] % probeNumber) * ((float)Math.PI * 2f / probeNumber);
            rotValue += 0.04f;
            while (rotValue > (float)Math.PI * 2f) rotValue -= (float)Math.PI * 2f;

            int aiTimerFire = Main.expertMode ? 350 : 400;

            if (Main.netMode != 1) { npc.ai[2]++; }

            if (npc.ai[2] >= aiTimerFire)
            {
                SwordAI(player);
                if (npc.ai[2] > 560 && Main.netMode != 1)
                {
                    MoveToPoint(BaseUtility.RotateVector(zero.Center, zero.Center + new Vector2(300, 0f), rotValue));
                    if (Vector2.Distance(BaseUtility.RotateVector(zero.Center, zero.Center + new Vector2(300, 0f), rotValue), npc.Center) < 32)
                    {
                        npc.ai[2] = 0;
                    }
                }
            }
            else
            {
                for (int m = npc.oldPos.Length - 1; m > 0; m--)
                {
                    npc.oldPos[m] = npc.oldPos[m - 1];
                }
                npc.oldPos[0] = npc.position;

                npc.Center = BaseUtility.RotateVector(zero.Center, zero.Center + new Vector2(300, 0f), rotValue);

                Vector2 vector2 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                float num1 = player.position.X + (player.width / 2) - vector2.X;
                float num2 = player.position.Y + (player.height / 2) - vector2.Y;
                npc.rotation = (float)Math.Atan2(num2, num1) - 1.57f;
            }
        }

        public void SwordAI(Player target)
        {
            if (npc.target < 0 || npc.target == 255 || target.dead)
            {
                npc.TargetClosest(true);
            }
            if (npc.ai[3] == 0f)
            {
                float num312 = 9f;
                Vector2 vector32 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                float num313 = target.position.X + (float)(target.width / 2) - vector32.X;
                float num314 = target.position.Y + (float)(target.height / 2) - vector32.Y;
                float num315 = (float)Math.Sqrt(num313 * num313 + num314 * num314);
                num315 = num312 / num315;
                num313 *= num315;
                num314 *= num315;
                npc.velocity.X = num313;
                npc.velocity.Y = num314;
                npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) - 1.57f;
                npc.ai[3] = 1f;
                internalAI[0] = 0f;
                npc.netUpdate = true;
                return;
            }
            if (npc.ai[3] == 1f)
            {
                if (npc.justHit)
                {
                    npc.ai[3] = 2f;
                    internalAI[0] = 0f;
                }
                npc.velocity *= 0.99f;
                internalAI[0] += 1f;
                if (internalAI[0] >= 100f)
                {
                    npc.netUpdate = true;
                    npc.ai[3] = 2f;
                    internalAI[0] = 0f;
                    npc.velocity.X = 0f;
                    npc.velocity.Y = 0f;
                    return;
                }
            }
            else
            {
                if (npc.justHit)
                {
                    npc.ai[3] = 2f;
                    internalAI[0] = 0f;
                }
                npc.velocity *= 0.96f;
                internalAI[0] += 1f;
                float num316 = internalAI[0] / 120f;
                num316 = 0.1f + num316 * 0.4f;
                npc.rotation += num316 * npc.direction;
                if (internalAI[0] >= 120f)
                {
                    npc.netUpdate = true;
                    npc.ai[3] = 0f;
                    internalAI[0] = 0f;
                    return;
                }
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/RiftShredderZ");
            BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, npc, GenericUtils.COLOR_GLOWPULSE);
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }

        public void MoveToPoint(Vector2 point)
        {
            if (moveSpeed == 0f || npc.Center == point) return; //don't move if you have no move speed
            float velMultiplier = 1f;
            Vector2 dist = point - npc.Center;
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
    }
}