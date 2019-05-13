using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Enemies.Mire
{
    public class Hotwasp : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hot Wasp");
			Main.npcFrameCount[npc.type] = 4;
		}

		public override void SetDefaults()
		{
            npc.aiStyle = 1;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.width = 32;
			npc.height = 26;
			npc.damage = 40;
			npc.defense = 10;
			npc.lifeMax = 200;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 1000f;
            npc.lavaImmune = true;
            npc.knockBackResist = 0.5f;
        }

        public override void AI()
        {
            BaseAI.AIFlier(npc, ref npc.ai, false, 0.2f, 0.1f, 3, 2.5f, true, 250);
            npc.rotation = npc.velocity.X * 0.05f;
        }

        public override void NPCLoot()
		{
            if (Main.rand.Next(Main.expertMode ? 49 : 99) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Bezoar);
            }
            if (Main.rand.NextFloat(100) < .67f)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.TatteredBeeWing);
            }
        }
	}
}
