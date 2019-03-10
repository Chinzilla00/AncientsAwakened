using System;
using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Grips;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Shaders;
using System.IO;

namespace AAMod.NPCs.Bosses.Zero.Protocol
{
    [AutoloadBossHead]
    public class ZeroProtocol : ModNPC
    {

        private bool Killed = false;
        public int timer;
        public static int type;
        private bool Panic = false;
        private bool introPlayed = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zero Protocol Beta");
            Main.npcFrameCount[npc.type] = 4;
            NPCID.Sets.TrailCacheLength[npc.type] = 15;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 120000;
            npc.damage = 140;
            npc.defense = 110;
            npc.knockBackResist = 0f;
            npc.width = 78;
            npc.height = 78;
            npc.friendly = false;
            npc.aiStyle = 0;
            npc.value = Item.buyPrice(2, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/Sounds/Zerohit");
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/Sounds/ZeroDeath");
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Zero2");
            npc.netAlways = true;
            bossBag = mod.ItemType("ZeroBag");
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public Vector2 offsetBasePoint = Vector2.Zero;
        public float[] internalAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
                writer.Write(offsetBasePoint.X);
                writer.Write(offsetBasePoint.Y);
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
                offsetBasePoint.X = reader.ReadFloat();
                offsetBasePoint.Y = reader.ReadFloat();
            }
        }

        public override void NPCLoot()
        {
            if (Main.expertMode)
            {
                npc.DropLoot(Items.Vanity.Mask.ZeroMask.type, 1f / 7);
                npc.DropLoot(Items.Boss.Zero.ZeroTrophy.type, 1f / 10);
                if (Main.rand.NextFloat() < 0.1f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EXSoul"));
                }
                if (Main.rand.NextFloat() < 0.05f && AAWorld.RealityDropped == false)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RealityStone"));
                    AAWorld.RealityDropped = true;
                }
                npc.DropBossBags();
                return;
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;   //boss drops
            AAWorld.downedZero = true;
            Projectile.NewProjectile((new Vector2(npc.Center.X, npc.Center.Y)), (new Vector2(0f, 0f)), mod.ProjectileType("ZeroDeath1"), 0, 0);
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            float Eggroll = Math.Abs(Main.GameUpdateCount) / 0.5f;
            float Pie = 1f * (float)Math.Sin(Eggroll);
            Color color1 = Color.Lerp(Color.Red, Color.Black, Pie);

            if (npc.life <= npc.lifeMax / 3 && Panic == false && !AAWorld.downedZero)
            {
                Panic = true;
                Main.NewText("WARNING. DRASTIC DAMAGE DETECTED, FAILURE IMMINENT. ENGAGE T0TAL 0FFENCE PR0T0C0L", Color.Red.R, Color.Red.G, Color.Red.B);
            }
            if (npc.life <= npc.lifeMax / 3 && Panic == false && AAWorld.downedZero)
            {
                Panic = true;
                Main.NewText("WARNING. DRASTIC DAMAGE DETECTED, FAILURE IMMINENT AGAIN. ENGAGE T0TAL 0FFENCE PR0T0C0L 0MEGA", Color.Red.R, Color.Red.G, Color.Red.B);
            }
            if (damage > 30)
            {
                if (Main.rand.Next(0, 10) == 0)
                {
                    int Xint = Main.rand.Next(-400, 400);
                    int Yint = Main.rand.Next(-400, 400);
                    if ((Xint < -100 || Xint > 100) && (Yint < -90 || Yint > 90))
                    {
                        //Main.NewText("CALLED! XINT: " + Xint + ". YINT: " + Yint);
                        Player player = Main.player[npc.target];
                        Vector2 tele = new Vector2((player.Center.X + Xint), (player.Center.Y + Yint));
                        npc.Center = tele;
                    }
                }
            }
            if (npc.life <= 0 && !Main.expertMode && npc.type == mod.NPCType<ZeroAwakened>())
            {
                Main.NewText("CHEATER ALERT CHEATER ALERT. N0 DR0PS 4 U", Color.Red.R, Color.Red.G, Color.Red.B);
            }
        }

        public static Texture2D glowTex = null;
        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            if (glowTex == null)
            {
                glowTex = mod.GetTexture("Glowmasks/ZeroAwakened_Glow");
            }
            Texture2D tex = Main.npcTexture[npc.type];
            if (GlitchBool)
            {
                tex = mod.GetTexture("NPCs/Bosses/Zero/Protocol/ZeroProtocol1");
            }

            Texture2D ZeroTrail = mod.GetTexture("NPCs/Bosses/Zero/Protocol/ZeroTrail");
            float Eggroll = Math.Abs(Main.GameUpdateCount) / 0.5f;
            float Pie = 1f * (float)Math.Sin(Eggroll);
            Color color1 = Color.Lerp(Color.Red, Color.Black, Pie);
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, npc.height * 0.5f);
            BaseDrawing.DrawAfterimage(spritebatch, ZeroTrail, 0, npc, 1f, 1f, 10, false, 0f, 0f, AAColor.Oblivion);
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            BaseDrawing.DrawTexture(spritebatch, tex, 0, npc, dColor);
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
                    Main.NewText("Y0UR CHEAT SHEET BUTCHER T00L WILL N0T SAVE Y0U HERE", Color.Red);
                    damage = 0;
                }

                return false;
            }

            return true;
        }
        private int Glitch = 0;
        private bool GlitchBool = false;

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter >= 10)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 170;
                if (npc.frame.Y > (108 * 3))
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = 0;
                    GlitchBool = false;
                }
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.8f);
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }
        public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
        {
            spriteEffects = (npc.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
        }

        public float moveSpeed = 10f;
        public int MinionTimer = 0;


        public override void AI()
        {
            npc.TargetClosest();
            Player targetPlayer = Main.player[npc.target];

            offsetBasePoint = new Vector2(250, -250f);
            
            Glitch = Main.rand.Next(8);
            if (Glitch == 0)
            {
                GlitchBool = true;
            }

            internalAI[2]++;
            if (internalAI[2] > 240)
            {

                int RandPos = Main.rand.Next(5);

                switch (RandPos)
                {
                    case 0:
                        offsetBasePoint = new Vector2(-250, 0);
                        break;
                    case 1:
                        offsetBasePoint = new Vector2(250, 0);
                        break;
                    case 2:
                        offsetBasePoint = new Vector2(-250, -250f);
                        break;
                    case 3:
                        offsetBasePoint = new Vector2(250, -250f);
                        break;
                    default:
                        offsetBasePoint = new Vector2(0, -250);
                        break;
                }
                internalAI[2] = 0;
            }

            if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
            {
                npc.TargetClosest(false);
                DespawnHandler();
                return;
            }

            if (targetPlayer.dead)
            {
                if (Killed == false)
                {
                    Main.NewText("TARGET NEUTRALIZED. RETURNING T0 0RBIT.", Color.Red.R, Color.Red.G, Color.Red.B);
                    Killed = true;
                }
                npc.TargetClosest(false);
                Panic = false;
                music = 0;
                npc.velocity.Y = npc.velocity.Y - 0.04f;
                if (npc.timeLeft > 10)
                {
                    npc.timeLeft = 10;
                }
                if (npc.position.Y + npc.height - npc.velocity.Y <= 0 && Main.netMode != 1) { BaseAI.KillNPC(npc); npc.netUpdate2 = true; }
                return;
            }
            if (Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
            {
                if (Killed == false)
                {
                    Main.NewText("TARGET L0ST. RETURNING T0 0RBIT.", Color.Red.R, Color.Red.G, Color.Red.B);
                    Killed = true;
                }
                npc.TargetClosest(false);
                Panic = false;
                music = 0;
                npc.velocity.Y = npc.velocity.Y - 0.04f;
                if (npc.timeLeft > 10)
                {
                    npc.timeLeft = 10;
                }
                if (npc.position.Y + npc.height - npc.velocity.Y <= 0 && Main.netMode != 1) { BaseAI.KillNPC(npc); npc.netUpdate2 = true; }
                return;
            }
            bool forceChange = false;
            if (Main.netMode != 1 && npc.ai[0] != 2 && npc.ai[0] != 3)
            {
                int stopValue = 100;
                npc.ai[3]++;
                if (npc.ai[3] > stopValue) npc.ai[3] = stopValue;
                forceChange = npc.ai[3] >= stopValue;
            }
            if (npc.ai[0] == 1) //move to starting charge position
            {
                moveSpeed = 15;
                Vector2 point = targetPlayer.Center + offsetBasePoint + new Vector2(0f, -250f);
                MoveToPoint(point);
                if (Main.netMode != 1 && (Vector2.Distance(npc.Center, point) < 10f || forceChange))
                {
                    npc.ai[0] = 2;
                    npc.ai[1] = 0;
                    npc.ai[2] = 0;
                    npc.ai[3] = 0;
                    npc.netUpdate = true;
                }
                BaseAI.LookAt(targetPlayer.Center, npc, 0, 0f, 0.1f, false);
            }
            else
            if (npc.ai[0] == 2) //dive down
            {
                moveSpeed = 30f;
                Vector2 targetCenter = new Vector2(npc.ai[1], npc.ai[2]);
                Vector2 point = targetCenter - offsetBasePoint + new Vector2(0f, 250f);
                MoveToPoint(point);
                if (Main.netMode != 1 && Vector2.Distance(npc.Center, point) < 10f)
                {
                    npc.ai[0] = 3;
                    npc.ai[1] = targetPlayer.Center.X;
                    npc.ai[2] = targetPlayer.Center.Y;
                    npc.ai[3] = 0;
                    npc.netUpdate = true;
                }
                BaseAI.Look(npc, 0, 0f, 0.1f, false);
            }
            else
            if (npc.ai[0] == 3) //dive up
            {
                moveSpeed = 30f;
                Vector2 targetCenter = new Vector2(npc.ai[1], npc.ai[2]);
                Vector2 point = targetCenter + offsetBasePoint + new Vector2(0f, -250f);
                MoveToPoint(point);
                if (Main.netMode != 1 && Vector2.Distance(npc.Center, point) < 10f)
                {
                    bool TripleDive = (npc.life < npc.lifeMax / 2);
                    npc.ai[0] = 4;
                    npc.ai[1] = targetPlayer.Center.X;
                    npc.ai[2] = targetPlayer.Center.Y;
                    npc.ai[3] = 0;
                    npc.netUpdate = true;
                }
                BaseAI.Look(npc, 0, 0f, 0.1f, false);
            }
            else
            if (npc.ai[0] == 4) //dive back down
            {
                moveSpeed = 30f;
                Vector2 targetCenter = new Vector2(npc.ai[1], npc.ai[2]);
                Vector2 point = targetCenter - offsetBasePoint + new Vector2(0f, 250f);
                MoveToPoint(point);
                if (Main.netMode != 1 && Vector2.Distance(npc.Center, point) < 10f)
                {
                    npc.ai[0] = 0;
                    npc.ai[1] = 0;
                    npc.ai[2] = 0;
                    npc.ai[3] = 0;
                    npc.netUpdate = true;
                }
                BaseAI.Look(npc, 0, 0f, 0.1f, false);
            }
            else //standard movement
            {
                moveSpeed = 12f;
                Vector2 point = targetPlayer.Center + offsetBasePoint;
                MoveToPoint(point);
                int ShootThis = mod.ProjectileType<GlitchBomb>();
                internalAI[0] = Main.rand.Next(2);
                ShootThis = mod.ProjectileType<GlitchBomb>();
                if (internalAI[0] == 0)
                {
                    ShootThis = mod.ProjectileType<DeathLaser>();
                }
                else
                {
                    ShootThis = mod.ProjectileType<GlitchBomb>();
                }
                BaseAI.ShootPeriodic(npc, new Vector2(targetPlayer.position.X, targetPlayer.position.Y), targetPlayer.width, targetPlayer.height, ShootThis, ref internalAI[3], 90, (int)(npc.damage * (Main.expertMode ? 0.5f : 0.25f)), 10f, true, new Vector2(20f, 15f));
                if (Main.netMode != 1)
                {
                    npc.ai[1]++;
                    if (npc.ai[1] > 150)
                    {
                        npc.ai[0] = 1;
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                        npc.ai[3] = 0;
                        npc.netUpdate = true;
                    }
                }
                BaseAI.LookAt(targetPlayer.Center, npc, 0, 0f, 0.1f, false);
            }

            if (internalAI[1] == 1)
            {
                Attack(Main.rand.Next(4));
                internalAI[1] = 0;
            }
        }



        public void MoveToPoint(Vector2 point, bool goUpFirst = false)
        {
            if (moveSpeed == 0f || npc.Center == point) return; //don't move if you have no move speed
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

        private void DespawnHandler()
        {
            Player player = Main.player[npc.target];
            npc.TargetClosest(false);
            player = Main.player[npc.target];
            if (!player.active || player.dead || Main.dayTime)        // If the player is dead and not active, the npc flies off-screen and despawns
            {
                npc.velocity.X = 0;
                npc.velocity.Y -= 1;
            }
        }



        public void Attack(int Attack)
        {
            Player player = Main.player[npc.target];

            if (Attack == 0 && NPC.CountNPCS(mod.NPCType<NullZP>()) > 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i == 1)
                    {
                        NPC.NewNPC((int)npc.Center.X + 10, (int)npc.Center.Y + 10, mod.NPCType<NullZP>());
                    }
                    else if (i == 2)
                    {
                        NPC.NewNPC((int)npc.Center.X + 10, (int)npc.Center.Y - 10, mod.NPCType<NullZP>());
                    }
                    else if (i == 3)
                    {
                        NPC.NewNPC((int)npc.Center.X - 10, (int)npc.Center.Y + 10, mod.NPCType<NullZP>());
                    }
                    else
                    {
                        NPC.NewNPC((int)npc.Center.X - 10, (int)npc.Center.Y - 10, mod.NPCType<NullZP>());
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
                    offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(Math.Sin(offsetAngle) * 6f), (float)(Math.Cos(offsetAngle) * 6f), mod.ProjectileType("GlitchRocket"), npc.damage / 2, 0, Main.myPlayer, 0f, 0f);
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
                    offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(Math.Sin(offsetAngle) * 6f), (float)(Math.Cos(offsetAngle) * 6f), mod.ProjectileType("ERROR"), npc.damage / 2, 0, Main.myPlayer, 0f, 0f);
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
                    offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(Math.Sin(offsetAngle) * 6f), (float)(Math.Cos(offsetAngle) * 6f), mod.ProjectileType("VoidBreaker"), npc.damage / 2, 0, Main.myPlayer, 0f, 0f);
                }
            }
            else
            {
                float spread = 12f * 0.0174f;
                double startAngle = Math.Atan2(npc.velocity.X, npc.velocity.Y) - spread / 2;
                double deltaAngle = 4;
                double offsetAngle;
                for (int i = 0; i < 4; i++)
                {
                    offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(Math.Sin(offsetAngle) * 6f), (float)(Math.Cos(offsetAngle) * 6f), mod.ProjectileType("StaticSphere"), npc.damage / 2, 0, Main.myPlayer, 0f, 0f);
                }
            }
        }
    }
}
