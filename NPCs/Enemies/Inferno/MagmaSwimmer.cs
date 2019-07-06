using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using System;

namespace AAMod.NPCs.Enemies.Inferno
{
    public class MagmaSwimmer : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Magma Swimmer");
            Main.npcFrameCount[npc.type] = 4;
		}

		public override void SetDefaults()
        {
            npc.width = 86;
            npc.height = 38;
            npc.damage = 60;
			npc.defense = 30;
			npc.lifeMax = 110;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 5000;
            npc.knockBackResist = .10f;
            npc.aiStyle = -1;
            npc.lavaImmune = true;
            npc.buffImmune[BuffID.OnFire] = true;
        }

        public override void AI()
        {
            Lighting.AddLight(npc.Center, AAColor.Lantern.R / 255, AAColor.Lantern.G / 255, AAColor.Lantern.B / 255);
            if (npc.wet)
            {
                npc.noGravity = true;
                BaseAI.AIFish(npc, ref npc.ai, true, false, true, 4f, 3f);
                BaseAI.Look(npc, 1);
                if (!Collision.WetCollision(npc.position + npc.velocity, npc.width, npc.height)) { npc.velocity.Y -= 3f; }
            }
            else
            {
                if (npc.velocity.Y == 0f)
                {
                    npc.velocity.Y = Main.rand.Next(-50, -20) * 0.1f;
                    npc.velocity.X = Main.rand.Next(-20, 20) * 0.1f;
                    npc.netUpdate = true;
                }
                npc.velocity.Y = npc.velocity.Y + 0.3f;
                if (npc.velocity.Y > 10f)
                {
                    npc.velocity.Y = 10f;
                }
                npc.ai[0] = 1f;
                npc.noGravity = false;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            Player player = Main.player[npc.target];
            float playerDistX = Math.Abs(player.Center.X - npc.Center.X);
            float playerDistY = Math.Abs(player.Center.Y - npc.Center.Y);
            bool BiteAttack = playerDistX < 35f && playerDistY < 40f;
            int frameMax = (BiteAttack ? 8 : 5);
            if (npc.frameCounter++ >= frameMax)
            {
                npc.frameCounter = 0;
                if (BiteAttack)
                {
                    npc.frame.Y += 44;
                    if (npc.frame.Y < 44 * 2 || npc.frame.Y > 44 * 3)
                    {
                        npc.frame.Y = 44 * 6;
                    }
                }
                else
                {
                    npc.frame.Y += 44;
                    if (npc.frame.Y > 44)
                    {
                        npc.frame.Y = 0;
                    }
                }
            }
        }

		public override void NPCLoot()
		{
            if (Main.rand.Next(Main.expertMode ? 49 : 99) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.AdhesiveBandage);
            }
            if (Main.rand.Next(100) < 4)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.RobotHat);
            }
        }
	}
}