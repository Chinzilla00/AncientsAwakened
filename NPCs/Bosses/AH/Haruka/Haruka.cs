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
            npc.width = 84;
            npc.height = 70;
            npc.friendly = false;
            npc.damage = 80;
            npc.defense = 50;
            npc.lifeMax = 120000;
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
                AAWorld.downedSisters = true;
            }
            if (!Main.expertMode)
            {
                npc.DropLoot(mod.ItemType("EventideAbyssium"), 5, 10);
                string[] lootTable = { "Masamune" };
                int loot = Main.rand.Next(lootTable.Length);
                npc.DropLoot(mod.ItemType(lootTable[loot]));
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

            bool throwing = false;
            bool startSpin = false;
            bool spin = false;

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
                if (!npc.collideY) //Only count up when not on the ground
                {
                    internalAI[4]++;
                }
                if (internalAI[4] >= 60 && !npc.collideY) //Start throwing
                {
                    throwing = true;
                    internalAI[4] = 0;
                    npc.netUpdate = true;
                }
                if (npc.collideY)
                {
                    throwing = false;
                    npc.netUpdate = true;
                }
                if (internalAI[2] < 7 && internalAI[1] == 4) //Throwing
                {
                    Vector2 targetCenter = player.position + new Vector2(player.width * 0.5f, player.height * 0.5f);
                    Vector2 fireTarget = npc.Center;
                    int projType = mod.ProjectileType<HarukaKunai>();
                    BaseAI.FireProjectile(targetCenter, fireTarget, projType, npc.damage, 0f, 14f);
                }
                if ((internalAI[2] < 3 || internalAI[2] > 5) && !throwing) //Not throwing yet
                {
                    internalAI[2] = 3;
                }
                if ((internalAI[2] < 6 || internalAI[2] > 8) && throwing) //Throwing
                {
                    internalAI[2] = 6;
                }
                if (npc.velocity.Y == 0) // Once she hits the ground, reset back to neutral AI
                {
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
                        if (internalAI[2] < 3 || (int)internalAI[2] > 5)
                        {
                            internalAI[2] = 3;
                        }
                    }
                }

                if (internalAI[4] == 0 && npc.alpha < 255) //Turning Invisible
                {
                    npc.alpha += 15;
                    if (npc.alpha > 255)
                    {
                        SetMovePos = true;
                        npc.alpha = 255;
                        internalAI[4] = 1;
                        npc.netUpdate = true;
                    }
                }

                if (SetMovePos) //Set the position to move to
                {
                    SetMovePos = false;
                    XPos = Main.rand.Next(2) == 0 ? 20 : -20f;
                    npc.netUpdate = true;
                }

                Vector2 point = player.Center + new Vector2(XPos, 0); //Position to move to

                if (internalAI[4] == 1 || npc.ai[3] < 240) //Move to point 
                {
                    npc.ai[3]++;
                    MoveToPoint(point);
                }
                if (Main.netMode != 1 && (Vector2.Distance(npc.Center, point) < 10f || npc.ai[3] > 240) && CanHit) //If close enough or timer exceeds 4 seconds, and isn't in a block/can see the player, turn visible
                {
                    internalAI[4] = 2;
                }
                if (internalAI[4] == 2) //Turn Visible
                {
                    npc.alpha -= 15;
                    if (npc.alpha <= 0)
                    {
                        npc.alpha = 0;
                        internalAI[2] = 19; //Set frame to start of slash animation
                        internalAI[4] = 3; //Set To Slash AI
                        npc.netUpdate = true;
                    }
                }
                if (internalAI[4] == 3)
                {
                    if (npc.ai[2] > 28) //Reset to regular AI
                    {
                        internalAI[2] = 0;
                        internalAI[3] = 0;
                        internalAI[4] = 0;
                        npc.ai[3] = 0;
                        npc.ai = new float[4];
                    }
                }
            }
            else if (internalAI[0] == AISTATE_CATCHUP) //Catching up to the player
            {
                if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                {
                    internalAI[2] = 0;
                    internalAI[3] = 0;
                    internalAI[4] = 0;
                    npc.ai = new float[4];
                }
                npc.alpha += 15; //Turn Invisible
                if (npc.alpha > 255)
                {
                    npc.alpha = 255;
                }
            }
            else
            {
                if ((internalAI[2] < 3 && internalAI[2] > 5) && !startSpin) //Set up initial jump frames
                {
                    internalAI[2] = 3;
                    startSpin = true;
                    npc.netUpdate = true;
                }
                else if (internalAI[2] > 5 && startSpin && !spin) //Initial Jump Animation
                {
                    internalAI[2] = 12; //Set frame to beginning of Spin animation
                    spin = true;
                    npc.netUpdate = true;
                }
                else if (internalAI[2] > 18 && spin) //Spin Animation
                {
                    internalAI[2] = 16; //Set frame back to first frame of spin instead of starting frame
                    internalAI[4]++;
                    if (npc.velocity.Y == 0 || internalAI[4] > 240) //Reset AI again once she hits the ground
                    {
                        internalAI[2] = 0;
                        internalAI[3] = 0;
                        internalAI[4] = 0;
                        spin = false;
                        npc.ai = new float[4];
                    }
                }
            }

            if (internalAI[0] == AISTATE_JUMP || internalAI[0] == AISTATE_THROW) //When jumping/throwing stuff at the player
            {
                BaseAI.AISlime(npc, ref npc.ai, false, 60, 20f, -12f, 22f, -15f);
            }
            else if (internalAI[0] == AISTATE_SLASH) //When Turning invisible and slashing
            {

            }
            else if (internalAI[0] == AISTATE_CATCHUP) //When Turning invisible to keep up w/ player
            {
                BaseAI.AISpaceOctopus(npc, ref npc.ai, .3f, 7, 300);
                npc.rotation = 0;
            }
            else //When spinning
            {
                if (spin)
                {
                    BaseAI.AIPounce(npc, player, 3.3f, 12 * 2f, -5.2f, 20, 6);
                }
            }
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

            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, 0, 24, npc.frame, npc.GetAlpha(dColor), false);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, 0, 24, npc.frame, Color.White, false);
            BaseDrawing.DrawAfterimage(spritebatch, glowTex, 0, npc, 0.8f, 1f, 4, true, 0f, 0f, Color.White, npc.frame, 24);
            return false;
        }
    }
}