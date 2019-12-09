using Terraria.ModLoader.Config;
using System;
using System.IO;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;

namespace AAMod
{
    public class AAConfigClient : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public static AAConfigClient Instance; // See ExampleConfigServer.Instance for info.

        [Label("$Mods.AAMod.Common.AATownNPC")]
        [Tooltip("$Mods.AAMod.Common.AATownNPCInfo")]
        public bool NoAATownNPC;

        [Label("$Mods.AAMod.Common.DisableBossDialogue")]
        [Tooltip("$Mods.AAMod.Common.DisableBossDialogueInfo")]
        public bool NoBossDialogue;

        [DefaultValue(true)]
        [Label("$Mods.AAMod.Common.AAStyleMainPage")]
        [Tooltip("$Mods.AAMod.Common.AAStyleMainPageInfo")]
        public bool AAStyleMainPage;
    }

    public static class Config
	{
		public static void Load()
		{
			if (!Config.ReadConfig())
			{
				Config.SetDefaults();
				ModContent.GetInstance<AAMod>().Logger.Warn("Couldn't find config file! Creating a new one...");
			}
			Config.SaveConfig();
		}

        private static readonly string ConfigPath = Path.Combine(Main.SavePath, "Mod Configs", "AAConfig.json");
		private static Preferences Configuration = new Preferences(Config.ConfigPath, false, false);

        public static void SetDefaults()
		{
            Config.LuckyOre = new Dictionary<int, int>();
            Config.LuckyPotion = new Dictionary<int, int>();
            Config.ListRareNpc = new List<int>();
        }

        public static bool ReadConfig()
		{
			if (Config.Configuration.Load())
			{
                try
                {
                    Config.Configuration.Get<Dictionary<int, int>>("LuckyOreMine", ref Config.LuckyOre);
                    Config.Configuration.Get<Dictionary<int, int>>("LuckyPotionGet", ref Config.LuckyPotion);
                    Config.Configuration.Get<List<int>>("RareNpcList", ref Config.ListRareNpc);
                }
				catch
                {
                    return false;
                }
				return true;
			}
			return false;
        }

        public static void SaveConfig()
		{
            Config.Configuration.Clear();
			Config.Configuration.Put("LuckyOreMine", Config.LuckyOre);
			Config.Configuration.Put("LuckyPotionGet", Config.LuckyPotion);
            Config.Configuration.Put("RareNpcList", Config.ListRareNpc);
            Config.Configuration.Save(true);
        }

        public static Dictionary<int, int> LuckyOre = new Dictionary<int, int>();
        public static Dictionary<int, int> LuckyPotion = new Dictionary<int, int>();
        public static List<int> ListRareNpc = new List<int>();
        
    }
}
