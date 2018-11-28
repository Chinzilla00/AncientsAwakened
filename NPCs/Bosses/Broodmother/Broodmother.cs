using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Broodmother
{

    [AutoloadBossHead]
    public class Broodmother : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Broodmother");
            Main.npcFrameCount[npc.type] = 6;

        }

        public override void SetDefaults()
        {
            npc.aiStyle = 0;
            npc.width = 202;
            npc.height = 100;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.chaseable = true;
            npc.damage = 35;
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/BroodTheme");
            npc.defense = 10;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.buffImmune[BuffID.OnFire] = true;
            animationType = NPCID.Mothron;
            npc.netAlways = true;
            npc.friendly = false;
            npc.lifeMax = 6000;
            npc.value = 20000;
            npc.HitSound = new LegacySoundStyle(3, 6, Terraria.Audio.SoundType.Sound);
            npc.DeathSound = new LegacySoundStyle(4, 8, Terraria.Audio.SoundType.Sound);
            bossBag = mod.ItemType("BroodBag");
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BroodmotherTrophy"));
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                if (Main.rand.Next(10) == 0)
                {
                    //Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BroodMask"));
                }
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BroodEgg"));
                }
                npc.DropLoot(mod.ItemType("Incinerite"), 75, 100);
                npc.DropLoot(mod.ItemType("BroodScale"), 50, 75);
            }
        }

        /*public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            {
                SpriteEffects spriteEffects = SpriteEffects.None;
                if (npc.spriteDirection == 1)
                {
                    spriteEffects = SpriteEffects.FlipHorizontally;
                }
                spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/Broodmother/Broodmother_Glow"), new Vector2(npc.Center.X - Main.screenPosition.X, npc.Center.Y - Main.screenPosition.Y),
                npc.frame, Color.White, npc.rotation,
                new Vector2(npc.width * 0.5f, npc.height * 0.5f), 1f, spriteEffects, 0f);
            }
        }*/

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;   //boss drops
            AAWorld.downedBrood = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 0.8f);  //boss damage increase in expermode
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)          //this make so when the npc has 0 life(dead) he will spawn this
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BroodGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BroodGore4"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BroodGore2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BroodGore2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BroodGore2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BroodGore2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BroodGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BroodGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BroodGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BroodGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BroodGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BroodGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BroodGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BroodGore3"), 1f);
            }
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (Main.rand.Next(2) == 0 || (Main.expertMode && Main.rand.Next(0) == 0))       //Chances for it to inflict the debuff
            {
                target.AddBuff(BuffID.OnFire, Main.rand.Next(100, 180));       //Main.rand.Next part is the length of the buff, so 8.3 seconds to 16.6 seconds
            }
            /*if (Main.rand.Next(9) == 0 || (Main.expertMode && Main.rand.Next(7) == 0))
            {
                target.AddBuff(BuffID.Poisoned, Main.rand.Next(250, 500));                 //there is no need for this, unless it inflicts a different debuff
            }*/
        }

        public override void AI()
        {
            int num1305 = 7;
            bool DespawnAttempt = false;
            npc.noTileCollide = false;
            npc.noGravity = true;
            npc.knockBackResist = 0.2f * Main.expertKnockBack;
            npc.damage = npc.defDamage;
            if (Main.player[npc.target].GetModPlayer<AAPlayer>().ZoneInferno == false)
            {
                DespawnAttempt = true;
            }

            else if (npc.target < 0 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
                Vector2 vector204 = Main.player[npc.target].Center - npc.Center;
                if (Main.player[npc.target].dead || vector204.Length() > 3000f)
                {
                    DespawnAttempt = true;
                }
            }
            else
            {
                Vector2 vector205 = Main.player[npc.target].Center - npc.Center;
                if (npc.ai[0] > 1f && vector205.Length() > 1000f)
                {
                    DespawnAttempt = false;
                    npc.ai[0] = 1f;
                }
            }
            if (DespawnAttempt == true)
            {
                Vector2 value50 = new Vector2(0f, -8f);
                npc.velocity = ((npc.velocity * 3f) + value50) / 10f;
                npc.noTileCollide = true;
            }
            if (npc.ai[0] == 0f)
            {
                npc.TargetClosest(true);
                if (npc.Center.X < Main.player[npc.target].Center.X - 2f)
                {
                    npc.direction = 1;
                }
                if (npc.Center.X > Main.player[npc.target].Center.X + 2f)
                {
                    npc.direction = -1;
                }
                npc.spriteDirection = npc.direction;
                npc.rotation = ((npc.rotation * 9f) + (npc.velocity.X * 0.4f)) / 10f;
                if (npc.collideX)
                {
                    npc.velocity.X = npc.velocity.X * (-npc.oldVelocity.X * 0.6f);
                    if (npc.velocity.X > 5f)
                    {
                        npc.velocity.X = 5f;
                    }
                    if (npc.velocity.X < -5f)
                    {
                        npc.velocity.X = -5f;
                    }
                }
                if (npc.collideY)
                {
                    npc.velocity.Y = npc.velocity.Y * (-npc.oldVelocity.Y * 0.6f);
                    if (npc.velocity.Y > 5f)
                    {
                        npc.velocity.Y = 5f;
                    }
                    if (npc.velocity.Y < -5f)
                    {
                        npc.velocity.Y = -5f;
                    }
                }
                Vector2 value51 = Main.player[npc.target].Center - npc.Center;
                value51.Y -= 200f;
                if (value51.Length() > 800f)
                {
                    npc.ai[0] = 1f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                }
                else if (value51.Length() > 80f)
                {
                    float scaleFactor15 = 6f;
                    float num1306 = 30f;
                    value51.Normalize();
                    value51 *= scaleFactor15;
                    npc.velocity = ((npc.velocity * (num1306 - 1f)) + value51) / num1306;
                }
                else if (npc.velocity.Length() > 2f)
                {
                    npc.velocity *= 0.99f;
                }
                else if (npc.velocity.Length() < 1f)
                {
                    npc.velocity *= 1.11f;
                }
                npc.ai[1] += 1f;
                if (npc.justHit)
                {
                    npc.ai[1] += (float)Main.rand.Next(10, 30);
                }
                if (npc.ai[1] >= 180f && Main.netMode != 1)
                {
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                    npc.netUpdate = true;
                    while (npc.ai[0] == 0f)
                    {
                        int num1307 = Main.rand.Next(3);
                        if (num1307 == 0 && Collision.CanHit(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1))
                        {
                            npc.ai[0] = 2f;
                        }
                        else if (num1307 == 1)
                        {
                            npc.ai[0] = 3f;
                        }
                        else if (num1307 == 2 && NPC.CountNPCS(mod.NPCType("BroodEgg")) + NPC.CountNPCS(mod.NPCType("Broodmini")) < num1305)
                        {
                            npc.ai[0] = 4f;
                        }
                    }
                    return;
                }
            }
            else
            {
                if (npc.ai[0] == 1f)
                {
                    npc.collideX = false;
                    npc.collideY = false;
                    npc.noTileCollide = true;
                    npc.knockBackResist = 0f;
                    if (npc.target < 0 || !Main.player[npc.target].active || Main.player[npc.target].dead)
                    {
                        npc.TargetClosest(true);
                    }
                    if (npc.velocity.X < 0f)
                    {
                        npc.direction = -1;
                    }
                    else if (npc.velocity.X > 0f)
                    {
                        npc.direction = 1;
                    }
                    npc.spriteDirection = npc.direction;
                    npc.rotation = ((npc.rotation * 9f) + (npc.velocity.X * 0.10f)) / 10f;
                    Vector2 value52 = Main.player[npc.target].Center - npc.Center;
                    if (value52.Length() < 300f && !Collision.SolidCollision(npc.position, npc.width, npc.height))
                    {
                        npc.ai[0] = 0f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                        npc.ai[3] = 0f;
                    }
                    float scaleFactor16 = 7f + (value52.Length() / 100f);
                    float num1308 = 25f;
                    value52.Normalize();
                    value52 *= scaleFactor16;
                    npc.velocity = ((npc.velocity * (num1308 - 1f)) + value52) / num1308;
                    return;
                }
                if (npc.ai[0] == 2f)
                {
                    npc.damage = (int)((double)npc.defDamage * 0.5);
                    npc.knockBackResist = 0f;
                    if (npc.target < 0 || !Main.player[npc.target].active || Main.player[npc.target].dead)
                    {
                        npc.TargetClosest(true);
                        npc.ai[0] = 0f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                        npc.ai[3] = 0f;
                    }
                    if (Main.player[npc.target].Center.X - 10f < npc.Center.X)
                    {
                        npc.direction = -1;
                    }
                    else if (Main.player[npc.target].Center.X + 10f > npc.Center.X)
                    {
                        npc.direction = 1;
                    }
                    npc.spriteDirection = npc.direction;
                    npc.rotation = ((npc.rotation * 4f) + (npc.velocity.X * 0.1f)) / 5f;
                    if (npc.collideX)
                    {
                        npc.velocity.X = npc.velocity.X * (-npc.oldVelocity.X * 0.5f);
                        if (npc.velocity.X > 5f)
                        {
                            npc.velocity.X = 5f;
                        }
                        if (npc.velocity.X < -5f)
                        {
                            npc.velocity.X = -5f;
                        }
                    }
                    if (npc.collideY)
                    {
                        npc.velocity.Y = npc.velocity.Y * (-npc.oldVelocity.Y * 0.5f);
                        if (npc.velocity.Y > 5f)
                        {
                            npc.velocity.Y = 5f;
                        }
                        if (npc.velocity.Y < -5f)
                        {
                            npc.velocity.Y = -5f;
                        }
                    }
                    Vector2 value53 = Main.player[npc.target].Center - npc.Center;
                    value53.Y -= 20f;
                    npc.ai[2] += 0.0222222228f;
                    if (Main.expertMode)
                    {
                        npc.ai[2] += 0.0166666675f;
                    }
                    float scaleFactor17 = 4f + npc.ai[2] + (value53.Length() / 120f);
                    float num1309 = 20f;
                    value53.Normalize();
                    value53 *= scaleFactor17;
                    npc.velocity = ((npc.velocity * (num1309 - 1f)) + value53) / num1309;
                    npc.ai[1] += 1f;
                    if (npc.ai[1] > 240f || !Collision.CanHit(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1))
                    {
                        npc.ai[0] = 0f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                        npc.ai[3] = 0f;
                        return;
                    }
                }
                else
                {
                    if (npc.ai[0] == 3f)
                    {
                        npc.knockBackResist = 0f;
                        npc.noTileCollide = true;
                        if (npc.velocity.X < 0f)
                        {
                            npc.direction = -1;
                        }
                        else
                        {
                            npc.direction = 1;
                        }
                        npc.spriteDirection = npc.direction;
                        npc.rotation = ((npc.rotation * 4f) + (npc.velocity.X * 0.09f)) / 5f;
                        Vector2 value54 = Main.player[npc.target].Center - npc.Center;
                        value54.Y -= 12f;
                        if (npc.Center.X > Main.player[npc.target].Center.X)
                        {
                            value54.X += 400f;
                        }
                        else
                        {
                            value54.X -= 400f;
                        }
                        if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) > 350f && Math.Abs(npc.Center.Y - Main.player[npc.target].Center.Y) < 20f)
                        {
                            npc.ai[0] = 3.1f;
                            npc.ai[1] = 0f;
                        }
                        npc.ai[1] += 0.0333333351f;
                        float scaleFactor18 = 8f + npc.ai[1];
                        float num1310 = 4f;
                        value54.Normalize();
                        value54 *= scaleFactor18;
                        npc.velocity = ((npc.velocity * (num1310 - 1f)) + value54) / num1310;
                        return;
                    }
                    if (npc.ai[0] == 3.1f)
                    {
                        npc.knockBackResist = 0f;
                        npc.noTileCollide = true;
                        npc.rotation = ((npc.rotation * 4f) + (npc.velocity.X * 0.09f)) / 5f;
                        Vector2 vector206 = Main.player[npc.target].Center - npc.Center;
                        vector206.Y -= 12f;
                        float scaleFactor19 = 16f;
                        float num1311 = 8f;
                        vector206.Normalize();
                        vector206 *= scaleFactor19;
                        npc.velocity = ((npc.velocity * (num1311 - 1f)) + vector206) / num1311;
                        if (npc.velocity.X < 0f)
                        {
                            npc.direction = -1;
                        }
                        else
                        {
                            npc.direction = 1;
                        }
                        npc.spriteDirection = npc.direction;
                        npc.ai[1] += 1f;
                        if (npc.ai[1] > 10f)
                        {
                            npc.velocity = vector206;
                            if (npc.velocity.X < 0f)
                            {
                                npc.direction = -1;
                            }
                            else
                            {
                                npc.direction = 1;
                            }
                            npc.ai[0] = 3.2f;
                            npc.ai[1] = 0f;
                            npc.ai[1] = (float)npc.direction;
                            return;
                        }
                    }
                    else
                    {
                        if (npc.ai[0] == 3.2f)
                        {
                            npc.damage = (int)((double)npc.defDamage * 1.3);
                            npc.collideX = false;
                            npc.collideY = false;
                            npc.knockBackResist = 0f;
                            npc.noTileCollide = true;
                            npc.ai[2] += 0.0333333351f;
                            npc.velocity.X = (16f + npc.ai[2]) * npc.ai[1];
                            if ((npc.ai[1] > 0f && npc.Center.X > Main.player[npc.target].Center.X + 260f) || (npc.ai[1] < 0f && npc.Center.X < Main.player[npc.target].Center.X - 260f))
                            {
                                if (!Collision.SolidCollision(npc.position, npc.width, npc.height))
                                {
                                    npc.ai[0] = 0f;
                                    npc.ai[1] = 0f;
                                    npc.ai[2] = 0f;
                                    npc.ai[3] = 0f;
                                }
                                else if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) > 800f)
                                {
                                    npc.ai[0] = 1f;
                                    npc.ai[1] = 0f;
                                    npc.ai[2] = 0f;
                                    npc.ai[3] = 0f;
                                }
                            }
                            npc.rotation = ((npc.rotation * 4f) + (npc.velocity.X * 0.09f)) / 5f;
                            return;
                        }
                        if (npc.ai[0] == 4f)
                        {
                            npc.ai[0] = 0f;
                            npc.TargetClosest(true);
                            if (Main.netMode != 1)
                            {
                                npc.ai[1] = -1f;
                                npc.ai[2] = -1f;
                                for (int num1312 = 0; num1312 < 1000; num1312++)
                                {
                                    int num1313 = (int)Main.player[npc.target].Center.X / 16;
                                    int num1314 = (int)Main.player[npc.target].Center.Y / 16;
                                    int num1315 = 30 + (num1312 / 50);
                                    int num1316 = 20 + (num1312 / 75);
                                    num1313 += Main.rand.Next(-num1315, num1315 + 1);
                                    num1314 += Main.rand.Next(-num1316, num1316 + 1);
                                    if (!WorldGen.SolidTile(num1313, num1314))
                                    {
                                        while (!WorldGen.SolidTile(num1313, num1314) && (double)num1314 < Main.worldSurface)
                                        {
                                            num1314++;
                                        }
                                        if ((new Vector2((float)((num1313 * 16) + 8), (float)((num1314 * 16) + 8)) - Main.player[npc.target].Center).Length() < 600f)
                                        {
                                            npc.ai[0] = 4.1f;
                                            npc.ai[1] = (float)num1313;
                                            npc.ai[2] = (float)num1314;
                                            break;
                                        }
                                    }
                                }
                            }
                            npc.netUpdate = true;
                            return;
                        }
                        if (npc.ai[0] == 4.1f)
                        {
                            if (npc.velocity.X < -2f)
                            {
                                npc.direction = -1;
                            }
                            else if (npc.velocity.X > 2f)
                            {
                                npc.direction = 1;
                            }
                            npc.spriteDirection = npc.direction;
                            npc.rotation = ((npc.rotation * 9f) + (npc.velocity.X * 0.1f)) / 10f;
                            npc.noTileCollide = true;
                            int num1317 = (int)npc.ai[1];
                            int num1318 = (int)npc.ai[2];
                            float x2 = (float)((num1317 * 16) + 8);
                            float y2 = (float)((num1318 * 16) - 20);
                            Vector2 vector207 = new Vector2(x2, y2);
                            vector207 -= npc.Center;
                            float num1319 = 6f + (vector207.Length() / 150f);
                            if (num1319 > 10f)
                            {
                                num1319 = 10f;
                            }
                            float num1320 = 10f;
                            if (vector207.Length() < 10f)
                            {
                                npc.ai[0] = 4.2f;
                            }
                            vector207.Normalize();
                            vector207 *= num1319;
                            npc.velocity = ((npc.velocity * (num1320 - 1f)) + vector207) / num1320;
                            return;
                        }
                        if (npc.ai[0] == 4.2f)
                        {
                            npc.rotation = ((npc.rotation * 9f) + (npc.velocity.X * 0.1f)) / 10f;
                            npc.knockBackResist = 0f;
                            npc.noTileCollide = true;
                            int num1321 = (int)npc.ai[1];
                            int num1322 = (int)npc.ai[2];
                            float x3 = (float)((num1321 * 16) + 8);
                            float y3 = (float)((num1322 * 16) - 20);
                            Vector2 vector208 = new Vector2(x3, y3);
                            vector208 -= npc.Center;
                            float num1323 = 4f;
                            float num1324 = 2f;
                            if (Main.netMode != 1 && vector208.Length() < 4f)
                            {
                                int num1325 = 70;
                                if (Main.expertMode)
                                {
                                    num1325 = (int)((double)num1325 * 0.75);
                                }
                                npc.ai[3] += 1f;
                                if (npc.ai[3] == (float)num1325)
                                {
                                    NPC.NewNPC((num1321 * 16) + 8, num1322 * 16, mod.NPCType("Broodmini"), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                                }
                                else if (npc.ai[3] == (float)(num1325 * 2))
                                {
                                    npc.ai[0] = 0f;
                                    npc.ai[1] = 0f;
                                    npc.ai[2] = 0f;
                                    npc.ai[3] = 0f;
                                    if (NPC.CountNPCS(mod.NPCType("BroodEgg")) + NPC.CountNPCS(mod.NPCType("Broodmini")) < num1305 && Main.rand.Next(3) != 0)
                                    {
                                        npc.ai[0] = 4f;
                                    }
                                    else if (Collision.SolidCollision(npc.position, npc.width, npc.height))
                                    {
                                        npc.ai[0] = 1f;
                                    }
                                }
                            }
                            if (vector208.Length() > num1323)
                            {
                                vector208.Normalize();
                                vector208 *= num1323;
                            }
                            npc.velocity = ((npc.velocity * (num1324 - 1f)) + vector208) / num1324;
                            return;
                        }
                    }
                }
            }
        }
    }
}