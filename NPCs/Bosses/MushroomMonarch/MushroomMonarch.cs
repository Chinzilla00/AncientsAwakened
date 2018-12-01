using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Bosses.MushroomMonarch
{

    [AutoloadBossHead]
    public class MushroomMonarch : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mushroom Monarch");

            Main.npcFrameCount[npc.type] = 8;
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
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Monarch");
            npc.netAlways = true;
            bossBag = mod.ItemType("MonarchBag");

        }

        private int aiTimer;
        private bool Jump;
        private bool Charge;
        private bool Walk;
        private int JumpFrame;
        private int JumpCounter;
        private int JumpTimer;


        public override void AI()
        {
            Player player = Main.player[npc.target]; // makes it so you can reference the player the npc is targetting
            npc.frameCounter++;
            if (Walk == true || Charge == true)
            {
                npc.frameCounter++;
                if (npc.velocity.Y == 0)
                {
                    if (npc.frameCounter >= 10)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y += 74;
                        if (npc.frame.Y > 432)
                        {
                            npc.frameCounter = 0;
                            npc.frame.Y = 0;
                        }
                    }
                }
                else
                {
                    if (npc.velocity.Y < 0)
                    {
                        npc.frame.Y = 648;
                    }
                    if (npc.velocity.Y > 0)
                    {
                        npc.frame.Y = 756;
                    }
                }
            }
            if (Jump == true)
            {
                if (npc.velocity.Y == 0)
                {
                    npc.frame.Y = 540;
                }
                else
                {
                    if (npc.velocity.Y < 0)
                    {
                        npc.frame.Y = 648;
                    }
                    if (npc.velocity.Y > 0)
                    {
                        npc.frame.Y = 756;
                    }
                }
            }
            else
            {
                npc.frameCounter = 0;
                npc.frame.Y = 0;
            }
            if (player.Center.X > npc.Center.X) // so it faces the player
            {
                npc.spriteDirection = -1;
            }
            else
            {
                npc.spriteDirection = 1;
            }
            aiTimer++;
            if (aiTimer >= 300)
            {
                aiTimer = 0;
                int choice = Main.rand.Next(3);
                if (choice == 0)
                {
                    Charge = false;
                    Jump = false;
                    Walk = true;
                    AAAI.InfernoFighterAI(npc, ref npc.ai, true, false, 0, 0.07f, 1f, 3, 4, 60, true, 10, 60, true, null, false);
                }
                if (choice == 1)
                {
                    Charge = false;
                    Walk = false;
                    Jump = true;
                    BaseAI.AISlime(npc, ref npc.ai, true, 10, 6f, 4f, 6f, 5f);
                }
                if (choice == 2)
                {
                    Walk = false;
                    Jump = false;
                    Charge = true;
                    BaseAI.AICharger(npc, ref npc.ai, 0.07f, 10f, false, 30);
                }
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {   //boss drops
            AAWorld.downedMonarch = true;
            Projectile.NewProjectile(new Vector2(npc.position.X, npc.position.Y - 2), new Vector2(0f, 0f), mod.ProjectileType("MonarchRUNAWAY"), 0, 0);
            if (Main.expertMode == true)
            {
                npc.DropBossBags();
            }
            else
            {

                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Mushium"), Main.rand.Next(25, 35));
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 1.1f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 1.1f);  //boss damage increase in expermode
        }
    }
}


