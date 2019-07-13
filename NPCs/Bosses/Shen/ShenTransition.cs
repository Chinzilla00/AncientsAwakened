using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen
{
    public class ShenTransition : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discordian Awakening");
        }
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 100;
            npc.friendly = false;
            npc.alpha = 255;
            npc.lifeMax = 10000000;
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
            npc.TargetClosest();
            Player player = Main.player[npc.target];
            npc.Center = player.Center - new Vector2(0, 300f); ;
            npc.ai[0]++;
            if (npc.timeLeft <= 10)
            {
                npc.timeLeft = 10;
            }
            if (npc.alpha > 0 && npc.ai[0] > 375)
            {
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ShenA");
                for (int LOOP = 0; LOOP < 8; LOOP++)
                {
                    Dust dust1;
                    Vector2 position1 = npc.Center;
                    dust1 = Main.dust[Dust.NewDust(position1, 20, 20, mod.DustType<Dusts.Discord>(), 0, 0, 0, default(Color), 1f)];
                    dust1.noGravity = false;
                    dust1.scale *= 1.3f;
                    dust1.velocity.Y -= 6;
                }
            }
            if (npc.ai[0] == 375)    
            {
                BaseUtility.Chat("Heh...", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
            }
            if (npc.ai[0] == 475)
            {
                BaseUtility.Chat("Heheheh...", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
            }
            if (npc.ai[0] == 600)
            {
                BaseUtility.Chat("HAHAHAHAHAHAHA!!!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
            }
            if (npc.ai[0] == 820)
            {
                BaseUtility.Chat("Have you forgotten about our last battles...?", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
            }
            if (npc.ai[0] == 960)
            {
                BaseUtility.Chat("There's always a last stand, kiddo...", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
            }
            if (npc.ai[0] >= 1100)
            {
                npc.alpha -= 5;
            }
            if (npc.ai[0] == 1100)
            {
                BaseUtility.Chat("I have only been using a fraction of my true power...and now...heheheh...", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
            }

            if (npc.ai[0] >= 1400)
            {
                SummonShen();
                npc.active = false;        
            }
        }

        public void SummonShen()
        {
            Player player = Main.player[npc.target];
            BaseUtility.Chat("Shen Doragon has been Awakened!", Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);
            BaseUtility.Chat("YOU WILL BURN IN THE FLAMES OF DISCORDIAN HELL!!!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);

            AAModGlobalNPC.SpawnBoss(player, mod.NPCType("ShenA"), false, npc.Center, "Shen Awakened", false);
        }

        public Color GetColorAlpha()
        {
            return new Color(233, 0, 233) * (Main.mouseTextColor / 255f);
        }

        public float auraPercent = 0f;
        public bool auraDirection = true;
        public bool saythelinezero = false;

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            if (npc.alpha <= 0)
            {
                BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc, dColor);
                BaseDrawing.DrawAura(sb, Main.npcTexture[npc.type], 0, npc, auraPercent, 1f, 0f, 0f, GetColorAlpha());
                BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc, GetColorAlpha());
                return false;
            }

            return true;
        }
    }
}