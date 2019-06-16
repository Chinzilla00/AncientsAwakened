using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Corruption
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/blushiemagic/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class FeralMonster : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Feral Monster");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.FaceMonster];
        }

        public override void SetDefaults()
        {
            npc.width = 18;
            npc.height = 44;
            npc.aiStyle = -1;
            npc.damage = 30;
            npc.defense = 8;
            npc.lifeMax = 60;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath2;
            npc.knockBackResist = 0.4f;
            npc.value = 200f;
            npc.buffImmune[20] = true;
            npc.buffImmune[31] = false;
            animationType = NPCID.FaceMonster;
        }

        public override void AI()
        {
            AAAI.CorruptFighterAI(npc, ref npc.ai, true, -1, 0.07f, 1f, 3, 4, 120, true, 10, 60, false, null, false);
        }


        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.Corruption.Chance * .8f;
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.RottenChunk);
        }
    }
}
