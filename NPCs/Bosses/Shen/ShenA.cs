using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen
{
    [AutoloadBossHead]
    public class ShenA : ShenDoragon
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shen Doragon Awakened; Unyielding Chaos Incarnate");
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 260;
            npc.defense = 210;
            npc.lifeMax = 1200000;
            npc.value = Item.sellPrice(1, 0, 0, 0);
            bossBag = mod.ItemType("ShenCache");
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ShenA");
            musicPriority = (MusicPriority)11;
            isAwakened = true;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.5f * bossLifeScale);
            npc.defense = (int)(npc.defense * 1.2f);
            npc.damage = (int)(npc.damage * .8f);
            damageDiscordianInferno = (int)(damageDiscordianInferno * 1.2f);
        }

        public bool Health9 = false;
        public bool Health8 = false;
        public bool Health7 = false;
        public bool Health6 = false;
        public bool HealthOneHalf = false;

        public override void HitEffect(int hitDirection, double damage)
        {
            base.HitEffect(hitDirection, damage);
            if (npc.life <= npc.lifeMax * 0.9f && !Health9)
            {
                if (AAWorld.downedShen)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("I must say, child. You impress me.", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                else
                {
                    if (Main.netMode != 1) BaseUtility.Chat("Face it, child! You’ll never defeat the living embodiment of disarray itself!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                Health9 = true;
                npc.netUpdate = true;
            }
            if (npc.life <= npc.lifeMax * 0.8f && !Health8)
            {
                if (AAWorld.downedShen)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("You fight, even when the odds are stacked against you.", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                else
                {
                    if (Main.netMode != 1) BaseUtility.Chat("You’re still going? How amusing...", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                Health8 = true;
                npc.netUpdate = true;
            }
            if (npc.life <= npc.lifeMax * 0.7f && !Health7)
            {
                if (AAWorld.downedShen)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("You remind me of myself quite a bit, to be honest...", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                else
                {
                    if (Main.netMode != 1) BaseUtility.Chat("Putting up a fight when you know Death is inevitable...", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                Health7 = true;
                npc.netUpdate = true;
            }
            if (npc.life <= npc.lifeMax * 0.6f && !Health6)
            {
                if (AAWorld.downedShen)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("Maybe some day, you'll have your own realm to rule over...", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                else
                {
                    if (Main.netMode != 1) BaseUtility.Chat("Now stop making this hard! Stand still and take it like a man!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                Health6 = true;
                npc.netUpdate = true;
            }
            if (npc.life <= npc.lifeMax * 0.4f && !Health4)
            {
                if (AAWorld.downedShen)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("But today, we clash! Now show me what you got!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                else
                {
                    if (Main.netMode != 1) BaseUtility.Chat("DIE ALREADY YOU INSIGNIFICANT LITTLE WORM!!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                Health4 = true;
                npc.netUpdate = true;
            }
            if (npc.life <= npc.lifeMax * 0.3f && !Health3)
            {
                if (AAWorld.downedShen)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("Still got it? I'm impressed. Show me your true power!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                else
                {
                    if (Main.netMode != 1) BaseUtility.Chat("WHAT?! HOW HAVE YOU- ENOUGH! YOU WILL KNOW WHAT IT MEANS TO FEEL UNYIELDING CHAOS!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                Health3 = true;
                npc.netUpdate = true;
            }
            if (npc.life <= npc.lifeMax * 0.2f && !Health2)
            {
                if (AAWorld.downedShen)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("Come on! KEEP PUSHING!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                else
                {
                    if (Main.netMode != 1) BaseUtility.Chat("NO! I WILL NOT LOSE! NOT TO YOU!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                Health2 = true;
                npc.netUpdate = true;
            }
            if (npc.life <= npc.lifeMax * 0.1f && !Health1)
            {
                if (AAWorld.downedShen)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("SHOW ME! SHOW ME THE TRUE POWER YOU HOLD!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                else
                {
                    if (Main.netMode != 1) BaseUtility.Chat("GRAAAAAAAAAH!!!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                Health1 = true;
                npc.netUpdate = true;
            }
            if (Health2)
            {
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/LastStand");
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color drawColor)
        {
            Texture2D currentTex = Main.npcTexture[npc.type];
            Texture2D currentWingTex1 = mod.GetTexture("NPCs/Bosses/Shen/ShenWingBack");
            Texture2D currentWingTex2 = mod.GetTexture("NPCs/Bosses/Shen/ShenWingFront");
            Texture2D glowTex = mod.GetTexture("NPCs/Bosses/Shen/ShenA_Glow");

            //offset
            npc.position.Y += 130f;

            //draw body/charge afterimage
            BaseDrawing.DrawTexture(sb, currentWingTex1, 0, npc.position + new Vector2(0, npc.gfxOffY), npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, 5, wingFrame, drawColor);
            if (Charging)
            {
                BaseDrawing.DrawAfterimage(sb, currentTex, 0, npc, 1.5f, 1f, 3, false, 0f, 0f, new Color(drawColor.R, drawColor.G, drawColor.B, 150));
            }
            BaseDrawing.DrawTexture(sb, currentTex, 0, npc, drawColor);

            //draw glow/glow afterimage
            BaseDrawing.DrawTexture(sb, glowTex, 0, npc, AAColor.Shen3);
            BaseDrawing.DrawAfterimage(sb, glowTex, 0, npc, 0.3f, 1f, 8, false, 0f, 0f, AAColor.Shen3);

            //draw wings
            BaseDrawing.DrawTexture(sb, currentWingTex2, 0, npc.position + new Vector2(0, npc.gfxOffY), npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, 5, wingFrame, drawColor);

            //deoffset
            npc.position.Y -= 130f; // offsetVec;			

            return false;
        }
    }

}
