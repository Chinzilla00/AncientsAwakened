using System;
using System.IO;
using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Zero
{
    public class OmegaVolley : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Omega Volley");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 70;
            npc.damage = 25;
            npc.defense = 40;
            npc.lifeMax = 37500;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCHit4;
            npc.noGravity = true;
            animationType = NPCID.PrimeVice;
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

        public float[] shootAI = new float[4];
        public float[] internalAI = new float[1];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write((float)internalAI[0]);
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

        public override void HitEffect(int hitDirection, double damage)
        {
            bool flag = (npc.life <= 0 || (!npc.active && NPC.AnyNPCs(mod.NPCType<Zero>())));
            if (flag && Main.netMode != 1)
            {
                int ind = NPC.NewNPC((int)(npc.position.X + (double)(npc.width / 2)), (int)npc.position.Y + (npc.height / 2), mod.NPCType("TeslaHand"), npc.whoAmI, 2f, npc.ai[1], 0f, 0f, byte.MaxValue);
                Main.npc[ind].life = 1;
                Main.npc[ind].rotation = npc.rotation;
                Main.npc[ind].velocity = npc.velocity;
                Main.npc[ind].netUpdate = true;
                Main.npc[(int)npc.ai[1]].ai[3]++;
                Main.npc[(int)npc.ai[1]].netUpdate = true;
            }
        }

        public override void AI()
        {
            npc.spriteDirection = -(int)npc.ai[0];
            if (!Main.npc[(int)npc.ai[1]].active)
            {
                if (npc.ai[2] > 50.0 || Main.netMode != 2)
                {
                    npc.life = -1;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                }
            }
            if (Main.player[npc.target].GetModPlayer<AAPlayer>().ZoneVoid == false)
            {
                npc.defense = 999999999;
            }
            else
            {
                npc.defense = 70;
            }
            if (Main.npc[(int)npc.ai[1]].ai[1] == 3.0 && npc.timeLeft > 10)
            {
                npc.timeLeft = 10;
            }
            if (Main.npc[(int)npc.ai[1]].ai[1] != 0f)
            {
                internalAI[0] += 3f;
                if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y)
                {
                    if (npc.velocity.Y > 0.0)
                        npc.velocity.Y *= 0.96f;
                    npc.velocity.Y -= 0.07f;
                    if (npc.velocity.Y > 6.0)
                        npc.velocity.Y = 6f;
                }
                else if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y)
                {
                    if (npc.velocity.Y < 0.0)
                        npc.velocity.Y *= 0.96f;
                    npc.velocity.Y += 0.07f;
                    if (npc.velocity.Y < -6.0)
                        npc.velocity.Y = -6f;
                }
                if (npc.position.X + (double)(npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) - (120.0 * npc.ai[0]))
                {
                    if (npc.velocity.X > 0.0)
                        npc.velocity.X *= 0.96f;
                    npc.velocity.X -= 0.1f;
                    if (npc.velocity.X > 8.0)
                        npc.velocity.X = 8f;
                }
                if (npc.position.X + (double)(npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) - (120.0 * npc.ai[0]))
                {
                    if (npc.velocity.X < 0.0)
                        npc.velocity.X *= 0.96f;
                    npc.velocity.X += 0.1f;
                    if (npc.velocity.X < -8.0)
                        npc.velocity.X = -8f;
                }
            }
            else
            {
                ++npc.ai[3];
                if (npc.ai[3] >= 800.0)
                {
                    ++npc.ai[2];
                    npc.ai[3] = 0.0f;
                    npc.netUpdate = true;
                }
                if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y)
                {
                    if (npc.velocity.Y > 0.0)
                        npc.velocity.Y *= 0.96f;
                    npc.velocity.Y -= 0.1f;
                    if (npc.velocity.Y > 3.0)
                        npc.velocity.Y = 3f;
                }
                else if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y)
                {
                    if (npc.velocity.Y < 0.0)
                        npc.velocity.Y *= 0.96f;
                    npc.velocity.Y += 0.1f;
                    if (npc.velocity.Y < -3.0)
                        npc.velocity.Y = -3f;
                }
                if (npc.position.X + (double)(npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) - (180.0 * npc.ai[0]))
                {
                    if (npc.velocity.X > 0.0)
                        npc.velocity.X *= 0.96f;
                    npc.velocity.X -= 0.14f;
                    if (npc.velocity.X > 8.0)
                        npc.velocity.X = 8f;
                }
                if (npc.position.X + (double)(npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) - (180.0 * npc.ai[0]))
                {
                    if (npc.velocity.X < 0.0)
                        npc.velocity.X *= 0.96f;
                    npc.velocity.X += 0.14f;
                    if (npc.velocity.X < -8.0)
                        npc.velocity.X = -8f;
                }
            }
            npc.TargetClosest(true);
            Vector2 vector2 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
            float num1 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector2.X;
            float num2 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector2.Y;
            npc.rotation = (float)Math.Atan2(num2, num1) - 1.57f;
            ++internalAI[0];
            if (internalAI[0] >= 200.0)
            {
                BaseAI.ShootPeriodic(npc, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height, mod.ProjectileType("OmegaBullet"), ref shootAI[0], 5, (int)(npc.damage * (Main.expertMode ? 0.25f : 0.5f)), 24f, true, new Vector2(20f, 15f));
            }
            if (internalAI[0] > 300)
            {
                internalAI[0] = 0.0f;
            }
        }


        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Vector2 vector7 = new Vector2(npc.position.X + ((float)npc.width * 0.5f) - (5f * npc.ai[0]), npc.position.Y + 20f);
            for (int l = 0; l < 2; l++)
            {
                float num21 = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - vector7.X;
                float num22 = Main.npc[(int)npc.ai[1]].position.Y + (float)(Main.npc[(int)npc.ai[1]].height / 2) - vector7.Y;
                float num23;
                if (l == 0)
                {
                    num21 -= 200f * npc.ai[0];
                    num22 += 130f;
                    num23 = (float)Math.Sqrt((double)((num21 * num21) + (num22 * num22)));
                    num23 = 92f / num23;
                    vector7.X += num21 * num23;
                    vector7.Y += num22 * num23;
                }
                else
                {
                    num21 -= 50f * npc.ai[0];
                    num22 += 80f;
                    num23 = (float)Math.Sqrt((double)((num21 * num21) + (num22 * num22)));
                    num23 = 60f / num23;
                    vector7.X += num21 * num23;
                    vector7.Y += num22 * num23;
                }
                float rotation7 = (float)Math.Atan2((double)num22, (double)num21) - 1.57f;
                Color color7 = Lighting.GetColor((int)vector7.X / 16, (int)(vector7.Y / 16f));
                Texture2D Arm = mod.GetTexture("NPCs/Bosses/Zero/ZeroArm");
                Texture2D ArmGlow = mod.GetTexture("Glowmasks/ZeroArm_Glow");
                Main.spriteBatch.Draw(Arm, new Vector2(vector7.X - Main.screenPosition.X, vector7.Y - Main.screenPosition.Y), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, Arm.Width, Arm.Height)), color7, rotation7, new Vector2((float)Main.boneArmTexture.Width * 0.5f, (float)Main.boneArmTexture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(ArmGlow, new Vector2(vector7.X - Main.screenPosition.X, vector7.Y - Main.screenPosition.Y), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, Arm.Width, Arm.Height)), GetGlowAlpha(), rotation7, new Vector2((float)Main.boneArmTexture.Width * 0.5f, (float)Main.boneArmTexture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
                if (l == 0)
                {
                    vector7.X += num21 * num23 / 2f;
                    vector7.Y += num22 * num23 / 2f;
                }
                else if (Main.rand.Next(2) == 0)
                {

                    vector7.X += (num21 * num23) - 16f;
                    vector7.Y += (num22 * num23) - 6f;
                    int num24 = Dust.NewDust(new Vector2(vector7.X, vector7.Y), 30, 10, mod.DustType<Dusts.VoidDust>(), num21 * 0.02f, num22 * 0.02f, 0, default(Color), 2.5f);
                    Main.dust[num24].noGravity = false;
                }
            }
            return base.PreDraw(spriteBatch, drawColor);
        }

        public Color GetGlowAlpha()
        {
            return AAColor.ZeroShield * (Main.mouseTextColor / 255f);
        }

        public static Texture2D glowTex = null;
        public float auraPercent = 0f;
        public bool auraDirection = true;



        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (glowTex == null)
            {
                glowTex = mod.GetTexture("Glowmasks/OmegaVolley_Glow");
            }
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            BaseMod.BaseDrawing.DrawAura(spriteBatch, glowTex, 0, npc, auraPercent, 1f, 0f, 0f, GetGlowAlpha());
            BaseMod.BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, npc, GetGlowAlpha());
        }

    }
}