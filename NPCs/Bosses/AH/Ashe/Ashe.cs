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
            npc.damage = 100;
            npc.defense = 40;
            npc.lifeMax = 120000;
            npc.knockBackResist = 0f;
            npc.value = Item.buyPrice(0, 0, 75, 45);
            npc.knockBackResist = 0f;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.lavaImmune = true;
            npc.boss = true;
            npc.netAlways = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/AH");
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

        bool HasStopped = false;
        bool FlyingBack = false;
        bool FlyingPositive = false;
        bool FlyingNegative = false;
        public float MeleeSpeed;

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

            if (internalAI[0] == AISTATE_HOVER || internalAI[0] == AISTATE_DRAGON) //Hovering/Summoning Dragon
            {
                if (Main.netMode != 1 && internalAI[0] == AISTATE_HOVER) //Only randomly select AI if not doing a dragon summon
                {
                    internalAI[3]++;
                    if (internalAI[3] >= 240)
                    {
                        internalAI[3] = 0;
                        internalAI[0] = Main.rand.Next(7);
                        npc.ai = new float[4];
                        npc.netUpdate = true;
                    }
                }

                if ((int)internalAI[2] > 3) 
                {
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                }
            }
            else
            {
                if(internalAI[0] != AISTATE_CAST4 || internalAI[0] != AISTATE_MELEE) //Weak magic cast frame
                {
                    if (internalAI[2] == 12 && internalAI[1] == 4)
                    {
                        FireMagic(npc, npc.velocity);
                    }
                    if ((int)internalAI[2] < 8)
                    {
                        internalAI[1] = 0;
                        internalAI[2] = 8;
                    }
                    if ((int)internalAI[2] > 15)
                    {
                        internalAI[0] = 0;
                        internalAI[1] = 0;
                        internalAI[2] = 0;
                        internalAI[3] = 0;
                        npc.ai = new float[4];
                        npc.netUpdate = true;
                    }
                }
                else //Strong magic cast frame
                {
                    if (internalAI[2] == 20 && internalAI[1] == 4 && internalAI[0] != AISTATE_MELEE) //Only Shoot if not in melee mode
                    {
                        FireMagic(npc, npc.velocity);
                    }
                    if ((int)internalAI[2] < 16) //Sets to frame 16
                    {
                        internalAI[1] = 0;
                        internalAI[2] = 16;
                    }
                    if ((int)internalAI[2] > 23) //If frame is greater than 23, reset AI
                    {
                        internalAI[0] = 0;
                        internalAI[1] = 0;
                        internalAI[2] = 0;
                        internalAI[3] = 0;
                        npc.ai = new float[4];
                        npc.netUpdate = true;
                    }
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
            if (player.Center.X > npc.Center.X) //If NPC's X position is higher than the player's
            {
                npc.spriteDirection = -1;
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
                npc.spriteDirection = 1;

                if (FlyingNegative)
                {
                    FlyingBack = true;
                }
                else
                {
                    FlyingBack = false;
                }
            }

            if (internalAI[0] == AISTATE_MELEE) //Melee Damage/Speed boost
            {
                npc.damage++;
                if (npc.damage > 160)
                {
                    npc.damage = 160;
                }
                if (internalAI[2] < 21)
                {
                    MeleeSpeed += .01f;
                }
                if (MeleeSpeed > .08f)
                {
                    MeleeSpeed = .08f;
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
                BaseAI.AIFlier(npc, ref npc.ai, true, MeleeSpeed, MeleeSpeed, 6f, 6f, false, 1);
            }
            else if (internalAI[0] == AISTATE_DRAGON) //When summoning a noodle
            {
                npc.velocity *= .8f;
            }
            else //Anything else
            {
                BaseAI.AISpaceOctopus(npc, ref npc.ai, .3f, 7, 300);
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
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), mod.ProjectileType<AsheShot>(), npc.damage / 2, 4);
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
                double startAngle = Math.Atan2(npc.velocity.X, npc.velocity.Y) - spread / 2;
                double deltaAngle = spread / (Main.expertMode ? 5 : 4);
                double offsetAngle;
                for (int i = 0; i < (Main.expertMode ? 5 : 4); i++)
                {
                    offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(Math.Sin(offsetAngle) * 4f), (float)(Math.Cos(offsetAngle) * 4f), mod.ProjectileType<AsheSpell>(), npc.damage / 2, 0, Main.myPlayer, 0f, 0f);
                }
            }
            if (internalAI[0] == 4)
            {
                BaseAI.FireProjectile(player.Center, npc, mod.ProjectileType<AsheFire>(), npc.damage, 3, 5f, 0, 0, -1);
            }
        }


        public override void NPCLoot()
        {
            int Haruka = NPC.CountNPCS(mod.NPCType("Haruka"));
            if (Haruka == 0)
            {
                AAWorld.downedSisters = true;
            }
            if (!Main.expertMode)
            {
                npc.DropLoot(mod.ItemType("DaybreakIncinerite"), 5, 10);
                string[] lootTable = { "" };
                int loot = Main.rand.Next(lootTable.Length);
                npc.DropLoot(mod.ItemType(lootTable[loot]));
            }
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

        public void MoveToPoint(Vector2 point, bool goUpFirst = false)
        {
            float moveSpeed = 4f;
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


        public bool Summon = false;
        
        public float alpha = 255;
        public float scale = 0;
        public float RingRotation = 0;

        private void RingEffects()
        {
            RingRotation += 0.0149599658f;

            if (internalAI[0] == AISTATE_DRAGON) //If summoning noodle
            {
                if (alpha > 0)
                {
                    alpha -= 8; //Lower Alpha
                }
                if (alpha <= 0)
                {
                    alpha = 0; 
                }
                if (scale < 1f)
                {
                    scale += .05f; //Raise Scale
                }
                if (scale >= 1f)
                {
                    scale = 1f;
                }
            }
            else
            {
                if (alpha >= 255)
                {
                    alpha = 255;
                    scale = 0;
                }
                if (alpha < 255)
                {
                    scale -= .05f;
                    alpha += 8;
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

            Color alphaColor = new Color(Color.White.R, Color.White.G, Color.White.B, alpha);

            if (internalAI[0] == AISTATE_DRAGON) //Only draw if summoning a noodle
            {
                BaseDrawing.DrawTexture(spritebatch, RitualTex, blue, npc.position, npc.width, npc.height, scale, RingRotation, 0, 1, RingFrame, npc.GetAlpha(alphaColor), true);
                BaseDrawing.DrawTexture(spritebatch, RingTex, red, npc.position, npc.width, npc.height, scale, -RingRotation, 0, 1, RingFrame, npc.GetAlpha(alphaColor), true);
                BaseDrawing.DrawTexture(spritebatch, RingTex1, blue, npc.position, npc.width, npc.height, scale, -RingRotation, 0, 1, RitualFrame, npc.GetAlpha(alphaColor), true);
            }

            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, 0, 24, npc.frame, npc.GetAlpha(dColor), true);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, 0, 24, npc.frame, Color.White, true);
            BaseDrawing.DrawTexture(spritebatch, eyeTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, 0, 24, npc.frame, Color.White, true);
            BaseDrawing.DrawAfterimage(spritebatch, eyeTex, 0, npc, 0.8f, 1f, 4, true, 0f, 0f, Color.White, npc.frame, 24);
            return false;
        }
    }

    
}


