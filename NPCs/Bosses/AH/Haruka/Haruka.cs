using Terraria;
using System;
using Terraria.ID;
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
            Main.npcFrameCount[npc.type] = 27;
        }

        public override void SetDefaults()
        {
            npc.width = 50;
            npc.height = 60;
            npc.friendly = false;
            npc.damage = 80;
            npc.defense = 50;
            npc.lifeMax = 90000;
            npc.HitSound = SoundID.NPCHit1;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.knockBackResist = 0f;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.lavaImmune = true;
            npc.boss = true;
            npc.netAlways = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/AH");
            npc.noGravity = true;
            bossBag = mod.ItemType("AHBag");
        }


        public int[] internalAI = new int[6];

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
                writer.Write(internalAI[5]);
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
                internalAI[5] = reader.ReadInt();
            }
        }


        public override void HitEffect(int hitDirection, double damage)
        {
            Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, mod.DustType<Dusts.AcidDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
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

                string[] lootTableH = { "HarukaKunai", "Masamune", "MizuArashi", "HarukaBox" };
                int lootH = Main.rand.Next(lootTableH.Length);
                npc.DropLoot(mod.ItemType(lootTableH[lootH]));
            }
            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<HarukaVanish>());
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

        public float pos = 250f;

        public static int AISTATE_PROJ = 0, AISTATE_SLASH = 1, AISTATE_SPIN = 2, AISTATE_IDLE = 3;

        public int ProjectileShoot = -1;
        public int repeat = 10;
        public bool isSlashing = false;
        
        public Vector2 MovePoint;
        public bool SelectPoint = false;

        public bool Invisible = false;

        public override void AI()
        {
            Player player = Main.player[npc.target];

            npc.frame.Y = 74 * internalAI[2];

            Vector2 wantedVelocity = player.Center - new Vector2(pos, 0);

            if (player.dead || !player.active || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
            {
                npc.TargetClosest(false);
                if (player.dead || !player.active || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
                {
                    if (internalAI[2] > 3)
                    {
                        internalAI[1] = 0;
                        internalAI[2] = 0;
                    }
                    npc.alpha += 4;
                    if (npc.alpha > 255)
                    {
                        npc.active = false;
                    }
                    return;
                }
            }
            if (Invisible)
            {
                if (npc.alpha < 255)
                {
                    npc.alpha += 5;
                }
                else
                {
                    npc.chaseable = false;
                    npc.alpha = 255;
                }
            }
            else
            {
                if (npc.alpha > 0)
                {
                    npc.alpha -= 8;
                }
                else
                {
                    npc.chaseable = true;
                    npc.alpha = 0;
                }
            }

            internalAI[1]++;

            internalAI[5]++;

            int InvisTimer1 = 1000;

            int InvisTimer2 = 1300;

            if (npc.life < npc.lifeMax * .66f)
            {
                InvisTimer1 = 800;

                InvisTimer2 = 1100;
            }
            if (npc.life < npc.lifeMax * .33f)
            {
                InvisTimer1 = 600;

                InvisTimer2 = 900;
            }
            if (internalAI[5] > InvisTimer1)
            {
                if (!Invisible)
                {
                    Invisible = true;
                    npc.netUpdate = true;
                }
            }
            if (internalAI[5] > InvisTimer2)
            {
                Invisible = false;
                internalAI[5] = 0;
                npc.netUpdate = true;
            }

            

            if (ProjectileShoot == 0 || internalAI[0] == AISTATE_SLASH)
            {
                if (internalAI[1] > 4)
                {
                    internalAI[1] = 0;
                    internalAI[2]++;
                }
            }
            else
            {
                if (internalAI[1] > 8)
                {
                    internalAI[1] = 0;
                    internalAI[2]++;
                }
            }


            if (internalAI[0] == AISTATE_IDLE)
            {
                if (Main.netMode != 1) 
                {
                    internalAI[3]++;
                    if (internalAI[3] >= 90)
                    {
                        internalAI[3] = 0;
                        internalAI[0] = Main.rand.Next(3);
                        npc.ai = new float[4];
                        npc.netUpdate = true;
                    }
                }

                if (internalAI[2] > 3)
                {
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                }
            }
            else if (internalAI[0] == AISTATE_PROJ)
            {
                if (ProjectileShoot == -1)
                {
                    ProjectileShoot = Main.rand.Next(2);
                    npc.netUpdate = true;
                }
                if (ProjectileShoot == 0)
                {
                    if (internalAI[2] == 5 && internalAI[1] == 3)
                    {
                        repeat -= 1;
                        int projType = mod.ProjectileType<HarukaKunai>();
                        float spread = 45f * 0.0174f;
                        Vector2 dir = Vector2.Normalize(player.Center - npc.Center);
                        dir *= 14f;
                        float baseSpeed = (float)Math.Sqrt((dir.X * dir.X) + (dir.Y * dir.Y));
                        double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
                        double deltaAngle = spread / 6f;
                        for (int i = 0; i < 3; i++)
                        {
                            double offsetAngle = startAngle + (deltaAngle * i);
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), projType, (int)(npc.damage / 1.5f), 5, Main.myPlayer);
                        }
                        npc.netUpdate = true;
                    }
                    if (internalAI[2] < 4 || internalAI[2] > 6)
                    {
                        internalAI[1] = 0;
                        internalAI[2] = 4;
                    }
                    if (repeat <= 0)
                    {
                        internalAI[0] = 3;
                        internalAI[1] = 0;
                        internalAI[2] = 0;
                        internalAI[3] = 0;
                        ProjectileShoot -= 1;
                        repeat = 12;
                        npc.ai = new float[4];
                        npc.netUpdate = true;
                    }
                }
                else if (ProjectileShoot == 1)
                {
                    internalAI[3]++;
                    if (internalAI[3] == 100 || internalAI[3] == 200 || internalAI[3] == 299)
                    {
                        isSlashing = true;
                    }
                    if (isSlashing)
                    {
                        if (internalAI[2] < 7 || internalAI[2] > 9) //Sets to frame 16
                        {
                            internalAI[1] = 0;
                            internalAI[2] = 7;
                        }
                    }
                    else
                    {
                        if (internalAI[2] > 3)
                        {
                            internalAI[1] = 0;
                            internalAI[2] = 0;
                        }
                    }
                    if (internalAI[2] == 8 && internalAI[1] == 4)
                    {
                        Vector2 targetCenter = player.position + new Vector2(player.width * 0.5f, player.height * 0.5f);
                        Vector2 fireTarget = npc.Center;
                        int projType = mod.ProjectileType<HarukaProj>();
                        BaseAI.FireProjectile(targetCenter, fireTarget, projType, (int)(npc.damage * 1.3f), 0f, 18f);
                    }
                    if (isSlashing && internalAI[2] > 9)
                    {
                        isSlashing = false;
                        npc.netUpdate = true;
                    }
                    if (internalAI[3] > 300)
                    {
                        internalAI[0] = 3;
                        internalAI[1] = 0;
                        internalAI[2] = 0;
                        internalAI[3] = 0;
                        ProjectileShoot -= 1;
                        npc.ai = new float[4];
                        npc.netUpdate = true;
                    }
                }
            }
            else if (internalAI[0] == AISTATE_SLASH)
            {
                internalAI[3]++;

                MoveToPoint(player.Center);

                if (internalAI[2] < 17)
                {
                    internalAI[1] = 0;
                    internalAI[2] = 17;
                }
                if (internalAI[2] > 26)
                {
                    internalAI[1] = 0;
                    internalAI[2] = 17;
                    internalAI[4] += 1;
                }
                if (internalAI[4] > 5)
                {
                    internalAI[0] = 3;
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                    internalAI[3] = 0;
                    internalAI[4] = 0;
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
            }
            else if (internalAI[0] == AISTATE_SPIN)
            {
                if (internalAI[2] < 10)
                {
                    internalAI[1] = 0;
                    internalAI[2] = 10;
                }
                if (internalAI[2] > 16)
                {
                    internalAI[1] = 0;
                    internalAI[2] = 13;
                }

                internalAI[4]++;

                if (SelectPoint)
                {
                    float Point = 500 * -npc.direction;
                    MovePoint = player.Center + new Vector2(Point, 0);
                    SelectPoint = false;
                    npc.netUpdate = true;
                }

                MoveToPoint(MovePoint);

                if (Main.netMode != 1 && (Vector2.Distance(npc.Center, player.Center) > 300f || internalAI[4] > 120))
                {
                    internalAI[0] = 3;
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                    internalAI[3] = 0;
                    internalAI[4] = 0;
                    pos *= -1f;
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
            }
            else
            {
                internalAI[0] = 3;
                internalAI[1] = 0;
                internalAI[2] = 0;
                internalAI[3] = 0;
                npc.ai = new float[4];
                npc.netUpdate = true;
            }

            if (internalAI[0] == AISTATE_SLASH || internalAI[0] == AISTATE_SPIN) //Melee Damage/Speed boost
            {
                npc.damage = 200;
                npc.defense = 300;
            }
            else //Reset Stats
            {
                npc.defense = npc.defDefense;
                npc.damage = 80;
            }


            if (internalAI[0] == AISTATE_IDLE || internalAI[0] == AISTATE_PROJ) //When charging the player
            {
                MoveToPoint(wantedVelocity);
            }
            else if (internalAI[0] == AISTATE_SLASH) //When charging the player
            {
                MoveToPoint(npc.Center);
            }
            npc.rotation = 0;

            npc.noTileCollide = true;
        }

        public override void PostAI()
        {
            Player player = Main.player[npc.target];
            if (internalAI[0] != AISTATE_SPIN)
            {
                if (player.Center.X > npc.Center.X) //If NPC's X position is higher than the player's
                {
                    if (pos == -250)
                    {
                        pos = 250;
                    }
                    npc.direction = 1;
                }
                else //If NPC's X position is lower than the player's
                {
                    if (pos == 250)
                    {
                        pos = -250;
                    }
                    npc.direction = -1;
                }
            }
            else
            {
                npc.direction = npc.velocity.X > 0 ? 1 : -1;
            }
        }

        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 10f;
            if (Vector2.Distance(npc.Center, point) > 500)
            {
                moveSpeed = 16;
            }
            if (internalAI[0] == AISTATE_SLASH || internalAI[0] == AISTATE_SPIN)
            {
                moveSpeed = 20f;
            }
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
            Texture2D Slash = mod.GetTexture("NPCs/Bosses/AH/Haruka/HarukaSlash");
            if (internalAI[0] == AISTATE_SPIN)
            {
                BaseDrawing.DrawAfterimage(spritebatch, Main.npcTexture[npc.type], 0, npc, 1.5f, 1f, 3, false, 0f, 0f, Color.Navy);
            }
            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, 27, npc.frame, npc.GetAlpha(dColor), false);
            BaseDrawing.DrawTexture(spritebatch, Slash, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, 27, npc.frame, dColor, false);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, 27, npc.frame, Color.White, false);

            BaseDrawing.DrawAfterimage(spritebatch, glowTex, 0, npc, 1f, 1f, 7, true, 0f, 0f, AAColor.YamataA);
            return false;
        }
    }
}