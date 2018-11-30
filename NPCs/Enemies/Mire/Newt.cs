using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Mire
{
    public class Newt : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Newt");
            Main.npcFrameCount[npc.type] = 19;
        }

        public override void SetDefaults()
        {
            npc.width = 112;
            npc.height = 30;
            npc.damage = 10;
            npc.defense = 10;
            npc.lifeMax = 200;
            npc.damage = 45;
            npc.defense = 14;
            npc.lifeMax = 210;
            npc.knockBackResist = 0.55f;
            npc.value = 100f;
            npc.aiStyle = 3;
            aiType = NPCID.Crawdad;
            animationType = NPCID.Crawdad;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/NewtGoreTail"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/NewtGoreBody"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/NewtGoreLeg"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/NewtGoreLeg"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/NewtGoreLeg"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/NewtGoreLeg"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/NewtGoreHead"), 1f);
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.GetModPlayer<AAPlayer>(mod).ZoneMire && !Main.dayTime ? 1.25f : 0f;
        }
    }
}