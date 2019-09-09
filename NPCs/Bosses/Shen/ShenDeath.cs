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
            Terraria.ID.NPCID.Sets.TechnicallyABoss[npc.type] = true;
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
            if (Main.netMode != 1)
            {
                npc.ai[1]++;
            }
            npc.TargetClosest();
            Player player = Main.player[npc.target];
            npc.Center = player.Center;
            if (npc.ai[0] == 0)
            {
                if (npc.ai[1] == 180)
                {
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat("Split again...", new Color(180, 41, 32));
                    npc.netUpdate = true;
                }

                if (npc.ai[1] == 360)
                {
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat("This is YOUR fault you insolent worm..! I knew we should have been more aggressive but NOOOOOOOOO..! YOU said we could squash them without even trying!", new Color(45, 46, 70));
                    npc.netUpdate = true;
                }

                if (npc.ai[1] == 540)
                {
                    string Name = Main.netMode != 0 ? "Warriors" : player.name;
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat(Name + ", you will face our fury again one day...either when we gain enough power again...", new Color(180, 41, 32));
                    npc.netUpdate = true;
                }

                if (npc.ai[1] == 720)
                {
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat("...or you decide to use that Sigil again..!", new Color(45, 46, 70));
                    npc.netUpdate = true;
                }

                if (npc.ai[1] >= 900)
                {
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat("Your choice, child.", new Color(180, 41, 32));
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat("Your choice, child.", new Color(45, 46, 70));
                    AAWorld.downedShen = true;
                    npc.active = false;
                    npc.netUpdate = true;
                }
                return;
            }
            else
            {
                if (npc.ai[1] == 180)
                {
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat("YOU IMBECILE! WE LOST! AGAAAAAAAAAAAAIN!!!", new Color(45, 46, 70));
                    npc.netUpdate = true;
                }

                if (npc.ai[1] == 360)
                {
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat("Rgh, my head...", new Color(180, 41, 32));
                }

                if (npc.ai[1] == 540)
                {
                    string Name = Main.netMode != 0 ? "BUNCH OF CLOWNS" : player.Male ? "BOY" : "GIRL";
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat("And YOU, " + Name + "! NEXT TIME I'M GONNA TEAR YOUR HEADS OFF!!!", new Color(45, 46, 70));
                    npc.netUpdate = true;
                }

                if (npc.ai[1] == 720)
                {
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat("And trust us, kid.", new Color(180, 41, 32));
                    npc.netUpdate = true;
                }

                if (npc.ai[1] >= 900)
                {
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat("There's always a next time.", new Color(180, 41, 32));
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat("There's always a next time.", new Color(45, 46, 70));
                    npc.active = false;
                    npc.netUpdate = true;
                }
            }
        }
    }
}