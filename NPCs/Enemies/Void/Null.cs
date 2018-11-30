using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Void
{
    public class Null : ModNPC
	{
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Null");
            Main.npcFrameCount[npc.type] = 4;
        }
		
		public override void SetDefaults()
		{
            npc.CloneDefaults(NPCID.Ghost);
            animationType = NPCID.Ghost;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.width = 24;
            npc.height = 40;
            npc.aiStyle = 36;
            npc.damage = 50;
            npc.defense = 9999999;
            npc.lifeMax = 100;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.alpha = 70;
            npc.value = 7000f;
            npc.knockBackResist = 0.7f;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return ((spawnInfo.player.GetModPlayer<AAPlayer>(mod).ZoneVoid && AAWorld.downedZero && !Main.expertMode) || (spawnInfo.player.GetModPlayer<AAPlayer>(mod).ZoneVoid && AAWorld.downedZeroA && Main.expertMode)) ? 1f : 0f;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (npc.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            spriteBatch.Draw(mod.GetTexture("Glowmasks/Null_Glow"), new Vector2(npc.Center.X - Main.screenPosition.X, npc.Center.Y - Main.screenPosition.Y),
            npc.frame, Color.White, npc.rotation,
            new Vector2(npc.width * 0.5f, npc.height * 0.5f), 1f, spriteEffects, 0f);
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("UnstableSingularity"), 1);
        }
    }
}