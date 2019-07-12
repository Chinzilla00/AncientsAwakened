using Terraria;
using System;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using BaseMod;
using System.IO;
using AAMod.NPCs.Bosses.Akuma.Awakened;
using Terraria.Graphics.Shaders;
using Terraria.Localization;

namespace AAMod.NPCs.Bosses.Akuma
{
    [AutoloadBossHead]
    public class Akuma : ModNPC
	{
        public override string Texture => "AAMod/NPCs/Bosses/Akuma/Akuma";

        public bool loludided;
        private bool weakness;

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Akuma; Draconian Demon");
			NPCID.Sets.TechnicallyABoss[npc.type] = true;
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.5f * bossLifeScale);
        }

        public override void SetDefaults()
		{
			npc.noTileCollide = true;
			npc.height = 120;
			npc.width = 84;
			npc.aiStyle = -1;
			npc.netAlways = true;
			npc.knockBackResist = 0f;
            npc.damage = 90;
            npc.defense = 250;
            npc.lifeMax = 600000;
            if (Main.expertMode)
            {
                npc.value = Item.sellPrice(0, 0, 0, 0);
            }
            else
            {
                npc.value = Item.sellPrice(0, 55, 0, 0);
            }
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.aiStyle = -1;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.behindTiles = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Akuma");
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/Sounds/AkumaRoar");
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.buffImmune[103] = false;
            npc.alpha = 255;
            musicPriority = MusicPriority.BossHigh;
            if (AAWorld.downedAllAncients)
            {
                npc.damage = 90;
                npc.defense = 270;
                npc.lifeMax = 260000;
            }
        }

        public bool isAwakened = false;
        public bool fireAttack = false;
        private bool spawnAshe = false;
        public static int MinionCount = 0;
        public int MaxMinons = Main.expertMode ? 3 : 4;
        private static readonly int Timer = Main.expertMode ? 200 : 300;
        private static readonly int TimerMax = Timer + 100;

        public float[] internalAI = new float[5];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
                writer.Write(internalAI[4]);
                writer.Write(fireAttack);
                writer.Write(isAwakened);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
                internalAI[4] = reader.ReadFloat();
                fireAttack = reader.ReadBool();
                isAwakened = reader.ReadBool();
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void FindFrame(int frameHeight)
        {
            if (fireAttack == true || internalAI[0] >= 450)
            {
                npc.frameCounter++;
                if (npc.frameCounter > 10)
                {
                    npc.frame.Y += frameHeight;
                    npc.frameCounter = 0;
                }
                if (npc.frame.Y >= frameHeight * 3)
                {
                    npc.frame.Y = 2;
                }
            }
            else
            {
                npc.frame.Y = 0;
                npc.frameCounter = 0;
            }
        }

        public override bool PreAI()
        {
            if (npc.type != mod.NPCType<Akuma>() || npc.type != mod.NPCType<AkumaA>()) //If body segment
            {
                BodyAI();
            }
            else //If Head
            {
                npc.TargetClosest(true);

                Player player = Main.player[npc.target];

                int FireBreath = isAwakened ? mod.ProjectileType<AkumaABreath>() : mod.ProjectileType<AkumaBreath>();

                int Arms = isAwakened ? mod.NPCType<AkumaAArms>() : mod.NPCType<AkumaArms>();
                int Body = isAwakened ? mod.NPCType<AkumaABody>() : mod.NPCType<AkumaBody>();
                int Body1 = isAwakened ? mod.NPCType<AkumaABody1>() : mod.NPCType<AkumaBody1>();
                int Tail = isAwakened ? mod.NPCType<AkumaATail>() : mod.NPCType<AkumaTail>();

                internalAI[0]++;
                if (internalAI[0] == Timer && Main.netMode != 1)
                {
                    if (Main.netMode != 1)
                    {
                        QuoteSaid = false; //Set Quote to false so he can say shit again
                        internalAI[1] = Main.rand.Next(isAwakened ? 5 : 4);
                        npc.netUpdate = true;
                    }
                    Roar(roarTimerMax, false);
                }
                if (internalAI[0] > Timer && Main.netMode != 1)
                {
                    if (Main.netMode != 1)
                    {
                        if (isAwakened)
                        {
                            AAttack(npc);
                        }
                        else
                        {
                            Attack(npc);
                        }
                    }
                }
                if (internalAI[0] >= TimerMax && Main.netMode != 1)
                {
                    internalAI[0] = 0;
                    npc.netUpdate = true;
                }

                if (Main.rand.Next(50) == 1 && fireAttack == false && internalAI[0] < 500 && Main.netMode != 1)
                {
                    fireAttack = true;
                    npc.netUpdate = true;
                }

                if (fireAttack == true && Main.netMode != 1)
                {
                    internalAI[4]++;
                    if ((internalAI[4] == 20 || internalAI[4] == 50 || internalAI[4] == 79) && npc.HasBuff(BuffID.Wet))
                    {
                        for (int spawnDust = 0; spawnDust < 5; spawnDust++)
                        {
                            int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("MireBubbleDust"), 0f, 0f, 90);
                            Main.dust[num935].noGravity = true;
                            Main.dust[num935].scale = 2f;
                            Main.dust[num935].velocity.Y -= 1f;
                        }
                        if (weakness == false)
                        {
                            weakness = true;
                            BaseUtility.Chat(isAwakened ? "ACK..! WATER! I LOATHE WATER!!!" : "Water?! ACK..! I CAN'T BREATHE!", new Color(180, 41, 32));
                        }
                        npc.netUpdate = true;
                    }
                    else if (!npc.HasBuff(BuffID.Wet))
                    {
                        AAAI.BreatheFire(npc, true, FireBreath, 2, 2);
                    }
                    if (internalAI[4] >= 80)
                    {
                        internalAI[4] = 0;
                        fireAttack = false;
                        npc.netUpdate = true;
                    }
                }

                AAAI.DustOnNPCSpawn(npc, mod.DustType("AkumaDust"), 2, 12);

                npc.spriteDirection = npc.velocity.X > 0 ? -1 : 1;

                if (!Main.player[npc.target].active || Main.player[npc.target].dead)
                {
                    npc.TargetClosest(true);
                    if (!Main.player[npc.target].active || Main.player[npc.target].dead)
                    {
                        internalAI[3]++;
                        npc.velocity.Y = npc.velocity.Y + 0.11f;
                        if (internalAI[3] >= 300)
                        {
                            npc.active = false;
                        }
                    }
                    else
                    {
                        internalAI[3] = 0;
                    }
                }

                if (Main.netMode != 1)
                {
                    if (internalAI[2] == 0)
                    {
                        npc.realLife = npc.whoAmI;
                        int latestNPC = npc.whoAmI;

                        for (int i = 0; i < 3; ++i)
                        {
                            latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, Body, npc.whoAmI, 0, latestNPC);
                            Main.npc[latestNPC].realLife = npc.whoAmI;
                            Main.npc[latestNPC].ai[3] = npc.whoAmI;

                            latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, Arms, npc.whoAmI, 0, latestNPC);
                            Main.npc[latestNPC].realLife = npc.whoAmI;
                            Main.npc[latestNPC].ai[3] = npc.whoAmI;
                        }

                        latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, Body, npc.whoAmI, 0, latestNPC);
                        Main.npc[latestNPC].realLife = npc.whoAmI;
                        Main.npc[latestNPC].ai[3] = npc.whoAmI;

                        latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, Body1, npc.whoAmI, 0, latestNPC);
                        Main.npc[latestNPC].realLife = npc.whoAmI;
                        Main.npc[latestNPC].ai[3] = npc.whoAmI;

                        latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, Tail, npc.whoAmI, 0, latestNPC); ;
                        Main.npc[latestNPC].realLife = npc.whoAmI;
                        Main.npc[latestNPC].ai[3] = npc.whoAmI;

                        internalAI[2] = 1;
                        npc.netUpdate = true;
                    }
                }

                if (!Main.dayTime && isAwakened && Main.netMode != 1)
                {
                    if (isAwakened)
                    {
                        BaseUtility.Chat("Nighttime won't save you from me this time, kid! The day is born anew!", Color.DeepSkyBlue);
                        Main.dayTime = true;
                        Main.time = 0;
                        npc.netUpdate = true;
                    }
                    else
                    {
                        if (loludided == false)
                        {
                            BaseUtility.Chat("Yaaaaaaaaawn. I'm bushed kid, I'm gonna have to take a rain check. Come back tomorrow.", new Color(180, 41, 32));
                            loludided = true;
                        }
                        npc.velocity.Y = npc.velocity.Y + 1f;
                        if (npc.position.Y - npc.height - npc.velocity.Y >= Main.maxTilesY && Main.netMode != 1) { BaseAI.KillNPC(npc); npc.netUpdate2 = true; }
                    }
                }

                if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
                {
                    if (loludided == false)
                    {
                        BaseUtility.Chat(isAwakened ? "You just got burned, kid." : "I thought you terrarians put up more of a fight. Guess not.", new Color(180, 41, 32));
                        loludided = true;
                    }
                    npc.velocity.Y = npc.velocity.Y - 1f;
                    if (npc.position.Y < 0)
                    {
                        npc.velocity.Y = npc.velocity.Y - 1f;
                    }
                    if (npc.position.Y < 0)
                    {
                        for (int num957 = 0; num957 < 200; num957++)
                        {
                            if (Main.npc[num957].aiStyle == npc.aiStyle)
                            {
                                Main.npc[num957].active = false;
                            }
                        }
                    }
                    return false;
                }

                if (npc.life <= npc.lifeMax / 2 && !spawnAshe && isAwakened && Main.netMode != 1)
                {
                    spawnAshe = true;
                    if (AAWorld.downedAkuma)
                    {
                        BaseUtility.Chat("Ashe? Help your dear old dad with this kid again!", Color.DeepSkyBlue);
                        BaseUtility.Chat("You got it, daddy..!", new Color(102, 20, 48));
                        AAModGlobalNPC.SpawnBoss(player, mod.NPCType("AsheA"), false, 0, 0);
                    }
                    else
                    {
                        BaseUtility.Chat("Hey! Hands off my papa!", new Color(102, 20, 48));
                        BaseUtility.Chat("Atta-girl..!", Color.DeepSkyBlue);
                        AAModGlobalNPC.SpawnBoss(player, mod.NPCType("AsheA"), false, 0, 0);
                    }
                    npc.netUpdate = true;
                }

                bool collision = true;

                float speed = 12f;
                float acceleration = 0.13f;

                Vector2 npcCenter = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float targetXPos = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2);
                float targetYPos = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2);

                float targetRoundedPosX = (int)(targetXPos / 16.0) * 16;
                float targetRoundedPosY = (int)(targetYPos / 16.0) * 16;
                npcCenter.X = npcCenter.X / 16 * 16;
                npcCenter.Y = npcCenter.Y / 16 * 16;
                float dirX = targetRoundedPosX - npcCenter.X;
                float dirY = targetRoundedPosY - npcCenter.Y;

                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                if (!collision)
                {
                    npc.TargetClosest(true);
                    npc.velocity.Y = npc.velocity.Y + 0.11f;
                    if (npc.velocity.Y > speed)
                        npc.velocity.Y = speed;
                    if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < speed * 0.4)
                    {
                        if (npc.velocity.X < 0.0)
                            npc.velocity.X = npc.velocity.X - acceleration * 1.1f;
                        else
                            npc.velocity.X = npc.velocity.X + acceleration * 1.1f;
                    }
                    else if (npc.velocity.Y == speed)
                    {
                        if (npc.velocity.X < dirX)
                            npc.velocity.X = npc.velocity.X + acceleration;
                        else if (npc.velocity.X > dirX)
                            npc.velocity.X = npc.velocity.X - acceleration;
                    }
                    else if (npc.velocity.Y > 4.0)
                    {
                        if (npc.velocity.X < 0.0)
                            npc.velocity.X = npc.velocity.X + acceleration * 0.9f;
                        else
                            npc.velocity.X = npc.velocity.X - acceleration * 0.9f;
                    }
                }
                else
                {
                    if (npc.soundDelay == 0)
                    {
                        float num1 = length / 40f;
                        if (num1 < 10.0)
                            num1 = 10f;
                        if (num1 > 20.0)
                            num1 = 20f;
                        npc.soundDelay = (int)num1;
                    }
                    float absDirX = Math.Abs(dirX);
                    float absDirY = Math.Abs(dirY);
                    float newSpeed = speed / length;
                    dirX *= newSpeed;
                    dirY *= newSpeed;
                    if (npc.velocity.X > 0.0 && dirX > 0.0 || npc.velocity.X < 0.0 && dirX < 0.0 || (npc.velocity.Y > 0.0 && dirY > 0.0 || npc.velocity.Y < 0.0 && dirY < 0.0))
                    {
                        if (npc.velocity.X < dirX)
                            npc.velocity.X = npc.velocity.X + acceleration;
                        else if (npc.velocity.X > dirX)
                            npc.velocity.X = npc.velocity.X - acceleration;
                        if (npc.velocity.Y < dirY)
                            npc.velocity.Y = npc.velocity.Y + acceleration;
                        else if (npc.velocity.Y > dirY)
                            npc.velocity.Y = npc.velocity.Y - acceleration;
                        if (Math.Abs(dirY) < speed * 0.2 && (npc.velocity.X > 0.0 && dirX < 0.0 || npc.velocity.X < 0.0 && dirX > 0.0))
                        {
                            if (npc.velocity.Y > 0.0)
                                npc.velocity.Y = npc.velocity.Y + acceleration * 2f;
                            else
                                npc.velocity.Y = npc.velocity.Y - acceleration * 2f;
                        }
                        if (Math.Abs(dirX) < speed * 0.2 && (npc.velocity.Y > 0.0 && dirY < 0.0 || npc.velocity.Y < 0.0 && dirY > 0.0))
                        {
                            if (npc.velocity.X > 0.0)
                                npc.velocity.X = npc.velocity.X + acceleration * 2f;
                            else
                                npc.velocity.X = npc.velocity.X - acceleration * 2f;
                        }
                    }
                    else if (absDirX > absDirY)
                    {
                        if (npc.velocity.X < dirX)
                            npc.velocity.X = npc.velocity.X + acceleration * 1.1f;
                        else if (npc.velocity.X > dirX)
                            npc.velocity.X = npc.velocity.X - acceleration * 1.1f;
                        if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < speed * 0.5)
                        {
                            if (npc.velocity.Y > 0.0)
                                npc.velocity.Y = npc.velocity.Y + acceleration;
                            else
                                npc.velocity.Y = npc.velocity.Y - acceleration;
                        }
                    }
                    else
                    {
                        if (npc.velocity.Y < dirY)
                            npc.velocity.Y = npc.velocity.Y + acceleration * 1.1f;
                        else if (npc.velocity.Y > dirY)
                            npc.velocity.Y = npc.velocity.Y - acceleration * 1.1f;
                        if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < speed * 0.5)
                        {
                            if (npc.velocity.X > 0.0)
                                npc.velocity.X = npc.velocity.X + acceleration;
                            else
                                npc.velocity.X = npc.velocity.X - acceleration;
                        }
                    }
                }
                npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;

                if (collision)
                {
                    if (npc.localAI[0] != 1)
                        npc.netUpdate = true;
                    npc.localAI[0] = 1f;
                }
                else
                {
                    if (npc.localAI[0] != 0.0)
                        npc.netUpdate = true;
                    npc.localAI[0] = 0.0f;
                }
                if ((npc.velocity.X > 0.0 && npc.oldVelocity.X < 0.0 || npc.velocity.X < 0.0 && npc.oldVelocity.X > 0.0 || (npc.velocity.Y > 0.0 && npc.oldVelocity.Y < 0.0 || npc.velocity.Y < 0.0 && npc.oldVelocity.Y > 0.0)) && !npc.justHit)
                    npc.netUpdate = true;
            }
            return false;
        }

        public void BodyAI()
        {
            Vector2 chasePosition = Main.npc[(int)npc.ai[1]].Center;
            Vector2 directionVector = chasePosition - npc.Center;
            npc.spriteDirection = ((directionVector.X > 0f) ? 1 : -1);
            if (npc.ai[3] > 0)
                npc.realLife = (int)npc.ai[3];
            if (npc.target < 0 || npc.target == byte.MaxValue || Main.player[npc.target].dead)
                npc.TargetClosest(true);
            if (Main.player[npc.target].dead && npc.timeLeft > 300)
                npc.timeLeft = 300;

            if (Main.netMode != 1)
            {
                if (!Main.npc[(int)npc.ai[1]].active || Main.npc[(int)npc.ai[3]].type != (isAwakened ? mod.NPCType("AkumaA") : mod.NPCType("Akuma")))
                {
                    npc.life = 0;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                    NetMessage.SendData(28, -1, -1, null, npc.whoAmI, -1f, 0.0f, 0.0f, 0, 0, 0);
                }
            }

            if (npc.ai[1] < (double)Main.npc.Length)
            {
                Vector2 npcCenter = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float dirX = Main.npc[(int)npc.ai[1]].position.X + Main.npc[(int)npc.ai[1]].width / 2 - npcCenter.X;
                float dirY = Main.npc[(int)npc.ai[1]].position.Y + Main.npc[(int)npc.ai[1]].height / 2 - npcCenter.Y;
                npc.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                float dist = (length - npc.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;

                if (dirX < 0f)
                {
                    npc.spriteDirection = 1;

                }
                else
                {
                    npc.spriteDirection = -1;
                }

                npc.velocity = Vector2.Zero;
                npc.position.X = npc.position.X + posX;
                npc.position.Y = npc.position.Y + posY;
            }
        }

        public bool Quote1;
        public bool Quote2;
        public bool Quote3;
        public bool Quote4;
        public bool QuoteSaid;

        public void Attack(NPC npc)
        {
            Player player = Main.player[npc.target];
            if (internalAI[1] == 0)
            {
                if (!QuoteSaid)
                {
                    BaseUtility.Chat((!Quote1) ? "Hey kid! Sky's fallin', watch out!" : "Down comes fire and fury!", new Color(180, 41, 32));
                    QuoteSaid = true;
                    Quote1 = true;
                }
                if (internalAI[0] == Timer + 20 || internalAI[0] == Timer + 40 || internalAI[0] == Timer + 60 || internalAI[0] == Timer + 80)
                {
                    int Fireballs = Main.expertMode ? 5 : 4;
                    for (int Loops = 0; Loops < Fireballs; Loops++)
                    {
                        AkumaAttacks.Dragonfire(npc, mod, isAwakened);
                    }
                    npc.netUpdate = true;
                }
            }
            else if (internalAI[1] == 1)
            {
                if (!QuoteSaid)
                {
                     if (!Quote2) BaseUtility.Chat("Spirits of the volcano! help me crush this kid!", new Color(180, 41, 32));
                    QuoteSaid = true;
                    Quote2 = true;
                }
                AkumaAttacks.SpawnLung(player, mod, false);
                internalAI[0] = 0;
                npc.netUpdate = true;
            }
            else if (internalAI[1] == 2)
            {
                if (!QuoteSaid)
                {
                    BaseUtility.Chat((!Quote3) ? "Hey kid! Watch out!" : "Incoming!", new Color(180, 41, 32));
                    QuoteSaid = true;
                    Quote3 = true;
                }
                if (internalAI[0] == Timer + 20)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, npc.velocity.X * 2, npc.velocity.Y, mod.ProjectileType<AkumaFireProj>(), npc.damage / (Main.expertMode ? 2 : 4), 3, Main.myPlayer);
                    npc.netUpdate = true;
                }
                if (internalAI[0] > Timer + 50)
                {
                    internalAI[0] = 0;
                    npc.netUpdate = true;
                }
            }
            else
            {
                if (!QuoteSaid)
                {
                    BaseUtility.Chat((!Quote4) ? "Face the flames of despair, kid!" : "Heads up, kid!", new Color(180, 41, 32));
                    QuoteSaid = true;
                    Quote4 = true;
                }
                if (internalAI[0] == Timer + 20)
                {
                    int Fireballs = Main.expertMode ? 3 : 5;
                    float spread = 70f * 0.0174f;
                    float baseSpeed = (float)Math.Sqrt((npc.velocity.X * npc.velocity.X) + (npc.velocity.Y * npc.velocity.Y));
                    double startAngle = Math.Atan2(npc.velocity.X, npc.velocity.Y) - .1d;
                    double deltaAngle = spread / 6f;
                    for (int i = 0; i < Fireballs; i++)
                    {
                        double offsetAngle = startAngle + (deltaAngle * i);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle) * 2, baseSpeed * (float)Math.Cos(offsetAngle) * 2, mod.ProjectileType<AkumaBomb>(), npc.damage / (Main.expertMode ? 2 : 4), 3, Main.myPlayer);
                    }
                    npc.netUpdate = true;
                }
                if (internalAI[0] > Timer + 50)
                {
                    internalAI[0] = 0;
                    npc.netUpdate = true;
                }
            }
        }

        public void AAttack(NPC npc)
        {
            Player player = Main.player[npc.target];
            if (internalAI[1] == 0)
            {
                if (!QuoteSaid)
                {
                    BaseUtility.Chat((!Quote1) ? "Sky's fallin' again! On your toes!" : "Down comes the flames of fury again!", Color.DeepSkyBlue);
                    QuoteSaid = true;
                    Quote1 = true;
                }
                if (internalAI[0] == Timer + 20 || internalAI[0] == Timer + 40 || internalAI[0] == Timer + 60 || internalAI[0] == Timer + 80)
                {
                    int Fireballs = Main.expertMode ? 5 : 4;
                    for (int Loops = 0; Loops < Fireballs; Loops++)
                    {
                        AkumaAttacks.Dragonfire(npc, mod, true);
                    }
                    npc.netUpdate = true;
                }
            }

            if (internalAI[1] == 1)
            {
                if (!QuoteSaid)
                {
                    BaseUtility.Chat((!Quote1) ? "You underestimate the artillery of a dragon, kid!" : "Flames don't give in till the end, kid!", Color.DeepSkyBlue);
                    QuoteSaid = true;
                    Quote1 = true;
                }
                if (internalAI[0] == Timer + 20)
                {
                    int Fireballs = Main.expertMode ? 3 : 5;
                    float spread = 70f * 0.0174f;
                    float baseSpeed = (float)Math.Sqrt((npc.velocity.X * npc.velocity.X) + (npc.velocity.Y * npc.velocity.Y));
                    double startAngle = Math.Atan2(npc.velocity.X, npc.velocity.Y) - .1d;
                    double deltaAngle = spread / 6f;
                    for (int i = 0; i < Fireballs; i++)
                    {
                        double offsetAngle = startAngle + (deltaAngle * i);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle) * 2, baseSpeed * (float)Math.Cos(offsetAngle) * 2, mod.ProjectileType<AkumaABomb>(), npc.damage / (Main.expertMode ? 2 : 4), 3, Main.myPlayer);
                    }
                    npc.netUpdate = true;
                }
                
                if (internalAI[0] > Timer + 50)
                {
                    internalAI[0] = 0;
                    npc.netUpdate = true;
                }
            }

            if (internalAI[1] == 2)
            {
                int Fireballs = Main.expertMode ? 12 : 14;
                if (!QuoteSaid)
                {
                    BaseUtility.Chat((!Quote1) ? "Heads up! Volcano's eruptin' kid!" : "INCOMING!", Color.DeepSkyBlue);
                    QuoteSaid = true;
                    Quote1 = true;
                }
                if (internalAI[0] == Timer + 20 || internalAI[0] == Timer + 40 || internalAI[0] == Timer + 60 || internalAI[0] == Timer + 80)
                {
                    for (int Loops = 0; Loops < Fireballs; Loops++)
                    {
                        AkumaAttacks.Eruption(npc, mod);
                    }
                    npc.netUpdate = true;
                }
            }

            if (internalAI[1] == 3)
            {
                AkumaAttacks.SpawnLung(player, mod, false);
                internalAI[0] = 0;
                npc.netUpdate = true;
            }

            else
            {
                if (!QuoteSaid)
                {
                    BaseUtility.Chat((!Quote1) ? "Hey Kid? Like Fireworks? No? Too Bad!" : "Here comes the grand finale, kid!", Color.DeepSkyBlue);
                    QuoteSaid = true;
                    Quote1 = true;
                }
                if (internalAI[0] == Timer + 20)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, npc.velocity.X * 2, npc.velocity.Y, mod.ProjectileType<AFireProjHostile>(), npc.damage / (Main.expertMode ? 2 : 4), 3, Main.myPlayer);
                    npc.netUpdate = true;
                }
                if (internalAI[0] > Timer + 50)
                {
                    internalAI[0] = 0;
                    npc.netUpdate = true;
                }
            }
        }

        public override void NPCLoot()
        {
            if (isAwakened)
            {
                if (Main.expertMode)
                {
                    if (!AAWorld.downedAkuma)
                    {
                        Item.NewItem((int)npc.Center.X, (int)npc.Center.Y, npc.width, npc.height, mod.ItemType("DraconianRune"));
                    }
                    BaseUtility.Chat(AAWorld.downedAkuma ? "Heh, not too shabby this time kid. I'm impressed. Here. Take your prize." : "GRAH..! HOW!? HOW COULD I LOSE TO A MERE MORTAL TERRARIAN?! Hmpf...fine kid, you win, fair and square. Here's your reward.", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
                    AAWorld.downedAkuma = true;
                    if (Main.rand.Next(50) == 0 && AAWorld.downedAllAncients)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PowerStone"));
                    }
                    if (Main.rand.Next(10) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AkumaATrophy"));
                    }
                    if (Main.rand.Next(20) == 0)
                    {
                        Item.NewItem((int)npc.Center.X, (int)npc.Center.Y, npc.width, npc.height, mod.ItemType("EXSoul"));
                    }
                    npc.DropBossBags();
                    npc.value = 0f;
                    npc.boss = false;
                    AAWorld.downedAkuma = true;
                    return;
                }
                BaseUtility.Chat("Nice. You cheated. Now come fight me in expert mode like a real man.", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
            }
            else
            {
                npc.DropLoot(Items.Vanity.Mask.AkumaMask.type, 1f / 7);
                if (!Main.expertMode)
                {
                    if (!AAWorld.downedAkuma)
                    {
                        BaseUtility.Chat("The volcanoes of the inferno are finally quelled...", Color.DarkOrange.R, Color.DarkOrange.G, Color.DarkOrange.B, false);
                    }
                    if (Main.rand.Next(50) == 0 && AAWorld.downedAllAncients)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PowerStone"));
                    }
                    string[] lootTable = { "AkumaTerratool", "DayStorm", "LungStaff", "MorningGlory", "RadiantDawn", "Solar", "SunSpear", "ReignOfFire", "DaybreakArrow", "Daycrusher", "Dawnstrike", "SunStorm", "SunStaff", "DragonSlasher" };
                    AAAI.DownedBoss(npc, mod, lootTable, AAWorld.downedAkuma, true, mod.ItemType("CrucibleScale"), 20, 30, false, false, true, 0, mod.ItemType("AkumaTrophy"), false);
                    BaseUtility.Chat("Hmpf...you’re pretty good kid, but not good enough. Come back once you’ve gotten a bit better.", new Color(180, 41, 32));

                }
                if (Main.expertMode)
                {
                    int npcID = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AkumaTransition"), 0, 0, 0, 0, 0, npc.target);
                    Main.npc[npcID].Center = npc.Center;
                    Main.npc[npcID].netUpdate2 = true; Main.npc[npcID].netUpdate = true;
                }
                npc.value = 0f;
                npc.boss = false;
                AAWorld.downedAkuma = true;
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            if (Main.expertMode && !isAwakened)
            {
                potionType = 0;
            }
            else
            {
                potionType = ItemID.SuperHealingPotion;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                int dust1 = mod.DustType<Dusts.AkumaDust>();
                for (int i = 0; i < 2; i++)
                {
                    Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), npc.width, npc.height, dust1, 0f, 0f, 0);
                    Main.dust[dust1].velocity *= 0.5f;
                    Main.dust[dust1].scale *= 1.3f;
                    Main.dust[dust1].fadeIn = 1f;
                    Main.dust[dust1].noGravity = i == 1 ? true : false;
                }
            }
        }
        
        public int roarTimer = 0;
        public int roarTimerMax = 120;
        public bool Roaring => roarTimer > 0;

        public void Roar(int timer, bool fireSound)
        {
            roarTimer = timer;
            if (fireSound)
            {
                Main.PlaySound(4, (int)npc.Center.X, (int)npc.Center.Y, 60);
            }
            else
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/AkumaRoar"), npc.Center);
            }
        }
        
        public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
        {
            spriteEffects = (npc.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }

        public override bool PreDraw(SpriteBatch sb, Color dc)
        {
            if (isAwakened)
            {
                DrawAkumaA(sb, dc);
            }
            else
            {
                DrawAkuma(sb, dc);
            }
            return false;
        }

        public void DrawAkuma(SpriteBatch sb, Color dc)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            sb.Draw(texture, npc.Center - Main.screenPosition, npc.frame, dc, npc.rotation, npc.frame.Size() / 2, npc.scale, npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
        }

        private int Head = -1;

        public void DrawAkumaA(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D AkumaTex = Main.npcTexture[npc.type];
            
            if (npc.type == mod.NPCType<AkumaA>())
            {
                if (npc.ai[1] == 1 || npc.ai[2] >= 200)
                {
                    AkumaTex = mod.GetTexture("NPCs/Bosses/Akuma/Awakened/AkumaA1");
                }
                else
                {
                    AkumaTex = mod.GetTexture("NPCs/Bosses/Akuma/Awakened/AkumaA");
                }
            }

            Texture2D glowTex = mod.GetTexture("Glowmasks/AkumaA_Glow");
            Texture2D glowTex1 = mod.GetTexture("Glowmasks/AkumaA1_Glow");
            Texture2D glowTex2 = mod.GetTexture("Glowmasks/AkumaAArms_Glow");
            Texture2D glowTex3 = mod.GetTexture("Glowmasks/AkumaABody_Glow");
            Texture2D glowTex4 = mod.GetTexture("Glowmasks/AkumaABody1_Glow");
            Texture2D glowTex5 = mod.GetTexture("Glowmasks/AkumaATail_Glow");

            GetAkuma();

            if (Head == -1) return;

            NPC akuma = Main.npc[Head];
            if (akuma == null || akuma.life <= 0 || !akuma.active || akuma.type != mod.NPCType("AkumaA")) { return; }

            int shader;

            if (internalAI[0] > Timer || fireAttack || ((AkumaA)akuma.modNPC).fireAttack || ((AkumaA)akuma.modNPC).internalAI[0] > Timer)
            {
                shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingFlameDye);
            }
            else
            {
                shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingOceanDye);
            }


            Texture2D HeadGlow = (npc.ai[1] == 1 || npc.ai[2] >= 200) ? glowTex1 : glowTex;

            Texture2D myGlowTex = (npc.type == mod.NPCType<AkumaA>() ? HeadGlow : npc.type == mod.NPCType<AkumaAArms>() ? glowTex2 : npc.type == mod.NPCType<AkumaABody>() ? glowTex3 : npc.type == mod.NPCType<AkumaABody1>() ? glowTex4 : glowTex5);
            BaseDrawing.DrawTexture(spriteBatch, AkumaTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, 3, npc.frame, npc.GetAlpha(drawColor), true);
            BaseDrawing.DrawTexture(spriteBatch, myGlowTex, shader, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, 3, npc.frame, npc.GetAlpha(Color.White), true);
        }

        public void GetAkuma()
        {
            if (Head == -1)
            {
                int npcID = BaseAI.GetNPC(npc.Center, mod.NPCType("AkumaA"), -1, null);
                if (npcID >= 0) Head = npcID;
            }
        }
    }

    
    [AutoloadBossHead]
    public class AkumaArms : Akuma
    {
        public override string Texture => "AAMod/NPCs/Bosses/Akuma/AkumaArms";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Akuma; Draconian Demon");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 60;
            npc.height = 60;
            npc.dontCountMe = true;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }
    }
    
    [AutoloadBossHead]
    public class AkumaBody : AkumaArms
    {
        public override string Texture => "AAMod/NPCs/Bosses/Akuma/AkumaBody";
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
    }
    
    [AutoloadBossHead]
    public class AkumaBody1 : AkumaArms
    {
        public override string Texture => "AAMod/NPCs/Bosses/Akuma/AkumaBody";
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
    }   

    [AutoloadBossHead]
    public class AkumaTail : AkumaArms
    {
        public override string Texture => "AAMod/NPCs/Bosses/Akuma/AkumaBody";
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
    }
}
