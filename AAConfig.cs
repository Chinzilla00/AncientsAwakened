﻿using Terraria.ModLoader.Config;
using System.IO;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using System.ComponentModel;
using System.Collections.Generic;

namespace AAMod
{
    public class AAConfigClient : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public static AAConfigClient Instance; // See ExampleConfigServer.Instance for info.

        [Label("$Mods.AAMod.AAConfig.AATownNPC")]
        [Tooltip("$Mods.AAMod.AAConfig.AATownNPCInfo")]
        public bool NoAATownNPC;

        [Label("$Mods.AAMod.AAConfig.DisableBossDialogue")]
        [Tooltip("$Mods.AAMod.AAConfig.DisableBossDialogueInfo")]
        public bool NoBossDialogue;

        [DefaultValue(false)]
        [Label("$Mods.AAMod.AAConfig.AAStyleMainPage")]
        [Tooltip("$Mods.AAMod.AAConfig.AAStyleMainPageInfo")]
        public bool AAStyleMainPage;

        [DefaultValue(true)]
        [Label("$Mods.AAMod.AAConfig.AABossTitle")]
        [Tooltip("$Mods.AAMod.AAConfig.AABossTitleInfo")]
        public bool AncientIntroText;
    }

    public static class Config
	{
		public static void Load()
		{
			if (!ReadConfig())
			{
                SetDefaults();
				ModContent.GetInstance<AAMod>().Logger.Warn("Couldn't find config file! Creating a new one...");
			}
            SaveConfig();
		}

        private static readonly string ConfigPath = Path.Combine(Main.SavePath, "Mod Configs", "AAConfig.json");
		private static readonly Preferences Configuration = new Preferences(ConfigPath, false, false);

        public static void SetDefaults()
		{
            LuckyOre = new Dictionary<int, int>();
            LuckyPotion = new Dictionary<int, int>();
            ListRareNpc = new List<int>();
        }

        public static bool ReadConfig()
		{
			if (Configuration.Load())
			{
                try
                {
                    Configuration.Get("LuckyOreMine", ref LuckyOre);
                    Configuration.Get("LuckyPotionGet", ref LuckyPotion);
                    Configuration.Get("RareNpcList", ref ListRareNpc);
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
            Configuration.Clear();
            Configuration.Put("LuckyOreMine", LuckyOre);
            Configuration.Put("LuckyPotionGet", LuckyPotion);
            Configuration.Put("RareNpcList", ListRareNpc);
            Configuration.Save(true);
        }

        public static Dictionary<int, int> LuckyOre = new Dictionary<int, int>();
        public static Dictionary<int, int> LuckyPotion = new Dictionary<int, int>();
        public static List<int> ListRareNpc = new List<int>();
        
    }
}
