using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen
{
    public class ShenDeath : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discord's Death");
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
            npc.alpha = 255;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ChaosSissy");
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        } 

        public override void AI()
        {
            npc.ai[1]++;
            npc.TargetClosest();
            Player player = Main.player[npc.target];
            npc.Center = player.Center;
            if (!AAWorld.downedShen)
            {
                if (npc.ai[1] == 180)
                {
                    Main.NewText("Split again...", new Color(180, 41, 32));
                }

                if (npc.ai[1] == 360)
                {
                    Main.NewText("This is YOUR fault you insolent worm..! I knew we should have been more aggressive but NOOOOOOOOO..! YOU said we could squash them without even trying!", new Color(45, 46, 70));
                }

                if (npc.ai[1] == 540)
                {
                    string Name = Main.netMode != 0 ? "Warriors" : player.name;
                    Main.NewText(Name + ", you will face our fury again one day...either when we gain enough power again...", new Color(180, 41, 32));
                }

                if (npc.ai[1] == 720)
                {
                    Main.NewText("...or you decide to use that Sigil again..!", new Color(45, 46, 70));
                }

                if (npc.ai[1] >= 900)
                {
                    Main.NewText("Your choice, child.", new Color(180, 41, 32));
                    Main.NewText("Your choice, child.", new Color(45, 46, 70));
                    npc.active = false;
                }
                return;
            }
            if (npc.ai[1] == 180)
            {
                Main.NewText("YOU IMBECILE! WE LOST! AGAAAAAAAAAAAAIN!!!", new Color(45, 46, 70));
            }

            if (npc.ai[1] == 360)
            {
                Main.NewText("Rgh, my head...", new Color(180, 41, 32));
            }

            if (npc.ai[1] == 540)
            {
                string Name = Main.netMode != 0 ? "BUNCH OF CLOWNS" : player.Male ? "BOY" : "GIRL";
                Main.NewText("And YOU, " + Name + "! NEXT TIME I'M GONNA TEAR YOUR HEADS OFF!!!", new Color(45, 46, 70));
            }

            if (npc.ai[1] == 720)
            {
                Main.NewText("And trust us, kid.", new Color(180, 41, 32));
            }

            if (npc.ai[1] >= 900)
            {
                Main.NewText("There's always a next time.", new Color(180, 41, 32));
                Main.NewText("There's always a next time.", new Color(45, 46, 70));
                npc.active = false;
            }
        }

        public override bool CheckActive()
        {
            return false;
        }
    }
}