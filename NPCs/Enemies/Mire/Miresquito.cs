using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


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
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.width = 64;
			npc.height = 64;
			npc.damage = 70;
			npc.defense = 10;
			npc.lifeMax = 300;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 6000f;
            npc.lavaImmune = false;
            npc.knockBackResist = 0.5f;
            banner = npc.type;
			bannerItem = mod.ItemType("MiresquitoBanner");
        }

        public override void FindFrame(int frameHeight)
        {
            if (npc.frameCounter++ > 7)
            {
                npc.frame.Y += 60;
                npc.frameCounter = 0;
                if (npc.frame.Y >= 240)
                {
                    npc.frame.Y = 0;
                }
            }
        }

        public override void AI()
        {
            BaseAI.AIFlier(npc, ref npc.ai, false, 0.2f, 0.1f, 3, 2.5f, true, 250);
            npc.rotation = npc.velocity.X * 0.05f;
            if (npc.velocity.X > 0)
            {
                npc.spriteDirection = 1;
            }
            else
            {
                npc.spriteDirection = -1;
            }
        }

        public override void NPCLoot()
		{
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HydraToxin"), Main.rand.Next(1,2));
        }
	}
}
