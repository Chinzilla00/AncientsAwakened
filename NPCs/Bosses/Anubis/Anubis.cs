using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Items.Boss.Anubis;
using BaseMod;
using System.IO;

namespace AAMod.NPCs.Bosses.Anubis
{
    public class Anubis : ModNPC
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Anubis Legendscribe");
            Main.npcFrameCount[npc.type] = 9;
        }

        public override void SetDefaults()
        {
            npc.width = 76;
            npc.height = 100;
            npc.aiStyle = -1;
            npc.damage = 70;
            npc.defense = 40;
            npc.lifeMax = 50000;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.knockBackResist = 0.4f;
            npc.boss = true;
        }

        public float[] internalAI = new float[4];

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
            }
        }

        public int LocustCount = Main.expertMode ? 6 : 4;

        public override void AI()
        {
            if (!npc.HasPlayerTarget)
            {
                npc.TargetClosest();
            }
            if (internalAI[0] != 1)
            {
                Preamble();
                return;
            }

            if (npc.life < npc.lifeMax / 3 && internalAI[2] == 0)
            {
                if (Main.netMode != 1)
                {
                    for (int m = 0; m < LocustCount; m++)
                    {
                        int npcID = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Locust"), 0);
                        Main.npc[npcID].Center = npc.Center;
                        Main.npc[npcID].velocity = new Vector2(MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()), MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()));
                        Main.npc[npcID].velocity *= 8f;
                        Main.npc[npcID].ai[0] = m;
                        Main.npc[npcID].netUpdate2 = true;
                        if (!Main.expertMode)
                        {
                            Main.npc[npcID].ai[2] = 0;
                            if (m == 0 || m == 2)
                            {
                                Main.npc[npcID].ai[2] = 40;
                            }
                        }
                        else
                        {
                            Main.npc[npcID].ai[2] = 0;
                            if (m == 0 || m == 3)
                            {
                                Main.npc[npcID].ai[2] = 40;
                            }
                            else if (m == 2 || m == 4)
                            {
                                Main.npc[npcID].ai[2] = 80;
                            }
                        }
                        int dustType = ModContent.DustType<Rune>();
                        int pieCut = 20;
                        for (int i = 0; i < pieCut; i++)
                        {
                            int dustID = Dust.NewDust(Main.npc[npcID].position, Main.npc[npcID].width, Main.npc[npcID].height, dustType, 0f, 0f, 100, Color.White, 1.6f);
                            Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), i / (float)pieCut * 6.28f);
                            Main.dust[dustID].noLight = false;
                            Main.dust[dustID].noGravity = true;
                        }
                        for (int i = 0; i < pieCut; i++)
                        {
                            int dustID = Dust.NewDust(Main.npc[npcID].position, Main.npc[npcID].width, Main.npc[npcID].height, dustType, 0f, 0f, 100, Color.White, 2f);
                            Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), i / (float)pieCut * 6.28f);
                            Main.dust[dustID].noLight = false;
                            Main.dust[dustID].noGravity = true;
                        }
                    }
                }
            }

            npc.ai[1]++;

            switch (npc.ai[0])
            {
                case 0:

                    break;
            }
        }

        public void Teleport()
        {
            Vector2 position = npc.Center + (Vector2.One * -20f);
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 3; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, 240, 0f, 0f, 100, default, 1.5f);
                Main.dust[num86].position = npc.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
            }
            for (int num87 = 0; num87 < 15; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 50, default, 3.7f);
                Main.dust[num88].position = npc.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += npc.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                num88 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 25, default, 1.5f);
                Main.dust[num88].position = npc.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].fadeIn = 1f;
                Main.dust[num88].color = Color.Black * 0.5f;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity += npc.DirectionTo(Main.dust[num88].position) * 8f;
            }
            for (int num89 = 0; num89 < 10; num89++)
            {
                int num90 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 0, default, 2.7f);
                Main.dust[num90].position = npc.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(npc.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num90].noGravity = true;
                Main.dust[num90].noLight = true;
                Main.dust[num90].velocity *= 3f;
                Main.dust[num90].velocity += npc.DirectionTo(Main.dust[num90].position) * 2f;
            }
            for (int num91 = 0; num91 < 30; num91++)
            {
                int num92 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 0, default, 1.5f);
                Main.dust[num92].position = npc.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(npc.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num92].noGravity = true;
                Main.dust[num92].velocity *= 3f;
                Main.dust[num92].velocity += npc.DirectionTo(Main.dust[num92].position) * 3f;
            }

            Player player = Main.player[npc.target];
            Vector2 targetPos = player.Center;
            int posX = Main.rand.Next(-400, 400);

            int posY = Main.rand.Next(0, 400);
            if (posX > -100 && posX < 100)
            {
                 posY = Main.rand.Next(100, 400);
            }

            npc.position = new Vector2(targetPos.X + posX, targetPos.Y + posY);
            int pieCut = 20;
            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 10);
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(npc.Center.X - 1, npc.Center.Y - 1), 2, 2, ModContent.DustType<Rune>(), 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(npc.Center.X - 1, npc.Center.Y - 1), 2, 2, ModContent.DustType<Rune>(), 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
        }

        public void ScepterTeleport()
        {
            Vector2 position = npc.Center + (Vector2.One * -20f);
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 3; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, 240, 0f, 0f, 100, default, 1.5f);
                Main.dust[num86].position = npc.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
            }
            for (int num87 = 0; num87 < 15; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 50, default, 3.7f);
                Main.dust[num88].position = npc.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += npc.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                num88 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 25, default, 1.5f);
                Main.dust[num88].position = npc.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].fadeIn = 1f;
                Main.dust[num88].color = Color.Black * 0.5f;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity += npc.DirectionTo(Main.dust[num88].position) * 8f;
            }
            for (int num89 = 0; num89 < 10; num89++)
            {
                int num90 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 0, default, 2.7f);
                Main.dust[num90].position = npc.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(npc.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num90].noGravity = true;
                Main.dust[num90].noLight = true;
                Main.dust[num90].velocity *= 3f;
                Main.dust[num90].velocity += npc.DirectionTo(Main.dust[num90].position) * 2f;
            }
            for (int num91 = 0; num91 < 30; num91++)
            {
                int num92 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 0, default, 1.5f);
                Main.dust[num92].position = npc.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(npc.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num92].noGravity = true;
                Main.dust[num92].velocity *= 3f;
                Main.dust[num92].velocity += npc.DirectionTo(Main.dust[num92].position) * 3f;
            }

            Vector2 targetPos = Main.player[npc.target].Center;
            targetPos.X += 300 * (npc.Center.X < targetPos.X ? 1 : -1);
            targetPos.Y -= 300;
            npc.position = targetPos;

            int pieCut = 20;
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(npc.Center.X - 1, npc.Center.Y - 1), 2, 2, ModContent.DustType<Rune>(), 0f, 0f, 100, Color.White, 1f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(npc.Center.X - 1, npc.Center.Y - 1), 2, 2, ModContent.DustType<Rune>(), 0f, 0f, 100, Color.White, 1.5f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
        }

        public void Preamble()
        {
            npc.dontTakeDamage = true;
            if (Main.netMode != 1)
            {
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/silence");
                if (npc.velocity.Y == 0)
                {
                    if (internalAI[1]++ < 420)
                    {
                        if (!AAWorld.downedAnubis)
                        {
                            if (internalAI[1] == 60)
                            {
                                string s = Main.ActivePlayersCount > 1 ? "guys" : "bud";
                                if (Main.netMode != 1) BaseUtility.Chat("Well, " + s + ". Here we are.", Color.Gold);
                            }

                            if (internalAI[1] == 150)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat("I hope you're ready for a real fight.", Color.Gold);
                            }

                            if (internalAI[1] == 240)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat("Especially since I'm in my superior form.", Color.Gold);
                            }

                            if (internalAI[1] == 320)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat("You ready? I won't hesitate to slap you silly!", Color.Gold);
                            }

                            if (internalAI[1] >= 410)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat("Let's go!", Color.Gold);
                                internalAI[2] = 1;
                                Teleport();
                                npc.netUpdate = true;
                            }
                        }
                        else
                        {
                            if (Main.netMode != 1) BaseUtility.Chat("A rematch eh? Alright, this should be fun!", Color.Gold);
                            internalAI[0] = 1;
                            Teleport();
                            npc.netUpdate = true;
                        }
                    }
                }
            }
        }
    }
}