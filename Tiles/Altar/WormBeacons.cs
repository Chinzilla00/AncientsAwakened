
/*namespace AAMod.Tiles.Altar
{
	public class WormBeaconDay : ModNPC
	{
        public override string Texture => "AAMod/BlankTex";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Strange Beacon");
        }
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.lifeMax = 20000;
			npc.damage = 0;
			npc.defense = 20;
			npc.knockBackResist = 0f;
            npc.width = 16;
            npc.height = 16;
            npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.alpha = 255;
			npc.dontTakeDamage = true;
			npc.boss = false;
            npc.npcSlots = 0;
            npc.scale = .01f;
        }

		public override bool CheckActive()
		{
			return false;
		}		

		public override void AI()
		{
            if (npc.scale < 1)
            {
                npc.scale += .05f;
            }
            if (npc.alpha > 0)
            {
                npc.alpha += 5;
            }
            if (!AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<DayBeam>()))
            {
                Projectile.NewProjectile(npc.Center, new Vector2(0, -1), ModContent.ProjectileType<DayBeam>(), 0, 0, Main.myPlayer, 0, npc.whoAmI);
            }
        }

        public override bool PreDraw(SpriteBatch spritebatch, Color drawColor)
        {
            BaseDrawing.DrawTexture(spritebatch, Main.extraTexture[50], 0, npc.position, npc.width, npc.height, npc.scale, -npc.rotation, 0, 1, npc.frame, npc.GetAlpha(Color.White), true);
            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, 0, 1, npc.frame, npc.GetAlpha(Color.White), true);
            
            return false;
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            damage = 0;
            return false;
        }
    }

    public class WormBeaconNight : ModNPC
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Strange Beacon");
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 20000;
            npc.damage = 0;
            npc.defense = 20;
            npc.knockBackResist = 0f;
            npc.width = 16;
            npc.height = 16;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.alpha = 255;
            npc.dontTakeDamage = true;
            npc.boss = false;
            npc.npcSlots = 0;
            npc.scale = .01f;
        }

        public override bool CheckActive()
        {
            return false;
        }

        public override void AI()
        {
            if (npc.scale < 1)
            {
                npc.scale += .05f;
            }
            if (npc.alpha > 0)
            {
                npc.alpha += 5;
            }
            if (!AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<NightBeam>()))
            {
                Projectile.NewProjectile(npc.Center, new Vector2(0, -1), ModContent.ProjectileType<NightBeam>(), 0, 0, Main.myPlayer, 0, npc.whoAmI);
            }
        }

        public override bool PreDraw(SpriteBatch spritebatch, Color drawColor)
        {
            BaseDrawing.DrawTexture(spritebatch, Main.extraTexture[50], 0, npc.position, npc.width, npc.height, npc.scale, -npc.rotation, 0, 1, npc.frame, npc.GetAlpha(Color.White), true);
            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, 0, 1, npc.frame, npc.GetAlpha(Color.White), true);

            return false;
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            damage = 0;
            return false;
        }
    }

    public class ZeroHandler : ModWorld
    {
        public static int DX = -1;
        public static int DY = -1;
        public static int NX = -1;
        public static int NY = -1;
        public static int Shield;

        public override void Initialize()
        {
            DX = -1;
            DY = -1;
            NX = -1;
            NY = -1;
        }

        public override TagCompound Save()
        {
            var tag = new TagCompound();
            if (DX != -1)
            {
                tag.Add("DX", DX);
                tag.Add("DY", DY);
            }
            if (NX != -1)
            {
                tag.Add("NX", NX);
                tag.Add("NY", NY);
            }
            return tag;
        }

        public override void Load(TagCompound tag)
        {
			Reset(); //reset it so it doesn't fuck up between world loads	
            if (tag.ContainsKey("DX"))
            {
                DX = tag.GetInt("DX");
                DY = tag.GetInt("DY");
				if(AAWorld.StarActive)			
					NPC.NewNPC(DX, DY, mod.NPCType("WormBeaconDay"));
            }
            if (tag.ContainsKey("NX"))
            {
                DX = tag.GetInt("NX");
                DY = tag.GetInt("NY");
                if (AAWorld.GravActive)
                    NPC.NewNPC(DX, DY, mod.NPCType("WormBeaconNight"));
            }
        }

        public override void PostUpdate()
        {
            if (Main.netMode != 1 && AAWorld.StarActive)
            {
                SpawnDayBeacon();
            }
            if (Main.netMode != 1 && AAWorld.GravActive)
            {
                SpawnNightBeacon();
            }
        }

		public void Reset()
		{
			DX = -1;
			DY = -1;
            NX = -1;
            NY = -1;
        }

        public void SpawnDayBeacon()
        {
            float x = Main.maxTilesX * 0.15f;
            Vector2 spawnPos = new Vector2(x + (30  * 16), 131);

			bool anyBeaconsExist = NPC.AnyNPCs(ModContent.NPCType<WormBeaconDay>());			
			if (!anyBeaconsExist)
            {
                int whoAmI = NPC.NewNPC((int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<WormBeaconDay>());
                DX = (int)spawnPos.X;
				DY = (int)spawnPos.Y;				
				if (Main.netMode == 2 && whoAmI != -1 && whoAmI < 200)
				{					
					NetMessage.SendData(MessageID.SyncNPC, number: whoAmI);
				}			
			}
        }

        public void SpawnNightBeacon()
        {
            float x = Main.maxTilesX * 0.15f;
            Vector2 spawnPos = new Vector2(x + (30 * 16), 131);

            bool anyBeaconsExist = NPC.AnyNPCs(ModContent.NPCType<WormBeaconNight>());
            if (!anyBeaconsExist)
            {
                int whoAmI = NPC.NewNPC((int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<WormBeaconNight>());
                NX = (int)spawnPos.X;
                NY = (int)spawnPos.Y;
                if (Main.netMode == 2 && whoAmI != -1 && whoAmI < 200)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: whoAmI);
                }
            }
        }
    }
}*/
