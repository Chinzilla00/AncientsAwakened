using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Inferno
{
    public class InfernoMimic : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Inferno Mimic");
			Main.npcFrameCount[npc.type] = Main.npcFrameCount[475];
		}

		public override void SetDefaults()
        {
            npc.width = 34;
            npc.height = 42;
            npc.damage = 50;
			npc.defense = 8;
			npc.lifeMax = 3500;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath6;
            npc.value = 240000f;
            npc.knockBackResist = .30f;
            npc.aiStyle = 87;
            aiType = NPCID.Zombie;
            animationType = 475;
            npc.lavaImmune = true;
            npc.buffImmune[BuffID.OnFire] = true;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.player;
            if (spawnInfo.player.GetModPlayer<AAPlayer>(mod).ZoneInferno && Main.hardMode && !spawnInfo.playerSafe)
            {
                return SpawnCondition.UndergroundMimic.Chance;
            }
            return 0f;
        }

        public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, Vector2.Zero, 13);
				Gore.NewGore(npc.position, Vector2.Zero, 12);
				Gore.NewGore(npc.position, Vector2.Zero, 11);
			}
		}

		public override void NPCLoot()
		{
			string[] lootTable = { "OrnateBand", "SunLance" };
			int loot = Main.rand.Next(lootTable.Length);
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType(lootTable[loot]));
		}
	}
}