using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Lucifer
{
    [AutoloadBossHead]
    public class Lucifer : ModNPC
    {
        public int damage = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lucifer");
            Main.npcFrameCount[npc.type] = 10;
        }

        public override void SetDefaults()
        {
            npc.width = 48;
            npc.height = 90;
            npc.aiStyle = -1;
            npc.damage = 120;
            npc.defense = 60;
            npc.lifeMax = 400000;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.value = 50000f;
            npc.HitSound = SoundID.NPCHit23;
            npc.DeathSound = SoundID.NPCDeath39;
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Lucifer");
            //bossBag = ModContent.ItemType<Items.Boss.Djinn.DjinnBag>();
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.damage = (int)(npc.damage * 1.6f);
            npc.defense = (int)(npc.defense * 1.2f);
        }


        public float[] internalAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]); //Attack Style
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

        public override void AI()
        {
            Player player = Main.player[npc.target];
            

            if (internalAI[0] > 3)
            {
                internalAI[0] = 0;
            }
        }

        public void ChangePattern()
        {
            MaskFrame = 0;
            if (internalAI[0] >= 3)
            {
                internalAI[0] = 0;
            }
            else
            {
                internalAI[0]++;
            }
        }

        int MaskCounter = 0;

        public override void FindFrame(int frameHeight)
        {
            if (npc.frameCounter++ >= 5)
            {
                npc.frameCounter = 0;
                npc.frame.Y += frameHeight;
                if (npc.frame.Y > frameHeight * 9)
                {
                    npc.frame.Y = 0;
                }
            }

            if (MaskFrame < 2)
            {
                if (MaskCounter++ >= 5)
                {
                    MaskCounter = 0;
                    MaskFrame++;
                }
            }
            else
            {
                MaskFrame = 2;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            int dust = DustID.Fire;
            for (int Loop = 0; Loop < 5; Loop++)
            {
                int d = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust, 0f, 0f, 0);
                Main.dust[d].velocity.Y = hitDirection * 0.1F;
                Main.dust[d].noGravity = false;
            }
            if (npc.life <= 0)
            {
                for (int Loop = 0; Loop < 60; Loop++)
                {
                    int d = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust, 0f, 0f, 0);
                    Main.dust[d].velocity.X *= 0f;
                    Main.dust[d].noGravity = false;
                }
            }
        }

        public void MoveToPoint(Vector2 point, float moveSpeed)
        {
            if (moveSpeed == 0f || npc.Center == point) return; //don't move if you have no move speed
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

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
            AAWorld.downedLucifer = true;
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("LuciferTrophy"));
            }
            if (!Main.expertMode)
            {

            }
            else
            {
                npc.DropBossBags();
            }
        }

        int MaskFrame = 0;

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D LuciTex = Main.npcTexture[npc.type];
            Texture2D MaskTex = mod.GetTexture("NPCs/Bosses/Lucifer/Mask" + internalAI[0]);

            Rectangle frame = BaseDrawing.GetFrame(MaskFrame, MaskTex.Width, MaskTex.Height / 12, 0, 0);

            BaseDrawing.DrawTexture(spriteBatch, LuciTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 10, npc.frame, npc.GetAlpha(drawColor), true);

            BaseDrawing.DrawTexture(spriteBatch, MaskTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 9, frame, npc.GetAlpha(drawColor), true);
            return false;
        }

    }
}
