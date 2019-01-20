using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.SoC.Bosses
{
    public class DeitySkull : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rhan-Tegoth");
        }
        public override void SetDefaults()
        {
            npc.width = 80;
            npc.height = 102;
            npc.aiStyle = -1;
            npc.damage = 100;
            npc.defense = 80;
            npc.lifeMax = 150000;
            npc.knockBackResist = 0.0f;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.npcSlots = 6f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.boss = true;
            npc.netAlways = true;
            npc.buffImmune[20] = true;
            npc.buffImmune[24] = true;
            npc.buffImmune[39] = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/SoC");
            for (int m = 0; m < npc.buffImmune.Length; m++) npc.buffImmune[m] = true;
            npc.lavaImmune = true;
            music = MusicID.Boss2;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((short)npc.localAI[0]);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            npc.localAI[0] = reader.ReadInt16();
        }

        public override void AI()
        {
            npc.damage = npc.defDamage;
            npc.defense = npc.defDefense;
            bool expert = Main.expertMode;

            
            if (npc.ai[3] != 6)
            {
                npc.dontTakeDamage = true;
            }
            else
            {
                npc.life = 0;
            }
            if(npc.ai[0] < 300f)npc.ai[0]++;
            if (npc.ai[0] == 300.0 && Main.netMode != 1)
            {
                npc.TargetClosest(true);
                npc.ai[0]++;
                int index1 = NPC.NewNPC((int)(npc.position.X + (double)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, mod.NPCType("DeitySkull_Hand"), npc.whoAmI, 0.0f, 0.0f, 0.0f, 0.0f, byte.MaxValue);
                Main.npc[index1].ai[0] = -1f;
                Main.npc[index1].ai[1] = npc.whoAmI;
                Main.npc[index1].target = npc.target;
                Main.npc[index1].netUpdate = true;
                int index2 = NPC.NewNPC((int)(npc.position.X + (double)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, mod.NPCType("DeitySkull_Hand1"), npc.whoAmI, 0.0f, 0.0f, 0.0f, 0.0f, byte.MaxValue);
                Main.npc[index2].ai[0] = -1f;
                Main.npc[index2].ai[1] = npc.whoAmI;
                Main.npc[index2].target = npc.target;
                Main.npc[index2].ai[3] = 150f;
                Main.npc[index2].netUpdate = true;
                int index3 = NPC.NewNPC((int)(npc.position.X + (double)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, mod.NPCType("DeitySkull_Han2"), npc.whoAmI, 0.0f, 0.0f, 0.0f, 0.0f, byte.MaxValue);
                Main.npc[index3].ai[0] = -1f;
                Main.npc[index3].ai[1] = npc.whoAmI;
                Main.npc[index3].target = npc.target;
                Main.npc[index3].ai[3] = 150f;
                Main.npc[index3].netUpdate = true;
                int index4 = NPC.NewNPC((int)(npc.position.X + (double)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, mod.NPCType("DeitySkull_Hand3"), npc.whoAmI, 0.0f, 0.0f, 0.0f, 0.0f, byte.MaxValue);
                Main.npc[index4].ai[0] = 1f;
                Main.npc[index4].ai[1] = npc.whoAmI;
                Main.npc[index4].target = npc.target;
                Main.npc[index4].netUpdate = true;
                Main.npc[index4].ai[3] = 150f;
                int index5 = NPC.NewNPC((int)(npc.position.X + (double)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, mod.NPCType("DeitySkull_Hand4"), npc.whoAmI, 0.0f, 0.0f, 0.0f, 0.0f, byte.MaxValue);
                Main.npc[index5].ai[0] = 1f;
                Main.npc[index5].ai[1] = npc.whoAmI;
                Main.npc[index5].target = npc.target;
                Main.npc[index5].netUpdate = true;
                Main.npc[index5].ai[3] = 150f;
                int index6 = NPC.NewNPC((int)(npc.position.X + (double)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, mod.NPCType("DeitySkull_Hand5"), npc.whoAmI, 0.0f, 0.0f, 0.0f, 0.0f, byte.MaxValue);
                Main.npc[index6].ai[0] = 1f;
                Main.npc[index6].ai[1] = npc.whoAmI;
                Main.npc[index6].ai[3] = 150f;
                Main.npc[index6].target = npc.target;
                Main.npc[index6].netUpdate = true;


            }
            if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000.0 || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000.0)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000.0 || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000.0)
                    npc.ai[1] = 3f;
            }
            if (((int)(Main.player[npc.target].position.Y/16) <= Main.maxTilesY - 190 -(int)(Main.maxTilesY/5)) && npc.ai[1] != 3.0 && npc.ai[1] != 2.0)
            {
                npc.ai[1] = 2f;
                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
            }
            if (npc.ai[1] == 0.0)
            {
                ++npc.ai[2];
                if (npc.ai[2] >= 600.0)
                {
                    npc.ai[2] = 0.0f;
                    npc.ai[1] = 1f;
                    npc.TargetClosest(true);
                    npc.netUpdate = true;
                }
                npc.rotation = npc.velocity.X / 15f;
                if (npc.position.Y > Main.player[npc.target].position.Y - 200.0)
                {
                    if (npc.velocity.Y > 0.0)
                        npc.velocity.Y *= 0.98f;
                    npc.velocity.Y -= 0.1f;
                    if (npc.velocity.Y > 2.0)
                        npc.velocity.Y = 2f;
                }
                else if (npc.position.Y < Main.player[npc.target].position.Y - 500.0)
                {
                    if (npc.velocity.Y < 0.0)
                        npc.velocity.Y *= 0.98f;
                    npc.velocity.Y += 0.1f;
                    if (npc.velocity.Y < -2.0)
                        npc.velocity.Y = -2f;
                }
                if (npc.position.X + (double)(npc.width / 2) > Main.player[npc.target].position.X + (double)(Main.player[npc.target].width / 2) + 100.0)
                {
                    if (npc.velocity.X > 0.0)
                        npc.velocity.X *= 0.98f;
                    npc.velocity.X -= 0.1f;
                    if (npc.velocity.X > 8.0)
                        npc.velocity.X = 8f;
                }


                if (npc.position.X + (double)(npc.width / 2) >= Main.player[npc.target].position.X + (double)(Main.player[npc.target].width / 2) - 100.0)
                    return;
                if (npc.velocity.X < 0.0)
                    npc.velocity.X *= 0.98f;
                npc.velocity.X += 0.1f;
                if (npc.velocity.X >= -8.0)
                    return;
                npc.velocity.X = -8f;



                if (Main.netMode == 1 || !expert || npc.ai[3] != 6)
                    return;
                ++npc.localAI[0];
                if (npc.localAI[0] <= 150.0)
                    return;
                npc.localAI[0] = 0.0f;
                Vector2 vector2_6 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float num41 = Main.player[npc.target].position.X + Main.player[npc.target].width / 2 - vector2_6.X;
                float num42 = Main.player[npc.target].position.Y + Main.player[npc.target].height / 2 - vector2_6.Y;
                float num43 = (float)Math.Sqrt(num41 * (double)num41 + num42 * (double)num42);
                float num4 = 8f;
                int Damage = 15;
                int Type = 258;
                float num5 = num4 / num43;
                float num6 = num41 * num5;
                float num7 = num42 * num5;
                float SpeedX = num6 + Main.rand.Next(-5, 6) * 0.05f;
                float SpeedY = num7 + Main.rand.Next(-5, 6) * 0.05f;
                vector2_6.X += SpeedX * 6f;
                vector2_6.Y += SpeedY * 6f;
                Projectile.NewProjectile(vector2_6.X, vector2_6.Y, SpeedX, SpeedY, Type, Damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);
            }
            else if (npc.ai[1] == 1.0)
            {
                npc.defense *= 2;
                npc.damage *= 2;
                ++npc.ai[2];
                if (npc.ai[2] == 2.0)
                    Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                if (npc.ai[2] >= 400.0)
                {
                    npc.ai[2] = 0.0f;
                    npc.ai[1] = 0.0f;
                }
                npc.rotation += npc.direction * 0.3f;
                Vector2 vector2 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float num1 = Main.player[npc.target].position.X + Main.player[npc.target].width / 2 - vector2.X;
                float num2 = Main.player[npc.target].position.Y + Main.player[npc.target].height / 2 - vector2.Y;
                float num3 = 2f / (float)Math.Sqrt(num1 * (double)num1 + num2 * (double)num2);
                npc.velocity.X = num1 * num3;
                npc.velocity.Y = num2 * num3;

            }
            else if (npc.ai[1] == 2.0)
            {
                npc.damage = 1000;
                npc.defense = 9999;
                npc.rotation += npc.direction * 0.3f;
                Vector2 vector2 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float num1 = Main.player[npc.target].position.X + Main.player[npc.target].width / 2 - vector2.X;
                float num2 = Main.player[npc.target].position.Y + Main.player[npc.target].height / 2 - vector2.Y;
                float num3 = (float)Math.Sqrt(num1 * (double)num1 + num2 * (double)num2);
                float num4 = 10f + num3 / 100f;
                if (num4 < 8.0)
                    num4 = 8f;
                if (num4 > 32.0)
                    num4 = 32f;
                float num5 = num4 / num3;
                npc.velocity.X = num1 * num5;
                npc.velocity.Y = num2 * num5;
            }
            else
            {
                if (npc.ai[1] != 3.0)
                    return;
                npc.velocity.Y += 0.1f;
                if (npc.velocity.Y < 0.0)
                    npc.velocity.Y *= 0.95f;
                npc.velocity.X *= 0.95f;
                if (npc.timeLeft <= 500)
                    return;
                npc.timeLeft = 500;
            }
        }
    }
}