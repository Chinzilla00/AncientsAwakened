using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Void
{
    public class Searcher : ModNPC
	{
		public int timer = 0;
		public bool start = true;
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Searcher");
            Main.npcFrameCount[npc.type] = 1;
        }
		
		public override void SetDefaults()
		{
            npc.CloneDefaults(NPCID.CursedSkull);
            npc.aiStyle = 5;
			npc.width = 30;
			npc.height = 30;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.chaseable = false;
			npc.damage = 30;
			npc.defense = 20;
			npc.lifeMax = 1000;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath14;
		}

        public Color GetGlowAlpha()
        {
            return new Color(233, 53, 53) * (Main.mouseTextColor / 255f);
        }

        public static Texture2D glowTex = null;

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            if (glowTex == null)
            {
                glowTex = mod.GetTexture("Glowmasks/Zero_Glow");
            }
            BaseMod.BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseMod.BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc, GetGlowAlpha());
            return false;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SearcherGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SearcherGore2"), 1f);
            }
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Apocalyptite"));
        }
    }
}