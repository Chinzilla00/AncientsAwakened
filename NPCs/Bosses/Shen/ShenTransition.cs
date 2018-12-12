
using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen
{
    public class ShenTransition : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spirit of Chaos");
        }
        public override void SetDefaults()
        {
            projectile.width = 100;
            projectile.height = 100;
            projectile.friendly = false;
            projectile.alpha = 255;
        }
        public int timer;
        public override void AI()
        {
            timer++;
            if (timer < 1100)
            {
                for (int LOOP = 0; LOOP < 8; LOOP++)
                {
                    Dust dust1;
                    Dust dust2;
                    Vector2 position1 = projectile.Center;
                    dust1 = Main.dust[Dust.NewDust(position1, 1, 1, mod.DustType<Dusts.Discord>(), 0, 0, 0, default(Color), 1f)];
                    dust1.noGravity = false;
                    dust2 = Main.dust[Dust.NewDust(position1, 1, 1, mod.DustType<Dusts.Discord>(), 0, 0, 0, default(Color), 1f)];
                    dust2.noGravity = true;
                    dust2.scale *= 1.3f;
                    dust2.velocity.Y -= 6;
                }
            }
            if (timer == 375)    
            {
                Main.NewText("Heh…", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
            }
            if (timer == 650)
            {
                Main.NewText("Heheheh…", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
            }
            if (timer == 900)
            {
                Main.NewText("HAHAHAHAHAHAHA", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
            }
            if (timer == 1100)
            {
                Main.NewText("You have no clue who you’re dealing with, do you, child…", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
            }

            if (timer >= 1100)
            {
                projectile.alpha--;
            }

            if (timer == 1300)
            {
                Main.NewText("For you see...I have only been using a fraction of my true power...and now...heheheh…", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
            }
            if (timer == 1455)
            {
                projectile.Kill();               
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
            BaseDrawing.DrawTexture(sb, Main.projectileTexture[projectile.type], 0, projectile, dColor);
            if (projectile.alpha <= 0)
            {
                BaseDrawing.DrawAura(sb, Main.projectileTexture[projectile.type], 0, projectile, auraPercent, 1f, 0f, 0f, GetColorAlpha());
                BaseDrawing.DrawTexture(sb, Main.projectileTexture[projectile.type], 0, projectile, GetColorAlpha());
            }
            return false;
        }

        public override void Kill(int timeleft)
        {
            for (int LOOP = 0; LOOP < 8; LOOP++)
            {
                Dust dust1;
                Dust dust2;
                Vector2 position1 = projectile.Center;
                dust1 = Main.dust[Dust.NewDust(position1, 1, 1, mod.DustType<Dusts.Discord>(), 0, 0, 0, default(Color), 1f)];
                dust1.noGravity = false;
                dust2 = Main.dust[Dust.NewDust(position1, 1, 1, mod.DustType<Dusts.Discord>(), 0, 0, 0, default(Color), 1f)];
                dust2.noGravity = true;
                dust2.scale *= 1.3f;
                dust2.velocity.Y -= 6;
            }

            SpawnBoss(projectile.Center, "ShenA", "ShenA");
            Main.NewText("Shen Doragon has been Awakened!", Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);
            Main.NewText("YOU WILL BURN IN THE FLAMES OF DISCORDIAN HELL!!!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
        }

        public void SpawnBoss(Vector2 center, string name, string displayName)
        {
            if (Main.netMode != 1)
            {
                int bossType = mod.NPCType(name);
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC((int)center.X, (int)center.Y, bossType, 0);
                Main.npc[npcID].Center = center - new Vector2(MathHelper.Lerp(-100f, 100f, (float)Main.rand.NextDouble()), 0f);
                Main.npc[npcID].netUpdate2 = true;			
                string npcName = (!string.IsNullOrEmpty(Main.npc[npcID].GivenName) ? Main.npc[npcID].GivenName : displayName);
                
            }
        }

    }
}