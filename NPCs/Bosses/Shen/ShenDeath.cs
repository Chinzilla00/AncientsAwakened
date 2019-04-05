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
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ChaosSissy");
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
            npc.ai[1]++;
            Player player = Main.player[npc.target];
            npc.TargetClosest();
            npc.Center = player.Center;
            if (npc.ai[1] == 180)
            {
                Main.NewText("Split again…", new Color(180, 41, 32));
            }

            if (npc.ai[1] == 360)
            {
                Main.NewText("This is YOUR fault you idiotic worm..! I knew we should have been more aggressive at the beginning..!", new Color(45, 46, 70));
            }

            if (npc.ai[1] == 540)
            {
                Main.NewText("id, you will know our wrath again one day...when we gain enough power again…", new Color(180, 41, 32));
            }

            if (npc.ai[1] == 720)
            {
                Main.NewText("...or you decide to use that Sigil again..!", new Color(45, 46, 70));
            }

            if (npc.ai[1] == 900)
            {
                Main.NewText("Your choice, child.", new Color(180, 41, 32));
                Main.NewText("Your choice, child.", new Color(45, 46, 70));
            }

            if (npc.ai[1] >= 900 && npc.ai[1] <= 960)
            {
                npc.alpha -= 20;
            }

            if (npc.ai[1] <= 960)
            {
                npc.alpha += 20;
            }

            if (npc.ai[1] >= 960 && npc.alpha >= 255)
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