using System;
using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Grips
{
    [AutoloadBossHead]
    public class GripOfChaosBlue : ModNPC
    {
        private Player player;
        private float speed;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grip of Chaos");
            Main.npcFrameCount[npc.type] = 4;    //boss frame/animation 

        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;  //5 is the flying AI
            npc.lifeMax = 1400;   //boss life
            npc.damage = 25;  //boss damage
            npc.defense = 10;    //boss defense
            npc.knockBackResist = 0f;
            npc.width = 66;
            npc.height = 60;
            npc.friendly = false;
            npc.value = Item.buyPrice(0, 4, 50, 0);
            npc.npcSlots = 1f;
            npc.boss = true;  
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/GripsTheme");
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.netAlways = true;
            bossBag = mod.ItemType("GripBag");
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)          //this make so when the npc has 0 life(dead) he will spawn this
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/MireGripGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/MireGripGore2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/MireGripGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/MireGripGore4"), 1f);
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter < 5)
            {
                npc.frame.Y = 0 * frameHeight;
            }
            else if (npc.frameCounter < 10)
            {
                npc.frame.Y = 1 * frameHeight;
            }
            else if (npc.frameCounter < 15)
            {
                npc.frame.Y = 2 * frameHeight;
            }
            else if (npc.frameCounter < 20)
            {
                npc.frame.Y = 3 * frameHeight;
            }
            else
            {
                npc.frameCounter = 0;
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void NPCLoot()
        {
            int GripRed = NPC.CountNPCS(mod.NPCType("GripOfChaosRed"));
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GripTrophyBlue"));
            }
            if (GripRed == 0)
            {
                AAWorld.downedGrips = true;
                if (Main.expertMode)
                {
                    npc.DropBossBags();
                }
            }
            else
            {
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GripMaskBlue"));
                }
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Abyssium"), Main.rand.Next(15, 44));
                }
                {
                    //Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MireInsignia"));
                }
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.LesserHealingPotion;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 0.8f);  //boss damage increase in expermode
        }
        public int timer = 0;
        private int ChargeTimer = 0;
        private bool ActivePhase = false; //Creates a bool for this .cs only
        public override void AI()
        {
            timer++;
            player = Main.player[npc.target];
            DespawnHandler();
            if (timer == 600)
            {
                ActivePhase = true;
            }
            if (timer == 900)
            {
                ActivePhase = false;
                timer = 0;
            }
            if (player.Center.X > npc.Center.X) // so it faces the player
            {
                npc.spriteDirection = 1;
            }
            else
            {
                npc.spriteDirection = -1;
            }
            if (ActivePhase == true)
            {
                ChargeTimer++;
                AAAI.GripAI(npc, ref npc.ai, 0.1f, 0.04f, 4f, 1.5f);
                if (ChargeTimer == 150 || ChargeTimer == 300 || ChargeTimer == 450)
                {

                }
            }
            if (ActivePhase == false)
            {
                Move(new Vector2(240, 0));
            }
        }

        private void Move(Vector2 offset)
        {
            if (Main.expertMode)
            {
                speed = 30f; // Increased movement speed in expert mode (The Keeper only thing, change if you wish)
            }
            else
            {
                speed = 20f; // Sets the max speed of the npc.
            }
            Vector2 moveTo = player.Center + offset; // Gets the point that the npc will be moving to.
            Vector2 move = moveTo - npc.Center;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            float turnResistance = 25f; // The larget the number the slower the npc will turn.
            move = ((npc.velocity * turnResistance) + move) / (turnResistance + 1f);
            magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            npc.velocity = move;
        }

        private void DespawnHandler()
        {
            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead || Main.dayTime)        // If the player is dead and not active, the npc flies off-screen and despawns
                {
                    npc.alpha -= 10;
                    npc.velocity.X = 0;
                    npc.velocity.Y = 0;
                    if (npc.alpha >= 255)
                    {
                        npc.active = false;
                    }
                }
            }
        }
        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt((mag.X * mag.X) + (mag.Y * mag.Y));
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (Main.rand.Next(2) == 0 || (Main.expertMode && Main.rand.Next(0) == 0))
            {
                target.AddBuff(BuffID.Poisoned, Main.rand.Next(180, 250));
            }
        }
    }
}