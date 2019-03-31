using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ReLogic.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.ModLoader;
using BaseMod;
using Terraria.Graphics.Shaders;

namespace AAMod.NPCs.Bosses.AH.Ashe
{
    [AutoloadBossHead]
    public class Ashe : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ashe Akuma");
            Main.npcFrameCount[npc.type] = 24;
        }

        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 80;
            npc.damage = 80;
            npc.defense = 40;
            npc.lifeMax = 100000;
            npc.value = Item.buyPrice(0, 0, 75, 45);
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.knockBackResist = 0f;
            npc.knockBackResist = 0f;
            npc.lavaImmune = true;
            npc.boss = true;
            npc.netAlways = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/AH");
            bossBag = mod.ItemType("AHBag");
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

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
        
        bool FlyingBack = false;
        bool FlyingPositive = false;
        bool FlyingNegative = false;
        public float MeleeSpeed;
        public float pos = 0f;
        private bool HasFiredProj = false;

        public static int AISTATE_HOVER = 0, AISTATE_CAST1 = 1, AISTATE_CAST2 = 2, AISTATE_FIRESPELL = 3, AISTATE_CAST4 = 4, AISTATE_MELEE = 5, AISTATE_DRAGON = 6;

        public override void AI()
        {
            Player player = Main.player[npc.target];

            npc.frame.Y = 82 * (int)internalAI[2]; //IAI[2] Is the current frame

            RingEffects();
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
                    if (internalAI[3] >= 120)
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
            else if(internalAI[0] == AISTATE_CAST4 || internalAI[0] == AISTATE_MELEE) //Weak magic cast frame
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
            if (internalAI[0] != AISTATE_MELEE)
            {
                if (player.Center.X > npc.Center.X) //If NPC's X position is higher than the player's
                {
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
                else //If NPC's X position is lower than the player's
                {
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
                float Point = 500 * npc.direction;
                npc.netUpdate = true;
                Vector2 point = player.Center + new Vector2(Point , 500f);
                MoveToPoint(point);
            }
            else //Anything else
            {
                npc.ai[0]++;
                if (npc.ai[0] > 180)
                {
                    npc.ai[0] = 0;
                    npc.ai[1] = Main.rand.Next(2);
                    if (npc.ai[1] == 0)
                    {
                        pos = -250;
                    }
                    else
                    {
                        pos = 250;
                    }
                }
                Vector2 wantedVelocity = player.Center - new Vector2(pos, 250);
                MoveToPoint(wantedVelocity);
            }

            if (internalAI[0] == AISTATE_DRAGON) //Summoning a dragon
            {
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
            npc.rotation = 0; //No ugly rotation.
        }

        public float[] shootAI = new float[4];

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
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle) * npc.direction, baseSpeed * (float)Math.Cos(offsetAngle), mod.ProjectileType<AsheShot>(), npc.damage / 2, 4);
                }
            }
            if (internalAI[0] == 2)
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
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), mod.ProjectileType<AsheFlameRing>(), npc.damage / 2, 4);
                }
            }
            if (internalAI[0] == 3)
            {
                float spread = 60f * 0.0174f;
                double startAngle = Math.Atan2(npc.velocity.X, -npc.velocity.Y) - spread / 2;
                double deltaAngle = spread / (Main.expertMode ? 5 : 4);
                double offsetAngle;
                for (int i = 0; i < (Main.expertMode ? 5 : 4); i++)
                {
                    offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(Math.Sin(offsetAngle) * 7f), (float)(Math.Cos(offsetAngle) * 7f), mod.ProjectileType<AsheSpell>(), npc.damage / 2, 0, Main.myPlayer, 0f, 0f);
                }
            }
            if (internalAI[0] == AISTATE_CAST4)
            {
                BaseAI.FireProjectile(player.Center, npc, mod.ProjectileType<AsheFire>(), npc.damage, 3, 10f, 0, 0, -1);
            }
        }


        public override void NPCLoot()
        {
            int Haruka = NPC.CountNPCS(mod.NPCType("Haruka"));
            if (Haruka == 0)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<AHDeath>());
                if (Main.expertMode)
                {
                    npc.DropBossBags();
                }
            }
            if (!Main.expertMode)
            {
                string[] lootTableA = { "AshRain", "FuryFlame", "FireSpiritStaff", "AsheSatchel" };
                int lootA = Main.rand.Next(lootTableA.Length);
                npc.DropLoot(mod.ItemType(lootTableA[lootA]));
            }
            int DeathAnim = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<AsheVanish>(), 0);
            Main.npc[DeathAnim].velocity = npc.velocity;
            Main.NewText("OW..! THAT HURT, YOU KNOW!", new Color(102, 20, 48));
            npc.value = 0f;
            npc.boss = false;
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

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/Ashe_Glow");
            Texture2D eyeTex = mod.GetTexture("Glowmasks/AsheEyes");

            Texture2D RingTex = mod.GetTexture("NPCs/Bosses/AH/Ashe/AsheRing1");
            Texture2D RingTex1 = mod.GetTexture("NPCs/Bosses/AH/Ashe/AsheRing2");
            Texture2D RitualTex = mod.GetTexture("NPCs/Bosses/AH/Ashe/AsheRitual");
            Rectangle RingFrame = new Rectangle(0, 0, RingTex.Width, RingTex.Height);
            Rectangle RitualFrame = new Rectangle(0, 0, RitualTex.Width, RitualTex.Height);
            
            int blue = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingOceanDye);
            int red = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingFlameDye);

            Color alphaColor = new Color(Color.White.R, Color.White.G, Color.White.B);

            if (scale > 0) //Only draw if summoning a noodle
            {
                BaseDrawing.DrawTexture(spritebatch, RitualTex, blue, npc.position, npc.width, npc.height, scale, RingRotation, 0, 1, RitualFrame, alphaColor, true);
                BaseDrawing.DrawTexture(spritebatch, RingTex, red, npc.position, npc.width, npc.height, scale, -RingRotation, 0, 1, RingFrame, alphaColor, true);
                BaseDrawing.DrawTexture(spritebatch, RingTex1, blue, npc.position, npc.width, npc.height, scale, -RingRotation, 0, 1, RingFrame, alphaColor, true);
            }

            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 24, npc.frame, dColor, true);
            BaseDrawing.DrawTexture(spritebatch, glowTex, red, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 24, npc.frame, Color.White, true);
            BaseDrawing.DrawTexture(spritebatch, eyeTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 24, npc.frame, Color.White, true);
            BaseDrawing.DrawAfterimage(spritebatch, eyeTex, 0, npc, 0.8f, 1f, 4, true, 0f, 0f, Color.White, npc.frame, 24);
            return false;
        }

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
    }
}


