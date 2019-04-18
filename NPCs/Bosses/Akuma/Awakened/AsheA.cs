using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using Terraria.Graphics.Shaders;
using AAMod.NPCs.Bosses.AH.Ashe;

namespace AAMod.NPCs.Bosses.Akuma.Awakened
{
    [AutoloadBossHead]
    public class AsheA : Ashe
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ashe Akuma");
            Main.npcFrameCount[npc.type] = 24;
        }

        public override void SetDefaults()
        {
            npc.boss = false;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Akuma");
        }

        public override void PostAI()
        {
            if (!NPC.AnyNPCs(mod.NPCType<AkumaA>()))
            {
                npc.life = 0;
            }
        }

        public override void NPCLoot()
        {
            npc.value = 0f;
            npc.boss = false;
            int DeathAnim = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<AsheVanish>(), 0);
            Main.npc[DeathAnim].velocity = npc.velocity;
            if (!NPC.AnyNPCs(mod.NPCType<AkumaA>()))
            {
                Main.NewText("Papa, NO! HEY! YOU! I'm gonna bake you alive next time we meet..!", new Color(102, 20, 48));
                return;
            }
            npc.DropLoot(mod.ItemType<Items.Blocks.DaybreakIncineriteOre>(), Main.rand.Next(10, 25));
            Main.NewText("OW, you Jerk..! I'm out!", new Color(102, 20, 48));
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = 0;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 1.3f);  //boss damage increase in expermode
        }
    }
}


