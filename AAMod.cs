using AAMod.Backgrounds;
using AAMod.Globals;
using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.Utilities;

namespace AAMod
{
    public class AAMod : Mod
    {
        // Miscellaneous
        public static int GoblinSoul = -1;

        // Hotkeys
        public static ModHotKey InfinityHotKey;
        public static ModHotKey AccessoryAbilityKey;
        public static ModHotKey ArmorAbilityKey;
        public static ModHotKey Rift;
        public static ModHotKey RiftReturn;
        public static ModHotKey TimeStone;

        // Textures
        public static IDictionary<string, Texture2D> Textures = null;
        public static Dictionary<string, Texture2D> precachedTextures = new Dictionary<string, Texture2D>();
        public static string BLANK_TEX = "AAMod/BlankTex";

        // UI
        internal UserInterface UserInterface;
        private static readonly int UI_ScreenAnchorX = Main.screenWidth - 800;
        private static int UIDisplay_ManaPerStar = 20;
        public static SpriteFont fontMouseText;

        public static int[] SNAKETYPES = new int[0];
        public static int[] SERPENTTYPES = new int[0];

        public static bool thoriumLoaded = false;

        internal static AAMod instance;
        public static AAMod self = null;

        public AAMod()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true,
                AutoloadBackgrounds = true
            };
            instance = this;
        }

        public static void SetupBannerItemTextures()
        {
            if (Main.netMode == 2 || Main.dedServ) return; //don't do any texture stuff on a server lol
            try
            {
                int fx = 16;
                Texture2D tex = Main.tileTexture[instance.TileType("Banners")];

                while (Tiles.Banners.Banners.GetBannerName(fx) != null)
                {
                    string name = Tiles.Banners.Banners.GetBannerName(fx);

                    if (name.Equals("DUMMY"))
                    {
                        fx += 16;
                        continue;
                    }

                    Main.itemTexture[instance.ItemType(name + "Banner")] = BaseDrawing.GetCroppedTex(tex, new Rectangle(fx, 0, 16, 16 * 3));
                    fx += 16;
                }
            }
            catch (Exception e)
            {
                ErrorLogger.Log(e.Message);
                ErrorLogger.Log(e.StackTrace);
            }
        }

        public static FieldInfo _bannerField = null;
        public static IDictionary<int, int> BannerToItemDict
        {
            get
            {
                if (_bannerField == null)
                {
                    _bannerField = typeof(NPCLoader).GetField("bannerToItem", BindingFlags.NonPublic | BindingFlags.Static);
                }
                return (IDictionary<int, int>)_bannerField.GetValue(null);
            }
            set
            {
                if (_bannerField != null)
                {
                    _bannerField.SetValue(null, value);
                }
            }
        }

        public static void SetupBannerNPCs()
        {
            Mod mod = instance;
            try
            {
                IDictionary<int, int> bannerToItem = BannerToItemDict;
                int fx = 16;

                while (Tiles.Banners.Banners.GetBannerName(fx) != null)
                {
                    string name = Tiles.Banners.Banners.GetBannerName(fx, false);

                    if (name.Equals("DUMMY"))
                    {
                        fx += 16;
                        continue;
                    }

                    if (name.Contains("Wyrmling"))
                    {
                        for (int m = 0; m < 4; m++)
                        {
                            ModNPC npc = mod.GetNPC(m == 0 ? "Wyrmling" : (m == 1 ? "WyrmlingBody" : (m == 2 ? "WyrmlingTail1" : "WyrmlingTail2")));
                            if (npc != null)
                            {
                                npc.banner = mod.NPCType("Wyrmling");
                                npc.bannerItem = mod.ItemType("WyrmlingBanner");
                                bannerToItem[npc.banner] = npc.bannerItem;
                            }
                        }
                    }
                    else if (name.Contains("Wyrm"))
                    {
                        for (int m = 0; m < 5; m++)
                        {
                            ModNPC npc = mod.GetNPC(m == 0 ? "Wyrm" : (m == 1 ? "WyrmBody1" : (m == 2 ? "WyrmBody2" : (m == 3 ? "WyrmBody3" : "WyrmBody4"))));
                            if (npc != null)
                            {
                                npc.banner = mod.NPCType("Wyrm");
                                npc.bannerItem = mod.ItemType("WyrmBanner");
                                bannerToItem[npc.banner] = npc.bannerItem;
                            }
                        }
                    }
                    else if (name.Contains("Snake"))
                    {
                        for (int m = 0; m < 3; m++)
                        {
                            ModNPC npc = mod.GetNPC(m == 0 ? "SnakeHead" : (m == 1 ? "SnakeBody" : "SnakeTail"));
                            if (npc != null)
                            {
                                npc.banner = mod.NPCType("SnakeHead");
                                npc.bannerItem = mod.ItemType("SnakeBanner");
                                bannerToItem[npc.banner] = npc.bannerItem;
                            }
                        }
                    }
                    else
                    {
                        ModNPC npc = mod.GetNPC(name);
                        if (npc != null)
                        {
                            npc.banner = mod.NPCType(name);
                            npc.bannerItem = mod.ItemType(name + "Banner");
                            bannerToItem[npc.banner] = npc.bannerItem;
                        }
                    }
                    fx += 16;
                }
                BannerToItemDict = bannerToItem;
            }
            catch (Exception e)
            {
                ErrorLogger.Log(e.Message);
                ErrorLogger.Log(e.StackTrace);
            }
        }

        public override void PostSetupContent()
        {
            WeakReferences.PerformModSupport();

            Mod Thorium = ModLoader.GetMod("ThoriumMod");

            if (Thorium != null)
                thoriumLoaded = true;
        }

        public static void PremultiplyTexture(Texture2D texture)
        {
            Color[] buffer = new Color[texture.Width * texture.Height];
            texture.GetData(buffer);
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = Color.FromNonPremultiplied(
                        buffer[i].R, buffer[i].G, buffer[i].B, buffer[i].A);
            }
            texture.SetData(buffer);
        }

        public override void Load()
        {
            instance = this;
            GoblinSoul = CustomCurrencyManager.RegisterCurrency(new Items.Currency.GSouls(ItemType<Items.Currency.GoblinSoul>()));
            if (Main.rand == null)
                Main.rand = new UnifiedRandom();

            GameShaders.Armor.BindShader(ItemType("BlazingDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorLivingFlame")).UseColor(Color.SkyBlue.R / 255f, Color.SkyBlue.G / 255f, Color.SkyBlue.B / 255f).UseSecondaryColor(Color.DeepSkyBlue.R / 255f, Color.DeepSkyBlue.G / 255f, Color.DeepSkyBlue.B / 255f);
            GameShaders.Armor.BindShader(ItemType("AbyssalDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorLivingFlame").UseColor(146f / 255f, 30f / 255f, 68f / 255f).UseSecondaryColor(105f / 255f, 20f / 255f, 50f / 255f));
            GameShaders.Armor.BindShader(ItemType("DoomsdayDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorVortex")).UseImage("Images/Misc/noise").UseColor(0f, 0f, 0f).UseSecondaryColor(1f, 0f, 0f).UseSaturation(1f);
            GameShaders.Armor.BindShader(ItemType("DiscordianDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorLivingFlame").UseColor(0.66f, 0f, 1f).UseSecondaryColor(0.66f, 0f, 1f));
            GameShaders.Armor.BindShader(ItemType("DiscordianInfernoDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorHades")).UseColor(0.88f, 0f, 1f).UseSecondaryColor(0.66f, 0f, 1f);
            GameShaders.Armor.BindShader(ItemType("AbyssalWrathDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorHades").UseColor(146f / 255f, 30f / 255f, 68f / 255f).UseSecondaryColor(105f / 255f, 20f / 255f, 50f / 255f));
            GameShaders.Armor.BindShader(ItemType("BlazingFuryDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorHades")).UseColor(Color.SkyBlue.R / 255f, Color.SkyBlue.G / 255f, Color.SkyBlue.B / 255f).UseSecondaryColor(Color.DeepSkyBlue.R / 255f, Color.DeepSkyBlue.G / 255f, Color.DeepSkyBlue.B / 255f);

            InfinityHotKey = RegisterHotKey("Snap", "G");

            Rift = RegisterHotKey("Rift Home", "C");
            RiftReturn = RegisterHotKey("Rift Back", "X");

            AccessoryAbilityKey = RegisterHotKey("AA Accessory Ability", "U");
            ArmorAbilityKey = RegisterHotKey("AA Armor Ability", "Y");

            TimeStone = RegisterHotKey("Time Stone", "K");

            if (!Main.dedServ)
            {
                LoadClient();
            }
        }

        public void LoadClient()
        {
            PremultiplyTexture(GetTexture("Backgrounds/VoidBH"));
            PremultiplyTexture(GetTexture("Backgrounds/Moon"));
            PremultiplyTexture(GetTexture("Backgrounds/Sun"));
            PremultiplyTexture(GetTexture("Backgrounds/fog"));
            PremultiplyTexture(GetTexture("Backgrounds/AkumaSun"));
            PremultiplyTexture(GetTexture("Backgrounds/YamataMoon"));
            PremultiplyTexture(GetTexture("Backgrounds/YamataBeam"));
            PremultiplyTexture(GetTexture("Backgrounds/AkumaAMeteor"));
            PremultiplyTexture(GetTexture("Backgrounds/AkumaMeteor"));
            PremultiplyTexture(GetTexture("Backgrounds/ShenSun"));
            PremultiplyTexture(GetTexture("Backgrounds/ShenMoon"));
            PremultiplyTexture(GetTexture("Backgrounds/ShenEclipse"));
            PremultiplyTexture(GetTexture("Backgrounds/Star 0"));
            PremultiplyTexture(GetTexture("Backgrounds/Star 1"));
            PremultiplyTexture(GetTexture("NPCs/Bosses/Zero/ZeroShield"));
            PremultiplyTexture(GetTexture("NPCs/Bosses/AH/Ashe/AsheBarrier"));
            PremultiplyTexture(GetTexture("Projectiles/RadiumStar"));
            PremultiplyTexture(GetTexture("Projectiles/Stars"));

            if (GetSoundSlot(SoundType.Music, "Sounds/Music/Monarch") != 0) //ensure music was loaded!
            {
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Monarch"), ItemType("MonarchBox"), TileType("MonarchBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Fungus"), ItemType("FungusBox"), TileType("FungusBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/GripsTheme"), ItemType("GripsBox"), TileType("GripsBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/HydraTheme"), ItemType("HydraBox"), TileType("HydraBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/BroodTheme"), ItemType("BroodBox"), TileType("BroodBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Shroom"), ItemType("MushBox"), TileType("MushBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/InfernoSurface"), ItemType("InfernoBox"), TileType("InfernoBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MireSurface"), ItemType("MireBox"), TileType("MireBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/InfernoUnderground"), ItemType("InfernoUBox"), TileType("InfernoUBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MireUnderground"), ItemType("MireUBox"), TileType("MireUBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Djinn"), ItemType("DjinnBox"), TileType("DjinnBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/TODE"), ItemType("ToadBox"), TileType("ToadBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Boss6"), ItemType("SerpentBox"), TileType("SerpentBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Siege"), ItemType("SiegeBox"), TileType("SiegeBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/RajahTheme"), ItemType("RajahBox"), TileType("RajahBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Equinox"), ItemType("Equibox"), TileType("Equibox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Stars"), ItemType("StarBox"), TileType("StarBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Void"), ItemType("VoidBox"), TileType("VoidBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/VoidButNowItsSpooky"), ItemType("FateBox"), TileType("FateBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Shrines"), ItemType("LakeBox"), TileType("LakeBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/AkumaShrine"), ItemType("PagodaBox"), TileType("PagodaBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Zero"), ItemType("ZeroBox"), TileType("ZeroBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Zero2"), ItemType("Zero2Box"), TileType("Zero2Box"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Akuma"), ItemType("AkumaBox"), TileType("AkumaBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Akuma2"), ItemType("AkumaABox"), TileType("AkumaABox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Yamata"), ItemType("YamataBox"), TileType("YamataBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Yamata2"), ItemType("YamataABox"), TileType("YamataABox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Terrarium"), ItemType("TerrariumBox"), TileType("TerrariumBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/SleepingDragon"), ItemType("SDBox"), TileType("SDBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/SleepingGiant"), ItemType("SGBox"), TileType("SGBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Shen"), ItemType("ShenBox"), TileType("ShenBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/ShenA"), ItemType("ShenABox"), TileType("ShenABox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/LastStand"), ItemType("SABox"), TileType("SABox"));
            }

            Filters.Scene["AAMod:MireSky"] = new Filter(new MireSkyData("FilterMiniTower").UseColor(0f, 0.20f, 1f).UseOpacity(0.3f), EffectPriority.High);
            SkyManager.Instance["AAMod:MireSky"] = new MireSky();
            MireSky.PlanetTexture = GetTexture("Backgrounds/Moon");
            MireSky.SkyTexture = GetTexture("Backgrounds/MireSky");

            Filters.Scene["AAMod:VoidSky"] = new Filter(new VoidSkyData("FilterMiniTower").UseColor(0.15f, 0.1f, 0.1f).UseOpacity(0.3f), EffectPriority.High);
            SkyManager.Instance["AAMod:VoidSky"] = new VoidSky();
            VoidSky.PlanetTexture = GetTexture("Backgrounds/VoidBH");
            VoidSky.Asteroids1 = GetTexture("Backgrounds/Asteroids1");
            VoidSky.Asteroids2 = GetTexture("Backgrounds/Asteroids2");
            VoidSky.Asteroids3 = GetTexture("Backgrounds/Asteroids3");
            VoidSky.Echo = GetTexture("Backgrounds/Echo");
            VoidSky.LB = GetTexture("Backgrounds/LB");
            VoidSky.boltTexture = GetTexture("Backgrounds/VoidBolt");
            VoidSky.flashTexture = GetTexture("Backgrounds/VoidFlash");
            VoidSky.Stars = GetTexture("Backgrounds/Void_Starfield");
            VoidSky.SkyTexture = GetTexture("Backgrounds/SkyTex");

            Filters.Scene["AAMod:VoidSky2"] = new Filter(new VoidSkyData("FilterMiniTower").UseColor(0.15f, 0.1f, 0.1f).UseOpacity(0.3f), EffectPriority.High);
            SkyManager.Instance["AAMod:VoidSky2"] = new VoidSky2();
            VoidSky2.Asteroids1 = GetTexture("Backgrounds/Asteroids1");
            VoidSky2.Asteroids2 = GetTexture("Backgrounds/Asteroids2");
            VoidSky2.Asteroids3 = GetTexture("Backgrounds/Asteroids3");
            VoidSky2.Stars = GetTexture("Backgrounds/Void_Starfield");

            Filters.Scene["AAMod:InfernoSky"] = new Filter(new InfernoSkyData("FilterMiniTower").UseColor(1f, 0.20f, 0f).UseOpacity(0.3f), EffectPriority.High);
            SkyManager.Instance["AAMod:InfernoSky"] = new InfernoSky();
            InfernoSky.PlanetTexture = GetTexture("Backgrounds/Sun");
            InfernoSky.SkyTex = GetTexture("Backgrounds/SkyTex");
            InfernoSky.MeteorTexture = GetTexture("Backgrounds/AkumaMeteor");

            Filters.Scene["AAMod:AkumaSky"] = new Filter(new AkumaSkyData("FilterMiniTower").UseColor(0f, 0.3f, 0.4f).UseOpacity(0.5f), EffectPriority.VeryHigh);
            SkyManager.Instance["AAMod:AkumaSky"] = new AkumaSky();
            AkumaSky.PlanetTexture = GetTexture("Backgrounds/AkumaSun");
            AkumaSky.SkyTex = GetTexture("Backgrounds/SkyTex");
            AkumaSky.MeteorTexture = GetTexture("Backgrounds/AkumaAMeteor");

            Filters.Scene["AAMod:YamataSky"] = new Filter(new YamataSkyData("FilterMiniTower").UseColor(.7f, 0f, 0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
            SkyManager.Instance["AAMod:YamataSky"] = new YamataSky();
            YamataSky.PlanetTexture = GetTexture("Backgrounds/YamataMoon");
            YamataSky.SkyTex = GetTexture("Backgrounds/StarTex");
            YamataSky.BeamTexture = GetTexture("Backgrounds/YamataBeam");

            Filters.Scene["AAMod:ShenSky"] = new Filter(new ShenSkyData("FilterMiniTower").UseColor(.5f, 0f, .5f).UseOpacity(0.2f), EffectPriority.VeryHigh);
            SkyManager.Instance["AAMod:ShenSky"] = new ShenSky();
            ShenSky.Sun = GetTexture("Backgrounds/ShenSun");
            ShenSky.Moon = GetTexture("Backgrounds/ShenMoon");
            ShenSky.SkyTex = GetTexture("Backgrounds/SkyTex");

            Filters.Scene["AAMod:ShenASky"] = new Filter(new ShenASkyData("FilterMiniTower").UseColor(.7f, 0f, .7f).UseOpacity(0.2f), EffectPriority.VeryHigh);
            SkyManager.Instance["AAMod:ShenASky"] = new ShenASky();
            ShenASky.PlanetTexture = GetTexture("Backgrounds/ShenEclipse");
            ShenASky.SkyTex = GetTexture("Backgrounds/StarTex");

            SkyManager.Instance["AAMod:StarSky"] = new StarSky();
            StarSky.starTextures = new Texture2D[2];
            for (int i = 0; i < StarSky.starTextures.Length; i++)
            {
                StarSky.starTextures[i] = GetTexture("Backgrounds/Star " + i);
            }

            ReplaceItemTexture(3460, "Resprites/Luminite");
            ReplaceItemTexture(512, "Resprites/SoulOfNight");

            sunTextureBackup = Main.sunTexture;
            sun3TextureBackup = Main.sun3Texture;
        }

        //DO NOT MAKE THESE STATIC! DOING SO WILL PREVENT WHAT IT FIXES FROM HAPPENING.
        private Texture2D sunTextureBackup = null;
        private Texture2D sun3TextureBackup = null;
        public Dictionary<int, Texture2D> vanillaTextureBackups = new Dictionary<int, Texture2D>();
        public void ReplaceItemTexture(int id, string texturePath)
        {
            vanillaTextureBackups.Add(id, Main.itemTexture[id]);
            Main.itemTexture[id] = GetTexture(texturePath);
        }
        public void ResetItemTexture(int id)
        {
            if (vanillaTextureBackups.ContainsKey(id))
            {
                Main.itemTexture[id] = vanillaTextureBackups[id];
            }
        }

        public override void Unload()
        {
            CleanupStaticArrays();
            instance = null;
            InfinityHotKey = null;
            Rift = null;
            RiftReturn = null;
            AccessoryAbilityKey = null;
            ArmorAbilityKey = null;
            TimeStone = null;

            if (!Main.dedServ)
            {
                UnloadClient();
            }
        }

        public void UnloadClient()
        {
            ResetItemTexture(3460);
            ResetItemTexture(512);

            if (sunTextureBackup != null)
                Main.sunTexture = sunTextureBackup;
            if (sun3TextureBackup != null)
                Main.sun3Texture = sun3TextureBackup;
        }

        public void CleanupStaticArrays()
        {
            if (Main.netMode != 2) //handle clearing all static texture arrays
            {
                precachedTextures.Clear();

                AkumaSky.PlanetTexture = null;
                AkumaSky.BGTexture = null;
                AkumaSky.SkyTex = null;

                InfernoSky.PlanetTexture = null;
                InfernoSky.BGTexture = null;
                InfernoSky.SkyTex = null;

                MireSky.PlanetTexture = null;
                MireSky.SkyTexture = null;
                MireSky.BGTexture = null;

                VoidSky.PlanetTexture = null;
                VoidSky.BGTexture = null;
                VoidSky.Echo = null;
                VoidSky.Asteroids1 = null;
                VoidSky.Asteroids2 = null;
                VoidSky.Asteroids3 = null;
                VoidSky.LB = null;
                VoidSky.boltTexture = null;
                VoidSky.flashTexture = null;

                YamataSky.PlanetTexture = null;
                YamataSky.BGTexture = null;
                YamataSky.SkyTex = null;

                ShenSky.MeteorTexture = null;
                ShenSky.SkyTex = null;
                ShenSky.Sun = null;
                ShenSky.Moon = null;
                ShenSky.BGTexture = null;

                ShenASky.MeteorTexture = null;
                ShenASky.PlanetTexture = null;
                ShenASky.SkyTex = null;            
            }
        }

        public override void AddRecipeGroups()
        {
            AARecipes.AddRecipeGroups();
        }

        public override void AddRecipes()
        {
            AARecipes.AddRecipes();
        }

        public static Texture2D GetGlowmask(string Name)
        {
            return instance.GetTexture("Glowmasks/" + Name + "_Glow");
        }

        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (Main.gameMenu)
                return;

            if (priority > MusicPriority.Environment)
                return;

            Player player = Main.LocalPlayer;

            if (!player.active)
                return;

            AAPlayer Ancients = player.GetModPlayer<AAPlayer>();

            bool zoneIZ = Ancients.ZoneVoid && !AAWorld.downedIZ;
            bool zoneShen = (Ancients.ZoneRisingSunPagoda || Ancients.ZoneRisingMoonLake) && !AAWorld.downedShen;

            if (zoneShen && AAWorld.downedAllAncients)
            {
                priority = MusicPriority.Event;
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/SleepingDragon");

                return;
            }

            if (zoneIZ && AAWorld.downedZero && !player.ZoneRockLayerHeight)
            {
                priority = MusicPriority.Event;
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/SleepingGiant");

                return;
            }

            if (Ancients.ZoneVoid)
            {
                priority = MusicPriority.Event;
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Void");

                if (player.ZoneRockLayerHeight)
                {
                    priority = MusicPriority.BiomeMedium;
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/UGVoid");

                    return;
                }

                if (NPC.downedMoonlord && !AAWorld.downedZero)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/VoidButNowItsSpooky");

                    return;
                }

                return;
            }

            if (Ancients.ZoneShip)
            {
                priority = MusicPriority.Event;
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Ship");

                return;
            }

            if (Ancients.ZoneInferno)
            {
                if (Ancients.ZoneRisingSunPagoda && NPC.downedMoonlord && !AAWorld.downedAkuma)
                {
                    priority = MusicPriority.BiomeHigh;
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/AkumaShrine");

                    return;
                }

                if (player.ZoneRockLayerHeight)
                {
                    priority = MusicPriority.BiomeMedium;
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/InfernoUnderground");

                    return;
                }
                else
                {
                    priority = MusicPriority.BiomeHigh;
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/InfernoSurface");

                    return;
                }
            }

            if (Ancients.ZoneMire)
            {
                if (Ancients.ZoneRisingMoonLake && NPC.downedMoonlord && !AAWorld.downedYamata)
                {
                    priority = MusicPriority.BiomeHigh;
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/Shrines");
                    return;
                }

                if (player.ZoneRockLayerHeight)
                {
                    priority = MusicPriority.BiomeMedium;
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/MireUnderground");

                    return;
                }
                else
                {
                    priority = MusicPriority.BiomeHigh;
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/MireSurface");

                    return;
                }
            }

            if (Ancients.ZoneStars)
            {
                priority = MusicPriority.Event;
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Stars");

                return;
            }

            if (Ancients.Terrarium)
            {

                priority = MusicPriority.BiomeHigh;
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Terrarium");

                return;
            }

            if (Ancients.ZoneMush)
            {

                priority = MusicPriority.BiomeMedium;
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Shroom");
                
                return;
            }      
        }

        public override object Call(params object[] args)
        {
            if (args.Length <= 0 || !(args[0] is string))
                return new Exception("ANCIENTS AWAKENED CALL ERROR: NO METHOD NAME! First param MUST be a method name!");

            string methodName = (string)args[0];

            if (methodName.Equals("Downed")) //returns a Func which will return a downed value based on player and name.
            {
                Func<string, bool> downed = (name) =>
                {
                    name = name.ToLower();
                    switch (name)
                    {
                        default: return false;
                        case "mushroommonarch": return AAWorld.downedMonarch;
                        case "broodmother": return AAWorld.downedBrood;
                        case "hydra": return AAWorld.downedHydra;
                        case "grips":
                        case "gripsofchaos": return AAWorld.downedGrips;
                        case "tode": return AAWorld.downedToad;
                        case "retriever": return AAWorld.downedRetriever;
                        case "orthrus": return AAWorld.downedOrthrus;
                        case "raider": return AAWorld.downedRaider;
                        case "stormany": return AAWorld.downedStormAny;
                        case "stormall": return AAWorld.downedStormAll;
                        case "daybringer": return AAWorld.downedDB;
                        case "nightcrawler": return AAWorld.downedNC;
                        case "equinox": return AAWorld.downedEquinox;
                        case "ancient":
                        case "ancientany": return AAWorld.downedAncient;
                        case "sancient":
                        case "sancientany": return AAWorld.downedSAncient;
                        case "gripsS":
                        case "discordgrips": return AAWorld.downedGripsS;
                        case "kraken": return AAWorld.downedKraken;
                        case "akuma": return AAWorld.downedAkuma;
                        case "yamata": return AAWorld.downedYamata;
                        case "zero": return AAWorld.downedZero;
                        case "shen":
                        case "shendoragon": return AAWorld.downedShen;
                        case "iz":
                        case "infinityzero": return AAWorld.downedIZ;
                        case "soc":
                        case "soulofcthulhu": return AAWorld.downedSoC;
                    }
                };
                return downed;
            }
            else if (methodName.Equals("InZone")) //returns a Func which will return a zone value based on player and name.
            {
                Func<Player, string, bool> inZone = (p, name) =>
                {
                    name = name.ToLower();
                    AAPlayer aap = p.GetModPlayer<AAPlayer>();
                    switch (name)
                    {
                        default: return false;
                        case "mire": return aap.ZoneMire;
                        case "lake": return aap.ZoneRisingMoonLake;
                        case "inferno": return aap.ZoneInferno;
                        case "pagoda": return aap.ZoneRisingSunPagoda;
                        case "ship": return aap.ZoneShip;
                        case "storm": return aap.ZoneStorm;
                        case "void": return aap.ZoneVoid;
                        case "mush": return aap.ZoneMush;
                        case "terrarium": return aap.Terrarium;
                    }
                };
                return inZone;
            }
            return new Exception("ANCIENTS AWAKENED CALL ERROR: NO METHOD FOUND: " + methodName);
        }
		
        public override void HandlePacket(BinaryReader bb, int whoAmI)
        {
            AANet.HandlePacket(bb, whoAmI);
        }

        public static void DrawStars()
        {
            Mod mod = instance;
            UIDisplay_ManaPerStar = 20;
            Texture2D Stars = mod.GetTexture("UI/ManaGreen");
            if (Main.player[Main.myPlayer].statManaMax > 200)
            {
                int arg_30_0 = Main.player[Main.myPlayer].statManaMax2 / 20;
                Main.spriteBatch.DrawString(fontMouseText, "Mana", new Vector2(750 + UI_ScreenAnchorX, 6f), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                for (int i = 1; i < Main.player[Main.myPlayer].statManaMax2 / UIDisplay_ManaPerStar + 1; i++)
                {
                    bool flag = false;
                    float num = 1f;
                    int num2;
                    if (Main.player[Main.myPlayer].statMana >= i * UIDisplay_ManaPerStar)
                    {
                        num2 = 255;
                        if (Main.player[Main.myPlayer].statMana == i * UIDisplay_ManaPerStar)
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        float num3 = (float)(Main.player[Main.myPlayer].statMana - (i - 1) * UIDisplay_ManaPerStar) / UIDisplay_ManaPerStar;
                        num2 = (int)(30f + 225f * num3);
                        if (num2 < 30)
                        {
                            num2 = 30;
                        }
                        num = num3 / 4f + 0.75f;
                        if (num < 0.75)
                        {
                            num = 0.75f;
                        }
                        if (num3 > 0f)
                        {
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        num += Main.cursorScale - 1f;
                    }
                    int a = (int)((float)num2 * 0.9);
                    Main.spriteBatch.Draw(Stars, new Vector2((775 + UI_ScreenAnchorX), (30 + Stars.Height / 2) + (Stars.Height - Stars.Height * num) / 2f + (28 * (i - 1))), new Rectangle?(new Rectangle(0, 0, Stars.Width, Stars.Height)), new Color(num2, num2, num2, a), 0f, new Vector2((Stars.Width / 2), (Stars.Height / 2)), num, SpriteEffects.None, 0f);
                }
            }
        }
    }

    public enum MPMessageType : byte
    {
        RequestUpdateSquidLady
    }
}
