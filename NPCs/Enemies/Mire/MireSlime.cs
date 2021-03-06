using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Mire
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/blushiemagic/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class MireSlime : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Murky Slime");
			Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.BlueSlime];
		}

		public override void SetDefaults()
		{
            npc.aiStyle = 1;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.width = 32;
			npc.height = 26;
			npc.damage = 14;
			npc.defense = 2;
			npc.lifeMax = 100;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 60f;
            npc.lavaImmune = true;
            npc.knockBackResist = 0.5f;
            animationType = 81;
        }

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
            return spawnInfo.player.GetModPlayer<AAPlayer>(mod).ZoneMire && spawnInfo.spawnTileY > Main.worldSurface && !Main.dayTime ? 2f : 0f;
        }

        public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.AbyssiumDust>(), 0f, 0f, 200, default(Color), 0.8f);
                Main.dust[dustIndex].velocity *= 0.3f;
			}
		}
	}
}
