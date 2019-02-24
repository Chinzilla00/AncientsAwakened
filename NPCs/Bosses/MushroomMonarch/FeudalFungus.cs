using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ReLogic.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Bosses.MushroomMonarch
{
    [AutoloadBossHead]
    public class FeudalFungus : ModNPC
    {
		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if((Main.netMode == 2 || Main.dedServ))
			{
				writer.Write((float)internalAI[0]);
				writer.Write((float)internalAI[1]);
                writer.Write((float)internalAI[2]);
                writer.Write((float)internalAI[3]);
            }
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
			if(Main.netMode == 1)
			{
				internalAI[0] = reader.ReadFloat();
				internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
            }	
		}	

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Feudal Fungus");
            Main.npcFrameCount[npc.type] = 11;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 1200;   //boss life
            npc.damage = 12;  //boss damage
            npc.defense = 12;    //boss defense
            npc.knockBackResist = 0f;   //this boss will behavior like the DemonEye  //boss frame/animation 
            npc.value = Item.buyPrice(0, 0, 75, 45);
            npc.aiStyle = 26;
            npc.width = 74;
            npc.height = 108;
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.buffImmune[46] = true;
            npc.buffImmune[47] = true;
            npc.netAlways = true;
            bossBag = mod.ItemType("FungusBag");
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Monarch");

        }

        public static int AISTATE_WALK = 0, AISTATE_MAGIC = 1, AISTATE_FLY = 2;
		public float[] internalAI = new float[4];
		
        public override void AI()
        {
            Player player = Main.player[npc.target]; // makes it so you can reference the player the npc is targetting
            

            npc.frameCounter++;
            if (internalAI[1] == AISTATE_WALK) //walk or charge
            {
				if (npc.frameCounter >= 10)
				{
					npc.frameCounter = 0;
					npc.frame.Y += 108;
					if (npc.frame.Y > (108 * 4))
					{
						npc.frameCounter = 0;
						npc.frame.Y = 0;
					}
				}
                if(npc.velocity.Y != 0)
                {
                    if (npc.velocity.Y < 0)
                    {
                        npc.frame.Y = 648;
                    }else
                    if (npc.velocity.Y > 0)
                    {
                        npc.frame.Y = 756;
                    }
                }
            }else if (internalAI[1] == AISTATE_MAGIC) //magic
            {
                if (npc.velocity.Y != 0)
                {
                    npc.frame.Y = 108 * 5;
                }
                else
                {
                    internalAI[2]++;
                    if (internalAI[2] > 15)
                    {
                        npc.frame.Y = 108 * 7;
                        internalAI[3] = Main.rand.Next(4);
                        FungusAttack(internalAI[3]);
                        internalAI[0] = 0;
                        internalAI[1] = AISTATE_WALK;
                        npc.netUpdate = true;
                    }

                }
            }
            if (player.Center.X > npc.Center.X) // so it faces the player
            {
                npc.spriteDirection = -1;
            }else
            {
                npc.spriteDirection = 1;
            }

			if(Main.netMode != 1)
			{
                if (Math.Abs(player.Center.X - npc.Center.X) < 300f)
                {
                    internalAI[0]++;
                    if (internalAI[0] >= 180)
                    {
                        internalAI[0] = 0;
                        internalAI[1] = Main.rand.Next(2);
                        npc.ai = new float[4];
                        npc.netUpdate = true;
                    }
                }
                else
                {
                    internalAI[1] = AISTATE_FLY;
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
            }
			if(internalAI[1] == AISTATE_WALK) //fighter
			{
                AAAI.InfernoFighterAI(npc, ref npc.ai, true, false, 0, 0.07f, 3f, 3, 4, 60, true, 10, 60, true, null, false);				
			}
        }

        public override void BossLoot(ref string name, ref int potionType)
        {   //boss drops
            AAWorld.downedMonarch = true;
            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<FungusDig>());
            if (Main.expertMode == true)
            {
                npc.DropBossBags();
            }
            else
            {

                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GlowingMushium"), Main.rand.Next(25, 35));
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 1.1f);  //boss damage increase in expermode
        }

        public void FungusAttack(float Attack)
        {
            Player player = Main.player[npc.target];

            if (Attack == 0)
            {

            }
            if (Attack == 1)
            {

            }
            if (Attack == 2)
            {
                float spread = 12f * 0.0174f;
                double startAngle = Math.Atan2(npc.velocity.X, npc.velocity.Y) - spread / 2;
                double deltaAngle = spread / (Main.expertMode ? 5 : 4);
                double offsetAngle;
                for (int i = 0; i < (Main.expertMode ? 5 : 4); i++)
                {
                    offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(Math.Sin(offsetAngle) * 6f), (float)(Math.Cos(offsetAngle) * 6f), mod.ProjectileType("FungusCloud"), npc.damage / 2, 0, Main.myPlayer, 0f, 0f);
                }
            }
            else
            {

            }
        }
    }

    
}


