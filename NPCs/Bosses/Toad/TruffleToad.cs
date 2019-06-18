using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Bosses.Toad
{
    [AutoloadBossHead]
    public class TruffleToad : ModNPC
    {
        public float bossLife;

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
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
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Truffle Toad");
            Main.npcFrameCount[npc.type] = 12;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 3000;
            npc.damage = 30;
            npc.defense = 10;
            npc.knockBackResist = 0f;
            npc.value = Item.buyPrice(0, 1, 0, 0);
            npc.aiStyle = -1;
            npc.width = 98;
            npc.height = 72;
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = false;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/TODE");
            npc.netAlways = true;
            bossBag = mod.ItemType("ToadBag");
            npc.alpha = 255;
        }

        public static int AISTATE_JUMP = 0, AISTATE_BARF = 1, AISTATE_JUMPALOT = 2, AISTATE_TONGUE = 3;
        public float[] internalAI = new float[4];
        public int NOM = 0;
        public bool tonguespawned = false;
        public bool TongueAttack = false;

        public override void AI()
        {
            Player player = Main.player[npc.target]; // makes it so you can reference the player the npc is targetting
            AAModGlobalNPC.Toad = npc.whoAmI;

            if (player.dead || !player.active || !player.ZoneGlowshroom)
            {
                npc.TargetClosest();
                if (player.dead || !player.active || !player.ZoneGlowshroom)
                {
                    npc.alpha += 5;
                    if (npc.alpha >= 255)
                    {
                        npc.active = false;
                        npc.netUpdate = true;
                    }
                }
            }

            if (player != null)
            {
                float dist = npc.Distance(player.Center);
                if (dist > 800)
                {
                    npc.alpha += 5;
                    if (npc.alpha >= 255)
                    {
                        Vector2 tele = new Vector2(player.Center.X + (Main.rand.Next(2) == 0 ? 300 : -300), player.Center.Y - 16);
                        npc.Center = tele;
                        npc.netUpdate = true;
                    }
                }
                else
                {
                    npc.alpha -= 3;
                    if (npc.alpha <= 0)
                    {
                        npc.alpha = 0;
                    }
                }
            }

            if (npc.velocity.Y != 0)
            {
                if (npc.velocity.X < 0)
                {
                    npc.spriteDirection = 1;
                }
                else if (npc.velocity.X > 0)
                {
                    npc.spriteDirection = -1;
                }
            }
            else
            {
                if (player.position.X < npc.position.X)
                {
                    npc.spriteDirection = 1;
                }
                else if (player.position.X > npc.position.X)
                {
                    npc.spriteDirection = -1;
                }
            }

            if (internalAI[0] == AISTATE_JUMP)
            {
                npc.wet = false;
                BaseAI.AISlime(npc, ref npc.ai, true, 30, 6f, -8f, 6f, -10f);
                if (Main.netMode !=1)
                {
                    internalAI[1]++;
                }
                if (internalAI[1] >= 180)
                {
                    internalAI[1] = 0;
                    internalAI[0] = Main.rand.Next(2);
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
            }
            else if (internalAI[0] == AISTATE_BARF)
            {
                if (Main.netMode != 1)
                {
                    internalAI[1]++;
                }
                npc.velocity.X = 0;
                if (internalAI[1] >= 35)
                {
                    if (npc.velocity.Y == 0 && Main.netMode != 1)
                    {
                        internalAI[2]++;
                    }
                    if (internalAI[2] > 5)
                    {
                        internalAI[2] = 0;
                        if (npc.direction == -1)
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2(-6 + Main.rand.Next(0, 6), -4 + Main.rand.Next(-4, 0)), mod.ProjectileType("ToadBomb"), 35, 3);
                        }
                        else
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2(6 + Main.rand.Next(-6, 0), -4 + Main.rand.Next(-4, 0)), mod.ProjectileType("ToadBomb"), 35, 3);
                        }
                        npc.netUpdate = true;
                    }
                }
                if (internalAI[1] >= 100)
                {
                    internalAI[0] = AISTATE_JUMP;
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                    npc.netUpdate = true;
                }
            }
            else if (internalAI[0] == AISTATE_JUMPALOT)
            {
                internalAI[1]++; if (npc.ai[0] < -10) npc.ai[0] = -10; //force rapid jumping
                npc.wet = false;
                BaseAI.AISlime(npc, ref npc.ai, false, -10, 5, -5, 13, -13);
                if (Main.netMode != 1)
                {
                    internalAI[1]++;
                }
                if (internalAI[1] >= 180)
                {
                    internalAI[1] = 0;
                    internalAI[0] = Main.rand.Next(2);
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.velocity.Y == 0)
            {
                if (internalAI[0] == AISTATE_BARF)
                {
                    if (npc.frame.Y < frameHeight * 6)
                    {
                        npc.frame.Y = frameHeight * 6;
                    }
                    if (npc.frameCounter >= 10)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y += frameHeight;
                        if (npc.frame.Y > frameHeight * 11)
                        {
                            npc.frame.Y = frameHeight * 11;
                        }
                    }
                }
                else
                {
                    npc.frame.Y = 0;
                }
            }
            else
            {
                if (npc.frameCounter >= 10)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += frameHeight;
                    if (npc.frame.Y > (frameHeight * 4))
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = frameHeight * 4;
                    }
                }
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ToadTrophy"));
            }
            AAWorld.downedToad = true;
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ToadMask"));
                }
                string[] lootTable = { "MushrockStaff", "ToadTongue", "Todegun" };
                int loot = Main.rand.Next(lootTable.Length);
                npc.DropLoot(mod.ItemType(lootTable[loot]));
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 1.4f);  //boss damage increase in expermode
        }
    }
}


