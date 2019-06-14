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
    public class RealityCannon2 : ModNPC
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
                int ind = NPC.NewNPC((int)(npc.position.X + (double)(npc.width / 2)), (int)npc.position.Y + (npc.height / 2), mod.NPCType("TeslaHand"), npc.whoAmI, -2f, npc.ai[1], 0f, 0f, byte.MaxValue);
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

        public override void AI()
        {
            npc.noGravity = true;

            if (body == -1)
            {
                int npcID = BaseAI.GetNPC(npc.Center, mod.NPCType("Zero2"), 400f, null);
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

            int aiTimerFire = Main.expertMode ? 150 : 200;

            for (int m = npc.oldPos.Length - 1; m > 0; m--)
            {
                npc.oldPos[m] = npc.oldPos[m - 1];
            }
            npc.oldPos[0] = npc.position;

            npc.Center = BaseUtility.RotateVector(zero.Center, zero.Center + new Vector2(300, 0f), rotValue);

            if (Main.netMode != 1) { npc.ai[2]++; }

            if (npc.ai[2] == aiTimerFire)
            {
                npc.ai[2] = 0;
                if (Collision.CanHit(npc.position, npc.width, npc.height, player.Center, player.width, player.height))
                {
                    float spread = 45f * 0.0174f;
                    Vector2 dir = Vector2.Normalize(player.Center - npc.Center);
                    dir *= 14f;
                    float baseSpeed = (float)Math.Sqrt((dir.X * dir.X) + (dir.Y * dir.Y));
                    double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
                    double deltaAngle = spread / 6f;
                    for (int i = 0; i < (Main.expertMode ? 3 : 4); i++)
                    {
                        double offsetAngle = startAngle + (deltaAngle * i);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), mod.ProjType("RealityLaser"), (int)(npc.damage / 1.5f), 5, Main.myPlayer);
                    }
                }
            }

            Vector2 vector2 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
            float num1 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector2.X;
            float num2 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector2.Y;
            float NewRotation = (float)Math.Atan2(num2, num1) - 1.57f;
            npc.rotation = MathHelper.Lerp(npc.rotation, NewRotation, 1f / 30f);
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/TaserZ");
            BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, npc, GenericUtils.COLOR_GLOWPULSE);
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }
    }
}