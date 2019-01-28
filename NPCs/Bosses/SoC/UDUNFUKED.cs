using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.SoC
{
    public class UDUNFUKED : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lovecraftian Shadow");
        }

        public override void SetDefaults()
        {
            npc.npcSlots = 100;
            npc.width = 130;
            npc.height = 130;
            npc.aiStyle = -1;
            npc.damage = 999999999;
            npc.dontTakeDamage = true;
            npc.lifeMax = 1000000;
            npc.DeathSound = new LegacySoundStyle(2, 88, Terraria.Audio.SoundType.Sound);
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.noGravity = true;
            npc.netAlways = true;
            for (int m = 0; m < npc.buffImmune.Length; m++) npc.buffImmune[m] = true;
        }

        public float Rotation = 0;
        public float RiftSpin = 0;
        public bool Line = false;


        public override void AI()
        {
            Player player = Main.player[npc.target];
            if (player.dead || !player.active)
            {
                if (Line == false && Main.netMode != 1)
                {
                    Line = true;
                    Main.NewText("Do not return...", Color.DarkCyan);
                }
                
            }
            if (Line == true)
            {
                npc.velocity.X *= 0.8f;
                npc.velocity.Y *= 0.8f;
                npc.alpha += 10;
                if (npc.alpha >= 255)
                {
                    npc.active = false;
                }
                return;
            }
            AAPlayer modPlayer = Main.player[npc.target].GetModPlayer<AAPlayer>();
            npc.rotation += npc.direction * 0.7f;
            Vector2 vector44 = new Vector2(npc.position.X + ((float)npc.width * 0.5f), npc.position.Y + ((float)npc.height * 0.5f));
            float num441 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector44.X;
            float num442 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector44.Y;
            float num443 = (float)Math.Sqrt((double)((num441 * num441) + (num442 * num442)));
            float num4 = 9f + num443 / 100f;
            if (num4 < 8.0)
                num4 = 8f;
            if (num4 > 32.0)
                num4 = 32f;
            float num5 = num4 / num443;
            npc.velocity.X = num441 * num5;
            npc.velocity.Y = num442 * num5;
            Rotation += npc.velocity.X * .08f;
            RiftSpin -= npc.velocity.X * .08f;
            return;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture2D13 = Main.npcTexture[npc.type];
            Texture2D WheelTex = mod.GetTexture("NPCs/Bosses/SoC/UDUNFUKED_Wheel");;
            Texture2D Rift = mod.GetTexture("NPCs/Bosses/SoC/Rift");
            Vector2 vector38 = npc.position + new Vector2(npc.width, npc.height) / 2f + Vector2.UnitY * npc.gfxOffY - Main.screenPosition;
            int num214 = Main.npcTexture[npc.type].Height;
            int y6 = 0;
            Vector2 drawCenter = new Vector2(npc.Center.X, npc.Center.Y);

            Main.spriteBatch.Draw(Rift, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, Rift.Width, Rift.Height)), AAColor.Cthulhu, RiftSpin, new Vector2(Rift.Width / 2f, Rift.Height / 2f), 1.5f, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(WheelTex, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, WheelTex.Width, WheelTex.Height)), drawColor, Rotation, new Vector2(texture2D13.Width / 2f, texture2D13.Height / 2f), npc.scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(texture2D13, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, texture2D13.Width, texture2D13.Height)), drawColor, npc.rotation, new Vector2(texture2D13.Width / 2f, texture2D13.Height / 2f), npc.scale, SpriteEffects.None, 0f);

            return false;
        }

        private void RainStart()
        {
            if (!Main.raining)
            {
                int num = 86400;
                int num2 = num / 24;
                Main.rainTime = Main.rand.Next(num2 * 8, num);
                if (Main.rand.Next(3) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2);
                }
                if (Main.rand.Next(4) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 2);
                }
                if (Main.rand.Next(5) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 2);
                }
                if (Main.rand.Next(6) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 3);
                }
                if (Main.rand.Next(7) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 4);
                }
                if (Main.rand.Next(8) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 5);
                }
                float num3 = 1f;
                if (Main.rand.Next(2) == 0)
                {
                    num3 += 0.05f;
                }
                if (Main.rand.Next(3) == 0)
                {
                    num3 += 0.1f;
                }
                if (Main.rand.Next(4) == 0)
                {
                    num3 += 0.15f;
                }
                if (Main.rand.Next(5) == 0)
                {
                    num3 += 0.2f;
                }
                Main.rainTime = (int)((float)Main.rainTime * num3);
                Main.raining = true;
                if (Main.netMode == 2)
                {
                    NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
                }
            }
        }
    }
}