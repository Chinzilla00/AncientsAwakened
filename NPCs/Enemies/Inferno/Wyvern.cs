using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using BaseMod;
using System.IO;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Inferno
{
    public class Wyvern : ModNPC
	{
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Drake");
            Main.npcFrameCount[npc.type] = 8;
		}

		public override void SetDefaults()
		{
            npc.width = 40;
            npc.height = 40;
            npc.value = BaseMod.BaseUtility.CalcValue(0, 0, 60, 50);
            npc.npcSlots = 1;
            npc.aiStyle = -1;
            npc.lifeMax = 250;
            npc.defense = 50;
            npc.damage = 35;
            npc.HitSound = SoundID.NPCHit2;
            npc.DeathSound = SoundID.NPCDeath5;
            npc.knockBackResist = 0.5f;
            npc.noTileCollide = false;
            npc.lavaImmune = true;
            npc.buffImmune[BuffID.OnFire] = true;
        }

		public bool brokenJaw = false;
		public bool canFire = false;
		public float[] shootAI = new float[4];
        public float[] internalAI = new float[4];

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
            }
        }

        //SPAWN CONDITIONS:
        public override void NPCLoot()
		{
			BaseAI.DropItem(npc, mod.ItemType("DragonFire"), 1 + Main.rand.Next(2), 2, 100, true);
		}

        public bool shootFrame = false;
        public int shootTimer = 0;
        public int frameHeight = 84;

        public override void FindFrame(int dummy)
        {
            npc.frameCounter++;
            if (npc.frameCounter > 5)
            {
                npc.frame.Y = frameHeight;
                if (shootFrame)
                {
                    npc.frame.Y = frameHeight * 5;
                }
                npc.frame.Y = frameHeight;
            }
            if (npc.frameCounter > 10)
            {
                npc.frame.Y = frameHeight * 2;
                if (shootFrame)
                {
                    npc.frame.Y = frameHeight * 6;

                }
            }
            if (npc.frameCounter > 15)
            {
                npc.frame.Y = frameHeight * 3;
                if (shootFrame)
                {
                    npc.frame.Y = frameHeight * 7;

                }
            }
            if (npc.frameCounter > 20)
            {
                npc.frame.Y = 0;
                npc.frameCounter = 0;
                if (shootFrame)
                {
                    npc.frame.Y = frameHeight * 4;
                }
            }
        }


        public override void AI()
		{
            internalAI[0]++;
            Lighting.AddLight(npc.Center, Color.DarkOrange.R / 255, Color.DarkOrange.G / 255, Color.DarkOrange.B / 255);
            shootTimer--;
            if (shootTimer < 0)
            {
                shootTimer = 0;
            }
            if (shootTimer > 0)
            {
                shootFrame = true ;
            }
            else
            {
                shootFrame = false;
            }
            npc.noGravity = true;
			npc.noTileCollide = true;
			if (internalAI[0] < 300)
			{
				Player player = Main.player[npc.target];
				Vector2 offsetVec = !brokenJaw ? default(Vector2) : BaseUtility.RotateVector(default(Vector2), new Vector2(0, 20f), BaseUtility.RotationTo(npc.Center, player.Center));
				BaseAI.AITackle(npc, ref npc.ai, player.Center + offsetVec, 0.35f, 6f, true, 60);
				BaseAI.LookAt(player.Center, npc, 0);
			}
            else
			{
				Player player = Main.player[npc.target];
				bool canSee = Collision.CanHit(npc.position, npc.width, npc.height, player.position, player.width, player.height);
                BaseAI.AIFlier(npc, ref npc.ai, true, .05f, .4f, 4, 2, true, 300);
				BaseAI.LookAt(player.Center, npc, 0);
				if (Vector2.Distance(npc.Center, player.Center) <= 220f && canSee)
                { 
					if (Main.netMode != 1)
					{
						BaseAI.ShootPeriodic(npc, player.position, player.width, player.height, mod.ProjType("Dragonflame"), ref shootAI[0], 5, (int)(npc.damage * (Main.expertMode ? 0.25f : 0.5f)), 24f, true, new Vector2(20f, 15f));
					}
                    shootTimer = 120;
                    internalAI[0] = 0;

                }
			}
            npc.rotation = 0;
		}
	}
}