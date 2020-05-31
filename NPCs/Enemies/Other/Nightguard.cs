using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using AAMod.Dusts;
using AAMod.Misc;

namespace AAMod.NPCs.Enemies.Other
{
    public class Nightguard : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Night Guard");
            Main.npcFrameCount[npc.type] = 1;
		}

		public override void SetDefaults()
		{
            npc.width = 38;
            npc.height = 38;
            npc.value = 0;
            npc.npcSlots = 1;
            npc.aiStyle = -1;
            npc.lifeMax = 1200;
            npc.defense = 120;
            npc.damage = 80;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath14;
            npc.knockBackResist = 0.3f;
			npc.noGravity = true;
			npc.noTileCollide = true;
			banner = npc.type;
			bannerItem = mod.ItemType("NightGuardBanner");
		}

		public override void HitEffect(int hitDirection, double damage)
		{		
			bool isDead = npc.life <= 0;
			for (int m = 0; m < (isDead ? 25 : 5); m++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, ModContent.DustType<NightcrawlerDust>(), npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 100, Color.White, isDead ? 2f : 1.1f);
			}
		}

		float shootAI = 0;
		public override void AI()
		{
			BaseAI.AISkull(npc, ref npc.ai, false, 6f, 350f, 0.1f, 0.15f);
			Player player = Main.player[npc.target];
			bool playerActive = player != null && player.active && !player.dead;
			BaseAI.LookAt(playerActive ? player.Center : (npc.Center + npc.velocity), npc, 0);		
			if(Main.netMode != NetmodeID.MultiplayerClient && playerActive)
			{
				shootAI++;
				if(shootAI >= 90)
				{
					shootAI = 0;
					int projType = mod.ProjType("Moonray");					
					if(Collision.CanHit(npc.position, npc.width, npc.height, player.position, player.width, player.height))
						BaseAI.FireProjectile(player.Center, npc, projType, (int)(npc.damage * 0.25f), 0f, 2f);
				}
			}
		}

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (AAWorld.Darkmatter < 5)
            {
                return 0f;
            }
            return SpawnCondition.Underground.Chance * 0.1f;
        }

        public override void NPCLoot()
        {
			if(AAWorld.downedEquinox)
			{
				for (int Ammount = 0; Ammount < Main.rand.Next(3); Ammount++)
				{
					npc.DropLoot(ModContent.ItemType<Items.Materials.DarkEnergy>());
				}
			}
        }

        public override Color? GetAlpha(Color dColor)
		{
			Color c = Color.White * (Main.mouseTextColor / 255f);
			c.A = 255;
			return c;
		}		
	}
}