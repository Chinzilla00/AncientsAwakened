using System.IO;
using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Raider
{

    [AutoloadBossHead]
    public class Raider : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Raider Ultima");
            Main.npcFrameCount[npc.type] = 8;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = 0;
            npc.width = 202;
            npc.height = 196;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.chaseable = true;
            npc.damage = 70;
            npc.defense = 30;
            npc.lifeMax = 30000;
            npc.value = Item.sellPrice(0, 10, 0, 0);
            npc.buffImmune[BuffID.Ichor] = true;
            npc.lavaImmune = true;
            npc.boss = true;
            npc.netAlways = true;
            npc.friendly = false;
            npc.HitSound = new LegacySoundStyle(3, 4, Terraria.Audio.SoundType.Sound);
            npc.DeathSound = new LegacySoundStyle(4, 14, Terraria.Audio.SoundType.Sound);
            bossBag = mod.ItemType("RaiderBag");
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Siege");
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public float[] internalAI = new float[6];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == 2 || Main.dedServ)
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
                writer.Write(internalAI[4]);
                writer.Write(internalAI[5]);
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
                internalAI[5] = reader.ReadFloat();
            }
        }


        public override void NPCLoot()
        {
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/RaiderGore1"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/RaiderGore2"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/RaiderGore3"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/RaiderGore4"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/RaiderGore5"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/RaiderGore6"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/RaiderGore7"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/RaiderGoreJaw"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/RaiderGoreHorn"), 1f);
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RaiderTrophy"));
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {

                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SoulofFright, Main.rand.Next(20, 40));
                if (Main.rand.Next(10) == 0)
                {
                    //Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BroodMask"));
                }
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RaidEgg"));
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FulguriteBar"), Main.rand.Next(30, 64));

            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;   //boss drops
            AAWorld.downedRaider = true;
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
                target.AddBuff(BuffID.Electrified, Main.rand.Next(100, 180));       //Main.rand.Next part is the length of the buff, so 8.3 seconds to 16.6 seconds
            }
        }

        public int projectileInterval = 300; //how long until you fire projectiles
        private int projectileTimer = 0;
        public int ProjectileChoice = 0;

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter > 8)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 192;
                bool isCharging = internalAI[1] == AISTATE_CHARGEATPLAYER; //all ai states between charges
                if (isCharging && (npc.frame.Y >= 192 * 8 || npc.frame.Y < 192 * 5))
                {
                    npc.frame.Y = 192 * 4;
                }
                else
                if (!isCharging && npc.frame.Y >= 192 * 4)
                {
                    npc.frame.Y = 192 * 0;
                }
            }
        }

        public Color color;

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/Raider_Glow1");
            Texture2D glowTex1 = mod.GetTexture("Glowmasks/Raider_Glow2");
            color = BaseUtility.MultiLerpColor(Main.player[Main.myPlayer].miscCounter % 100 / 100f, BaseDrawing.GetLightColor(npc.position), BaseDrawing.GetLightColor(npc.position), Color.Violet, BaseDrawing.GetLightColor(npc.position), Color.Violet, BaseDrawing.GetLightColor(npc.position));
            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc, color);
            BaseDrawing.DrawTexture(spritebatch, glowTex1, 0, npc, Color.White);
            return false;
        }

        int MaxMinions = Main.expertMode ? 8 : 5;
        private float pos = 250;
        public const float AISTATE_RUNAWAY = -1f, AISTATE_FLYABOVEPLAYER = 0f, AISTATE_ROCKETS = 1f, AISTATE_SHOCKBOMB = 2f, AISTATE_CHARGEATPLAYER = 3f, AISTATE_SPAWNEGGS = 4f;

        public override void AI()
        {
            Player player = Main.player[npc.target];

            int Minions = NPC.CountNPCS(mod.NPCType<RaidEgg>()) + NPC.CountNPCS(mod.NPCType<Raidmini>());
            color = BaseUtility.MultiLerpColor(Main.player[Main.myPlayer].miscCounter % 100 / 100f, BaseDrawing.GetLightColor(npc.position), BaseDrawing.GetLightColor(npc.position), Color.Violet, BaseDrawing.GetLightColor(npc.position), Color.Violet, BaseDrawing.GetLightColor(npc.position));

            Lighting.AddLight((int)(npc.Center.X + (npc.width / 2)) / 16, (int)(npc.position.Y + (npc.height / 2)) / 16, color.R / 255, color.G / 255, color.B / 255);

            npc.TargetClosest();
            if (Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest();
                if (Main.player[npc.target].dead || !Main.player[npc.target].active)
                {
                    internalAI[1] = AISTATE_RUNAWAY;
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
            }

            if (internalAI[1] == AISTATE_RUNAWAY)
            {
                npc.noTileCollide = true;
                npc.ai[1] = 0;
                npc.ai[2] = 0;
                npc.ai[3] = 0;
                internalAI[0]++;

                npc.dontTakeDamage = true;

                if (npc.timeLeft < 10)
                    npc.timeLeft = 10;
                npc.velocity.X *= 0.9f;

                if (internalAI[0] > 300)
                {
                    npc.velocity.Y -= 4;
                    npc.netUpdate = true;
                    if (npc.position.Y + npc.velocity.Y <= 0f && Main.netMode != 1) { BaseAI.KillNPC(npc); npc.netUpdate = true; }
                    return;
                }
                return;
            }

            if (Main.netMode != 1)
            {
                internalAI[0]++;
                if (internalAI[0] >= 180)
                {
                    internalAI[0] = 0;
                    internalAI[1] = Minions < MaxMinions ? Main.rand.Next(5) : Main.rand.Next(4);
                    npc.ai = new float[4];
                    if (internalAI[1] == AISTATE_FLYABOVEPLAYER)
                    {
                        npc.ai[1] = 1 + Main.rand.Next(2);
                    }
                    else
                    if (internalAI[1] == AISTATE_SPAWNEGGS)
                    {
                        npc.ai[1] = npc.ai[1] == 0 ? 1 : 0;
                    }
                    if (internalAI[1] == AISTATE_CHARGEATPLAYER)
                    {
                        SelectPoint = true;
                        npc.netUpdate = true;
                    }
                }
            }
            pos = npc.ai[1] == 0 ? -250 : 250;

            if (Main.dayTime)
            {
                internalAI[1] = AISTATE_RUNAWAY;
                npc.ai = new float[4];
                npc.netUpdate = true;
            }

            Vector2 wantedVelocity = player.Center - new Vector2(pos, 250);
            MoveToPoint(wantedVelocity);

            if (Main.dayTime)
            {
                internalAI[1] = AISTATE_RUNAWAY;
                npc.netUpdate = true;
            }

            if (internalAI[1] == AISTATE_ROCKETS)
            {
                if (Main.netMode != 1)
                {
                    for (int i = 0; i < (Main.expertMode ? 5 : 4); i++)
                    {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("RaidRocket"), 0);
                    }
                    internalAI[0] = 0;
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }

            }
            else if (internalAI[1] == AISTATE_SPAWNEGGS)
            {
                if (Main.netMode != 1)
                {
                    projectileTimer++;
                    if (projectileTimer > 20)
                    {
                        projectileTimer = 0;
                        Vector2 firePos = new Vector2(npc.Center.X + (32 * npc.direction), npc.Center.Y + 40f);
                        firePos = BaseUtility.RotateVector(npc.Center, firePos, npc.rotation); //+ (npc.direction == -1 ? (float)Math.PI : 0f)));
                        if (Minions < MaxMinions)
                        {
                            int NPCID = NPC.NewNPC((int)firePos.X, (int)firePos.Y, mod.NPCType<RaidEgg>(), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                            Main.npc[NPCID].velocity.Y = 4f;
                            Main.npc[NPCID].netUpdate = true;
                        }
                        npc.netUpdate = true;
                    }
                }
            }
            else if (internalAI[1] == AISTATE_CHARGEATPLAYER)
            {
                if (Main.netMode != 1)
                {
                    if (SelectPoint)
                    {
                        float Point = 500 * npc.direction;
                        MovePoint = player.Center + new Vector2(Point, 500f);
                        SelectPoint = false;
                        internalAI[5] = 1;
                        npc.netUpdate = true;
                    }
                }
                Charge(MovePoint);

                if (Vector2.Distance(npc.Center, MovePoint) < 5)
                {
                    internalAI[0] = 0;
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                    internalAI[5] = 0;
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
            }
            else if (internalAI[1] == AISTATE_SHOCKBOMB)
            {
                if (Main.netMode != 1) //only fire bombs when (attempting to) fly above the player
                {
                    projectileTimer++;
                    if (projectileTimer >= projectileInterval && projectileTimer % 10 == 0)
                    {
                        if (projectileTimer > (projectileInterval + 50))
                            projectileTimer = 0;
                        Vector2 dir = new Vector2(npc.velocity.X * 2f + (4f * npc.direction), npc.velocity.Y * 0.5f + 1f);
                        Vector2 firePos = new Vector2(npc.Center.X + (64 * npc.direction), npc.Center.Y + 28f);
                        firePos = BaseUtility.RotateVector(npc.Center, firePos, npc.rotation); //+ (npc.direction == -1 ? (float)Math.PI : 0f)));
                        int projID = Projectile.NewProjectile(firePos, dir, mod.ProjectileType("RaidSphere"), npc.damage / Main.expertMode ? 2 : 4, 1, 255);
                        Main.projectile[projID].netUpdate = true;
                    }
                }
            }
            if (internalAI[5] == 1 && Main.netMode != 1)
            {
                internalAI[5] = 2;
                npc.netUpdate = true;
            }
            else if (internalAI[5] == 2 && Main.netMode != 1)
            {
                npc.netUpdate = false;
            }
        }

        public Vector2 MovePoint;
        public bool SelectPoint = false;

        public void Charge(Vector2 point)
        {
            float MeleeSpeed = 18f;
            float velMultiplier = 1f;
            Vector2 dist = point - npc.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < MeleeSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / MeleeSpeed);
            }
            if (length < 200f)
            {
                MeleeSpeed *= 0.5f;
            }
            if (length < 100f)
            {
                MeleeSpeed *= 0.5f;
            }
            if (length < 50f)
            {
                MeleeSpeed *= 0.5f;
            }
            npc.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            npc.velocity *= MeleeSpeed;
            npc.velocity *= velMultiplier;
        }

        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 9f;
            float velMultiplier = 1f;
            Vector2 dist = point - npc.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
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
            npc.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            npc.velocity *= moveSpeed;
            npc.velocity *= velMultiplier;
        }
    }
}