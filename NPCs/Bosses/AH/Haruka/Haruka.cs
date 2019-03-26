using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using BaseMod;

namespace AAMod.NPCs.Bosses.AH.Haruka
{
    [AutoloadBossHead]
    public class Haruka : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Haruka Yamata");
            Main.npcFrameCount[npc.type] = 29;
        }

        public override void SetDefaults()
        {
            npc.width = 50;
            npc.height = 60;
            npc.friendly = false;
            npc.damage = 80;
            npc.defense = 50;
            npc.lifeMax = 130000;
            npc.HitSound = SoundID.NPCHit1;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.knockBackResist = 0f;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.lavaImmune = true;
            npc.boss = true;
            npc.netAlways = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/AH");
            npc.noGravity = false;
            npc.noTileCollide = false;
            bossBag = mod.ItemType("AHBag");
        }


        public int[] internalAI = new int[5];

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write(internalAI[0]); //Used as the AI selector
                writer.Write(internalAI[1]); //Used as the Frame Counter
                writer.Write(internalAI[2]); //Used for current frame
                writer.Write(internalAI[3]); //Used to count down to AI change
                writer.Write(internalAI[4]); //Used as an AI Timer
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadInt();
                internalAI[1] = reader.ReadInt();
                internalAI[2] = reader.ReadInt();
                internalAI[3] = reader.ReadInt();
                internalAI[4] = reader.ReadInt();
            }
        }

        public static int AISTATE_JUMP = 0, AISTATE_THROW = 1, AISTATE_SLASH = 2, AISTATE_SPIN = 3, AISTATE_CATCHUP = 4;

        public override void HitEffect(int hitDirection, double damage)
        {
            Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, mod.DustType<Dusts.AcidDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
        }
        public override void NPCLoot()
        {
            int Ashe = NPC.CountNPCS(mod.NPCType("Ashe"));
            if (Ashe == 0)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<AHDeath>());
                if (Main.expertMode)
                {
                    npc.DropBossBags();
                }
            }
            if (!Main.expertMode)
            {

                string[] lootTableH = { "HarukaKunai", "Masamune", "MizuArashi" };
                int lootH = Main.rand.Next(lootTableH.Length);
                npc.DropLoot(mod.ItemType(lootTableH[lootH]));
            }
            Main.NewText("Rgh..! Ow...", new Color(72, 78, 117));
            npc.value = 0f;
            npc.boss = false;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.6f);
        }

        public bool SetMovePos = false;
        public float XPos = 20f;

        public override void AI()
        {
            Player player = Main.player[npc.target];

            float dist = npc.Distance(player.Center);

            bool CanHit = Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height);

            npc.frame.Y = 70 * internalAI[2]; //IAI[2] Is the current frame

            if (internalAI[0] == AISTATE_JUMP || internalAI[0] == AISTATE_THROW) //When jumping/throwing stuff at the player
            {
                BaseAI.AISlime(npc, ref npc.ai, false, 60, 20f, -12f, 22f, -15f);
            }
            else if (internalAI[0] == AISTATE_SLASH) //When Turning invisible and slashing
            {

            }
            else if (internalAI[0] == AISTATE_CATCHUP) //When Turning invisible to keep up w/ player
            {

            }
            else //When spinning
            {
                BaseAI.AIPounce(npc, player, 3.3f, 12 * 2f, -5.2f, 70, 70);
            }

            if (player.Center.X > npc.Center.X) //If NPC's X position is higher than the player's
            {
                npc.spriteDirection = -1;
                npc.direction = -1;
            }
            else //If NPC's X position is lower than the player's
            {
                npc.spriteDirection = 1;
                npc.direction = 1;
            }

            internalAI[1]++;

            if (internalAI[1] >= 8) //IAI[1] is the frame counter
            {
                internalAI[1] = 0;
                internalAI[2]++;
            }

            if (!Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
            {
                internalAI[0] = AISTATE_CATCHUP;
            }

            if (internalAI[0] != AISTATE_SLASH || internalAI[0] != AISTATE_CATCHUP) //Turn visible when in AI states that don't use invisibility
            {
                if (npc.alpha > 0)
                {
                    npc.alpha -= 15;
                }
                if (npc.alpha < 0)
                {
                    npc.alpha = 0;
                }
            }

            if (internalAI[0] == AISTATE_JUMP) //Neutral Jump Animation
            {
                if (Main.netMode != 1)
                {
                    internalAI[3]++;
                    if (internalAI[3] >= 300)
                    {
                        internalAI[3] = 0;
                        internalAI[4] = 0;
                        internalAI[0] = Main.rand.Next(4);
                        npc.ai = new float[4];
                        npc.netUpdate = true;
                    }
                }


                if (npc.velocity.Y == 0) //Idle
                {
                    if (internalAI[2] > 2)
                    {
                        internalAI[2] = 0;
                    }
                }
                else
                {
                    internalAI[4]++;
                    if (dist < 80 || internalAI[4] > 180) // Slash
                    {

                        internalAI[4] = 0;
                        if (internalAI[2] < 10 && internalAI[1] == 4 && dist > 80) //Slash Projectile if she isn't close to the player
                        {
                            Vector2 targetCenter = player.position + new Vector2(player.width * 0.5f, player.height * 0.5f);
                            Vector2 fireTarget = npc.Center;
                            int projType = mod.ProjectileType<HarukaProj>();
                            BaseAI.FireProjectile(targetCenter, fireTarget, projType, npc.damage, 0f, 14f);
                        }

                        if (internalAI[2] < 9 || internalAI[2] > 11)
                        {
                            internalAI[2] = 9;
                        }
                    }
                    else
                    {
                        if (internalAI[2] < 3 || internalAI[2] > 5)
                        {
                            internalAI[2] = 3;
                        }
                    }
                }
            }
            else if (internalAI[0] == AISTATE_THROW)
            {
                if (!npc.collideY)
                {
                    internalAI[4] = 0;
                    if (internalAI[2] < 10 && internalAI[1] == 4) 
                    {
                        Vector2 targetCenter = player.position + new Vector2(player.width * 0.5f, player.height * 0.5f);
                        Vector2 fireTarget = npc.Center;
                        int projType = mod.ProjectileType<HarukaKunai>();
                        BaseAI.FireProjectile(targetCenter, fireTarget, projType, npc.damage, 0f, 14f);
                    }

                    if (internalAI[2] < 9 || internalAI[2] > 11)
                    {
                        internalAI[2] = 9;
                    }
                }
                else
                {
                    if (internalAI[2] < 3 || internalAI[2] > 5)
                    {
                        internalAI[2] = 3;
                    }
                }
                if (npc.velocity.Y == 0) // Once she hits the ground, reset back to neutral AI
                {
                    internalAI[0] = 0;
                    internalAI[2] = 0;
                    internalAI[3] = 0;
                    internalAI[4] = 0;
                }
            }
            else if (internalAI[0] == AISTATE_SLASH)
            {
                if (internalAI[4] != 3) //When Not Slashing, use default frames
                {
                    if (npc.velocity.Y == 0)
                    {
                        if (internalAI[2] > 2)
                        {
                            internalAI[2] = 0;
                        }
                    }
                    else
                    {
                        if (internalAI[2] < 3 || internalAI[2] > 5)
                        {
                            internalAI[2] = 3;
                        }
                    }
                }

                if (internalAI[4] == 0 && npc.alpha < 255) //Turning Invisible
                {
                    npc.alpha += 10;
                    if (npc.alpha > 255)
                    {
                        npc.Center = player.Center - (Main.rand.Next(2) == 0 ? new Vector2(20, 0) : new Vector2(-20, 0));
                        npc.alpha = 255;
                        internalAI[4] = 1;
                        npc.netUpdate = true;
                    }
                }
                if (internalAI[4] == 1) //Turn Visible
                {
                    npc.alpha -= 10;
                    if (npc.alpha <= 0)
                    {
                        npc.alpha = 0;
                        internalAI[2] = 19; //Set frame to start of slash animation
                        internalAI[4] = 2; //Set To Slash AI
                        npc.netUpdate = true;
                    }
                }
                if (internalAI[4] == 2)
                {
                    if (npc.ai[2] > 28) //Reset to regular AI
                    {
                        internalAI[0] = 0;
                        internalAI[2] = 0;
                        internalAI[3] = 0;
                        internalAI[4] = 0;
                        npc.ai = new float[4];
                    }
                }
            }
            else if (internalAI[0] == AISTATE_CATCHUP) //Catching up to the player
            {
                if (npc.velocity.Y == 0)
                {
                    if (internalAI[2] > 2)
                    {
                        internalAI[2] = 0;
                    }
                }
                else
                {
                    if (internalAI[2] < 3 || internalAI[2] > 5)
                    {
                        internalAI[2] = 3;
                    }
                }
                if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                {
                    internalAI[0] = 0;
                    internalAI[2] = 0;
                    internalAI[3] = 0;
                    internalAI[4] = 0;
                    npc.ai = new float[4];
                }
                npc.alpha += 15; //Turn Invisible
                if (npc.alpha >= 255)
                {
                    npc.alpha = 255;
                    int Xint = Main.rand.Next(-200, 200);
                    int Yint = Main.rand.Next(0, 200);
                    if ((Xint < -100 || Xint > 100) && Yint > 90)
                    {
                        Vector2 tele = new Vector2((player.Center.X + Xint), (player.Center.Y + Yint));
                        npc.Center = tele;
                    }
                }
            }
            else
            {
                if (internalAI[2] > 18 && internalAI[2] < 12) //Spin Animation
                {
                    if (internalAI[4] < 1)
                    {
                        internalAI[2] = 12;
                    }
                    internalAI[4]++;
                    if (internalAI[2] > 18)
                    {
                        internalAI[2] = 15;
                    }
                    if (internalAI[4] > 240) //Reset AI again once timer runs out
                    {
                        internalAI[0] = 0;
                        internalAI[2] = 0;
                        internalAI[3] = 0;
                        internalAI[4] = 0;
                        npc.ai = new float[4];
                    }
                }
            }

            
            npc.noGravity = false;
        }

        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 13f;
            if (moveSpeed == 0f || npc.Center == point) return;
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


        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = 0;
        }

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/Haruka_Glow");

            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, new Vector2(npc.position.X, npc.position.Y + 10), npc.width, npc.height, npc.scale, npc.rotation, 0, 24, npc.frame, npc.GetAlpha(dColor), false);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, new Vector2 (npc.position.X, npc.position.Y + 10), npc.width, npc.height, npc.scale, npc.rotation, 0, 24, npc.frame, Color.White, false);
            BaseDrawing.DrawAfterimage(spritebatch, glowTex, 0, npc, 0.8f, 1f, 4, true, 0f, 0f, Color.White, npc.frame, 24);
            return false;
        }
    }
}