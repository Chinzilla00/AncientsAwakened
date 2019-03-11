
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Yamata
{
    public class YamataTransition : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spirit of Wrath");
            Main.npcFrameCount[npc.type] = 4;
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
            npc.scale *= 1.3f;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }
        public int timer;


        public int RVal = 125;
        public int BVal = 255;

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(RVal, 0, BVal);
        }

        public override void AI()
        {
            timer++;
            npc.frameCounter++;
            if (npc.frameCounter >= 7)
            {
                npc.frameCounter = 0;
                npc.frame.Y += Main.npcTexture[npc.type].Height / 4 ;
            }

            if (npc.frame.Y > (Main.npcTexture[npc.type].Height / 4) * 3)
            {
                npc.frame.Y = 0 ;
            }
            if (timer == 375)    
            {
                Main.NewText("NYEHEHEHEHEHEHEHEH~!", new Color(45, 46, 70));
                AAMod.YamataMusic = true;
            }
            if (timer == 650)
            {
                Main.NewText("You thought I was DONE..?!", new Color(45, 46, 70));
            }
            if (timer == 900)
            {
                Main.NewText("HAH! AS IF!", new Color(45, 46, 70));
            }

            if (timer >= 900)
            {
                RVal += 5;
                BVal -= 5;
                if (RVal <= 90)
                {
                    BVal = 90;
                }
                if (RVal >= 255)
                {
                    RVal = 255;
                }
            }

            if (timer == 1100)
            {
                Main.NewText("I HOPE YOU ARE READY...", new Color(146, 30, 68));
            }
            if (timer == 1455)
            {
                SpawnBoss(npc.Center, "YamataA", "Yamata Awakened");
                Main.NewText("Yamata has been Awakened!", Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);
                Main.NewText("...TO FACE MY TRUE ABYSSAL WRATH, YOU LITTLE WRETCH!!!", new Color(146, 30, 68));
                AAMod.YamataMusic = false;
                npc.active = false;
            }
        }

        public void SpawnBoss(Vector2 center, string name, string displayName)
        {
            if (Main.netMode != 1)
            {
                int bossType = mod.NPCType(name);
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC((int)center.X, (int)center.Y, bossType, 0, 0, 0, 0, 0, npc.target);
                Main.npc[npcID].Center = center - new Vector2(MathHelper.Lerp(-100f, 100f, (float)Main.rand.NextDouble()), 0f);
                Main.npc[npcID].netUpdate2 = true;			
            }
        }

    }
}