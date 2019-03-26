using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Akuma
{
    public class AkumaTransition : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Of Fury");
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 100;
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

            Player player = Main.player[npc.target];
            MoveToPoint(player.Center - new Vector2(0, 300f));

            timer++;
            npc.frameCounter++;
            if (npc.frameCounter >= 7)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 52;
            }

            if (npc.frame.Y > 52 * 5)
            {
                npc.frame.Y = 0;
            }

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
                RVal -= 5;
                BVal += 5;
                if (RVal <= 0)
                {
                    RVal = 0;
                }
                if (BVal >= 255)
                {
                    BVal = 255;
                }
            }

            if (timer == 900)
            {
                Main.NewText("fanning the flames doesn't put them out...", Color.DeepSkyBlue);
            }

            if (timer >= 1165)
            {
                Main.NewText("Akuma has been Awakened!", Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);
                Main.NewText("IT ONLY MAKES THEM STRONGER!", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);

                AAMod.AkumaMusic = false;

                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AkumaA"));
                npc.active = false;
            }

        }

        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 14f;
            if (moveSpeed == 0f || npc.Center == point) return; //don't move if you have no move speed
            float velMultiplier = 1f;
            Vector2 dist = point - npc.Center;
            float length = (dist == Vector2.Zero ? 0f : dist.Length());
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            if (length < 200f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 100f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 50f)
            {
                moveSpeed *= 0.5f;
            }
            npc.velocity = (length == 0f ? Vector2.Zero : Vector2.Normalize(dist));
            npc.velocity *= moveSpeed;
            npc.velocity *= velMultiplier;
        }

    }
}