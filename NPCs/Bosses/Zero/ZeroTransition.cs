using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Zero
{
    public class ZeroTransition : ModNPC
    {
        public override void FindFrame(int frameHeight)
        {
        }
        bool frame = false;
        bool frame1 = false;
        bool frame2 = false;
        bool frame3 = false;

        int FrameCounter = 0;
        int Frame = 0;
        int FrameCounter1 = 0;
        int Frame1 = 0;
        int FrameCounter2 = 0;
        int Frame2 = 0;
        int FrameCounter3 = 0;
        int Frame3 = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("???");
            Main.projFrames[npc.type] = 10;
        }
        public override void SetDefaults()
        {
            npc.width = 200;
            npc.height = 202;
            npc.lifeMax = 1;
            npc.dontTakeDamage = true;
        }
        public override void AI()
        {
            npc.velocity.X *= 0.00f;
            npc.velocity.Y += 0.00f;

            if (frame == false)
            {
                if (npc.frame.Y < 1818)
                {
                    npc.frameCounter++;
                }
                if (npc.frameCounter >= 3)
                {
                    frame = true;
                    frame1 = true;
                }
            }
            if (frame1 == true)
            {
                FrameCounter1++;
                if (FrameCounter1 > 3)
                {
                    Frame1++;
                    Frame1 = 0;
                }
                if (Frame1 >= 9)
                {
                    frame1 = false;
                    frame2 = true;
                }
            }
            
            if (frame2 == true)
            {
                FrameCounter2++;
                if (FrameCounter2 > 3)
                {
                    Frame2++;
                    Frame2 = 0;
                }
                if (Frame2 >= 5)
                {
                    frame2 = false;
                    frame3 = true;
                }
            }

            if (frame3 == true)
            {
                FrameCounter3++;
                if (FrameCounter3 > 3)
                {
                    Frame3++;
                    Frame3 = 0;
                }
                if (Frame3 >= 5)
                {
                    npc.life = 0;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.projectileTexture[npc.type];
            Texture2D texture1 = mod.GetTexture("NPCs/Bosses/Zero/ZeroTransition1");
            Texture2D texture2 = mod.GetTexture("NPCs/Bosses/Zero/ZeroTransition2");
            Texture2D texture3 = mod.GetTexture("NPCs/Bosses/Zero/ZeroTransition3");
            var effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            if (frame == false)
            {
                spriteBatch.Draw(texture, npc.Center - Main.screenPosition, npc.frame, drawColor, npc.rotation, npc.frame.Size() / 2, npc.scale, effects, 0);
            }
            if (frame1 == true)
            {
                Vector2 drawCenter = new Vector2(npc.Center.X, npc.Center.Y);
                int num214 = texture1.Height / 5;
                int y6 = num214 * Frame1;
                Main.spriteBatch.Draw(texture1, drawCenter - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, texture1.Width, num214)), drawColor, npc.rotation, new Vector2((float)texture1.Width / 2f, (float)num214 / 2f), npc.scale, npc.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            if (frame2 == true)
            {
                Vector2 drawCenter = new Vector2(npc.Center.X, npc.Center.Y);
                int num214 = texture2.Height / 5;
                int y6 = num214 * Frame2;
                Main.spriteBatch.Draw(texture2, drawCenter - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, texture2.Width, num214)), drawColor, npc.rotation, new Vector2((float)texture2.Width / 2f, (float)num214 / 2f), npc.scale, npc.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            if (frame3 == true)
            {
                Vector2 drawCenter = new Vector2(npc.Center.X, npc.Center.Y);
                int num214 = texture3.Height / 5;
                int y6 = num214 * Frame3;
                Main.spriteBatch.Draw(texture3, drawCenter - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, texture3.Width, num214)), drawColor, npc.rotation, new Vector2((float)texture2.Width / 2f, (float)num214 / 2f), npc.scale, npc.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }

            return false;
        }
        public override void NPCLoot()
        {
            Main.NewText("Zero has been Awakened!", Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);
            Main.NewText("INITIATING D00MSDAY PR0T0C0L. TARGET L0CKED. ENGAGING.", Color.Red.R, Color.Red.G, Color.Red.B);
            NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType<ZeroAwakened>());
        }
    }
}