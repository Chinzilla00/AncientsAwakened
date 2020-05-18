using AAMod.Backgrounds;
using AAMod.Globals;
using AAMod.UI;
using AAMod.UI.Core;
using AAMod.Items.Dev.Invoker;
using log4net;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.Utilities;
using ReLogic.Graphics;

namespace AAMod
{
    public class AAMod : Mod
    {
        public static Texture2D blankTexture;

        // Miscellaneous
        public static int Coin = -1;
        public static int GoblinSoul = -1;
        public static int BloodRune = -1;
        public static int PirateBooty = -1;
        public static int MonsterSoul = -1;
        public static int HalloweenTreat = -1;
        public static int ChristmasCheer = -1;
        public static int MartianCredit = -1;
        public static int DustIDSlashFX;

        public static int BoneAmmo = 10000;

        // Hotkeys
        public static ModHotKey AccessoryAbilityKey;
        public static ModHotKey ArmorAbilityKey;
        public static ModHotKey Rift;
        public static ModHotKey RiftReturn;

        // Textures
        public static IDictionary<string, Texture2D> Textures = null;
        public static Dictionary<string, Texture2D> precachedTextures = new Dictionary<string, Texture2D>();
        public static string BLANK_TEX = "AAMod/BlankTex";

        // UI
        internal UserInterface TerratoolInterface;
        internal TerratoolTUI TerratoolTState;
        internal TerratoolCUI TerratoolCState;
        internal TerratoolAUI TerratoolAState;
        internal TerratoolYUI TerratoolYState;
        internal TerratoolZUI TerratoolZState;
        internal TerratoolSUI TerratoolSState;
        internal TerratoolKipUI TerratoolKipState;
        internal TerratoolLizUI TerratoolLizState;
        internal TerratoolGroxUI TerratoolGroxState;
        internal TerratoolEXUI TerratoolEXState;

        //Fonts

        public static SpriteFont fontMouseText;

        internal static AAMod instance;
        public static AAMod self = null;
        internal ILog Logging = LogManager.GetLogger("AAMod");

        public static bool isFullyReady;

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
            if (Main.netMode == NetmodeID.Server || Main.dedServ) return; //don't do any texture stuff on a server lol
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
                instance.Logger.InfoFormat(e.Message);
                instance.Logger.InfoFormat(e.StackTrace);
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
                instance.Logger.InfoFormat(e.Message);
                instance.Logger.InfoFormat(e.StackTrace);
            }
        }

        public override void PostSetupContent()
        {
            WeakReferences.PerformModSupport();

            Array.Resize(ref AASets.Goblins, NPCLoader.NPCCount);

            isFullyReady = true;
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
            blankTexture = GetTexture("BlankTex");

            Logger.InfoFormat("{0} AA log", Name);

            instance = this;
            Coin = CustomCurrencyManager.RegisterCurrency(new Items.Currency.ACoin(ModContent.ItemType<Items.Currency.AncientCoin>()));
            GoblinSoul = CustomCurrencyManager.RegisterCurrency(new Items.Currency.GSouls(ModContent.ItemType<Items.Currency.GoblinSoul>()));
            BloodRune = CustomCurrencyManager.RegisterCurrency(new Items.Currency.BRune(ModContent.ItemType<Items.Currency.BloodRune>()));
            PirateBooty = CustomCurrencyManager.RegisterCurrency(new Items.Currency.PBooty(ModContent.ItemType<Items.Currency.PirateBooty>()));
            MonsterSoul = CustomCurrencyManager.RegisterCurrency(new Items.Currency.MSouls(ModContent.ItemType<Items.Currency.MonsterSoul>()));
            HalloweenTreat = CustomCurrencyManager.RegisterCurrency(new Items.Currency.HTreat(ModContent.ItemType<Items.Currency.HalloweenTreat>()));
            ChristmasCheer = CustomCurrencyManager.RegisterCurrency(new Items.Currency.CCheer(ModContent.ItemType<Items.Currency.ChristmasCheer>()));
            MartianCredit = CustomCurrencyManager.RegisterCurrency(new Items.Currency.MCredit(ModContent.ItemType<Items.Currency.MartianCredit>()));

            BoneAmmo = ItemID.Bone;
            if (Main.rand == null)
                Main.rand = new UnifiedRandom();

            GameShaders.Armor.BindShader(ItemType("BlazingDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorLivingFlame")).UseColor(Color.SkyBlue.R / 255f, Color.SkyBlue.G / 255f, Color.SkyBlue.B / 255f).UseSecondaryColor(Color.DeepSkyBlue.R / 255f, Color.DeepSkyBlue.G / 255f, Color.DeepSkyBlue.B / 255f);
            GameShaders.Armor.BindShader(ItemType("AbyssalDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorLivingFlame").UseColor(146f / 255f, 30f / 255f, 68f / 255f).UseSecondaryColor(105f / 255f, 20f / 255f, 50f / 255f));
            GameShaders.Armor.BindShader(ItemType("DoomsdayDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorVortex")).UseImage("Images/Misc/noise").UseColor(0f, 0f, 0f).UseSecondaryColor(1f, 0f, 0f).UseSaturation(1f);
            GameShaders.Armor.BindShader(ItemType("DiscordianDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorLivingFlame").UseColor(0.66f, 0f, 1f).UseSecondaryColor(0.66f, 0f, 1f));
            GameShaders.Armor.BindShader(ItemType("DiscordianInfernoDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorHades")).UseColor(0.88f, 0f, 1f).UseSecondaryColor(0.66f, 0f, 1f);
            GameShaders.Armor.BindShader(ItemType("AbyssalWrathDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorHades").UseColor(146f / 255f, 30f / 255f, 68f / 255f).UseSecondaryColor(105f / 255f, 20f / 255f, 50f / 255f));
            GameShaders.Armor.BindShader(ItemType("BlazingFuryDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorHades")).UseColor(Color.SkyBlue.R / 255f, Color.SkyBlue.G / 255f, Color.SkyBlue.B / 255f).UseSecondaryColor(Color.DeepSkyBlue.R / 255f, Color.DeepSkyBlue.G / 255f, Color.DeepSkyBlue.B / 255f);

            Rift = RegisterHotKey(Lang.Hotkey("Rifthotkey"), "C");
            RiftReturn = RegisterHotKey(Lang.Hotkey("RiftReturnhotkey"), "X");

            AccessoryAbilityKey = RegisterHotKey(Lang.Hotkey("AccessoryAbilityKey"), "U");
            ArmorAbilityKey = RegisterHotKey(Lang.Hotkey("ArmorAbilityKey"), "Y"); 
            
            On.Terraria.Wiring.ActuateForced += Wiring_ActuateForced;
            On.Terraria.Wiring.Actuate += Actuate;

            if (!Main.dedServ)
            {
                Config.Load();
                LoadClient();
            }
        }

        public void LoadClient()
        {
            TerratoolInterface = new UserInterface();
            TerratoolTState = new TerratoolTUI();
            TerratoolTState.Activate();
            TerratoolCState = new TerratoolCUI();
            TerratoolCState.Activate();
            TerratoolAState = new TerratoolAUI();
            TerratoolAState.Activate();
            TerratoolYState = new TerratoolYUI();
            TerratoolYState.Activate();
            TerratoolZState = new TerratoolZUI();
            TerratoolZState.Activate();
            TerratoolSState = new TerratoolSUI();
            TerratoolSState.Activate();
            TerratoolKipState = new TerratoolKipUI();
            TerratoolKipState.Activate();
            TerratoolLizState = new TerratoolLizUI();
            TerratoolLizState.Activate();
            TerratoolGroxState = new TerratoolGroxUI();
            TerratoolGroxState.Activate();
            TerratoolEXState = new TerratoolEXUI();
            TerratoolEXState.Activate();

            Ref<Effect> screenRef = new Ref<Effect>(GetEffect("Effects/Shockwave"));
            Filters.Scene["Shockwave"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.VeryHigh);
            Filters.Scene["Shockwave"].Load();

            BackupVanillaBG(-1);
            BackupVanillaBG(-2);
            BackupVanillaBG(-3);

            BackupVanillaBG(0);
            BackupVanillaBG(171);
            BackupVanillaBG(172);
            BackupVanillaBG(173);
            BackupVanillaBG(24);
            BackupVanillaBG(25);
            BackupVanillaBG(56);
            BackupVanillaBG(57);
            BackupVanillaBG(58);

            PremultiplyTexture(GetTexture("Backgrounds/VoidBH"));
            PremultiplyTexture(GetTexture("Backgrounds/Moon"));
            PremultiplyTexture(GetTexture("Backgrounds/Sun"));
            PremultiplyTexture(GetTexture("Backgrounds/FogTex"));
            PremultiplyTexture(GetTexture("Backgrounds/AkumaSun"));
            PremultiplyTexture(GetTexture("Backgrounds/YamataMoon"));
            PremultiplyTexture(GetTexture("Backgrounds/YamataBeam"));
            PremultiplyTexture(GetTexture("Backgrounds/AkumaAMeteor"));
            PremultiplyTexture(GetTexture("Backgrounds/AkumaMeteor"));
            PremultiplyTexture(GetTexture("Backgrounds/SkyTex"));
            PremultiplyTexture(GetTexture("Backgrounds/ShenMeteor"));
            PremultiplyTexture(GetTexture("Backgrounds/AthenaBolt"));
            PremultiplyTexture(GetTexture("Backgrounds/AthenaFlash"));
            PremultiplyTexture(GetTexture("NPCs/Bosses/Zero/ZeroShield"));
            PremultiplyTexture(GetTexture("Projectiles/RadiumStar"));
            PremultiplyTexture(GetTexture("Projectiles/Stars"));
            PremultiplyTexture(GetTexture("NPCs/Bosses/Toad/ToadBubble"));
            PremultiplyTexture(GetTexture("NPCs/Bosses/Zero/Protocol/ProtoStar"));
            PremultiplyTexture(GetTexture("Textures/SagittariusShield"));
            PremultiplyTexture(GetTexture("Projectiles/ArchwitchStar"));

            if (GetSoundSlot(SoundType.Music, "Sounds/Music/Monarch") != 0) //ensure music was loaded!
            {
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Monarch"), ItemType("MonarchBox"), TileType("MonarchBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Fungus"), ItemType("FungusBox"), TileType("FungusBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/GripsTheme"), ItemType("GripsBox"), TileType("GripsBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/HydraTheme"), ItemType("HydraBox"), TileType("HydraBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/BroodTheme"), ItemType("BroodBox"), TileType("BroodBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Shroom"), ItemType("MushBox"), TileType("MushBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/InfernoSurface"), ItemType("InfernoBox"), TileType("InfernoBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/IN"), ItemType("InfernoNightBox"), TileType("InfernoNightBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MireSurface"), ItemType("MireBox"), TileType("MireBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/DM"), ItemType("MireDayBox"), TileType("MireDayBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/InfernoUnderground"), ItemType("InfernoUBox"), TileType("InfernoUBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MireUnderground"), ItemType("MireUBox"), TileType("MireUBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Void"), ItemType("VoidBox"), TileType("VoidBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Djinn"), ItemType("DjinnBox"), TileType("DjinnBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/TODE"), ItemType("ToadBox"), TileType("ToadBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Boss6"), ItemType("SerpentBox"), TileType("SerpentBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Sag"), ItemType("SagBox"), TileType("SagBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Anubis"), ItemType("AnubisBox"), TileType("AnubisBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Acropolis"), ItemType("AcropolisBox"), TileType("AcropolisBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Hoard"), ItemType("HoardBox"), TileType("HoardBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Greed"), ItemType("GreedBox"), TileType("GreedBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Athena"), ItemType("AthenaBox"), TileType("AthenaBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/RajahTheme"), ItemType("RajahBox"), TileType("RajahBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/GreedA"), ItemType("GreedABox"), TileType("GreedABox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/AthenaA"), ItemType("AthenaABox"), TileType("AthenaABox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/AnubisA"), ItemType("AnubisFBox"), TileType("AnubisFBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Equinox"), ItemType("Equibox"), TileType("Equibox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Stars"), ItemType("StarBox"), TileType("StarBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/AH"), ItemType("SistersBox"), TileType("SistersBox"));
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
                //(GetSoundSlot(SoundType.Music, "Sounds/Music/SupremeRajah"), ItemType("SRajahBox"), TileType("SRajahBox"));
            }

            Filters.Scene["AAMod:ShenSky"] = new Filter(new ShenSkyData("FilterMiniTower").UseColor(.5f, 0f, .5f).UseOpacity(0.2f), EffectPriority.VeryHigh);
            SkyManager.Instance["AAMod:ShenSky"] = new ShenSky();

            Filters.Scene["AAMod:ShenASky"] = new Filter(new ShenASkyData("FilterMiniTower").UseColor(.7f, 0f, .7f).UseOpacity(0.2f), EffectPriority.VeryHigh);
            SkyManager.Instance["AAMod:ShenASky"] = new ShenASky();

            Filters.Scene["AAMod:MireSky"] = new Filter(new MireSkyData("FilterMiniTower").UseColor(0f, 0.20f, 1f).UseOpacity(0.3f), EffectPriority.High);
            SkyManager.Instance["AAMod:MireSky"] = new MireSky();

            VoidSky vSky = new VoidSky();
            Filters.Scene["AAMod:VoidSky"] = new Filter(new VoidSkyData("FilterMiniTower").UseColor(0.15f, 0.1f, 0.1f).UseOpacity(0.3f), EffectPriority.High);
            SkyManager.Instance["AAMod:VoidSky"] = vSky;

            AthenaSky aSky = new AthenaSky();
            Filters.Scene["AAMod:AthenaSky"] = new Filter(new VoidSkyData("FilterMiniTower").UseColor(0f, 0.1f, 0.1f).UseOpacity(0.3f), EffectPriority.High);
            SkyManager.Instance["AAMod:AthenaSky"] = aSky;

            InfernoSky iSky = new InfernoSky();
            Filters.Scene["AAMod:InfernoSky"] = new Filter(new InfernoSkyData("FilterMiniTower").UseColor(1f, 0.20f, 0f).UseOpacity(0.3f), EffectPriority.High);
            SkyManager.Instance["AAMod:InfernoSky"] = iSky;

            AkumaSky akSky = new AkumaSky();
            Filters.Scene["AAMod:AkumaSky"] = new Filter(new AkumaSkyData("FilterMiniTower").UseColor(0f, 0.3f, 0.4f).UseOpacity(0.5f), EffectPriority.VeryHigh);
            SkyManager.Instance["AAMod:AkumaSky"] = akSky;

            Filters.Scene["AAMod:YamataSky"] = new Filter(new YamataSkyData("FilterMiniTower").UseColor(.7f, 0f, 0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
            SkyManager.Instance["AAMod:YamataSky"] = new YamataSky();

            AnubisSky anSky = new AnubisSky();
            Filters.Scene["AAMod:AnubisSky"] = new Filter(new AnubisSkyData("FilterMiniTower").UseColor(.2f, .5f, .2f).UseOpacity(0.5f), EffectPriority.VeryHigh);
            SkyManager.Instance["AAMod:AnubisSky"] = anSky;

            ReplaceItemTexture(3460, "Resprites/Luminite");
            ReplaceItemTexture(512, "Resprites/SoulOfNight");

            sunTextureBackup = Main.sunTexture;
            sun3TextureBackup = Main.sun3Texture;

            AddEquipTexture(new InvokedCaligulaHead(), null, EquipType.Head, "InvokedCaligulaHead", "AAMod/Items/Dev/Invoker/InvokedCaligula_Head", "", "");
            AddEquipTexture(new InvokedCaligulaBody(), null, EquipType.Body, "InvokedCaligulaBody", "AAMod/Items/Dev/Invoker/InvokedCaligula_Body", "AAMod/Items/Dev/Invoker/InvokedCaligula_Arms", "");
            AddEquipTexture(new InvokedCaligulaLegs(), null, EquipType.Legs, "InvokedCaligulaLegs", "AAMod/Items/Dev/Invoker/InvokedCaligula_Legs", "", "");

            AddEquipTexture(new Items.Vanity.Ohno.onoHead(), null, EquipType.Head, "onoHead", "AAMod/Items/Vanity/Ohno/ono_Head");
            AddEquipTexture(new Items.Vanity.Ohno.onoBody(), null, EquipType.Body, "onoBody", "AAMod/Items/Vanity/Ohno/ono_Body", "AAMod/Items/Vanity/Ohno/ono_Arms");
            AddEquipTexture(new Items.Vanity.Ohno.onoLegs(), null, EquipType.Legs, "onoLegs", "AAMod/Items/Vanity/Ohno/ono_Legs");

            AddEquipTexture(new InvokerHead(), null, EquipType.Head, "InvokerHead", "AAMod/Items/Vanity/Cerberus/InvokerHood_Head", "", "");
            AddEquipTexture(new InvokerBody(), null, EquipType.Body, "InvokerBody", "AAMod/Items/Vanity/Cerberus/InvokerRobe_Body", "AAMod/Items/Vanity/Cerberus/InvokerRobe_Arms", "");
            AddEquipTexture(new InvokerLegs(), null, EquipType.Legs, "InvokerLegs", "AAMod/Items/Vanity/Cerberus/InvokerPants_Legs", "", "");

            AddEquipTexture(null, EquipType.Legs, "CCRobe_Legs", "AAMod/Items/Vanity/CC/CCRobe_Legs");
            AddEquipTexture(null, EquipType.Legs, "ShinyCCRobe_Legs", "AAMod/Items/Vanity/CC/Shiny/ShinyCCRobe_Legs");
        }

        //DO NOT MAKE THESE STATIC! DOING SO WILL PREVENT WHAT IT FIXES FROM HAPPENING.
        private Texture2D sunTextureBackup = null;
        private Texture2D sun3TextureBackup = null;


        public Dictionary<int, Texture2D> vanillaTextureBackups = new Dictionary<int, Texture2D>();
        public Dictionary<int, Texture2D> vanillaBGBackups = new Dictionary<int, Texture2D>();

        public static bool AAloadedOnly = true;

        public override void PostAddRecipes()
		{
            LuckyCheckProgress();
            foreach(Mod mo in ModLoader.Mods)
            {
                if(mo.Name != "ModLoader" && mo.Name != "AAMod" && mo.Name != "AAMod")
                {
                    AAloadedOnly = false;
                }
            }
			Config.SaveConfig();
		}

        private void LuckyCheckProgress()
        {
            Config.LuckyOre.Clear();
            Config.LuckyPotion.Clear();
            Config.ListRareNpc.Clear();
            Item item = new Item();
            for (int i = -48; i < ItemLoader.ItemCount; i++)
			{
				item.netDefaults(i);
                if(item.createTile > TileID.Dirt && Main.tileValue[item.createTile] > 0 && !Main.tileContainer[item.createTile] && item.createTile != TileID.FakeContainers && item.createTile != TileID.FakeContainers2)
                {
                    Config.LuckyOre.Add(item.type, Main.tileValue[item.createTile]);
                }
                if(item.buffType > 0 && item.buffType != 26 && item.buffTime > 0 && item.type > 3930)
                {
                    Config.LuckyPotion.Add(item.type, item.value);
                }
            }
            NPC npc = new NPC();
            for (int i = -65; i < NPCLoader.NPCCount; i++)
			{
				if (i != 0)
				{
					npc.SetDefaults(i, -1);
                }
                if (npc.rarity >= 1)
                {
                    Config.ListRareNpc.Add(i);
                }
            }
        }

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

        public void BackupVanillaBG(int id)
        {
            if(id > 0)
            {
                vanillaBGBackups.Add(id, Main.backgroundTexture[id]);
            }
            else if(id == -1)
            {
                vanillaBGBackups.Add(-1, Main.logoTexture);
            }
            else if(id == -2)
            {
                vanillaBGBackups.Add(-2, Main.logo2Texture);
            }
            else if(id == -3)
            {
                vanillaBGBackups.Add(-3, Main.sunTexture);
            }
           
        }

        public void ResetBGTexture(int id)
        {
            if (vanillaBGBackups.ContainsKey(id))
            {
                if(id > 0)
                {
                    Main.backgroundTexture[id] = vanillaBGBackups[id];
                }
                else if(id == -1)
                {
                    Main.logoTexture = vanillaBGBackups[-1];
                }
                else if(id == -2)
                {
                    Main.logo2Texture = vanillaBGBackups[-2];
                }
                else if(id == -3)
                {
                    Main.sunTexture = vanillaBGBackups[-3];
                }
            }
        }

        public override void Unload()
        {
            blankTexture = null;

            AAMenuset = false;

            if (!Main.dedServ)
            {
                CleanAAmenu();
                UnloadClient();
            }
            
            CleanupStaticArrays();

            instance = null;
            Rift = null;
            RiftReturn = null;
            AccessoryAbilityKey = null;
            ArmorAbilityKey = null;

            isFullyReady = false;
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

        public void CleanAAmenu()
        {
            ResetBGTexture(-1);
            ResetBGTexture(-2);
            ResetBGTexture(-3);
            //Main.logoTexture = ModContent.GetTexture("Logo");
            //Main.logo2Texture = ModContent.GetTexture("Logo2");
            ResetBGTexture(0);
            ResetBGTexture(171);
            ResetBGTexture(172);
            ResetBGTexture(173);
            ResetBGTexture(24);
            ResetBGTexture(25);
            ResetBGTexture(56);
            ResetBGTexture(57);
            ResetBGTexture(58);
        }

        public void CleanupStaticArrays()
        {
            if (Main.netMode != NetmodeID.Server) //handle clearing all static texture arrays
            {
                precachedTextures.Clear();
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

        private static GameTime lastUpdateUIGameTime;

        public override void UpdateUI(GameTime gameTime)
        {
            lastUpdateUIGameTime = gameTime;

            if (TerratoolInterface?.CurrentState != null)
            {
                TerratoolInterface.Update(gameTime);
            }
        }

        public override void PostUpdateInput()
		{
            if (!isFullyReady)
            {
                return;
            }
            if (Main.gameMenu && Main.menuMode >= 0)
            {
                if(Main.menuMode == 0)
                {
                    AAMenuset = true;
                }
                if(Main.menuMode >= 10002 && Main.menuMode <= 10006)
                {
                    AAMenuset = false;
                }
                if(AAConfigClient.Instance != null && AAConfigClient.Instance.AAStyleMainPage && (Main.bgStyle == 1 || Main.bgStyle == 0) && AAMenuset)
                {
                    AAMenuReset = true;
                    WorldGen.setBG(0, 6);
                    switch(Main.moonPhase % 3)
                    {
                        case 0:
                            Main.numClouds = 10;
                            if(Main.LogoB <= 255)
                            {
                                Main.logoTexture = ModContent.GetTexture("Terraria/Logo");
                            }
                            if(Main.LogoB < 10 || (!Main.dayTime && Main.LogoA <= 255))
                            {
                                Main.logo2Texture = ModContent.GetTexture("Terraria/Logo2");
                            }
                            Main.sunTexture = ModContent.GetTexture("Terraria/Sun");
                            if(SkyManager.Instance["AAMod:MireSky"].IsActive()) SkyManager.Instance.Deactivate("AAMod:MireSky", new object[0]);
                            if(SkyManager.Instance["AAMod:VoidSky"].IsActive()) SkyManager.Instance.Deactivate("AAMod:VoidSky", new object[0]);

                            if(Main.dayTime && (Main.bgAlpha2[0] < 0.10f || Main.bgAlpha2[0] == 1f))
                            {
                                Main.treeBG[0] = 173;
                                Main.backgroundTexture[0] = ModContent.GetTexture("Terraria/Background_" + 0);
                                Main.backgroundTexture[171] = ModContent.GetTexture("Terraria/Background_" + 171);
                                Main.backgroundTexture[172] = ModContent.GetTexture("Terraria/Background_" + 172);
                                Main.backgroundTexture[173] = ModContent.GetTexture("Terraria/Background_" + 173);
                                Main.backgroundTexture[24] = ModContent.GetTexture("Terraria/Background_" + 24);
                                Main.backgroundTexture[25] = ModContent.GetTexture("Terraria/Background_" + 25);
                                Main.backgroundTexture[56] = ModContent.GetTexture("Terraria/Background_" + 56);
                                Main.backgroundTexture[57] = ModContent.GetTexture("Terraria/Background_" + 57);
                                Main.backgroundTexture[58] = ModContent.GetTexture("Terraria/Background_" + 58);
                            }
                            break;
                        case 1:
                            Main.numClouds = 0;
                            if(Main.dayTime)
                            {
                                Main.sunTexture = instance.GetTexture("Backgrounds/Sun");
                            }
                            else
                            {
                                if(SkyManager.Instance["AAMod:MireSky"] != null) SkyManager.Instance.Activate("AAMod:MireSky",default, new object[0]);
                            }
                            if(Main.LogoB <= 255)
                            {
                                Main.logoTexture = instance.GetTexture("UI/LogoInferno");
                            }
                            if(Main.LogoB < 10 || (!Main.dayTime && Main.LogoA <= 255))
                            {
                                Main.logo2Texture = instance.GetTexture("UI/LogoMire");
                            }
                            if(Main.dayTime && (Main.bgAlpha2[0] < 0.10f || Main.bgAlpha2[0] == 1f))
                            {
                                Main.backgroundTexture[0] = instance.GetTexture("Backgrounds/InfernoSky");
                                Main.backgroundTexture[171] = instance.GetTexture("Backgrounds/InfernoBG");
                                Main.backgroundTexture[172] = instance.GetTexture("Backgrounds/InfernoBG");
                                Main.backgroundTexture[173] = instance.GetTexture("Backgrounds/InfernoBG");
                                Main.backgroundTexture[24] = instance.GetTexture("Backgrounds/MireBG");
                                Main.backgroundTexture[25] = instance.GetTexture("Backgrounds/MireFG2");
                                Main.backgroundTexture[56] = instance.GetTexture("Backgrounds/MireFG1");
                                Main.backgroundTexture[57] = instance.GetTexture("Backgrounds/MireFG1");
                                Main.backgroundTexture[58] = instance.GetTexture("Backgrounds/MireFG1");
                            }
                            if(!Main.dayTime && (Main.bgAlpha2[2] < 0.10f || Main.bgAlpha2[2] == 1f))
                            {
                                Main.backgroundTexture[0] = instance.GetTexture("Backgrounds/YamataStars");
                            }
                            break;
                        case 2:
                            Main.numClouds = 0;
                            if(Main.LogoB <= 255)
                            {
                                Main.logoTexture = instance.GetTexture("UI/LogoVoid");
                            }
                            if(Main.LogoB < 10 || (!Main.dayTime && Main.LogoA <= 255))
                            {
                                Main.logo2Texture = instance.GetTexture("UI/LogoVoid");
                            }
                            if(SkyManager.Instance["AAMod:VoidSky"] != null) SkyManager.Instance.Activate("AAMod:VoidSky",default, new object[0]);
                            if(Main.dayTime && (Main.bgAlpha2[0] < 0.10f || Main.bgAlpha2[0] == 1f))
                            {
                                Main.backgroundTexture[0] = blankTexture;
                                Main.backgroundTexture[171] = blankTexture;
                                Main.backgroundTexture[172] = blankTexture;
                                Main.backgroundTexture[173] = blankTexture;
                                Main.backgroundTexture[24] = blankTexture;
                                Main.backgroundTexture[25] = blankTexture;
                                Main.backgroundTexture[56] = blankTexture;
                                Main.backgroundTexture[57] = blankTexture;
                                Main.backgroundTexture[58] = blankTexture;
                            }
                            break;
                        default:
                            goto case 0;
                    }
                }
                else if(AAMenuReset)
                {
                    if(SkyManager.Instance["AAMod:MireSky"].IsActive()) SkyManager.Instance.Deactivate("AAMod:MireSky", new object[0]);
                    if(SkyManager.Instance["AAMod:VoidSky"].IsActive()) SkyManager.Instance.Deactivate("AAMod:VoidSky", new object[0]);
                    Main.sunTexture = ModContent.GetTexture("Terraria/Sun");
                    Main.logoTexture = ModContent.GetTexture("Terraria/Logo");
                    Main.logo2Texture = ModContent.GetTexture("Terraria/Logo2");
                    AAMenuReset = false;
                    Main.backgroundTexture[0] = ModContent.GetTexture("Terraria/Background_" + 0);
                    Main.backgroundTexture[171] = ModContent.GetTexture("Terraria/Background_" + 171);
                    Main.backgroundTexture[172] = ModContent.GetTexture("Terraria/Background_" + 172);
                    Main.backgroundTexture[173] = ModContent.GetTexture("Terraria/Background_" + 173);
                    Main.backgroundTexture[24] = ModContent.GetTexture("Terraria/Background_" + 24);
                    Main.backgroundTexture[25] = ModContent.GetTexture("Terraria/Background_" + 25);
                    Main.backgroundTexture[56] = ModContent.GetTexture("Terraria/Background_" + 56);
                    Main.backgroundTexture[57] = ModContent.GetTexture("Terraria/Background_" + 57);
                    Main.backgroundTexture[58] = ModContent.GetTexture("Terraria/Background_" + 58);
                }
            }
            else if(AAMenuReset)
            {
                AAMenuReset = false;
                Main.sunTexture = ModContent.GetTexture("Terraria/Sun");
                if(SkyManager.Instance["AAMod:MireSky"].IsActive()) SkyManager.Instance.Deactivate("AAMod:MireSky", new object[0]);
                if(SkyManager.Instance["AAMod:VoidSky"].IsActive()) SkyManager.Instance.Deactivate("AAMod:VoidSky", new object[0]);
                Main.backgroundTexture[0] = ModContent.GetTexture("Terraria/Background_" + 0);
                Main.backgroundTexture[171] = ModContent.GetTexture("Terraria/Background_" + 171);
                Main.backgroundTexture[172] = ModContent.GetTexture("Terraria/Background_" + 172);
                Main.backgroundTexture[173] = ModContent.GetTexture("Terraria/Background_" + 173);
                Main.backgroundTexture[24] = ModContent.GetTexture("Terraria/Background_" + 24);
                Main.backgroundTexture[25] = ModContent.GetTexture("Terraria/Background_" + 25);
                Main.backgroundTexture[56] = ModContent.GetTexture("Terraria/Background_" + 56);
                Main.backgroundTexture[57] = ModContent.GetTexture("Terraria/Background_" + 57);
                Main.backgroundTexture[58] = ModContent.GetTexture("Terraria/Background_" + 58);
            }
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

            bool zoneShen = (Ancients.ZoneRisingSunPagoda || Ancients.ZoneRisingMoonLake) && !AAWorld.downedShen;

            if (zoneShen && AAWorld.downedAllAncients)
            {
                priority = MusicPriority.Event;
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/SleepingDragon");

                return;
            }

            if (Ancients.ZoneVoid && AAWorld.downedZero && !player.ZoneRockLayerHeight)
            {
                priority = MusicPriority.Event;
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/SleepingGiant");

                return;
            }

            if (Ancients.ZoneHoard)
            {
                priority = MusicPriority.Event;
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Hoard");
                return;
            }

            if (Ancients.ZoneAcropolis)
            {
                priority = MusicPriority.Event;
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Acropolis");
                return;
            }

            if (Ancients.ZoneVoid)
            {
                priority = MusicPriority.Event;
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Void");


                if (NPC.downedMoonlord && !AAWorld.downedZero)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/VoidButNowItsSpooky");

                    return;
                }

                return;
            }

            if (Ancients.ZoneStars)
            {
                priority = MusicPriority.Event;
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Stars");

                return;
            }

            /*if (Ancients.ZoneShip)
            {
                priority = MusicPriority.Event;
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Ship");

                return;
            }*/

            if (Ancients.ZoneInferno)
            {
                if (player.ZoneRockLayerHeight)
                {
                    priority = MusicPriority.BiomeHigh;
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/InfernoUnderground");
                    return;
                }
                else
                {
                    if (!Main.dayTime)
                    {
                        priority = MusicPriority.BiomeHigh;
                        music = GetSoundSlot(SoundType.Music, "Sounds/Music/IN");
                        return;
                    }
                    if (Ancients.ZoneRisingSunPagoda && NPC.downedMoonlord && !AAWorld.downedAkuma)
                    {
                        priority = MusicPriority.BiomeHigh;
                        music = GetSoundSlot(SoundType.Music, "Sounds/Music/AkumaShrine");

                        return;
                    }
                    priority = MusicPriority.BiomeHigh;
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/InfernoSurface");

                    return;
                }
            }

            if (Ancients.ZoneMire)
            {
                if (player.ZoneRockLayerHeight)
                {
                    priority = MusicPriority.BiomeHigh;
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/MireUnderground");

                    return;
                }
                else
                {
                    if (Main.dayTime)
                    {
                        priority = MusicPriority.BiomeHigh;
                        music = GetSoundSlot(SoundType.Music, "Sounds/Music/DM");
                        return;
                    }

                    if (Ancients.ZoneRisingMoonLake && NPC.downedMoonlord && !AAWorld.downedYamata)
                    {
                        priority = MusicPriority.BiomeHigh;
                        music = GetSoundSlot(SoundType.Music, "Sounds/Music/Shrines");
                        return;
                    }

                    priority = MusicPriority.BiomeHigh;
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/MireSurface");

                    return;
                }
            }

            if (Ancients.Terrarium)
            {
                priority = MusicPriority.BiomeHigh;
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Keep");

                return;
            }

            if (Ancients.ZoneMush)
            {

                priority = MusicPriority.BiomeMedium;
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Shroom");

                return;
            }
        }

        private void Wiring_ActuateForced(On.Terraria.Wiring.orig_ActuateForced orig, int i, int j)
        {
            Tile tile = Main.tile[i, j];
            if (tile.type == ModContent.TileType<Tiles.AcropolisBlock2>() || tile.type == ModContent.TileType<Tiles.AcropolisBlock>() ||
                tile.type == ModContent.TileType<Tiles.GreedStone>() || tile.type == ModContent.TileType<Tiles.GreedBrick>())
            {
                return;
            }
            orig(i, j);
        }

        private static bool Actuate(On.Terraria.Wiring.orig_Actuate orig, int i, int j)
        {
            Tile tile = Main.tile[i, j];
            if (tile.type == ModContent.TileType<Tiles.AcropolisBlock2>() || tile.type == ModContent.TileType<Tiles.AcropolisBlock>() ||
                tile.type == ModContent.TileType<Tiles.GreedStone>() || tile.type == ModContent.TileType<Tiles.GreedBrick>())
            {
                return false;
            }
            return orig(i, j);
        }

        public static void Chat(string s, Color color, bool sync = true)
        {
            Chat(s, color.R, color.G, color.B, sync);
        }

        /*
         * Sends the given string to chat, with the given color values.
         */
        public static void Chat(string s, byte colorR = 255, byte colorG = 255, byte colorB = 255, bool sync = true)
        {
            if (!AAConfigClient.Instance.NoBossDialogue)
            {
                if (Main.netMode == NetmodeID.SinglePlayer) { Main.NewText(s, colorR, colorG, colorB); }
                else
                if (Main.netMode == NetmodeID.MultiplayerClient) { Main.NewText(s, colorR, colorG, colorB); }
                else //if(sync){ NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(s), new Color(colorR, colorG, colorB), Main.myPlayer); } }else
                if (sync && Main.netMode == NetmodeID.Server) { NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(s), new Color(colorR, colorG, colorB), -1); }
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
                        case "daybringer": return AAWorld.downedDB;
                        case "nightcrawler": return AAWorld.downedNC;
                        case "equinox": return AAWorld.downedEquinox;
                        case "ancient":
                        case "ancientany": return AAWorld.downedAncient;
                        case "sancient":
                        case "sancientany": return AAWorld.downedSAncient;
                        case "gripsS":
                        case "akuma": return AAWorld.downedAkuma;
                        case "yamata": return AAWorld.downedYamata;
                        case "zero": return AAWorld.downedZero;
                        case "shen":
                        case "shendoragon": return AAWorld.downedShen;
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

            MsgType msg = (MsgType)bb.ReadByte();
            if (msg == MsgType.ProjectileHostility) //projectile hostility and ownership
            {
                int owner = bb.ReadInt32();
                int projID = bb.ReadInt32();
                bool friendly = bb.ReadBoolean();
                bool hostile = bb.ReadBoolean();
                if (Main.projectile[projID] != null)
                {
                    Main.projectile[projID].owner = owner;
                    Main.projectile[projID].friendly = friendly;
                    Main.projectile[projID].hostile = hostile;
                }
                if (Main.netMode == NetmodeID.Server) MNet.SendBaseNetMessage(0, owner, projID, friendly, hostile);
            }
            else
            if (msg == MsgType.SyncAI) //sync AI array
            {
                int classID = bb.ReadByte();
                int id = bb.ReadInt16();
                int aitype = bb.ReadByte();
                int arrayLength = bb.ReadByte();
                float[] newAI = new float[arrayLength];
                for (int m = 0; m < arrayLength; m++)
                {
                    newAI[m] = bb.ReadSingle();
                }
                if (classID == 0 && Main.npc[id] != null && Main.npc[id].active && Main.npc[id].modNPC != null && Main.npc[id].modNPC is ParentNPC)
                {
                    ((ParentNPC)Main.npc[id].modNPC).SetAI(newAI, aitype);
                }
                else
                if (classID == 1 && Main.projectile[id] != null && Main.projectile[id].active && Main.projectile[id].modProjectile != null && Main.projectile[id].modProjectile is ParentProjectile)
                {
                    ((ParentProjectile)Main.projectile[id].modProjectile).SetAI(newAI, aitype);
                }
                if (Main.netMode == NetmodeID.Server) BaseNet.SyncAI(classID, id, newAI, aitype);
            }
        }

        public bool AAMenuset = false;
        public bool AAMenuReset = true;

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int wireSelectionLayerIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Wire Selection"));
            if (wireSelectionLayerIndex != -1)
            {
                layers.Insert(wireSelectionLayerIndex, new LegacyGameInterfaceLayer(
                "AAMod: Radial UIs",
                delegate
                {
                    if (TerratoolInterface?.CurrentState is ToggableUI && lastUpdateUIGameTime != null)
                    {
                        TerratoolInterface.Draw(Main.spriteBatch, lastUpdateUIGameTime);
                    }

                    return true;
                },
                InterfaceScaleType.UI));
            }

            Titles modPlayer = Main.player[Main.myPlayer].GetModPlayer<Titles>();
            if (modPlayer.text)
            {
                var textLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                var computerState = new LegacyGameInterfaceLayer("AAMod: UI",
                    delegate
                    {
                        BossTitle(modPlayer.BossID);
                        return true;
                    },
                    InterfaceScaleType.UI);
                layers.Insert(textLayer, computerState);
            }
        }

        private void BossTitle(int BossID)
        {
            string BossName = "";
            string BossTitle = "";
            Color titleColor = Color.White;

            switch (BossID)
            {
                case 1:
                    BossName = Lang.BossTitle("AnubisLegendscribeName");
                    BossTitle = "";
                    titleColor = Color.Goldenrod;
                    break;
                case 2:
                    BossName = Lang.BossTitle("AthenaName");
                    BossTitle = "";
                    titleColor = Color.CornflowerBlue;
                    break;
                case 3:
                    BossName = Lang.BossTitle("GreedName");
                    BossTitle = "";
                    titleColor = Color.Gold;
                    break;
                case 4:
                    BossName = Lang.BossTitle("FAnubisName");
                    BossTitle = Lang.BossTitle("FAnubisTitle");
                    titleColor = Color.DarkGreen;
                    break;
                case 5:
                    BossName = Lang.BossTitle("OAthenaName");
                    BossTitle = Lang.BossTitle("OAthenaTitle");
                    titleColor = Color.Turquoise;
                    break;
                case 6:
                    BossName = Lang.BossTitle("WKGName");
                    BossTitle = Lang.BossTitle("WKGTitle");
                    titleColor = Color.Gold;
                    break;
                case 7:
                    BossName = Lang.BossTitle("AkumaName");
                    BossTitle = Lang.BossTitle("AkumaTitle");
                    titleColor = Color.OrangeRed;
                    break;
                case 8:
                    BossName = Lang.BossTitle("AkumaAName");
                    BossTitle = Lang.BossTitle("AkumaATitle");
                    titleColor = Color.DeepSkyBlue;
                    break;
                case 9:
                    BossName = Lang.BossTitle("YamataName");
                    BossTitle = Lang.BossTitle("YamataTitle");
                    titleColor = Color.Indigo;
                    break;
                case 10:
                    BossName = Lang.BossTitle("YamataAName");
                    BossTitle = Lang.BossTitle("YamataATitle");
                    titleColor = Color.MediumVioletRed;
                    break;
                case 11:
                    BossName = Lang.BossTitle("ZER0Name");
                    BossTitle = Lang.BossTitle("ZER0Title");
                    titleColor = Color.Red;
                    break;
                case 12:
                    BossName = Lang.BossTitle("ZER0PName");
                    BossTitle = Lang.BossTitle("ZER0PTitle");
                    titleColor = Color.Red;
                    break;
                case 13:
                    BossName = Lang.BossTitle("CRajahRabbitName");
                    BossTitle = Lang.BossTitle("CRajahRabbitTitle");
                    titleColor = Color.LightCyan;
                    break;
                case 14:
                    BossName = Lang.BossTitle("ShenName");
                    BossTitle = Lang.BossTitle("ShenTitle");
                    titleColor = Color.Magenta;
                    break;
                case 15:
                    BossName = Lang.BossTitle("ShenAName");
                    BossTitle = Lang.BossTitle("ShenATitle");
                    titleColor = Color.Magenta;
                    break;
                case 16:
                    BossName = Lang.BossTitle("AHName");
                    BossTitle = Lang.BossTitle("AHTitle");
                    titleColor = Color.Magenta;
                    break;
                case 17:
                    BossName = Lang.BossTitle("EquinoxName");
                    BossTitle = Lang.BossTitle("EquinoxTitle");
                    titleColor = Color.BlueViolet;
                    break;
                case 18:
                    BossName = Lang.BossTitle("RajahName");
                    BossTitle = "";
                    titleColor = Color.LightCyan;
                    break;
            }

            Titles modPlayer2 = Main.player[Main.myPlayer].GetModPlayer<Titles>();
            float alpha = modPlayer2.alphaText;
            float alpha2 = modPlayer2.alphaText2;

            if (BossID == 16)
            {
                Vector2 textSize2 = Main.fontDeathText.MeasureString(BossTitle) * .6f;
                float text2PositionLeft = Main.screenWidth / 2 - textSize2.X / 2;

                Main.spriteBatch.DrawString(Main.fontDeathText, BossTitle, new Vector2(text2PositionLeft, (Main.screenHeight / 2) - 350), titleColor * ((255 - alpha2) / 255f), 0f, Vector2.Zero, .6f, SpriteEffects.None, 0f);

                float alpha3 = modPlayer2.alphaText3;
                float alpha4 = modPlayer2.alphaText4;

                Vector2 ASize = Main.fontDeathText.MeasureString(Lang.BossTitle("AsheName"));
                Vector2 AndSize = Main.fontDeathText.MeasureString(Lang.BossTitle("AHANd"));
                Vector2 HSize = Main.fontDeathText.MeasureString(Lang.BossTitle("HarukaName"));
                Vector2 BlankTexSize = Main.fontDeathText.MeasureString(" ");
                float APositionLeft = Main.screenWidth / 2 - (ASize.X + BlankTexSize.X + AndSize.X + BlankTexSize.X + HSize.X) / 2;
                float AndPositionLeft = APositionLeft + ASize.X + BlankTexSize.X;
                float HPositionLeft = AndPositionLeft + AndSize.X + BlankTexSize.X;

                Main.spriteBatch.DrawString(Main.fontDeathText, Lang.BossTitle("AsheName"), new Vector2(APositionLeft, Main.screenHeight / 2 - 300), Color.OrangeRed * ((255 - alpha) / 255f), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                Main.spriteBatch.DrawString(Main.fontDeathText, Lang.BossTitle("AHANd"), new Vector2(AndPositionLeft, Main.screenHeight / 2 - 300), Color.Magenta * ((255 - alpha3) / 255f), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                Main.spriteBatch.DrawString(Main.fontDeathText, Lang.BossTitle("HarukaName"), new Vector2(HPositionLeft, Main.screenHeight / 2 - 300), Color.Indigo * ((255 - alpha4) / 255f), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                return;
            }
            if (BossID == 14)
            {
                var BossNameSplit = BossName.Split(' ');
                var BossTitleSplit = BossTitle.Split(' ');
                if(Language.ActiveCulture == GameCulture.Chinese)
                {
                    BossNameSplit = Regex.Split(BossName,"",RegexOptions.IgnoreCase);;
                    BossTitleSplit = Regex.Split(BossTitle,"",RegexOptions.IgnoreCase);;
                }
                Vector2 textSize = Main.fontDeathText.MeasureString("~ " + BossName + " ~");
                Vector2 textSize2 = Main.fontDeathText.MeasureString(BossTitle) * .6f;;
                float textPositionLeft = Main.screenWidth / 2 - textSize.X / 2;
                float text2PositionLeft = Main.screenWidth / 2 - textSize2.X / 2;
                int k = 0;
                foreach(string i in BossTitleSplit)
                {
                    if(i == "" || i == " ")
                    {
                        continue;
                    }
                    Vector2 SplitSizeTitle = Main.fontDeathText.MeasureString(i) * .6f;;
                    Vector2 BlankTexSizeTitle = Main.fontDeathText.MeasureString(" ") * .6f;;
                    Main.spriteBatch.DrawString(Main.fontDeathText, (Language.ActiveCulture == GameCulture.Chinese? "" : " ") + i, new Vector2(text2PositionLeft, (Main.screenHeight / 2) - 350), (k % 2 == 0? Color.OrangeRed : Color.Indigo) * ((255 - alpha2) / 255f), 0f, Vector2.Zero, .6f, SpriteEffects.None, 0f);
                    text2PositionLeft += SplitSizeTitle.X + (Language.ActiveCulture == GameCulture.Chinese? 0 : BlankTexSizeTitle.X);
                    k ++;
                }
                Vector2 SplitSize = Main.fontDeathText.MeasureString("~");
                Main.spriteBatch.DrawString(Main.fontDeathText, "~" + (Language.ActiveCulture == GameCulture.Chinese? " " : ""), new Vector2(textPositionLeft, (Main.screenHeight / 2) - 300), Color.OrangeRed * ((255 - alpha2) / 255f), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                textPositionLeft += SplitSize.X;
                k = 0;
                foreach(string i in BossNameSplit)
                {
                    if(i == "" || i == " ")
                    {
                        continue;
                    }
                    Vector2 SplitSizeName = Main.fontDeathText.MeasureString(i);
                    Vector2 BlankTexSizeName = Main.fontDeathText.MeasureString(" ");
                    Main.spriteBatch.DrawString(Main.fontDeathText, (Language.ActiveCulture == GameCulture.Chinese? "" : " ") + i, new Vector2(textPositionLeft, (Main.screenHeight / 2) - 300), (k % 2 == 0? Color.OrangeRed : Color.Indigo) * ((255 - alpha2) / 255f), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    textPositionLeft += SplitSizeName.X + (Language.ActiveCulture == GameCulture.Chinese? 0 : BlankTexSizeName.X);
                    k ++;
                }
                Main.spriteBatch.DrawString(Main.fontDeathText, " ~", new Vector2(textPositionLeft, (Main.screenHeight / 2) - 300), Color.Indigo * ((255 - alpha2) / 255f), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                return;
            }
            else
            {
                Vector2 textSize = Main.fontDeathText.MeasureString("~ " + BossName + " ~");
                Vector2 textSize2 = Main.fontDeathText.MeasureString(BossTitle) * .6f;;
                float textPositionLeft = Main.screenWidth / 2 - textSize.X / 2;
                float text2PositionLeft = Main.screenWidth / 2 - textSize2.X / 2;

                Main.spriteBatch.DrawString(Main.fontDeathText, BossTitle, new Vector2(text2PositionLeft, (Main.screenHeight / 2) - 350), titleColor * ((255 - alpha2) / 255f), 0f, Vector2.Zero, .6f, SpriteEffects.None, 0f);
                Main.spriteBatch.DrawString(Main.fontDeathText, "~ " + BossName + " ~", new Vector2(textPositionLeft, Main.screenHeight / 2 - 300), titleColor * ((255 - alpha) / 255f), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }

        }

        public static void ShowTitle(NPC npc, int ID)
        {
            if (AAConfigClient.Instance.AncientIntroText)
            {
                Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<Title>(), 0, 0, Main.myPlayer, ID, 0);
            }
        }

        public static void ShowTitle(Player player, int ID)
        {
            if (AAConfigClient.Instance.AncientIntroText)
            {
                Projectile.NewProjectile(player.Center, Vector2.Zero, ModContent.ProjectileType<Title>(), 0, 0, Main.myPlayer, ID, 0);
            }
        }

        public static void ShowSistersTitle(NPC npc)
        {
            if (AAConfigClient.Instance.AncientIntroText)
            {
                Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<SistersTitle>(), 0, 0, Main.myPlayer, 16, 0);
            }
        }
    }

    enum MsgType : byte
    {
        ProjectileHostility,
        SyncAI
    }
}
