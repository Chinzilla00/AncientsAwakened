
/*namespace AAMod.NPCs.Bosses.Pantheon
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/blushiemagic/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
	public class Mercury : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mercury");
			Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.MoonLordFreeEye];
		}

		public override void SetDefaults()
		{
            npc.defense = 0;
            npc.damage = 60;
            npc.lifeMax = 20000;
            npc.aiStyle = 0;
            npc.width = 98;
            npc.height = 78;
            npc.value = 0f;
            npc.knockBackResist = 0f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.npcSlots = 0f;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (npc.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            spriteBatch.Draw(mod.GetTexture("Glowmasks/InfernalSlime_Glow"), new Vector2(npc.Center.X - Main.screenPosition.X, npc.Center.Y - Main.screenPosition.Y),
            npc.frame, Color.White, npc.rotation,
            new Vector2(npc.width * 0.5f, npc.height * 0.5f), 1f, spriteEffects, 0f);
        }

        public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.IncineriteDust>(), 0f, 0f, 200, default(Color), 0.8f);
                Main.dust[dustIndex].velocity *= 0.3f;
			}
		}
	}
}
*/