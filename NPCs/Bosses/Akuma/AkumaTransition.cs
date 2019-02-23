using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Akuma
{
    public class AkumaTransition : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Of Fury");
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 32;
            projectile.scale *= 1.2f;
            projectile.friendly = false;
        }
        public int timer;
        public bool ATransitionActive = false;
        public int RVal = 255;
        public int BVal = 0;

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(RVal, 125, BVal);
        }

        public override void AI()
        {
            timer++;
            ATransitionActive = true;
            
            if (timer == 375)          //if the timer has gotten to 7.5 seconds, this happens (60 = 1 second)
            {
                Main.NewText("Heh...", new Color(180, 41, 32));
                AAMod.AkumaMusic = true;
            }
            if (timer == 750)
            {
                Main.NewText("You know, kid...", new Color(180, 41, 32));
            }

            if (timer >= 750)
            {
                RVal--;
                BVal++;
                if (RVal <= 0)
                {
                    RVal = 0;
                }
                if (RVal >= 255)
                {
                    RVal = 255;
                }
            }

            if (timer == 900)
            {
                Main.NewText("fanning the flames doesn't put them out...", new Color(180, 41, 32));
            }

            if (timer == 1165)
            {
                projectile.Kill();
            }

        }

        public override void Kill(int timeleft)
        {
            Main.NewText("Akuma has been Awakened!", Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);
            Main.NewText("IT ONLY MAKES THEM STRONGER", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);

            AAMod.AkumaMusic = false;

            NPC.NewNPC((int)projectile.Center.X, (int)projectile.Center.Y, mod.NPCType("AkumaA"));
        }
        
    }
}