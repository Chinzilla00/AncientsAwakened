using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen
{
    public class ShenDeath : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discord's Death");
        }

        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 100;
            npc.friendly = false;
            npc.lifeMax = 1;
            npc.dontTakeDamage = true;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.alpha = 255;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ChaosSissy");
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        } 

        public override void AI()
        {
            npc.ai[1]++;
            npc.TargetClosest();
            Player player = Main.player[npc.target];
            npc.Center = player.Center;
            if (!AAWorld.downedShen)
            {
                if (npc.ai[1] == 180)
                {
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat(Lang.BossChat("ShenDeath1"), new Color(180, 41, 32));
                }

                if (npc.ai[1] == 360)
                {
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat(Lang.BossChat("ShenDeath2"), new Color(45, 46, 70));
                }

                if (npc.ai[1] == 540)
                {
                    string Name = Main.netMode != 0 ? Lang.BossChat("ShenDeath3") : player.name;
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat(Name + Lang.BossChat("ShenDeath4"), new Color(180, 41, 32));
                }

                if (npc.ai[1] == 720)
                {
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat(Lang.BossChat("ShenDeath5"), new Color(45, 46, 70));
                }

                if (npc.ai[1] >= 900)
                {
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat(Lang.BossChat("ShenDeath6"), new Color(180, 41, 32));
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat(Lang.BossChat("ShenDeath6"), new Color(45, 46, 70));
                    AAWorld.downedShen = true;
                    npc.active = false;
                }
                return;
            }
            else
            {
                if (npc.ai[1] == 180)
                {
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat(Lang.BossChat("ShenDeath7"), new Color(45, 46, 70));
                }

                if (npc.ai[1] == 360)
                {
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat(Lang.BossChat("ShenDeath8"), new Color(180, 41, 32));
                }

                if (npc.ai[1] == 540)
                {
                    string Name = Main.netMode != 0 ? Lang.BossChat("ShenDeath9") : player.Male ? Lang.BossChat("BOY") : Lang.BossChat("GIRL");
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat(Lang.BossChat("ShenDeath10") + Name + Lang.BossChat("ShenDeath11"), new Color(45, 46, 70));
                
                }

                if (npc.ai[1] == 720)
                {
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat(Lang.BossChat("ShenDeath12"), new Color(180, 41, 32));
                }

                if (npc.ai[1] >= 900)
                {
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat(Lang.BossChat("ShenDeath13"), new Color(180, 41, 32));
                    if (Main.netMode != 1) BaseMod.BaseUtility.Chat(Lang.BossChat("ShenDeath13"), new Color(45, 46, 70));
                    npc.active = false;
                }
            }
            
        }

        public override bool CheckActive()
        {
            if (npc.ai[1] >= 900)
            {
                return true;
            }
            return false;
        }
    }
}