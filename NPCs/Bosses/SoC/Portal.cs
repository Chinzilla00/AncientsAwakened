using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.SoC
{
    public class Portal : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rift Portal");
        }
        public override void SetDefaults()
        {
            npc.width = 120;
            npc.height = 120;
            npc.alpha = 255;
            npc.lifeMax = 1;
            npc.dontTakeDamage = true;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.timeLeft = 10;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public int Spawntimer = 0;

        public override void AI()
        {

            npc.alpha -= 6;
            if (npc.alpha >= 255)
            {
                Spawntimer++;
                npc.alpha = 255;
            }
            

            if (Spawntimer >= 240)
            {
                //SummonEnemy();
                npc.scale -= .05f;
            }
            if (npc.scale <= 0)
            {
                npc.life = 0;
            }
        }

        public override void NPCLoot()
        {
            for (int num468 = 0; num468 < 3; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), npc.width, 1, mod.DustType<Dusts.AkumaADust>(), -npc.velocity.X * 0.2f,
                    -npc.velocity.Y * 0.2f, 100, default(Color), 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), npc.width, npc.height, mod.DustType<Dusts.AkumaADust>(), -npc.velocity.X * 0.2f,
                    -npc.velocity.Y * 0.2f, 100, default(Color));
                Main.dust[num469].velocity *= 2f;
            }
        }

        public void SummonEnemy()
        {
            if (Main.netMode != 1)
            {
                int npcID = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("SoC"));
                Main.npc[npcID].Center = npc.Center;
                Main.npc[npcID].netUpdate = true;
            }

            npc.active = false;
        }
        
        public float auraPercent = 0f;
        public bool auraDirection = true;
        public static Texture2D EyeTex = null;
        public float Rotation1;
        public float Rotation2;

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {

            Vector2 vector38 = npc.position + new Vector2(npc.width, npc.height) / 2f + Vector2.UnitY * npc.gfxOffY - Main.screenPosition;
            int num214 = Main.npcTexture[npc.type].Height / Main.projFrames[npc.type];
            int y6 = 0;
            Rectangle Frame = BaseDrawing.GetFrame(0, 70, 79, 0, 2);
            Vector2 drawCenter = new Vector2(npc.Center.X, npc.Center.Y);
            Texture2D Portal1 = mod.GetTexture("NPCs/Bosses/SoC/Portal");

            Rotation1 += .005f;

            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            Main.spriteBatch.Draw(Portal1, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, Portal1.Width, num214)), Color.White, Rotation1, new Vector2(Portal1.Width / 2f, num214 / 2f), npc.scale, SpriteEffects.None, 0f);
            BaseDrawing.DrawAura(sb, Portal1, 0, drawCenter - Main.screenPosition, 120, 120, auraPercent, 1f, npc.scale, Rotation1, 0, 1, Frame, 0f, 0f, Color.White * .1f);
            return false;
        }
    }
}