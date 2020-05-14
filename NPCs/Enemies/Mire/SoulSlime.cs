using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using BaseMod;
namespace AAMod.NPCs.Enemies.Mire
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/blushiemagic/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class SoulSlime : ModNPC
      {
        public int damage = 0;

          public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soul Slime");
			Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.BlueSlime];
		}


	public override void SetDefaults()
	 {
     aiType = NPCID.BlueSlime;
     animationType = NPCID.BlueSlime;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.width = 44;
	    npc.height = 36;
	    npc.damage = 100;
	    npc.defense = 45;
	    npc.lifeMax = 500;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 6000f;
            npc.lavaImmune = true;
            npc.knockBackResist = 1f;
            animationType = 81;
            npc.buffImmune[BuffID.OnFire] = true;
         }


        public override bool PreAI()
        {
            Lighting.AddLight(npc.Center, Color.Red.R / 255, Color.Red.G / 255, Color.Red.B / 255);
            return true;
        }


        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D tex = Main.npcTexture[npc.type];
           // Texture2D glowTex = mod.GetTexture("Glowmasks/SoulSlime_Glow");
            BaseDrawing.DrawAfterimage(spriteBatch, Main.npcTexture[npc.type], 0, npc, 1f, 1f, 5, false, 0f, 0f, AAColor.YamataA);
            BaseDrawing.DrawTexture(spriteBatch, tex, 0, npc, drawColor);
          //  BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, npc, AAColor.COLOR_WHITEFADE1);
            return false;
        }
        public override void AI()
        {
          npc.ai[1]++;
            Player player = Main.player[npc.target]; // makes it so you can reference the player the npc is targetting
            if (player.Center.Y + player.height / 20 <= npc.Center.Y + npc.height / 2 + 20f && npc.ai[1] % 60 == 0)
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2(-6 + Main.rand.Next(0, 6), -4 + Main.rand.Next(-2, 0)), mod.ProjectileType("SSDartH"), damage, 3);
                        }
                        else if(npc.ai[1] % 80 == 0)
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2(6 + Main.rand.Next(-6, 0), -4 + Main.rand.Next(-2, 0)), mod.ProjectileType("SSDartH"), damage, 3);
                        }
       }


        public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, ModContent.DustType<Dusts.YamataADust>(), 0f, 0f, 200, default, 0.8f);
                Main.dust[dustIndex].velocity *= 0.3f;
			}
		}

		public override void NPCLoot()
		{
			BaseAI.DropItem(npc, mod.ItemType("TerrorSoul"), Main.expertMode ? 1 + Main.rand.Next(2) : Main.rand.Next(1), 3, 100, true);
                }
        }
}
