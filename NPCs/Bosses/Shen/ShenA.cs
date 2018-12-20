using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen
{
    [AutoloadBossHead]
    public class ShenA : ShenDoragon
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shen Doragon Awakened; Unyieldng Chaos Incarnate");
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 230;
            npc.defense = 230;
            npc.lifeMax = 1600000;
            npc.value = Item.buyPrice(40, 0, 0, 0);
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/ShenA");
			isAwakened = true;
        }

        public bool Health9 = false;
        public bool Health8 = false;
        public bool Health7 = false;
        public bool Health6 = false;
        public bool Health5 = false;
        public bool Health4 = false;
        public bool Health3 = false;
        public bool Health2 = false;
        public bool Health1 = false;
        public bool HealthOneHalf = false;

        public override void HitEffect(int hitDirection, double damage)
        {
            base.HitEffect(hitDirection, damage);			
            if (npc.life <= npc.lifeMax * 0.9f && !Health9)
            {
                if (Main.netMode != 1) BaseUtility.Chat("Face it, child..! You’ll never defeat the living embodiment of disarray itself..!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                Health9 = true;
            }
            if (npc.life <= npc.lifeMax * 0.8f && !Health8)
            {
                if (Main.netMode != 1) BaseUtility.Chat("You’re still going? How amusing…", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                Health8 = true;
            }
            if (npc.life <= npc.lifeMax * 0.7f && !Health7)
            {
                if (Main.netMode != 1) BaseUtility.Chat("Putting up a fight when you know Death is inevitable...", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                Health7 = true;
            }
            if (npc.life <= npc.lifeMax * 0.6f && !Health6)
            {
                if (Main.netMode != 1) BaseUtility.Chat("Now stop making this hard! Stand still and take it like a man!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                Health6 = true;
            }
            if (npc.life <= npc.lifeMax * 0.5f && !Health5)
            {
                if (Main.netMode != 1) BaseUtility.Chat("This is getting real obnoxious chasing you around, you know..!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                Health5 = true;
            }
            if (npc.life <= npc.lifeMax * 0.4f && !Health4)
            {
                if (Main.netMode != 1) BaseUtility.Chat("DIE ALREADY YOU INSIGNIFICANT LITTLE WORM!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                Health4 = true;
            }
            if (npc.life <= npc.lifeMax * 0.3f && !Health3)
            {
                if (Main.netMode != 1) BaseUtility.Chat("WHAT?! HOW HAVE YOU-- ENOUGH! YOU WILL KNOW WHAT IT MEANS TO FEEL UNYIELDING CHAOS!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                Health3 = true;
            }
            if (npc.life <= npc.lifeMax * 0.2f && !Health2)
            {
                if (Main.netMode != 1) BaseUtility.Chat("NO! I WILL NOT LOSE! NOT TO YOU!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                Health2 = true;
            }
            if (npc.life <= npc.lifeMax * 0.1f && !Health1)
            {
                if (Main.netMode != 1) BaseUtility.Chat("GRAAAAAAAAAH!!!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                Health1 = true;
            }
            if (npc.life <= npc.lifeMax * 0.05f && !Health1)
            {
                if (Main.netMode != 1) BaseUtility.Chat("I AM SHEN DORAGON! BRINGER OF DEATH AND DISASTER, AND I WILL NOT BE OUTDONE BY A HAIRLESS APE! PREPARE FOR YOUR DEMISE!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                HealthOneHalf = true;
            }
            if (Health3)
            {
                music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/LastStand");
            }			
        }

        public override bool PreDraw(SpriteBatch sb, Color drawColor)
        {
			Texture2D currentTex = Main.npcTexture[npc.type];
			Texture2D currentWingTex = mod.GetTexture("NPCs/Bosses/Shen/ShenAWings");
            Texture2D glowTex = mod.GetTexture("NPCs/Bosses/Shen/ShenA_Glow");

			//offset
			npc.position.Y += 130f;

			//draw body/charge afterimage
			if(Charging)
			{
				BaseDrawing.DrawAfterimage(sb, currentTex, 0, npc, 1.5f, 1f, 3, false, 0f, 0f, new Color(drawColor.R, drawColor.G, drawColor.B, (byte)150));	
			}
			BaseDrawing.DrawTexture(sb, currentTex, 0, npc, drawColor);
			
			//draw glow/glow afterimage
            BaseDrawing.DrawTexture(sb, glowTex, 0, npc, AAColor.Shen3);
			BaseDrawing.DrawAfterimage(sb, glowTex, 0, npc, 0.8f, 1f, 4, false, 0f, 0f, AAColor.Shen3);	
			
			//draw wings
			BaseDrawing.DrawTexture(sb, currentWingTex, 0, npc.position + new Vector2(0, npc.gfxOffY), npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, 5, wingFrame, drawColor);

			//deoffset
			npc.position.Y -= 130f; // offsetVec;			

            return false;
        }		
    }
    
}
