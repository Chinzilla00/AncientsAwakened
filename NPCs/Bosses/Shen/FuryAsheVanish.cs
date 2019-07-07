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
            DisplayName.SetDefault("Ashe Akuma");     
            Main.npcFrameCount[npc.type] = 17;     
        }

        public override void SetDefaults()
        {
            npc.dontTakeDamage = true;
            npc.lifeMax = 1;
            npc.width = 82;
            npc.height = 82;
            npc.friendly = false;
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

        public override void AI()
        {
            npc.velocity.X *= 0.97f;
            npc.velocity.Y *= 0.97f;

            if (++npc.frameCounter >= 5)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 88;
                if (npc.frame.Y > (88 * 13))
                {
                    npc.active = false;
                    npc.netUpdate = true;
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

            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 17, npc.frame, npc.GetAlpha(dColor), true);
            BaseDrawing.DrawTexture(spritebatch, glowTex, purple, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 17, npc.frame, Color.White, true);
            BaseDrawing.DrawTexture(spritebatch, eyeTex, blue, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 17, npc.frame, Color.White, true);
            BaseDrawing.DrawAfterimage(spritebatch, eyeTex, blue, npc, 0.8f, 1f, 4, true, 0f, 0f, Color.White, npc.frame, 17);
            return false;
        }
    }
}
