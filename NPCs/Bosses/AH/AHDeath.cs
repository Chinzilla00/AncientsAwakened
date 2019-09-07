using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.AH
{
    public class AHDeath : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sisters Defeat");
            Terraria.ID.NPCID.Sets.TechnicallyABoss[npc.type] = true;
        }

        public override void SetDefaults()
        {
            npc.dontTakeDamage = true;
            npc.lifeMax = 1;
            npc.width = 100;
            npc.height = 100;
            npc.friendly = false;
            npc.lifeMax = 1;
            npc.dontTakeDamage = true;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.timeLeft = 10;
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

            if (npc.ai[1] == 100)          //if the timer has gotten to 7.5 seconds, this happens (60 = 1 second)
            {
                if (AAWorld.downedSisters)
                {
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat("RRRRRRRRRGH! NOT AGAIN!!!", new Color(102, 20, 48));
                }
                else
                {
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat("You see Ashe? I told you this was a stupid idea, but YOU didn't listen...", new Color(72, 78, 117));
                }
            }

            if (npc.ai[1] == 300)
            {
                if (AAWorld.downedSisters)
                {
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat("Why do I keep going with you..? I should honestly just let you fight them yourself.", new Color(72, 78, 117));
                }
                else
                {
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat("Shut up! I thought if we ganged up on " + (player.Male ? "him" : "her") + ", we could just beat the daylights out of 'em!", new Color(102, 20, 48));
                }
            }

            if (npc.ai[1] == 500)
            {
                if (AAWorld.downedSisters)
                {
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat("Rgh..! Shut up..!", new Color(102, 20, 48));
                    npc.active = false;
                    AAWorld.downedSisters = true;
                }
                else
                {
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat("Whatever...I'm going back home. SOMEONE has to tell dad about this kid.", new Color(72, 78, 117));
                }
            }
            
            if (npc.ai[1] == 700)
            {
                if (Main.netMode != 1) BaseMod.BaseUtility.Chat("Hmpf..! Fine! Be that way! I'm going back to the inferno!", new Color(102, 20, 48));
                AAWorld.downedSisters = true;
                npc.active = false;
            }
        }
    }
}