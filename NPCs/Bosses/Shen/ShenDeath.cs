using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen
{
    public class ShenDeath : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discord");
        }
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 100;
            npc.alpha = 255;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ShenIntro");
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

        public int Speechtimer = 0;

        public int chargeWidth = 50;
        public int normalWidth = 250;

        public static bool NOTRELEASED = true;

        public override void AI()
        {

            if (NOTRELEASED)
            {
                Main.NewText("Patience, child...our battle will come in due time...", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                BaseAI.KillNPC(npc); return;
            }
            Speechtimer++;
            if (Speechtimer < 780)
            {
                for (int LOOP = 0; LOOP < 4; LOOP++)
                {
                    Dust dust1;
                    Dust dust2;
                    Vector2 position1 = new Vector2(npc.Center.X + 50, npc.Center.Y);
                    Vector2 position2 = new Vector2(npc.Center.X - 50, npc.Center.Y);
                    dust1 = Main.dust[Dust.NewDust(position1, 1, 1, mod.DustType<Dusts.AkumaDust>(), 0, 0, 0, default(Color), 1f)];
                    dust1.noGravity = false;
                    dust2 = Main.dust[Dust.NewDust(position2, 1, 1, mod.DustType<Dusts.YamataDust>(), 0, 0, 0, default(Color), 1f)];
                    dust2.noGravity = true;
                    dust2.scale *= 1.3f;
                    dust2.velocity.Y -= 6;
                }
            }
            
            if (Speechtimer == 180)
            {
                Main.NewText("Split again…", new Color(180, 41, 32));
            }

            if (Speechtimer == 360)
            {
                Main.NewText("This is YOUR fault you idiotic worm..! I knew we should have been more aggressive at the beginning..!", new Color(45, 46, 70));
            }

            if (Speechtimer == 540)
            {
                Main.NewText("id, you will know our wrath again one day...when we gain enough power again…", new Color(180, 41, 32));
            }

            if (Speechtimer == 720)
            {
                Main.NewText("...or you decide to use that Sigil again..!", new Color(45, 46, 70));
            }

            if (Speechtimer == 900)
            {
                Main.NewText("Your choice, child.", new Color(180, 41, 32));
                Main.NewText("Your choice, child.", new Color(45, 46, 70));
            }

            if (Speechtimer >= 900 && Speechtimer <= 960)
            {
                npc.alpha -= 20;
            }

            if (Speechtimer <= 960)
            {
                npc.alpha += 20;
            }

            if (Speechtimer <= 960 && npc.alpha >= 255)
            {
                npc.active = false;
            }
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