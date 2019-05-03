using Terraria;
using Terraria.ModLoader;
using BaseMod;
using Terraria.Audio;

namespace AAMod.NPCs.Bosses.Truffle
{
    public class TruffleProbe : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Truffle Probe");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.width = 14;
            npc.height = 14;
            npc.value = BaseUtility.CalcValue(0, 0, 0, 0);
            npc.npcSlots = 0;
            npc.aiStyle = -1;
            npc.lifeMax = 500;
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
            Player player = Main.player[npc.target]; // makes it so you can reference the player the npc is targetting

            BaseAI.AIFloater(npc, ref npc.ai, false, 0.2f, 2f, 1.5f, 0.04f, 1.5f, 3);

            npc.frameCounter++;
            if (npc.frameCounter > 8)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 20;
                if (npc.frame.Y > 60)
                {
                    npc.frame.Y = 0;
                }
            }
        }
    }
}


