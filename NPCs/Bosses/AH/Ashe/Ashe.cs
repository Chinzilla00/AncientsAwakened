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
            Main.npcFrameCount[npc.type] = 8;
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

        public static int AISTATE_HOVER = 0, AISTATE_MELEE = 1, AISTATE_CAST1 = 2, AISTATE_CAST2 = 3;
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
		
        public override void AI()
        {
            npc.active = false;
            Player player = Main.player[npc.target];
             
            if ((Main.dayTime && player.position.Y < Main.worldSurface) || !player.ZoneGlowshroom)
            {
                npc.velocity *= 0;

                if (npc.velocity.X <= .1f && npc.velocity.X >= -.1f)
                {
                    npc.velocity.X = 0;
                }
                if (npc.velocity.Y <= .1f && npc.velocity.Y >= -.1f)
                {
                    npc.velocity.Y = 0;
                }

                npc.alpha += 10;

                if (npc.alpha >= 255)
                {
                    npc.active = false;
                }
                return;
            }
            npc.alpha -= 10;
            if (npc.alpha < 0)
            {
                npc.alpha = 0;
            }
            npc.frameCounter++;
            if (npc.frameCounter >= 10)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 90;
                if (npc.frame.Y > (90 * 7))
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = 0;
                }
            }

            if (!Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
            {
                internalAI[0]++;
                MoveToPoint(new Vector2(player.Center.X, player.Center.Y - 170f));
            }

            /*if (Main.netMode != 1 && internalAI[1] != AISTATE_SHOOT)
			{
                internalAI[0]++;
                if (internalAI[0] >= 180)
                {
                    internalAI[0] = 0;
                    internalAI[1] = Main.rand.Next(3);
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
            }
			if(internalAI[1] == AISTATE_HOVER) 
            {
                BaseAI.AISpaceOctopus(npc, ref npc.ai, player.Center, 0.15f, 4f, 170, 56f, FireMagic);
            }
            else if (internalAI[1] == AISTATE_FLIER) 
            {
                BaseAI.AIFlier(npc, ref npc.ai, true, 0.1f,0.04f, 5f, 3f, false, 1);
            }
            else if (internalAI[1] == AISTATE_SHOOT)
            {

                if (HasStopped)
                {
                    internalAI[0]++;
                    npc.rotation = 0;
                }
                if (internalAI[0] >= 60)
                {
                    int attack = Main.rand.Next(4);
                    internalAI[1] = Main.rand.Next(3);
                    internalAI[0] = 0;
                    FungusAttack(attack);
                    npc.netUpdate = true;
                }

                npc.velocity *= 0.7f;

                if (npc.velocity.X <= .1f && npc.velocity.X >= -.1f)
                {
                    npc.velocity.X = 0;
                }
                if (npc.velocity.Y <= .1f && npc.velocity.Y >= -.1f)
                {
                    npc.velocity.Y = 0;
                }
                if (npc.velocity == new Vector2(0, 0))
                {
                    HasStopped = true;
                }
            }*/
        }


        public float[] shootAI = new float[4];

        public void FireMagic(NPC npc, Vector2 velocity)
        {
            Player player = Main.player[npc.target];
            BaseAI.ShootPeriodic(npc, player.position, player.width, player.height, mod.ProjType("Mushshot"), ref shootAI[0], 5, (int)(npc.damage * (Main.expertMode ? 0.25f : 0.5f)), 8f, true, new Vector2(20f, 15f));
        }


        public override void NPCLoot()
        {
            int Ashe = NPC.CountNPCS(mod.NPCType("Haruka"));
            if (Ashe == 0)
            {

            }
            if (!Main.expertMode)
            {
                npc.DropLoot(mod.ItemType("DaybreakIncinerite"), 5, 10);
                string[] lootTable = { "" };
                int loot = Main.rand.Next(lootTable.Length);
                npc.DropLoot(mod.ItemType(lootTable[loot]));
                Main.NewText("OW..! THAT HURT, YOU KNOW!", new Color(179, 74, 39));
            }
            npc.value = 0f;
            npc.boss = false;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 1.1f);  //boss damage increase in expermode
        }

        public void FungusAttack(int Attack)
        {
            Player player = Main.player[npc.target];

            if (Attack == 0)
            {

            }
            else if (Attack == 1)
            {

            }
            else if (Attack == 2)
            {

            }
            else
            {

            }
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

            if (Summon)
            {
                BaseDrawing.DrawTexture(spritebatch, RitualTex, blue, npc.position, npc.width, npc.height, scale, RingRotation, 0, 1, RingFrame, npc.GetAlpha(Color.White), true);
                BaseDrawing.DrawTexture(spritebatch, RingTex, red, npc.position, npc.width, npc.height, scale, -RingRotation, 0, 1, RingFrame, npc.GetAlpha(Color.White), true);
                BaseDrawing.DrawTexture(spritebatch, RingTex1, blue, npc.position, npc.width, npc.height, scale, -RingRotation, 0, 1, RitualFrame, npc.GetAlpha(Color.White), true);
            }

            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, 0, 8, npc.frame, npc.GetAlpha(dColor), true);
            BaseDrawing.DrawTexture(spritebatch, glowTex, red, npc.position, npc.width, npc.height, npc.scale, npc.rotation, 0, 8, npc.frame, Color.White, true);
            BaseDrawing.DrawTexture(spritebatch, eyeTex, blue, npc.position, npc.width, npc.height, npc.scale, npc.rotation, 0, 8, npc.frame, Color.White, true);
            BaseDrawing.DrawAfterimage(spritebatch, eyeTex, blue, npc, 0.8f, 1f, 4, false, 0f, 0f, Color.White);
            return false;
        }
    }

    
}


