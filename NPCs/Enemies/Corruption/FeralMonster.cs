using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
            npc.CloneDefaults(NPCID.FaceMonster);
            animationType = NPCID.FaceMonster;
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
