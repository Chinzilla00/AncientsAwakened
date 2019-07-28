using BaseMod;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Athena
{
    public class OlympianDragon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Olympian Dragon");
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void SetDefaults()
        {
            npc.width = 38;
            npc.height = 38;
            npc.aiStyle = 0;
            npc.damage = 30;
            npc.defense = 30;
            npc.lifeMax = 500;
            npc.HitSound = SoundID.DD2_WyvernHurt;
            npc.DeathSound = SoundID.DD2_WyvernDeath;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 0.05f;
            npc.npcSlots = 0f;
            npc.lavaImmune = true;
            npc.netAlways = true;
        }

        public override void AI()
        {
            BaseAI.AIFlier(npc, ref npc.ai, true, 0.15f, 0.08f, 6f, 5f, false, 300);
            Player player = Main.player[npc.target];
            if (player.Center.X > npc.Center.X)
            {
                npc.spriteDirection = 1;
            }
            else
            {
                npc.spriteDirection = -1;
            }
            npc.frameCounter++;
            if (npc.frameCounter >= 10)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 96;
                if (npc.frame.Y > (96 * 3))
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = 0;
                }
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.playerSafe || !Main.hardMode)
            {
                return 0f;
            }
            return SpawnCondition.Sky.Chance * 0.10f;
        }

    }
}