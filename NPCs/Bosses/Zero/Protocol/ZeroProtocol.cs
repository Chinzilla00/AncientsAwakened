using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace AAMod.NPCs.Bosses.Zero.Protocol
{
    [AutoloadBossHead]
    public class ZeroProtocol : ModNPC
    {
        public int timer;
        public static int type;
        public int damage = 0;
        public bool PlayerDead = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ZER0 PR0T0C0L");
            Main.npcFrameCount[npc.type] = 15; 
            NPCID.Sets.TrailCacheLength[npc.type] = 15;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 600000;
            npc.damage = 67;
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
            if (AAWorld.downedAllAncients)
            {
                npc.lifeMax = 700000;
                npc.damage = 220;
                npc.defense = 300;
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = (int)(npc.damage * .7f);
            npc.lifeMax = (int)(npc.lifeMax * 0.5f * bossLifeScale);
        }

        public override void NPCLoot()
        {
            if (Main.expertMode)
            {

                if (!AAWorld.downedZero)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("Doomstone stops glowing. You can now mine it.", Color.Silver);
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
                if (AAWorld.downedShen)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EXSoul"));
                }
                if (Main.rand.Next(50) == 0 && AAWorld.downedAllAncients)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RealityStone"));
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
                        npc.ai[0] = 0;
                        internalAI[3] = 1;
                        npc.netUpdate = true;
                    }
                }
            }
            if (npc.life <= 0 && !Main.expertMode)
            {
                if (Main.netMode != 1) BaseUtility.Chat("CHEATER ALERT CHEATER ALERT. N0 DR0PS 4 U", Color.Red.R, Color.Red.G, Color.Red.B);
            }
        }

        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/ZeroProtocol_Glow");
            float Color = Math.Abs(Main.GameUpdateCount) / 0.5f;
            float Flash = 1f * (float)Math.Sin(Color);
            Color color1 = Microsoft.Xna.Framework.Color.Lerp(Microsoft.Xna.Framework.Color.Red, Microsoft.Xna.Framework.Color.Black, Flash);
            Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.5f);
            for (int k = 0; k < npc.oldPos.Length; k++)
            {
                Texture2D ZeroTrail = mod.GetTexture("NPCs/Bosses/Zero/Protocol/ZeroTrail");
                Color color = npc.GetAlpha(color1) * ((npc.oldPos.Length - k) / (float)npc.oldPos.Length);
                spritebatch.Draw(ZeroTrail, npc.position, null, color, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0f);
            }
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseDrawing.DrawAura(spritebatch, glowTex, 0, npc, auraPercent, 1f, 0f, 0f, color1);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc, color1);
            return false;
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (AAWorld.Anticheat == true)
            {
                if (damage > npc.lifeMax / 8)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("Y0UR CHEAT SHEET BUTCHER T00L WILL N0T SAVE Y0U HERE", Color.Red);
                    damage = 0;
                }

                return false;
            }

            return true;
        }

        public float[] internalAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == 2 || Main.dedServ)
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

        float XPos = 0;
        float YPos = -400;
        bool isGlitching = false;
        bool SelectPoint = false;
        Vector2 ChargePoint = Vector2.Zero;

        public override void AI()
        {

            npc.TargetClosest();
            Player player = Main.player[npc.target];

            bool tooFar = Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 10000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 10000f;

            Vector2 Position = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
            float PlayerX = player.position.X + (player.width / 2) - Position.X;
            float PlayerY = player.position.Y + (player.height / 2) - Position.Y;
            npc.rotation = (float)Math.Atan2(PlayerX, PlayerY);

            if (player.Center.X > npc.Center.X)
            {
                npc.direction = 1;
            }
            else
            {
                npc.direction = -1;
            }

            if (player.dead || tooFar)
            {
                npc.TargetClosest(true);

                if (Main.player[npc.target].dead || tooFar)
                {
                    if (!PlayerDead)
                    {
                        if (player.dead)
                        {
                            if (Main.netMode != 1) BaseUtility.Chat("TARGET NEUTRALIZED. RETURNING T0 0RBIT.", Color.Red.R, Color.Red.G, Color.Red.B);
                        }
                        else if (tooFar)
                        {
                            if (Main.netMode != 1) BaseUtility.Chat("TARGET L0ST. RETURNING T0 0RBIT.", Color.Red.R, Color.Red.G, Color.Red.B);
                        }
                        PlayerDead = true;
                    }
                    npc.velocity.Y = npc.velocity.Y - 0.04f;
                    if (npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                    }
                    if (npc.position.Y + npc.height - npc.velocity.Y <= 0 && Main.netMode != 1) { BaseAI.KillNPC(npc); npc.netUpdate2 = true; }
                    return;
                }
                
            }

            if (npc.ai[1] == 0)
            {
                internalAI[3] = 1;
                npc.ai[1] = 1;
                npc.netUpdate = true;
            }

            if (npc.ai[0]++ > 300)
            {
                if (Main.netMode != 1)
                {
                    switch (Main.rand.Next(2))
                    {
                        case 0: YPos = 0; break;
                        default: YPos = -400; break;
                    }

                    switch (YPos == 0 ? Main.rand.Next(2) : Main.rand.Next(3)) //To prevent ZP from locking to the player's position
                    {
                        case 0: XPos = -400; break;
                        case 1: XPos = 400; break;
                        default: XPos = 0; break;
                    }
                    internalAI[3] = 1;
                    npc.ai[0] = 0;
                    npc.netUpdate = true;
                }
            }
            else
            {
                if (internalAI[3] == 0) //Regular Movement
                {
                    MoveToPoint(new Vector2(player.position.X + XPos, player.position.Y + YPos));
                }
                if (internalAI[3] == 1) //Teleport
                {
                    npc.velocity *= 0;
                    npc.ai[0] = 0;
                    npc.ai[2]++;
                    if (npc.ai[2] >= 30)
                    {
                        npc.Center = new Vector2(player.Center.X + XPos, player.Center.Y + YPos);
                    }
                    if (npc.ai[2] >= 70 && Main.netMode != 1)
                    {
                        internalAI[3] = 0; npc.ai[2] = 0; npc.netUpdate = true;
                    }
                }
            }

            internalAI[0]++;
            if (internalAI[0] >= 240 && Main.netMode != 1)
            {
                Attack(Main.rand.Next(4));
                internalAI[0] = 0;
            }
            
            int Proj = Main.rand.Next(2);
            switch (Proj) //switch for attack modes
            {
                case 0:
                    Proj = mod.ProjectileType<DeathLaser>();
                    break;
                default:
                    Proj = mod.ProjectileType<GlitchBomb>();
                    break;
            }
            BaseAI.ShootPeriodic(npc, player.position, player.width, player.height, Proj, ref npc.ai[3], 150, npc.damage / 4, 10, true);
        }

        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 16f;
            if (internalAI[3] == 2)
            {
                moveSpeed = 26f;
            }
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

        int Frame = 0;
        public override void FindFrame(int frameHeight)
        {
            if (npc.frameCounter++ > 10)
            {
                npc.frameCounter = 0;
                Frame += 1;
            }

            if (internalAI[3] == 1)
            {
                if (Frame < 8)
                {
                    Frame = 8;
                }
                if (Frame > 14)
                {
                    Frame = 14;
                }
            }
            else
            {
                if (isGlitching)
                {
                    if (Frame > 7)
                    {
                        Frame = 0;
                        isGlitching = false;
                    }
                }
                else
                {
                    if (Frame > 3)
                    {
                        Frame = 0;
                    }
                }
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
                        NPC.NewNPC((int)npc.Center.X + 30, (int)npc.Center.Y + 30, mod.NPCType<NullZP>());
                    }
                    else if (i == 2)
                    {
                        NPC.NewNPC((int)npc.Center.X + 30, (int)npc.Center.Y - 30, mod.NPCType<NullZP>());
                    }
                    else if (i == 3)
                    {
                        NPC.NewNPC((int)npc.Center.X - 30, (int)npc.Center.Y + 30, mod.NPCType<NullZP>());
                    }
                    else
                    {
                        NPC.NewNPC((int)npc.Center.X - 30, (int)npc.Center.Y - 30, mod.NPCType<NullZP>());
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
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(Math.Sin(offsetAngle) * 4f), (float)(Math.Cos(offsetAngle) * 2f), mod.ProjectileType("GlitchRocket"), damage, 0, Main.myPlayer, 0f, 0f);
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
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(Math.Sin(offsetAngle) * 4f), (float)(Math.Cos(offsetAngle) * 2f), mod.ProjectileType("Error"), damage, 0, Main.myPlayer, 0f, 0f);
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
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(Math.Sin(offsetAngle) * 2), (float)Math.Cos(offsetAngle), mod.ProjectileType("StaticSphere"), damage, 0, Main.myPlayer, 0f, 0f);
                }
            }
        }
    }
}
