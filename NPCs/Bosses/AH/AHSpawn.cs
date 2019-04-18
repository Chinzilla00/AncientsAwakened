using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.AH
{
    public class AHSpawn : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sisters of Discord");
        }

        public override void SetDefaults()
        {
            npc.dontTakeDamage = true;
            npc.lifeMax = 1;
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
        }
        public bool ATransitionActive = false;
        public int RVal = 255;
        public int BVal = 0;

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(RVal, 125, BVal);
        }

        public override void AI()
        {
            npc.ai[1]++;
            npc.TargetClosest();
            Player player = Main.player[npc.target];

            npc.Center = player.Center;

            if (npc.ai[1] == 60)          //if the timer has gotten to 7.5 seconds, this happens (60 = 1 second)
            {
                Main.NewText("Well hello there, what a surprise to see YOU here~!", new Color(102, 20, 48));
                AAMod.AHIntro = true;
            }

            if (npc.ai[1] == 300)
            {
                if (AAWorld.downedBrood)
                {
                    Main.NewText("Oh yes, I've heard PLENTY about you, kid...you're the little warm-blood who thrashed my mother..!", new Color(102, 20, 48));
                }
                else
                {
                    Main.NewText("Oh yes, I've heard PLENTY about you, kid...you've been stirring up quite a bit of trouble in these parts...", new Color(102, 20, 48));
                }
            }

            if (npc.ai[1] == 500)
            {
                if (AAWorld.downedHydra)
                {
                    if (AAWorld.downedBrood)
                    {
                        Main.NewText("And mine...", new Color(72, 78, 117));
                    }
                    else
                    {
                        Main.NewText("...and you also hurt my mom...", new Color(72, 78, 117));
                    }
                }
                else
                {
                    Main.NewText("You're a pretty annoying wretch, you know...", new Color(72, 78, 117));
                }
            }

            
            if (npc.ai[1] == 700)
            {
                Main.NewText("So now..! Heh...", new Color(102, 20, 48));
            }

            if (npc.ai[1] == 820)
            {
                AAMod.AHIntro = false;
                Main.NewText("We're gonna give you something to absolutely SCREAM about..! Come on, Hakie, let's torch this little warm-blood~!", new Color(102, 20, 48));
                SpawnBoss(player, "Ashe");
            }

            if (npc.ai[1] >= 960)
            {
                Main.NewText("Please don't call me Hakie again...ever.", new Color(72, 78, 117));
                SpawnBoss2(player, "Haruka");
                npc.active = false;
            }
        }

        public void SpawnBoss(Player player, string name)
        {
            if (Main.netMode != 1)
            {
                int bossType = mod.NPCType(name);
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-100f, 100f, (float)Main.rand.NextDouble()), 800f);
                Main.npc[npcID].netUpdate2 = true;
            }
        }

        public void SpawnBoss2(Player player, string name)
        {
            if (Main.netMode != 1)
            {
                int bossType = mod.NPCType(name);
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(800f, 0);
                Main.npc[npcID].netUpdate2 = true;
            }
        }


    }
}