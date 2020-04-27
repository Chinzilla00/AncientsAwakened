using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Void
{
    public class Null : ModNPC
	{
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Null");
            Main.npcFrameCount[npc.type] = 4;
        }
		
		public override void SetDefaults()
		{
            npc.CloneDefaults(NPCID.Poltergeist);
            npc.noGravity = true;
            npc.noTileCollide = true;
			npc.aiStyle = -1;
            npc.width = 24;
            npc.height = 40;
            npc.damage = 50;
            npc.defense = 9999999;
            npc.lifeMax = 100;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/Sounds/Glitch");
            npc.DeathSound = SoundID.NPCDeath6;
            npc.alpha = 70;
            npc.value = 7000f;
            npc.knockBackResist = 0.7f;
            npc.noGravity = true;
            banner = npc.type;
			bannerItem = mod.ItemType("NullBanner");
        }

		public int frameCount = 0;
		public int frameCounter = 0;
		public override void PostAI()
		{
			
			npc.frame = new Rectangle(0, frameCount * 40, 36, 38);
			npc.spriteDirection = npc.velocity.X > 0 ? -1 : 1;
			npc.rotation = npc.velocity.X * 0.25f;
		}

        public override void AI()
        {
            npc.noGravity = true;
            npc.noTileCollide = true;
            for (int m = 0; m < 2; m++)
            {
                BaseAI.AIEye(npc, ref npc.ai, false, true, 0.13f, 0.08f, 2f, 1.1f, 1.2f, 1.2f);
                BaseAI.Look(npc, 1);
            }
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("UnstableSingularity"), 1);

            if (Main.rand.Next(100) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ono"), 1);
            }
        }

        
    }
}