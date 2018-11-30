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

            Main.npcFrameCount[npc.type] = 8;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 120;
            npc.damage = 12;
            npc.defense = 6;
            npc.knockBackResist = 0f;
            npc.value = Item.buyPrice(0, 0, 6, 45);
            animationType = NPCID.Crawdad;
            npc.aiStyle = -1;
            npc.width = 82;
            npc.height = 78;
            npc.lavaImmune = true;
        }

        public override void AI()
        {
            AAAI.InfernoFighterAI(npc, ref npc.ai, true, false, 0, 0.07f, 1f, 3, 4, 60, true, 10, 60, true, null, false);
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (npc.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            spriteBatch.Draw(mod.GetTexture("Glowmasks/Flamebrute_Glow"), new Vector2(npc.Center.X - Main.screenPosition.X, npc.Center.Y - Main.screenPosition.Y),
            npc.frame, Color.White, npc.rotation,
            new Vector2(npc.width * 0.5f, npc.height * 0.5f), 1f, spriteEffects, 0f);
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


