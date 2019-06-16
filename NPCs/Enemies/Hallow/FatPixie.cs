using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Enemies.Hallow
{
    public class FatPixie : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fat Pixie");
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 500;
            npc.damage = 30;
            npc.defense = 15;
            npc.knockBackResist = 0f;
            npc.value = Item.buyPrice(0, 0, 75, 45);
            npc.aiStyle = -1;
            npc.width = 60;
            npc.height = 36;
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.buffImmune[46] = true;
            npc.buffImmune[47] = true;
            npc.netAlways = true;
            npc.HitSound = SoundID.NPCHit5;
            npc.DeathSound = SoundID.NPCDeath7;

        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.ZoneHoly && Main.hardMode ? .05f : 0f;
        }

        int frameCounter = 0;
        public override void AI()
        {
            npc.TargetClosest();
            Player player = Main.player[npc.target];
            if (npc.velocity.Y == 0 || npc.velocity.Y < 0)
            {
                frameCounter--;
                if (frameCounter <= 0)
                {
                    frameCounter = (npc.velocity.Y < 0 ? 3 : 10);
                    npc.frame.Y = (npc.frame.Y == 0 ? npc.frame.Height : 0);
                }
            }
            else
            {
                if (npc.velocity.Y > 0)
                {
                    npc.frame.Y = npc.frame.Height * 2;
                }
            }
            if (npc.velocity.X != 0)
            {
                if (npc.collideX)
                    npc.velocity.X *= -2f;
                if (npc.velocity.X > 0)
                {
                    npc.spriteDirection = 1;
                }
                else
                {
                    npc.spriteDirection = -1;
                }
            }
            float jumpWidth = 3f;
            float jumpHeight = -1f;
            if (npc.whoAmI % 30 == 0) //THE LEGENDARY SUPER FAT PIXIE
            {
                jumpWidth = 8f;
                jumpHeight = -25f;
            }
            BaseAI.AISlime(npc, ref npc.ai, false, 150, 4f, 2f, jumpWidth, jumpHeight);
            BaseDrawing.AddLight(npc.Center, new Color(212, 208, 107), 2f);
        }

        public override void NPCLoot()
        {
            if (Main.netMode != 1)
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.PixieDust, Main.rand.Next(5, 7));
        }

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            BaseMod.BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc, Color.White);
            return false;
        }
    }
}


