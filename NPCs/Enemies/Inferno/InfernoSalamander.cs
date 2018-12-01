using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Inferno
{

    public class InfernoSalamander : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Singemander");

            Main.npcFrameCount[npc.type] = 8;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 100;   //boss life
            npc.damage = 14;  //boss damage
            npc.defense = 14;    //boss defense
            npc.knockBackResist = 1f;   //this boss will behavior like the DemonEye  //boss frame/animation 
            npc.value = Item.buyPrice(0, 0, 6, 45);
            npc.aiStyle = -1;
            aiType = NPCID.GoblinScout;
            npc.width = 104;
            npc.height = 28;
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;

        }

        private bool biteAttack;
        private int biteFrame;
        private int biteCounter;
        private int biteTimer;

        public override void AI()
        {
            Player player = Main.player[npc.target];
            float distance = npc.Distance(Main.player[npc.target].Center);
            AAAI.InfernoFighterAI(npc, ref npc.ai, true, false, 0, 0.07f, 2f, 3, 4, 60, true, 10, 60, true, null, false);
            npc.frameCounter++;
            if (biteAttack == false)
            {
                if (npc.frameCounter >= 10)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += 28;
                    if (npc.frame.Y > 112)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = 0;
                    }
                }
            }
            else
            {
                npc.frameCounter = 0;
                npc.frame.Y = 0;
            }
            if (distance <= 50) // so it only bites when the player is right next to it
            {
                if (biteAttack == false) // so it doesnt bite while its currently biting, and if its doing the tongue attack
                {
                    biteAttack = true;
                }
            }
            if (biteAttack == true)
            {
                biteTimer++;
                npc.aiStyle = 0; // so the dude doesnt spaz right and left when not moving
                npc.velocity.X = 0; // stops the dude from moving right or left
                if (biteTimer >= 30) // when 30 frames have gone by, reset all those values
                {
                    biteAttack = false;
                    biteTimer = 0;
                    biteCounter = 0;
                    biteFrame = 0;
                }
            }
            if (biteAttack == true)
            {
                biteCounter++;
                if (npc.frameCounter > 10)
                {
                    npc.frameCounter = 140;
                    npc.frame.Y += 0;
                    if (npc.frame.Y > 224)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = 0;
                    }
                }
                if (biteFrame >= 3)
                {
                    biteFrame = 0;
                }
            }
           
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (npc.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            spriteBatch.Draw(mod.GetTexture("Glowmasks/InfernoSalamander_Glow"), new Vector2(npc.Center.X - Main.screenPosition.X, npc.Center.Y - Main.screenPosition.Y),
            npc.frame, Color.White, npc.rotation,
            new Vector2(npc.width * 0.5f, npc.height * 0.5f), 1f, spriteEffects, 0f);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {

            return spawnInfo.player.GetModPlayer<AAPlayer>(mod).ZoneInferno && Main.dayTime ? 1.25f : 0f;
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DragonScale"));
        }
    }
}


