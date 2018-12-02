using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Inferno
{

    public class Flamebrute : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("FlameBrute");

            Main.npcFrameCount[npc.type] = 6;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 120;
            npc.damage = 12;
            npc.defense = 6;
            npc.knockBackResist = 0f;
            npc.value = Item.buyPrice(0, 0, 6, 45);
            animationType = NPCID.GoblinScout;
            npc.aiStyle = 3;
            npc.width = 82;
            npc.height = 78;
            npc.lavaImmune = true;
        }

        private bool biteAttack;
        private int biteFrame;
        private int biteCounter;
        private int biteTimer;

        public override void AI()
        {
            Player player = Main.player[npc.target]; // makes it so you can reference the player the npc is targetting
            if (biteAttack == false)
            {
                npc.frameCounter++;
                if (npc.frameCounter >= 10)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += 78;
                    if (npc.frame.Y > 390)
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
            if (player.Center.X > npc.Center.X) // so it faces the player
            {
                npc.spriteDirection = -1;
            }
            else
            {
                npc.spriteDirection = 1;
            }
            if (biteAttack == true)
            {
                biteCounter++;
                if (biteCounter > 10)
                {
                    biteFrame++;
                    biteCounter = 0;
                }
                if (biteFrame >= 3)
                {
                    biteFrame = 0;
                }
            }
            float distance = npc.Distance(Main.player[npc.target].Center);
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
            if (biteAttack == false) // so it changes back to aiStyle 3 after the attacks are done
            {
                npc.aiStyle = 3;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Texture2D biteAni = mod.GetTexture("NPCs/Enemies/Inferno/Flamebrute");
            Texture2D tongueAni = mod.GetTexture("NPCs/Enemies/Inferno/Flamebrute_Smash");
            var effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            if (biteAttack == false) // i think this is important for it to not do its usual walking cycle while its also doing those attacks
            {
                spriteBatch.Draw(texture, npc.Center - Main.screenPosition, npc.frame, drawColor, npc.rotation, npc.frame.Size() / 2, npc.scale, npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            if (biteAttack == true)
            {
                Vector2 drawCenter = new Vector2(npc.Center.X, npc.Center.Y);
                int num214 = biteAni.Height / 3; // 3 is the number of frames in the sprite sheet
                int y6 = num214 * biteFrame;
                Main.spriteBatch.Draw(biteAni, drawCenter - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, biteAni.Width, num214)), drawColor, npc.rotation, new Vector2((float)biteAni.Width / 2f, (float)num214 / 2f), npc.scale, npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            return false;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/FlamebruteGoreBackArm"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/FlamebruteGoreBackLeg"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/FlamebruteGoreBody"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/FlamebruteGoreFrontArm"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/FlamebruteGoreFrontLeg"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/FlamebruteGoreHead"), 1f);
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.GetModPlayer<AAPlayer>(mod).ZoneInferno && Main.dayTime ? 1f : 0f;
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DragonScale"));
        }
    }
}


