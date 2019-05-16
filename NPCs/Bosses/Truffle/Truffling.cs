using Terraria;
using Terraria.ModLoader;
using BaseMod;
using Terraria.Audio;

namespace AAMod.NPCs.Bosses.Truffle
{
    public class Truffling : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Truffling");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.width = 14;
            npc.height = 14;
            npc.value = BaseUtility.CalcValue(0, 0, 0, 0);
            npc.npcSlots = 0;
            npc.aiStyle = -1;
            npc.lifeMax = 300;
            npc.defense = 0;
            npc.damage = 20;
            npc.HitSound = new LegacySoundStyle(3, 4, Terraria.Audio.SoundType.Sound);
            npc.DeathSound = new LegacySoundStyle(4, 14, Terraria.Audio.SoundType.Sound);
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.noTileCollide = true;
        }

        public override void AI()
        {
            Player player = Main.player[npc.target];

            BaseAI.AIEye(npc, ref npc.ai, true, true, .2f, .2f, 4, 2, 1, 1);

            npc.frameCounter++;
            if (npc.frameCounter > 8)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 22;
                if (npc.frame.Y > 66)
                {
                    npc.frame.Y = 0;
                }
            }
        }
    }
}


