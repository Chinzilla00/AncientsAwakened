using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Yamata.Awakened
{
    [AutoloadBossHead]
    public class YamataAHead : YamataHead
    {
        public override void SetStaticDefaults()
        {
			base.SetStaticDefaults();
            DisplayName.SetDefault("Yamata Awakened");
            Main.npcFrameCount[npc.type] = 7;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.5f * bossLifeScale);
            npc.damage = (int)(npc.damage * .8f);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            isAwakened = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Yamata2");

            npc.damage = 260;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }
    }
}
