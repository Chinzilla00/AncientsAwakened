using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.NPCs.Enemies.Mire.Toxitoad
{
    public class Toxitoad : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toxitoad");
            Main.npcFrameCount[npc.type] = 7;
        }
        public override void SetDefaults()
        {
            npc.width = 64;
            npc.height = 32;
            npc.friendly = false; // its a mean toad! :(
            npc.damage = 30;
            npc.defense = 5;
            npc.lifeMax = 120;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 100f;
            npc.knockBackResist = 0.1f;
            npc.aiStyle = 3;
            aiType = NPCID.GoblinScout;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode)
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0.25f;
            }
            return 0f;
        }

        private bool biteAttack;
        private bool tongueAttack;
        private int tongueFrame;
        private int tongueCounter;
        private int tongueTimer;
        private int biteFrame;
        private int biteCounter;
        private int biteTimer;

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int Money = 0; Money < 5; Money++)
            {
                if (Main.rand.Next(7) == 0 || Main.rand.Next(7) == 1)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.CopperCoin);       //Main.rand.Next part is the length of the buff, so 8.3 seconds to 16.6 seconds
                }
                if (Main.rand.Next(7) == 2 || Main.rand.Next(7) == 3 || Main.rand.Next(7) == 4)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SilverCoin);       //Main.rand.Next part is the length of the buff, so 8.3 seconds to 16.6 seconds
                }
                if (Main.rand.Next(7) == 5 || Main.rand.Next(7) == 6)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.GoldCoin);       //Main.rand.Next part is the length of the buff, so 8.3 seconds to 16.6 seconds
                }
                if (Main.rand.Next(7) == 7)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.PlatinumCoin);       //Main.rand.Next part is the length of the buff, so 8.3 seconds to 16.6 seconds
                }
            }
        }

        public override void AI()
        {
            Player player = Main.player[npc.target]; // makes it so you can reference the player the npc is targetting
            if (biteAttack == false && tongueAttack == false)
            {
                npc.frameCounter++;
                if (npc.frameCounter >= 10)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += 36;
                    if (npc.frame.Y > 214)
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
            if (tongueAttack == true)
            {
                if (tongueFrame < 8)
                {
                    tongueCounter++;
                }
                if (tongueCounter > 5)
                {
                    tongueFrame++;
                    tongueCounter = 0;
                }
                if (tongueFrame >= 8)
                {
                    tongueFrame = 7;
                }
            }
            float distance = npc.Distance(Main.player[npc.target].Center);
            if (distance <= 50) // so it only bites when the player is right next to it
            {
                if (biteAttack == false && tongueAttack == false) // so it doesnt bite while its currently biting, and if its doing the tongue attack
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
            if (distance <= 150) // distance until it does the tongue attack
            {
                if (Main.rand.Next(100) == 0) // so it wont do it repeatedly when the player is near. increase to lower the chance of it doing it
                {
                    if (tongueAttack == false && biteAttack == false)
                    {
                        tongueAttack = true;
                    }
                }
            }
            if (tongueAttack == true)
            {
                tongueTimer++;
                npc.aiStyle = 0;
                npc.velocity.X = 0;
                if (tongueTimer >= 35)
                {
                    // projectile code, donno how to do it though, so it just throws up dirt ¯\_(ツ)_/¯
                    if (npc.direction == -1)
                    {
                        //Main.PlaySound(SoundID.Item3, (int)npc.position.X, (int)npc.position.Y);
                        Projectile.NewProjectile((new Vector2(npc.position.X + 17f, npc.position.Y + 18f)), new Vector2(-6 + Main.rand.Next(-6, 0), -4 + Main.rand.Next(-4, 0)), mod.ProjectileType("AcidProj"), 15, 3);
                    }
                    else
                    {
                        //Main.PlaySound(SoundID.Item3, (int)npc.position.X, (int)npc.position.Y);
                        Projectile.NewProjectile((new Vector2(npc.position.X + 57f, npc.position.Y + 18f)), new Vector2(6 + Main.rand.Next(0, 6), -4 + Main.rand.Next(-4, 0)), mod.ProjectileType("AcidProj"), 15, 3);
                    }
                }
                if (tongueTimer >= 100)
                {
                    tongueAttack = false;
                    tongueTimer = 0;
                    tongueCounter = 0;
                    tongueFrame = 0;
                }
            }
            if (tongueAttack == false && biteAttack == false) // so it changes back to aiStyle 3 after the attacks are done
            {
                npc.aiStyle = 3;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Texture2D biteAni = mod.GetTexture("NPCs/Enemies/Mire/Toxitoad/ToxitoadBite");
            Texture2D tongueAni = mod.GetTexture("NPCs/Enemies/Mire/Toxitoad/ToxitoadTongueAttack");
            var effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            if (biteAttack == false && tongueAttack == false) // i think this is important for it to not do its usual walking cycle while its also doing those attacks
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
            if (tongueAttack == true)
            {
                Vector2 drawCenter = new Vector2(npc.Center.X, npc.Center.Y);
                int num214 = tongueAni.Height / 8;
                int y6 = num214 * tongueFrame;
                Main.spriteBatch.Draw(tongueAni, drawCenter - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, tongueAni.Width, num214)), drawColor, npc.rotation, new Vector2((float)tongueAni.Width / 2f, (float)num214 / 2f), npc.scale, npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            return false;
        }
    }
}