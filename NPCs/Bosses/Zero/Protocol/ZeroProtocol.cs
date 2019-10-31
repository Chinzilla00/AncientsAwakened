using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria.Graphics.Shaders;

namespace AAMod.NPCs.Bosses.Zero.Protocol
{
    [AutoloadBossHead]
    public class ZeroProtocol : ModNPC
    {
        public int timer;
        public static int type;
        public int damage = 0;
        public bool PlayerDead = false;
        public int deathTimer = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ZER0 PR0T0C0L");
            Main.npcFrameCount[npc.type] = 7; 
            NPCID.Sets.TrailCacheLength[npc.type] = 20;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 600000;
            npc.damage = 150;
            npc.defense = 70;
            npc.knockBackResist = 0f;
            npc.width = 170;
            npc.height = 170;
            npc.friendly = false;
            npc.aiStyle = 0;
            npc.value = Item.sellPrice(0, 40, 0, 0);
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/Sounds/Zerohit");
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/Sounds/ZeroDeath");
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Zero2");
            musicPriority = MusicPriority.BossHigh;
            npc.netAlways = true;
            bossBag = mod.ItemType("ZeroBag");
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = (int)(npc.damage * .7f);
            npc.lifeMax = (int)(npc.lifeMax * 0.5f * bossLifeScale);
        }

        public float[] Move = new float[4];
        public float[] start = new float[1];
        public float[] Minion = new float[1];

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(Move[0]);
                writer.Write(Move[1]);
                writer.Write(Move[2]);
                writer.Write(Move[3]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                Move[0] = reader.ReadFloat();
                Move[1] = reader.ReadFloat();
                Move[2] = reader.ReadFloat();
                Move[3] = reader.ReadFloat();
            }
        }

        public override void NPCLoot()
        {
            if (Main.expertMode)
            {

                if (!AAWorld.downedZero)
                {
                    if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("ZeroAwakened1"), Color.PaleVioletRed);
                    Item.NewItem((int)npc.Center.X, (int)npc.Center.Y, npc.width, npc.height, mod.ItemType("ZeroRune"));
                }
                AAWorld.downedZero = true;

                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ZeroTrophy"));
                }
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ZeroMask"));
                }
                if (Main.rand.Next(50) == 0 && AAWorld.downedAllAncients)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EXSoul"));
                }
                npc.DropBossBags();
                return;
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            if (Main.expertMode)
            {
                potionType = ItemID.SuperHealingPotion;
                Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y), new Vector2(0f, 0f), mod.ProjectileType("ZeroDeath1"), 0, 0);
            }
            else
            {
                potionType = 0;
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                npc.life = 1;
                npc.dontTakeDamage = true;
            }
            if (damage > 30)
            {
                int TeleportChance = 100 * (npc.life / npc.lifeMax);
                if (TeleportChance < 5)
                {
                    TeleportChance = 5;
                }
                if (Main.rand.Next(TeleportChance) == 0)
                {
                    if (Main.netMode != 1)
                    {
                        Teleport();
                        npc.netUpdate = true;
                    }
                }
            }
            if (npc.life <= 0 && !Main.expertMode)
            {
                if (Main.netMode != 1) AAMod.Chat(Lang.BossChat("ZeroAwakened4"), Color.Red.R, Color.Red.G, Color.Red.B);
            }
        }

        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }

            Texture2D tex = Main.npcTexture[npc.type];
            Texture2D afterimage = mod.GetTexture("NPCs/Bosses/Zero/Protocol/ZeroTrail");
            Texture2D glowTex = mod.GetTexture("Glowmasks/ZeroProtocol_Glow");
            if (isCharging)
            {
                tex = mod.GetTexture("NPCs/Bosses/Zero/Protocol/ZeroProtocolCharge");
                afterimage = mod.GetTexture("NPCs/Bosses/Zero/Protocol/ZeroProtocolChargeTrail");
                glowTex = mod.GetTexture("Glowmasks/ZeroProtocol_Glow");
            }

            BaseDrawing.DrawAfterimage(spritebatch, afterimage, 0, npc, 1, 1, 8, true, 0, 0, Color.Black, npc.frame, 7);
            BaseDrawing.DrawTexture(spritebatch, tex, 0, npc, dColor);
            BaseDrawing.DrawAura(spritebatch, glowTex, 0, npc.position, npc.width, npc.height, auraPercent, 1f, 1f, npc.rotation, npc.direction, 7, npc.frame, 0f, 0f, AAColor.Oblivion);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc, AAColor.Oblivion);
            return false;
        }

        bool isCharging = false;

        public override void AI()
        {
            int Repeats;
            if (npc.life < npc.life * (2 / 3))
            {
                Repeats = 4;
            }
            else if (npc.life < npc.life / 3)
            {
                Repeats = 5;
            }
            else
            {
                Repeats = 3;
            }
            npc.TargetClosest();
            Player player = Main.player[npc.target];

            if (Main.netMode != 1)
            {
                AAWorld.zeroUS = false;
            }

            for (int m = npc.oldPos.Length - 1; m > 0; m--)
            {
                npc.oldPos[m] = npc.oldPos[m - 1];
            }
            npc.oldPos[0] = npc.position;

            int Changerate = npc.life < npc.lifeMax / 2 ? 150 : 120;

            if (npc.ai[2]++ > Changerate)
            {
                if (npc.ai[0] != 0)
                {
                    npc.velocity *= .0f;
                }
                switch (npc.ai[0])
                {
                    case 0:
                        float spread = 45f * 0.0174f;
                        Vector2 dir = Vector2.Normalize(player.Center - npc.Center);
                        dir *= 12f;
                        float baseSpeed = (float)Math.Sqrt((dir.X * dir.X) + (dir.Y * dir.Y));
                        double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
                        double deltaAngle = spread / 6f;
                        for (int i = 0; i < 3; i++)
                        {
                            if (npc.ai[2] % Main.rand.Next(10) == 0 && Main.rand.Next(2) == 0)
                            {
                                double offsetAngle = startAngle + (deltaAngle * i);
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), ModContent.ProjectileType<StaticSphere>(), npc.damage / 4, 5, Main.myPlayer);
                            }
                        }
                        if (npc.ai[2] > 271)
                        {
                            AIChange();
                        }
                        break;
                    case 1:
                        if (npc.ai[2] % 30 == 0 && npc.ai[2] < 121)
                        {
                            Teleport();
                        }

                        if (npc.ai[2] == 240)
                        {
                            Attack(Main.rand.Next(4));
                        }

                        if (npc.ai[3] < Repeats && npc.ai[2] > 280)
                        {
                            npc.ai[3]++;
                            npc.ai[2] = Changerate;
                        }
                        else
                        {
                            AIChange();
                        }

                        break;
                    case 2:
                        npc.velocity *= 0;
                        if (npc.ai[2] == 90)
                        {
                            if (npc.life > npc.lifeMax / 2)
                            {
                                if (Main.rand.Next(2) == 0)
                                {
                                    int yPos = -300;
                                    for (int z = 0; z < 6; z++)
                                    {
                                        int a = Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y), Vector2.Zero, mod.ProjectileType("Blast"), damage, 3);

                                        int dir1 = -1;
                                        if (player.Center.X < npc.Center.X)
                                        {
                                            dir1 = 1;
                                            Main.projectile[a].ai[0] = 1;
                                        }
                                        Main.projectile[a].Center = npc.Center + (new Vector2(100, yPos) * dir1);
                                        yPos += 100;
                                    }
                                }
                                else
                                {
                                    int xPos = -300;
                                    for (int z = 0; z < 6; z++)
                                    {
                                        int h = Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y), Vector2.Zero, mod.ProjectileType("Blast"), damage, 3, 255, 2);
                                        Main.projectile[h].Center = npc.Center + (new Vector2(xPos, 100));
                                        xPos += 100;
                                    }
                                }
                            }
                            else
                            {
                                int yPos = -300;
                                for (int z = 0; z < 6; z++)
                                {
                                    int a = Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y), Vector2.Zero, mod.ProjectileType("Blast"), damage, 3);

                                    int dir1 = -1;
                                    if (player.Center.X < npc.Center.X)
                                    {
                                        dir1 = 1;
                                        Main.projectile[a].ai[0] = 1;
                                    }
                                    Main.projectile[a].Center = npc.Center + (new Vector2(100, yPos) * dir1);
                                    yPos += 100;
                                }
                                int xPos = -300;
                                for (int z = 0; z < 6; z++)
                                {
                                    int h = Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y), Vector2.Zero, mod.ProjectileType("Blast"), damage, 3, 255, 2);
                                    Main.projectile[h].Center = npc.Center + (new Vector2(xPos, 100));
                                    xPos += 100;
                                }
                            }
                        }
                        if (npc.ai[2] > 220)
                        {
                            npc.ai[0]++;
                            npc.ai[1] = 0;
                            npc.ai[2] = 0;
                            npc.ai[3] = 0;
                        }
                        break;
                    case 3:

                        if (npc.ai[2] == 30)
                        {
                            int a = Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y), new Vector2(0f, -12f), mod.ProjectileType("ProtoStar"), damage, 3);
                            Main.projectile[a].Center = npc.Center + new Vector2(-100, 0);
                            int b = Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y), new Vector2(0f, 12f), mod.ProjectileType("ProtoStar"), damage, 3);
                            Main.projectile[b].Center = npc.Center + new Vector2(100, 0); ;
                            int c = Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y), new Vector2(12f, 0), mod.ProjectileType("ProtoStar"), damage, 3);
                            Main.projectile[c].Center = npc.Center + new Vector2(0, 100); ;
                            int d = Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y), new Vector2(12f, 0), mod.ProjectileType("ProtoStar"), damage, 3);
                            Main.projectile[d].Center = npc.Center + new Vector2(0, -100);
                            if (npc.life < npc.lifeMax / 2)
                            {
                                int e = Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y), new Vector2(8f, -8f), mod.ProjectileType("ProtoStar"), damage, 3);
                                Main.projectile[e].Center = npc.Center + new Vector2(-80, -80);
                                int f = Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y), new Vector2(-8f, -8f), mod.ProjectileType("ProtoStar"), damage, 3);
                                Main.projectile[f].Center = npc.Center + new Vector2(80, 80); ;
                                int g = Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y), new Vector2(-8f, 8f), mod.ProjectileType("ProtoStar"), damage, 3);
                                Main.projectile[g].Center = npc.Center + new Vector2(-80, 80); ;
                                int h = Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y), new Vector2(8f, 8f), mod.ProjectileType("ProtoStar"), damage, 3);
                                Main.projectile[h].Center = npc.Center + new Vector2(80, -80);
                            }
                        }

                        if (npc.ai[2] > 120)
                        {
                            AIChange();
                        }

                        break;
                    case 4:
                        if (npc.ai[1] < 4)
                        {
                            if (npc.ai[2] > 60)
                            {
                                npc.ai[1]++;
                                npc.ai[2] = 0;
                                NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, ModContent.NPCType<ZeroEcho>());
                                Teleport();
                            }
                        }
                        else
                        {
                            npc.ai[0]++;
                            npc.ai[1] = 0;
                            npc.ai[2] = 0;
                            npc.ai[3] = 0;
                        }

                        break;

                    case 5:
                        if (!AliveCheck(player))
                            break;
                        if (npc.ai[1] >= 400)
                        {
                            npc.ai[0]++;
                            npc.ai[1] = 0;
                            npc.ai[2] = 0;
                            npc.ai[3] = 0;
                        }
                        break;
                    default:
                        npc.ai[0] = 0;
                        goto case 0;
                }
            }
            else
            {
                BaseAI.AISkull(npc, ref Move, true, 14, 350, .04f, .05f);
                int Frequency = Main.rand.Next(30, 50);
                if (npc.life < npc.lifeMax / 2)
                {
                    Frequency = Main.rand.Next(20, 50);
                }
                if (npc.life < npc.lifeMax / 4)
                {
                    Frequency = Main.rand.Next(10, 40);
                }
                if (Main.rand.Next(2) == 0)
                {
                    BaseAI.ShootPeriodic(npc, player.position, player.width, player.height, ModContent.ProjectileType<GlitchBomb>(), ref npc.ai[3], Frequency, npc.damage / 3, 10, true);
                }
                else
                {
                    BaseAI.ShootPeriodic(npc, player.position, player.width, player.height, ModContent.ProjectileType<GlitchRocket>(), ref npc.ai[3], Frequency, npc.damage / 3, 10, true);
                }
            }

            npc.rotation = 0;
        }

        private void AIChange()
        {
            if (Main.netMode != 1)
            {
                npc.ai[0] = Main.rand.Next(5);
                npc.ai[1] = 0;
                npc.ai[2] = 0;
                if (npc.ai[0] == 2 || npc.ai[0] == 4)
                {
                    Teleport();
                }
                else if ((npc.life < npc.lifeMax * (3 / 4)) && Main.rand.Next(3) == 0)
                {
                    Teleport();
                }
                else if ((npc.life < npc.lifeMax / 2) && Main.rand.Next(2) == 0)
                {
                    Teleport();
                }
                if (npc.life < npc.lifeMax / 4)
                {
                    Teleport();
                }
                npc.netUpdate = true;
            }
        }

        public bool AliveCheck(Player player)
        {
            bool tooFar = Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 10000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 10000f;
            if (player.dead || tooFar)
            {
                npc.TargetClosest(true);

                if (Main.player[npc.target].dead || tooFar)
                {
                    if (!PlayerDead)
                    {
                        if (player.dead)
                        {
                            if (Main.netMode != 1) AAMod.Chat(Lang.BossChat("ZeroAwakened6"), Color.Red.R, Color.Red.G, Color.Red.B);
                        }
                        else if (tooFar)
                        {
                            if (Main.netMode != 1) AAMod.Chat(Lang.BossChat("ZeroAwakened7"), Color.Red.R, Color.Red.G, Color.Red.B);
                        }
                        PlayerDead = true;
                    }
                    npc.velocity.Y = npc.velocity.Y - 0.04f;
                    if (npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                    }
                    if (npc.position.Y + npc.height - npc.velocity.Y <= 0 && Main.netMode != 1) { BaseAI.KillNPC(npc); npc.netUpdate2 = true; }
                    return false;
                }
            }
            return true;
        }


        public void Teleport(int a = 0)
        {
            bool safe = false;
            Player player = Main.player[npc.target];
            Vector2 targetPos = player.Center; 
            TPDust();

            if (a == 0)
            {
                while (!safe)
                {
                    int posX = Main.rand.Next(-500, 500);
                    int posY = Main.rand.Next(-500, 500);

                    if ((posX < 50 && posX > -50) && (posY < 50 && posY > -50))
                    {
                        return;
                    }
                    npc.position = new Vector2(targetPos.X + posX, targetPos.Y + posY);
                    safe = true;
                }
            }
            if (a == 1)
            {
                targetPos.X += 430 * (npc.Center.X > targetPos.X ? -1 : 1);
                targetPos.Y -= 430;
            }
            if (a == 2)
            {
                targetPos.X += 430 * (npc.Center.X > targetPos.X ? -1 : 1);
                targetPos.Y += 430;
            }
            else
            {
                npc.Center = player.Center + new Vector2(0, -200);
            }

            npc.velocity *= 0;
            TPDust();
        }

        public void TPDust()
        {
            Vector2 position = npc.Center + (Vector2.One * -20f);
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 3; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, 226, 0f, 0f, 100, default, 1.5f);
                Main.dust[num86].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                Main.dust[num86].position = npc.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
            }
            for (int num87 = 0; num87 < 7; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, 226, 0, 0, 100, new Color(), 2f);
                Main.dust[num88].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                Main.dust[num88].position = npc.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += npc.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                num88 = Dust.NewDust(position, num84, height3, 226, 0, 0, 100, new Color(), 2f);
                Main.dust[num88].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                Main.dust[num88].position = npc.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].fadeIn = 1f;
                Main.dust[num88].color = Color.Black * 0.5f;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity += npc.DirectionTo(Main.dust[num88].position) * 8f;
            }
            for (int num89 = 0; num89 < 5; num89++)
            {
                int num90 = Dust.NewDust(position, num84, height3, 226, 0, 0, 100, new Color(), 2f);
                Main.dust[num90].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                Main.dust[num90].position = npc.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(npc.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num90].noGravity = true;
                Main.dust[num90].noLight = true;
                Main.dust[num90].velocity *= 3f;
                Main.dust[num90].velocity += npc.DirectionTo(Main.dust[num90].position) * 2f;
            }
            for (int num91 = 0; num91 < 15; num91++)
            {
                int num92 = Dust.NewDust(position, num84, height3, 226, 0, 0, 100, new Color(), 2f);
                Main.dust[num92].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                Main.dust[num92].position = npc.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(npc.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num92].noGravity = true;
                Main.dust[num92].velocity *= 3f;
                Main.dust[num92].velocity += npc.DirectionTo(Main.dust[num92].position) * 3f;
            }
        }

        private void Movement(Vector2 targetPos, float speedModifier)
        {
            if (npc.Center.X < targetPos.X)
            {
                npc.velocity.X += speedModifier;
                if (npc.velocity.X < 0)
                    npc.velocity.X += speedModifier * 2;
            }
            else
            {
                npc.velocity.X -= speedModifier;
                if (npc.velocity.X > 0)
                    npc.velocity.X -= speedModifier * 2;
            }
            if (npc.Center.Y < targetPos.Y)
            {
                npc.velocity.Y += speedModifier;
                if (npc.velocity.Y < 0)
                    npc.velocity.Y += speedModifier * 2;
            }
            else
            {
                npc.velocity.Y -= speedModifier;
                if (npc.velocity.Y > 0)
                    npc.velocity.Y -= speedModifier * 2;
            }
            if (Math.Abs(npc.velocity.X) > 30)
                npc.velocity.X = 30 * Math.Sign(npc.velocity.X);
            if (Math.Abs(npc.velocity.Y) > 30)
                npc.velocity.Y = 30 * Math.Sign(npc.velocity.Y);
        }

        int Frame = 0;
        public override void FindFrame(int frameHeight)
        {
            if (npc.frameCounter++ > 10)
            {
                npc.frameCounter = 0;
                Frame += 1;
            }

            if (Frame > 6)
            {
                Frame = 0;
            }

            npc.frame.Y = frameHeight * Frame;
        }

        public void Attack(int Attack)
        {
            if (Attack == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i == 1)
                    {
                        NPC.NewNPC((int)npc.Center.X + 30, (int)npc.Center.Y + 30, ModContent.NPCType<NullZP>());
                    }
                    else if (i == 2)
                    {
                        NPC.NewNPC((int)npc.Center.X + 30, (int)npc.Center.Y - 30, ModContent.NPCType<NullZP>());
                    }
                    else if (i == 3)
                    {
                        NPC.NewNPC((int)npc.Center.X - 30, (int)npc.Center.Y + 30, ModContent.NPCType<NullZP>());
                    }
                    else
                    {
                        NPC.NewNPC((int)npc.Center.X - 30, (int)npc.Center.Y - 30, ModContent.NPCType<NullZP>());
                    }
                }
            }
            else if (Attack == 1)
            {

                float spread = 12f * 0.0174f;
                double startAngle = Math.Atan2(npc.velocity.X, npc.velocity.Y) - spread / 2;
                double deltaAngle = 6;
                double offsetAngle;
                for (int i = 0; i < 6; i++)
                {
                    offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(Math.Sin(offsetAngle) * 4f), (float)(Math.Cos(offsetAngle) * 2f), mod.ProjectileType("GlitchRocket"), 67, 0, Main.myPlayer, 0f, 0f);
                }
            }
            else if (Attack == 2)
            {
                float spread = 12f * 0.0174f;
                double startAngle = Math.Atan2(npc.velocity.X, npc.velocity.Y) - spread / 2;
                double deltaAngle = 5;
                double offsetAngle;
                for (int i = 0; i < 5; i++)
                {
                    offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(Math.Sin(offsetAngle) * 4f), (float)(Math.Cos(offsetAngle) * 2f), mod.ProjectileType("Error"), 67, 0, Main.myPlayer, 0f, 0f);
                }
            }
            else if (Attack == 3)
            {
                float spread = 12f * 0.0174f;
                double startAngle = Math.Atan2(npc.velocity.X, npc.velocity.Y) - spread / 2;
                double deltaAngle = 4;
                double offsetAngle;
                for (int i = 0; i < 4; i++)
                {
                    offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(Math.Sin(offsetAngle) * 2), (float)Math.Cos(offsetAngle), mod.ProjectileType("StaticSphere"), 67, 0, Main.myPlayer, 0f, 0f);
                }
            }
        }
    }
}
