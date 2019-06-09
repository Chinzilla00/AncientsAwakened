using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen
{
    public class WrathHarukaVanish : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Haruka Yamata");     
            Main.npcFrameCount[npc.type] = 17;     
        }

        public override void SetDefaults()
        {
            npc.width = 98;
            npc.height = 98;
            npc.life = 1;
            npc.immortal = true;
            npc.dontTakeDamage = true;
        }
        
        public override void AI()
        {
            npc.velocity.Y += .1f;

            npc.frame.Y = 78 * (int)npc.ai[1];

            if (npc.ai[2] == 0)
            {
                if (++npc.ai[0] >= 6)
                {
                    npc.ai[0] = 0;
                    npc.ai[1] += 1;
                    if (npc.velocity.Y != 0)
                    {
                        if (npc.ai[1] > 3)
                        {
                            npc.ai[1] = 0;

                        }
                    }
                    else
                    {
                        if (npc.frame.Y > (92 * 12))
                        {
                            npc.ai[2] = 1 ;
                            for (int Loop = 0; Loop < 20; Loop++)
                            {
                                int Smoke2 = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y + 31), npc.width, npc.height, 186, 1 * Main.rand.NextFloat(-1, 1), -1, 0, default(Color), 1f);
                                Main.dust[Smoke2].noGravity = true;
                                Main.dust[Smoke2].noLight = true;
                                int Smoke = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y + 31), npc.width, npc.height, 186, 1 * Main.rand.NextFloat(-1, 1), -1, 0, default(Color), 2f);
                                Main.dust[Smoke].noGravity = true;
                                Main.dust[Smoke].noLight = true;
                            }
                        }
                    }
                }
            }
            else
            {
                npc.alpha += 15;
                if (npc.alpha > 255)
                {
                    npc.active = false;
                    npc.netUpdate = true;
                }
                if (++npc.ai[0] >= 6)
                {
                    npc.ai[0] = 0;
                    npc.ai[1] += 1;
                    if (npc.ai[1] < 13 || npc.ai[1] > 16)
                    {
                        npc.ai[1] = 13;
                    }
                }
            }

        }

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/HarukVanish_Glow");

            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, 17, npc.frame, npc.GetAlpha(dColor), false);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, 17, npc.frame, Color.White, false);
            BaseDrawing.DrawAfterimage(spritebatch, glowTex, 0, npc, 0.8f, 1f, 4, true, 0f, 0f, Color.White, npc.frame, 17);
            return false;
        }
    }
}
