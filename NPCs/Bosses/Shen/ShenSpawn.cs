using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen
{
    public class ShenSpawn : ModNPC
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
            npc.life = 1;
            npc.dontTakeDamage = true;
            npc.noGravity = true;
            npc.aiStyle = -1;
        }

        public int Speechtimer = 0;

        public override void AI()
        {
            if (npc.timeLeft <= 10)
            {
                npc.timeLeft = 10;
            }
            Speechtimer++;
            if (Speechtimer > 780)
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
            
            if (Speechtimer == 60)
            {
                Main.NewText("Surprised to see us again, Kid?", new Color(180, 41, 32));
            }

            if (Speechtimer == 240)
            {
                Main.NewText("NYEHEHEHEHEH..! Yes..! Must be shocking to see us here..! But this time, we have a little tricksie up our sleeves..!", new Color(45, 46, 70));
            }

            if (Speechtimer == 420)
            {
                Main.NewText("That Sigil you just used gave us back our full power, which will let us reach our true, powerful form..!", new Color(180, 41, 32));
            }

            if (Speechtimer == 600)
            {
                Main.NewText("We used to be the same being..! But then a Terrarian wretch like you split our soul in half..! But now...heheheh…", new Color(45, 46, 70));
            }

            if (Speechtimer == 780)
            {
                Main.NewText("WE ARE COMPLETE AGAIN", new Color(180, 41, 32));
                Main.NewText("WE ARE COMPLETE AGAIN", new Color(45, 46, 70));
                Dust dust1;
                Dust dust2;
                Vector2 position1 = new Vector2(npc.Center.X + 40, npc.Center.Y);
                Vector2 position2 = new Vector2(npc.Center.X - 40, npc.Center.Y);
                dust1 = Main.dust[Dust.NewDust(position1, 1, 1, mod.DustType<Dusts.AkumaDust>(), 0, 0, 0, default(Color), 1f)];
                dust1.noGravity = false;
                dust2 = Main.dust[Dust.NewDust(position2, 1, 1, mod.DustType<Dusts.YamataDust>(), 0, 0, 0, default(Color), 1f)];
                dust2.noGravity = true;
                dust2.scale *= 1.3f;
                dust2.velocity.Y -= 6;
            }

            if (Speechtimer == 785)
            {
                Dust dust1;
                Dust dust2;
                Vector2 position1 = new Vector2(npc.Center.X + 40, npc.Center.Y);
                Vector2 position2 = new Vector2(npc.Center.X - 40, npc.Center.Y);
                dust1 = Main.dust[Dust.NewDust(position1, 1, 1, mod.DustType<Dusts.AkumaDust>(), 0, 0, 0, default(Color), 1f)];
                dust1.noGravity = false;
                dust2 = Main.dust[Dust.NewDust(position2, 1, 1, mod.DustType<Dusts.YamataDust>(), 0, 0, 0, default(Color), 1f)];
                dust2.noGravity = true;
                dust2.scale *= 1.3f;
                dust2.velocity.Y -= 6;
            }

            if (Speechtimer == 790)
            {
                Dust dust1;
                Dust dust2;
                Vector2 position1 = new Vector2(npc.Center.X + 20, npc.Center.Y);
                Vector2 position2 = new Vector2(npc.Center.X - 20, npc.Center.Y);
                dust1 = Main.dust[Dust.NewDust(position1, 1, 1, mod.DustType<Dusts.AkumaDust>(), 0, 0, 0, default(Color), 1f)];
                dust1.noGravity = false;
                dust2 = Main.dust[Dust.NewDust(position2, 1, 1, mod.DustType<Dusts.YamataDust>(), 0, 0, 0, default(Color), 1f)];
                dust2.noGravity = true;
                dust2.scale *= 1.3f;
                dust2.velocity.Y -= 6;
            }

            if (Speechtimer == 795)
            {
                Dust dust1;
                Dust dust2;
                Vector2 position1 = new Vector2(npc.Center.X + 20, npc.Center.Y);
                Vector2 position2 = new Vector2(npc.Center.X - 20, npc.Center.Y);
                dust1 = Main.dust[Dust.NewDust(position1, 1, 1, mod.DustType<Dusts.AkumaDust>(), 0, 0, 0, default(Color), 1f)];
                dust1.noGravity = false;
                dust2 = Main.dust[Dust.NewDust(position2, 1, 1, mod.DustType<Dusts.YamataDust>(), 0, 0, 0, default(Color), 1f)];
                dust2.noGravity = true;
                dust2.scale *= 1.3f;
                dust2.velocity.Y -= 6;
            }

            if (Speechtimer == 800)
            {
                Dust dust1;
                Dust dust2;
                Vector2 position1 = new Vector2(npc.Center.X + 20, npc.Center.Y);
                Vector2 position2 = new Vector2(npc.Center.X - 20, npc.Center.Y);
                dust1 = Main.dust[Dust.NewDust(position1, 1, 1, mod.DustType<Dusts.AkumaDust>(), 0, 0, 0, default(Color), 1f)];
                dust1.noGravity = false;
                dust2 = Main.dust[Dust.NewDust(position2, 1, 1, mod.DustType<Dusts.YamataDust>(), 0, 0, 0, default(Color), 1f)];
                dust2.noGravity = true;
                dust2.scale *= 1.3f;
                dust2.velocity.Y -= 6;
            }

            if (Speechtimer == 805)
            {
                Dust dust1;
                Dust dust2;
                Vector2 position1 = new Vector2(npc.Center.X + 10, npc.Center.Y);
                Vector2 position2 = new Vector2(npc.Center.X - 10, npc.Center.Y);
                dust1 = Main.dust[Dust.NewDust(position1, 1, 1, mod.DustType<Dusts.AkumaDust>(), 0, 0, 0, default(Color), 1f)];
                dust1.noGravity = false;
                dust2 = Main.dust[Dust.NewDust(position2, 1, 1, mod.DustType<Dusts.YamataDust>(), 0, 0, 0, default(Color), 1f)];
                dust2.noGravity = true;
                dust2.scale *= 1.3f;
                dust2.velocity.Y -= 6;
            }

            if (Speechtimer == 810)
            {
                Dust dust1;
                Dust dust2;
                Vector2 position1 = new Vector2(npc.Center.X, npc.Center.Y);
                Vector2 position2 = new Vector2(npc.Center.Y, npc.Center.Y);
                dust1 = Main.dust[Dust.NewDust(position1, 1, 1, mod.DustType<Dusts.AkumaDust>(), 0, 0, 0, default(Color), 1f)];
                dust1.noGravity = false;
                dust2 = Main.dust[Dust.NewDust(position2, 1, 1, mod.DustType<Dusts.YamataDust>(), 0, 0, 0, default(Color), 1f)];
                dust2.noGravity = true;
                dust2.scale *= 1.3f;
                dust2.velocity.Y -= 6;
            }

            if (Speechtimer > 815 && Speechtimer < 1410)
            {
                for (int LOOP = 0; LOOP < 8; LOOP++)
                {
                    Dust dust1;
                    Vector2 position1 = npc.Center;
                    dust1 = Main.dust[Dust.NewDust(position1, 1, 1, mod.DustType<Dusts.Discord>(), 0, 0, 0, default(Color), 1f)];
                    dust1.noGravity = false;
                    dust1.scale *= 1.3f;
                    dust1.velocity.Y -= 6;
                }
            }

            if (Speechtimer == 960)
            {
                Main.NewText("Heh....heheh...", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
            }

            if (Speechtimer == 1140)
            {
                Main.NewText("You messed up big time, child...", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
            }

            if (Speechtimer == 1320)
            {
                Main.NewText("For you see....", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
            }

            if (Speechtimer >= 1410)
            {
                npc.alpha--;
            }

            if (Speechtimer == 1500)
            {
                Main.NewText("I AM SHEN DORAGON, EMPEROR OF CHAOS AND ANARCHY!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);

            }

            if (Speechtimer == 1600)
            {
                Main.NewText("And you, my child, will face the wrath and fury of chaos itself..!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);

            }

            if (Speechtimer == 1680)
            {
                Main.NewText("DIE!!!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                SummonShen();
                npc.active = false;
            }
        }

        public void SummonShen()
        {
            if (Main.netMode != 1)
            {
                int npcID = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("ShenDoragon"));
                Main.npc[npcID].Center = npc.Center;
                Main.npc[npcID].netUpdate = true;
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