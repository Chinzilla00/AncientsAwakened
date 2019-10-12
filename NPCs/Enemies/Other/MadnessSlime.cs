using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Other
{
    public class MadnessSlime : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Madness Slime");
			Main.npcFrameCount[npc.type] = 2;
		}
		
		public override void SetDefaults()
		{
			npc.aiStyle = 1;
			npc.damage = 7;
            npc.width = 30;
			npc.height = 22;
			npc.defense = 4;
			npc.lifeMax = 25;
			npc.knockBackResist = 0f;
			animationType = 81;
			npc.value = Item.sellPrice(0, 0, 5, 0);
			npc.alpha = 60;
			npc.lavaImmune = false;
			npc.noGravity = false;
			npc.noTileCollide = false;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.playerSafe || Main.hardMode)
			{
				return 0f;
			}
			return SpawnCondition.OverworldDaySlime.Chance * 0.1f;
		}
		
		public override void HitEffect(int hitDirection, double damage)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, Main.rand.Next(2) == 0 ? ModContent.DustType<Dusts.InfinityOverloadR>() : ModContent.DustType<Dusts.InfinityOverloadP>(), hitDirection, -1f, 0);
			}
			if (npc.life <= 0)
			{
				for (int k = 0; k < 15; k++)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, Main.rand.Next(2) == 0 ? ModContent.DustType<Dusts.InfinityOverloadR>() : ModContent.DustType<Dusts.InfinityOverloadP>(), hitDirection, -1f, 0);
				}
			}
		}
		
		public override void NPCLoot()
		{
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MadnessFragment"), Main.rand.Next(2, 3));
		}
		
	}
}