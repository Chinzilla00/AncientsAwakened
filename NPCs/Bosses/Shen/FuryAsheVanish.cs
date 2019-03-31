using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen
{
    public class FuryAsheVanish : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ashe Akuma");     //The English name of the projectile
            Main.npcFrameCount[npc.type] = 17;     //The recording mode
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
            npc.velocity.X *= 0.97f;
            npc.velocity.Y *= 0.97f;

            if (++npc.frameCounter >= 5)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 92;
                if (npc.velocity != Vector2.Zero)
                {
                    if (npc.frame.Y > (92 * 2))
                    {
                        npc.frame.Y = 0;

                    }
                }
                else
                {
                    if (npc.frame.Y > (92 * 13))
                    {
                        npc.active = false;

                    }
                }
            }

        }

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/FuryAsheVanish_Glow2");
            Texture2D eyeTex = mod.GetTexture("Glowmasks/FuryAsheVanish_Glow1");

            int blue = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingOceanDye);
            int red = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingFlameDye);
            int purple = GameShaders.Armor.GetShaderIdFromItemId(mod.ItemType<Items.Dyes.DiscordianDye>());

            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, 0, 24, npc.frame, npc.GetAlpha(dColor), true);
            BaseDrawing.DrawTexture(spritebatch, glowTex, purple, npc.position, npc.width, npc.height, npc.scale, npc.rotation, 0, 24, npc.frame, Color.White, true);
            BaseDrawing.DrawTexture(spritebatch, eyeTex, blue, npc.position, npc.width, npc.height, npc.scale, npc.rotation, 0, 24, npc.frame, Color.White, true);
            BaseDrawing.DrawAfterimage(spritebatch, eyeTex, blue, npc, 0.8f, 1f, 4, true, 0f, 0f, Color.White, npc.frame, 24);
            return false;
        }
    }
}
