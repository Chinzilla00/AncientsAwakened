using Terraria;
using Terraria.ModLoader;
using BaseMod;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace AAMod.NPCs.Bosses.Truffle
{
    public class TruffleProbe : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Truffle Probe");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.width = 14;
            npc.height = 14;
            npc.value = BaseUtility.CalcValue(0, 0, 0, 0);
            npc.npcSlots = 0;
            npc.aiStyle = -1;
            npc.lifeMax = 500;
            npc.defense = 0;
            npc.damage = 20;
            npc.HitSound = new LegacySoundStyle(3, 4, Terraria.Audio.SoundType.Sound);
            npc.DeathSound = new LegacySoundStyle(4, 14, Terraria.Audio.SoundType.Sound);
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.alpha = 255;
        }

        public override void AI()
        {
            if (npc.alpha != 0)
            {
                for (int LOOP = 0; LOOP < 2; LOOP++)
                {
                    Dust dust1;
                    dust1 = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, DustID.Electric, 0, 0, 0, default(Color), 1f)];
                    dust1.noGravity = true;
                }
                npc.alpha -= 5;
            }
            Color color = BaseUtility.MultiLerpColor(Main.player[Main.myPlayer].miscCounter % 100 / 100f, BaseDrawing.GetLightColor(npc.position), BaseDrawing.GetLightColor(npc.position), Color.Violet, BaseDrawing.GetLightColor(npc.position), Color.Violet, BaseDrawing.GetLightColor(npc.position));

            Lighting.AddLight((int)(npc.Center.X + (npc.width / 2)) / 16, (int)(npc.position.Y + (npc.height / 2)) / 16, color.R / 255, color.G / 255, color.B / 255);

            BaseAI.AISpore(npc, ref npc.ai, 0.1f, 0.02f, 5f, 1f);

            npc.frameCounter++;
            if (npc.frameCounter > 8)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 20;
                if (npc.frame.Y > 60)
                {
                    npc.frame.Y = 0;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/TruffleProbe_Glow1");
            Texture2D glowTex1 = mod.GetTexture("Glowmasks/TruffleProbe_Glow2");
            Color color = BaseUtility.MultiLerpColor((Main.player[Main.myPlayer].miscCounter % 100) / 100f, BaseDrawing.GetLightColor(npc.position), BaseDrawing.GetLightColor(npc.position), Color.Violet, BaseDrawing.GetLightColor(npc.position), Color.Violet, BaseDrawing.GetLightColor(npc.position));

            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc, color);
            BaseDrawing.DrawTexture(spritebatch, glowTex1, 0, npc, Color.White);
            return false;
        }
    }
}


