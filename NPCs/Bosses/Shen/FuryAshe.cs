using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using Terraria.Graphics.Shaders;
using AAMod.NPCs.Bosses.AH.Ashe;

namespace AAMod.NPCs.Bosses.Shen
{
    [AutoloadBossHead]
    public class FuryAshe : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fury Ashe");
            Main.npcFrameCount[npc.type] = 24;
        }

        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 80;
            npc.damage = 100;
            npc.defense = 40;
            npc.lifeMax = 100000;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.knockBackResist = 0f;
            npc.knockBackResist = 0f;
            npc.lavaImmune = true;
            npc.netAlways = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ShenA");
        }

        public float[] internalAI = new float[4];

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write((float)internalAI[0]);
                writer.Write((float)internalAI[1]);
                writer.Write((float)internalAI[2]);
                writer.Write((float)internalAI[3]);
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
        
        bool FlyingBack = false;
        bool FlyingPositive = false;
        bool FlyingNegative = false;
        public float MeleeSpeed;
        public float pos = 0f;
        private bool HasFiredProj = false;


        public Vector2 MovePoint;
        public bool SelectPoint = false;


        public bool Health3 = false;
        public bool Health2 = false;
        public bool Health1 = false;

        public override bool CheckActive()
        {
            if (!NPC.AnyNPCs(mod.NPCType<ShenA>()))
            {
                return false;
            }

            return true;
        }


        public static int AISTATE_HOVER = 0, AISTATE_CAST1 = 1, AISTATE_CAST2 = 2, AISTATE_CAST3 = 3, AISTATE_CAST4 = 4, AISTATE_MELEE = 5, AISTATE_DRAGON = 6;

        public int[] Vortexes = null;
        
        public void ChangePos()
        {
            npc.ai[1] = Main.rand.Next(2);
            if (npc.ai[1] == 0)
            {
                pos = -250;
            }
            else
            {
                pos = 250;
            }
            npc.netUpdate = false;
        }

        public override void AI()
        {
            Player player = Main.player[npc.target];

            npc.frame.Y = 82 * (int)internalAI[2]; //IAI[2] Is the current frame

            RingEffects();
            RingEffects2();
            internalAI[1]++;


            if (internalAI[1] >= 8) //IAI[1] is the frame counter
            {
                internalAI[1] = 0;
                internalAI[2]++;
            }

            if (player.dead || !player.active || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
            {
                npc.TargetClosest(false);
                if (player.dead || !player.active || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
                {
                    npc.velocity.Y -= 0.1f;
                    if (npc.velocity.Y > 15f) npc.velocity.Y = 15f;
                    npc.rotation = 0f;
                    if (npc.position.Y - npc.height - npc.velocity.Y >= Main.maxTilesY && Main.netMode != 1) { BaseAI.KillNPC(npc); npc.netUpdate2 = true; }
                }

                if ((int)internalAI[2] > 3)
                {
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                }
                return;
            }

            if (internalAI[0] == AISTATE_HOVER || internalAI[0] == AISTATE_DRAGON) //Hovering/Summoning Dragon
            {
                if (Main.netMode != 1 && internalAI[0] == AISTATE_HOVER) //Only randomly select AI if not doing a dragon summon
                {
                    internalAI[3]++;
                    if (internalAI[3] >= 90)
                    {
                        internalAI[3] = 0;
                        if (NPC.CountNPCS(mod.NPCType<AsheDragon>()) < 1)
                        {
                            internalAI[0] = Main.rand.Next(7);
                        }
                        else
                        {
                            internalAI[0] = Main.rand.Next(6);
                        }
                        if (internalAI[0] == AISTATE_MELEE)
                        {
                            moveSpeed = 6f;
                            SelectPoint = true;
                        }
                        if (internalAI[0] == AISTATE_HOVER)
                        {
                            ChangePos();
                        }
                        npc.ai = new float[4];
                        npc.netUpdate = true;
                    }
                }
                if (FlyingBack)
                {
                    if ((int)internalAI[2] > 3)
                    {
                        internalAI[1] = 0;
                        internalAI[2] = 0;
                    }
                }
                else
                {
                    if ((int)internalAI[2] > 7 || (int)internalAI[2] < 4)
                    {
                        internalAI[1] = 0;
                        internalAI[2] = 4;
                    }
                }

            }
            else if (internalAI[0] == AISTATE_CAST4 || internalAI[0] == AISTATE_MELEE) //Strong
            {
                if (internalAI[2] == 20 && internalAI[1] == 4 && internalAI[0] != AISTATE_MELEE && !HasFiredProj) //Only Shoot if not in melee mode
                {
                    FireMagic(npc, npc.velocity);
                    HasFiredProj = true;
                    npc.netUpdate = true;
                }
                if ((int)internalAI[2] < 16) //Sets to frame 16
                {
                    internalAI[1] = 0;
                    internalAI[2] = 16;
                }
                if ((int)internalAI[2] > 23) //If frame is greater than 23, reset AI
                {
                    if (internalAI[0] == AISTATE_MELEE)
                    {
                        pos = -pos;
                    }
                    HasFiredProj = false;
                    internalAI[0] = 0;
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                    internalAI[3] = 0;
                    moveSpeed = 16f;
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
            }
            else if (internalAI[0] == AISTATE_CAST2)
            {
                if (internalAI[2] > 11)
                {
                    FireMagic(npc, npc.velocity);
                    npc.netUpdate = true;
                }
                if ((int)internalAI[2] < 8)
                {
                    internalAI[1] = 0;
                    internalAI[2] = 8;
                }
                if ((int)internalAI[2] > 15)
                {
                    HasFiredProj = false;
                    internalAI[0] = 0;
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                    internalAI[3] = 0;
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
            }
            else
            {
                if (internalAI[2] == 12 && internalAI[1] == 4 && !HasFiredProj)
                {
                    FireMagic(npc, npc.velocity);
                    HasFiredProj = true;
                    npc.netUpdate = true;
                }
                if ((int)internalAI[2] < 8)
                {
                    internalAI[1] = 0;
                    internalAI[2] = 8;
                }
                if ((int)internalAI[2] > 15)
                {
                    HasFiredProj = false;
                    internalAI[0] = 0;
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                    internalAI[3] = 0;
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
            }


            if (npc.velocity.X > 0) //Flying in the positive X direction
            {
                FlyingPositive = true;
                FlyingNegative = false;
            }
            else //Flying in the nagative X direction
            {
                FlyingPositive = false;
                FlyingNegative = true;
            }

            if (internalAI[0] == AISTATE_MELEE) //Melee Damage/Speed boost
            {
                npc.damage++;
                if (npc.damage > 160)
                {
                    npc.damage = 160;
                }
                if (internalAI[2] > 21)
                {
                    MeleeSpeed -= .01f;
                    npc.damage = 100;
                }
            }
            else //Reset Stats
            {
                npc.damage = 100;
                MeleeSpeed = 0;
            }


            if (internalAI[0] == AISTATE_MELEE) //When charging the player
            {
                if (SelectPoint)
                {
                    float Point = 500 * npc.direction;
                    MovePoint = player.Center + new Vector2(Point, 500f);
                    SelectPoint = false;
                    npc.netUpdate = true;
                }
                MeleeMovement(MovePoint);
                npc.netUpdate = true;
            }
            else //Anything else
            {
                Vector2 wantedVelocity = player.Center - new Vector2(pos, 250);
                MoveToPoint(wantedVelocity);
            }

            if (internalAI[0] == AISTATE_DRAGON) //Summoning a dragon
            {
                npc.dontTakeDamage = true;
                internalAI[3]++;
                if (internalAI[3] > 240)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<AsheDragon>(), 0);
                    internalAI[0] = 0;
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                    internalAI[3] = 0;
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
            }
            else
            {
                npc.dontTakeDamage = false;
            }

            npc.rotation = 0; //No ugly rotation.
        }

        public static int VortexDamage(Mod mod)
        {
            return 1 + (NPC.CountNPCS(mod.NPCType<AsheOrbiter>()) / 15);
        }

        public override void PostAI()
        {
            Player player = Main.player[npc.target];
            if (internalAI[0] != AISTATE_MELEE)
            {
                if (player.Center.X > npc.Center.X) //If NPC's X position is less than the player's
                {
                    if (pos == -250)
                    {
                        pos = 250;
                    }

                    npc.direction = -1;

                    if (FlyingPositive)
                    {
                        FlyingBack = true;
                    }
                    else
                    {
                        FlyingBack = false;
                    }
                }
                else //If NPC's X position is higher than the player's
                {
                    if (pos == 250)
                    {
                        pos = -250;
                    }

                    npc.direction = 1;

                    if (FlyingNegative)
                    {
                        FlyingBack = true;
                    }
                    else
                    {
                        FlyingBack = false;
                    }
                }
            }
            else
            {
                npc.direction = npc.velocity.X > 0 ? -1 : 1;
            }


        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = 0;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 1.3f);  //boss damage increase in expermode
        }


        public bool Summon = false;

        public float scale = 0;
        public float RingRotation = 0;

        public float scale2 = 0;
        public float RingRotation2 = 0;

        private void RingEffects()
        {
            if (internalAI[0] == AISTATE_DRAGON) //If summoning noodle
            {
                RingRotation += 0.02f;
                if (scale < 1f)
                {
                    scale += .02f; //Raise Scale
                }
                if (scale >= 1f)
                {
                    scale = 1f;
                }
            }
            else
            {
                RingRotation -= 0.02f;
                if (scale < .1f)
                {
                    scale = 0;
                }
                if (scale > 0)
                {
                    scale -= .02f;
                }
            }
        }

        private void RingEffects2()
        {
            if (internalAI[0] == AISTATE_DRAGON || NPC.AnyNPCs(mod.NPCType<AsheOrbiter>())) //If summoning noodle
            {
                RingRotation2 += 0.02f;
                if (scale2 < 1f)
                {
                    scale2 += .02f; //Raise Scale
                }
                if (scale2 >= 1f)
                {
                    scale2 = 1f;
                }
            }
            else
            {
                RingRotation2 -= 0.02f;
                if (scale2 < .1f)
                {
                    scale2 = 0;
                }
                if (scale2 > 0)
                {
                    scale2 -= .02f;
                }
            }
        }

        public float[] shootAI = new float[4];

        public int OrbiterCount = 12;


        public void FireMagic(NPC npc, Vector2 velocity)
        {
            Player player = Main.player[npc.target];
            if (internalAI[0] == 1)
            {
                int speedX = 8;
                int speedY = 8;
                float spread = 75f * 0.0174f;
                float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
                double startAngle = Math.Atan2(speedX, speedY) - .1d;
                double deltaAngle = spread / 6f;
                double offsetAngle;
                for (int i = 0; i < 5; i++)
                {
                    offsetAngle = startAngle + (deltaAngle * i);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle) * npc.direction, baseSpeed * (float)Math.Cos(offsetAngle), mod.ProjectileType<DiscordianInferno>(), npc.damage / 2, 4);
                }
            }
            else if(internalAI[0] == 2)
            {
                int speedX = 6;
                int speedY = 6;
                float spread = 75f * 0.0174f;
                float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
                double startAngle = Math.Atan2(speedX, speedY) - .1d;
                double deltaAngle = spread / 6f;
                double offsetAngle;
                for (int i = 0; i < 3; i++)
                {
                    offsetAngle = startAngle + (deltaAngle * i);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), mod.ProjectileType<ShenMeteor1>(), npc.damage / 2, 4);
                }
            }
            else if(internalAI[0] == 3)
            {
                float spread = 60f * 0.0174f;
                double startAngle = Math.Atan2(npc.velocity.X, npc.velocity.Y) - spread / 2;
                double deltaAngle = spread / (Main.expertMode ? 5 : 4);
                double offsetAngle;
                for (int i = 0; i < (Main.expertMode ? 5 : 4); i++)
                {
                    offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(Math.Sin(offsetAngle) * 7f), (float)(Math.Cos(offsetAngle) * 7f), mod.ProjectileType<ShenRain>(), npc.damage / 2, 0, Main.myPlayer, 0f, 0f);
                }
            }
            else if (internalAI[0] == 4)
            {
                BaseAI.FireProjectile(player.Center, npc, mod.ProjectileType<ShenStorm>(), npc.damage, 3, 5f, 0, 0, -1);
            }
        }

        private readonly bool DontSayDeathLine = false;

        public override void NPCLoot()
        {
            if (DontSayDeathLine)
            {
                Main.NewText("Papa, NO! You'll PAY for this, " + Main.player[Main.myPlayer].name + "!", new Color(102, 20, 48));
            }
            else
            {
                Main.NewText("AGH! Sorry papa..! I gotta bail!", new Color(102, 20, 48));
            }
            npc.value = 0f;
            npc.boss = false;
        }

        private float moveSpeed = 15f;

        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 16f;
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

        public void MeleeMovement(Vector2 point)
        {
            if (MeleeSpeed < 16f)
            {
                MeleeSpeed += .5f;
            }
            float velMultiplier = 1f;
            Vector2 dist = point - npc.Center;
            float length = (dist == Vector2.Zero ? 0f : dist.Length());
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
            npc.velocity = (length == 0f ? Vector2.Zero : Vector2.Normalize(dist));
            npc.velocity *= MeleeSpeed;
            npc.velocity *= velMultiplier;
        }



        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/FuryAshe_Glow2");
            Texture2D eyeTex = mod.GetTexture("Glowmasks/FuryAshe_Glow1");

            Texture2D RingTex = mod.GetTexture("NPCs/Bosses/AH/Ashe/AsheRing1");
            Texture2D RingTex1 = mod.GetTexture("NPCs/Bosses/AH/Ashe/AsheRing2");
            Texture2D RitualTex = mod.GetTexture("NPCs/Bosses/AH/Ashe/AsheRitual");
            Texture2D ShieldTex = mod.GetTexture("NPCs/Bosses/AH/Ashe/AsheShield");
            Texture2D Barrier = mod.GetTexture("NPCs/Bosses/AH/Ashe/AsheBarrier");
            Rectangle RingFrame = new Rectangle(0, 0, RingTex.Width, RingTex.Height);
            Rectangle RitualFrame = new Rectangle(0, 0, RitualTex.Width, RitualTex.Height);
            Rectangle BarrierFrame = new Rectangle(0, 0, ShieldTex.Width, ShieldTex.Height);
            Rectangle ShieldFrame = new Rectangle(0, 0, Barrier.Width, Barrier.Height);


            int blue = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingOceanDye);
            int red = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingFlameDye);
            int purple = GameShaders.Armor.GetShaderIdFromItemId(mod.ItemType<Items.Dyes.DiscordianDye>());

            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }

            if (internalAI[0] == AISTATE_MELEE)
            {
                BaseDrawing.DrawAfterimage(spritebatch, Main.npcTexture[npc.type], 0, npc, 1.5f, 1f, 3, false, 0f, 0f, Color.Purple);
            }

            if (scale > 0)
            {
                BaseDrawing.DrawTexture(spritebatch, RitualTex, purple, npc.position, npc.width, npc.height, scale, RingRotation, 0, 1, RitualFrame, Color.White, true);
                BaseDrawing.DrawTexture(spritebatch, RingTex, red, npc.position, npc.width, npc.height, scale, -RingRotation, 0, 1, RingFrame, Color.White, true);
                BaseDrawing.DrawTexture(spritebatch, RingTex1, purple, npc.position, npc.width, npc.height, scale, -RingRotation, 0, 1, RingFrame, Color.White, true);
            }

            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, 0, 24, npc.frame, npc.GetAlpha(dColor), true);
            BaseDrawing.DrawTexture(spritebatch, glowTex, purple, npc.position, npc.width, npc.height, npc.scale, npc.rotation, 0, 24, npc.frame, Color.White, true);
            BaseDrawing.DrawTexture(spritebatch, eyeTex, blue, npc.position, npc.width, npc.height, npc.scale, npc.rotation, 0, 24, npc.frame, Color.White, true);


            if (NPC.AnyNPCs(mod.NPCType<AsheOrbiter>()))
            {
                BaseDrawing.DrawAfterimage(spritebatch, eyeTex, 0, npc, .5f, 1f, 7, false, 0f, 0f, Color.DeepSkyBlue);
            }

            if (scale > 0)
            {
                BaseDrawing.DrawTexture(spritebatch, Barrier, purple, npc.position, npc.width, npc.height, scale2, -RingRotation2, 0, 1, BarrierFrame, dColor, true);
                BaseDrawing.DrawTexture(spritebatch, ShieldTex, red, npc.position, npc.width, npc.height, scale2, RingRotation2, 0, 1, ShieldFrame, dColor, true);
            }
            return false;
        }
    }
}


