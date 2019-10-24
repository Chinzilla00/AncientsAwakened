using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Critters
{
    public class VoidFlowey : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("???");
            Main.npcFrameCount[npc.type] = 8;
        }

        public override void SetDefaults()
        {
            npc.knockBackResist = 1f;
            npc.width = 28;
            npc.height = 46;
            npc.lifeMax = 1;
            npc.immortal = true;
            npc.friendly = true;
        }
        public override void AI()
        {
            npc.TargetClosest(true);
        }

        int Frame = 0;
        public override void FindFrame(int frameHeight)
        {
            if (npc.frameCounter++ > 2)
            {
                npc.frameCounter = 0;
                Frame += 1;
            }
            else
            {
                if (Frame == 8)
                {
                    //Frame = 0;
                    npc.active = false;
                }
            }

            npc.frame.Y = frameHeight * Frame;
        }
    }
}
