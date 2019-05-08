using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Enemies.Mire
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/blushiemagic/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class Miresquito : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Miresquito");
			Main.npcFrameCount[npc.type] = 4;
		}

		public override void SetDefaults()
		{
            npc.aiStyle = 1;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.width = 32;
			npc.height = 26;
			npc.damage = 5;
			npc.defense = 2;
			npc.lifeMax = 20;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 60f;
            npc.lavaImmune = true;
            npc.knockBackResist = 0.5f;
            animationType = 81;
        }

        public override void AI()
        {
            Player player = Main.player[npc.target];
            BaseAI.AIFlier(npc, ref npc.ai, false, 0.2f, 0.1f, 3, 2.5f, true, 250);
            npc.rotation = npc.velocity.X * 0.05f;
        }

        public override void NPCLoot()
		{
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HydraToxin"), Main.rand.Next(50,100));
        }
	}
}
