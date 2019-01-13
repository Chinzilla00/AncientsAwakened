using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod;
using BaseMod;

namespace AAMod.NPCs.Snow
{
	public class MiniSerpentHead : AANPC
	{
        public override string Texture { get { return "AAMod/NPCs/Enemies/Snow/Serpent"; } }

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Snow Serpent");
		}
		
		public override void SetDefaults()
		{
			npc.damage = 20;
			npc.npcSlots = 5f;
			npc.width = 28; //324
			npc.height = 42; //216
			npc.defense = 15;
			npc.lifeMax = 300;
			npc.knockBackResist = 0f;
			npc.aiStyle = 6;
            aiType = -1;
            animationType = 10;
			npc.value = Item.buyPrice(0, 0, 80, 0);
			npc.behindTiles = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.netAlways = true;
		}
        
        public override void AI() { BaseAI.AIWorm(npc, AAMod.SNAKETYPES, 5, 6f, 8f, 0.07f, false, false); }
        public override bool PreDraw(SpriteBatch sb, Color dColor) { npc.position.Y += npc.height * 0.5f; return true; }
        public override void PostDraw(SpriteBatch sb, Color dColor) { npc.position.Y -= npc.height * 0.5f; }

        public override bool CanSpawn(int x, int y, int type, Player player)
        {
            if (type != mod.NPCType("MiniSerpentHead")) { return false; } //only head can spawn
            if (Main.rand.Next(40) != 0) { return false; }
            return !Main.dayTime && player.InZone("Snow");
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
		{
            player.AddBuff(BuffID.Chilled, 200, true);
        }
		
		public override void HitEffect(int hitDirection, double damage)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.IceDust>(), hitDirection, -1f, 0, default(Color), 1f);
			}
			if (npc.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.SnowDust>(), hitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
		
		public override void NPCLoot()
		{
			if (Main.rand.Next(4) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SubzeroCrystal"));
			}
		}
	}

    public class MiniSerpentBody : MiniSerpentHead
    {

        public override string Texture { get { return "AAMod/NPCs/Enemies/Snow/SerpentBody1"; } }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.dontCountMe = true;
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
    }

    public class MiniSerpentTail : MiniSerpentHead
    {

        public override string Texture { get { return "AAMod/NPCs/Enemies/Snow/SerpentTail"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snow Serpent");
        }

        public override bool PreNPCLoot()
        {
            return false;
        }
    }
}