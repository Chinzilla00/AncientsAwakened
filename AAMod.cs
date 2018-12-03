using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.UI;
using AAMod.Backgrounds;
using Terraria.Graphics.Shaders;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using System.Reflection;
using AAMod.Items.Materials;
using AAMod.Items.Armor.Darkmatter;
using AAMod.Items.Armor.Radium;
using AAMod.Items.Boss;
using AAMod.Items.Blocks;

namespace AAMod
{
    class AAMod : Mod
    {
        public static int GoblinSoul;
        public static ModHotKey InfinityHotKey;
        internal static AAMod instance;
        internal UserInterface UserInterface;
        public static bool AkumaMusic = false;
        public static bool YamataMusic = false;
        public static bool Slayer = false;
        public static AAMod self = null;
        public static IDictionary<string, Texture2D> Textures = null;
        public static Dictionary<string, Texture2D> precachedTextures = new Dictionary<string, Texture2D>();

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

        public override void PostSetupContent() //Hey alpha... Please notice the method you are using... Anything called in this list is called on the menu... This comment was brought to you by: Chinzilla00!!
        {
            Mod AchievementLibs = ModLoader.GetMod("AchievementLibs");
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            Mod yabhb = ModLoader.GetMod("FKBossHealthBar");
            if (yabhb != null)
            {
                Call("RegisterHealthBarMini", instance.NPCType("YamataHeadF1"));

                Call("RegisterHealthBarMini", instance.NPCType("YamataHeadF2"));

                #region Zero HB
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/ZeroBarHead"),
                    GetTexture("Healthbars/ZeroBarBody"),
                    GetTexture("Healthbars/ZeroBarTail"),
                    GetTexture("Healthbars/ZeroBarFill"));
                yabhb.Call("hbSetColours",
                new Color(0.76f, 0.24f, 0.24f), // 100%
                new Color(0.631f, 0.152f, 0.215f), // 50%
                new Color(0.568f, 0.55f, 0.121f));// 0%
                yabhb.Call("hbSetMidBarOffset", 0, 10);
                yabhb.Call("hbSetBossHeadCentre", 54, 34);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("Zero")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/ZeroBarHead"),
                    GetTexture("Healthbars/ZeroBarBody"),
                    GetTexture("Healthbars/ZeroBarTail"),
                    GetTexture("Healthbars/ZeroBarFill"));
                yabhb.Call("hbSetColours",
                new Color(0.76f, 0.24f, 0.24f), // 100%
                new Color(0.631f, 0.152f, 0.215f), // 50%
                new Color(0.568f, 0.55f, 0.121f));// 0%
                yabhb.Call("hbSetMidBarOffset", 0, 10);
                yabhb.Call("hbSetBossHeadCentre", 54, 34);
                yabhb.Call("hbSetFillDecoOffsetSmall", 10);
                yabhb.Call("hbFinishSingle", (instance.NPCType("ZeroAwakened")));
                #endregion

            }
            if (bossChecklist != null)
            {
                bossChecklist.Call("AddBossWithInfo", "Mushroom Monarch", 0.00000000001f, (Func<bool>)(() => AAWorld.downedGrips), "Use a [i:" + ItemType("IntimidatingMushroom") + "] during the day");
                bossChecklist.Call("AddBossWithInfo", "Grips of Chaos", 2.00000000001f, (Func<bool>)(() => AAWorld.downedGrips), "Use a [i:" + ItemType("CuriousClaw") + "] or [i:" + ItemType("InterestingClaw") + "] at night");
                bossChecklist.Call("AddBossWithInfo", "Broodmother", 4.00000000001f, (Func<bool>)(() => AAWorld.downedBrood), "Use a [i:" + ItemType("DragonBell") + "] in the Inferno");
                bossChecklist.Call("AddBossWithInfo", "Retriever", 6.9999997f, (Func<bool>)(() => AAWorld.downedRetriever), "Use a [i:" + ItemType("CyberneticClaw") + "] at night");
                bossChecklist.Call("AddBossWithInfo", "Raider Ultima", 6.9999997f, (Func<bool>)(() => AAWorld.downedRaider), "Use a [i:" + ItemType("CyberneticBell") + "] at night");
                bossChecklist.Call("AddBossWithInfo", "Nightcrawler & Daybringer", 14.00000000001f, (Func<bool>)(() => AAWorld.downedEquinox), "Use a [i:" + ItemType("EquinoxWorm") + "]");

                if (Main.expertMode)
                {
                    bossChecklist.Call("AddBossWithInfo", "Yamata", 18.0001f, (Func<bool>)(() => AAWorld.downedYamataA), "Use a [i:" + ItemType("DreadSigil") + "] in the Mire at night");
                    bossChecklist.Call("AddBossWithInfo", "Akuma", 18.0001f, (Func<bool>)(() => AAWorld.downedAkumaA), "Use a [i:" + ItemType("DraconianSigil") + "] in the Inferno during the day");
                    bossChecklist.Call("AddBossWithInfo", "Zero", 18.0001f, (Func<bool>)(() => AAWorld.downedZeroA), "Use a [i:" + ItemType("ZeroTesseract") + "] in the Void");
                }
                else
                {
                    bossChecklist.Call("AddBossWithInfo", "Yamata", 18.0001f, (Func<bool>)(() => AAWorld.downedYamata), "Use a [i:" + ItemType("DreadSigil") + "] in the Mire at night");
                    bossChecklist.Call("AddBossWithInfo", "Akuma", 18.0001f, (Func<bool>)(() => AAWorld.downedAkuma), "Use a [i:" + ItemType("DraconianSigil") + "] in the Inferno during the day");
                    bossChecklist.Call("AddBossWithInfo", "Zero", 18.001f, (Func<bool>)(() => AAWorld.downedZero), "Use a [i:" + ItemType("ZeroTesseract") + "] in the Void");
                }

                //SlimeKing = 1f;
                //EyeOfCthulhu = 2f;
                //EaterOfWorlds = 3f;
                //QueenBee = 4f;
                //Skeletron = 5f;
                //WallOfFlesh = 6f;
                //TheTwins = 7f;
                //TheDestroyer = 8f;
                //SkeletronPrime = 9f;
                //Plantera = 10f;
                //Golem = 11f;
                //DukeFishron = 12f;
                //LunaticCultist = 13f;
                //Moonlord = 14f;
            }
            
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
            Textures = (IDictionary<string, Texture2D>)typeof(Mod).GetField("textures", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(this);
            instance = this;
            GoblinSoul = CustomCurrencyManager.RegisterCurrency(new CustomCurrency(ItemType<Items.Currency.GoblinSoul>(), 999L));
            if (Main.rand == null)
                Main.rand = new Terraria.Utilities.UnifiedRandom();

            InfinityHotKey = RegisterHotKey("Snap", "G");

            if (!Main.dedServ)
            {

                PremultiplyTexture(GetTexture("Backgrounds/VoidBH"));
                PremultiplyTexture(GetTexture("Backgrounds/MireMoon"));
                PremultiplyTexture(GetTexture("Backgrounds/InfernoSun"));
                PremultiplyTexture(GetTexture("Backgrounds/InfernoSky"));
                PremultiplyTexture(GetTexture("Backgrounds/MireSky"));
                PremultiplyTexture(GetTexture("Backgrounds/VoidSky"));
                PremultiplyTexture(GetTexture("Backgrounds/fogless"));
                PremultiplyTexture(GetTexture("Backgrounds/fog"));
                PremultiplyTexture(GetTexture("Backgrounds/AkumaSun"));
                PremultiplyTexture(GetTexture("Backgrounds/YamataMoon"));

                AddEquipTexture(null, EquipType.Legs, "N1_Legs", "AAMod/Items/Vanity/N1/N1_Legs");

                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Monarch"), ItemType("MonarchBox"), TileType("MonarchBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/GripsTheme"), ItemType("GripsBox"), TileType("GripsBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/WeAreNumberOne"), ItemType("N1Box"), TileType("N1Box"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/HydraTheme"), ItemType("HydraBox"), TileType("HydraBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/BroodTheme"), ItemType("BroodBox"), TileType("BroodBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/InfernoSurface"), ItemType("InfernoBox"), TileType("InfernoBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MireSurface"), ItemType("MireBox"), TileType("MireBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/InfernoUnderground"), ItemType("InfernoUBox"), TileType("InfernoUBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MireUnderground"), ItemType("MireUBox"), TileType("MireUBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Boss6"), ItemType("Boss6Box"), TileType("Boss6Box"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Equinox"), ItemType("Equibox"), TileType("Equibox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Void"), ItemType("VoidBox"), TileType("VoidBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Zero"), ItemType("ZeroBox"), TileType("ZeroBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Zero2"), ItemType("Zero2Box"), TileType("Zero2Box"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Akuma"), ItemType("AkumaBox"), TileType("AkumaBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Akuma2"), ItemType("AkumaABox"), TileType("AkumaABox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Yamata"), ItemType("YamataBox"), TileType("YamataBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Yamata2"), ItemType("YamataABox"), TileType("YamataABox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Shen"), ItemType("ShenBox"), TileType("ShenBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/ShenA"), ItemType("ShenABox"), TileType("ShenABox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/RayOfHope"), ItemType("RoHBox"), TileType("RoHBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/SARoH"), ItemType("SABox"), TileType("SABox"));

                Filters.Scene["AAMod:MireSky"] = new Filter(new MireSkyData("FilterMiniTower").UseColor(0f, 0.20f, 1f).UseOpacity(0.3f), EffectPriority.High);
                SkyManager.Instance["AAMod:MireSky"] = new MireSky();
                MireSky.PlanetTexture = GetTexture("Backgrounds/MireMoon");

                Filters.Scene["AAMod:VoidSky"] = new Filter(new VoidSkyData("FilterMiniTower").UseColor(0.15f, 0.1f, 0.1f).UseOpacity(0.3f), EffectPriority.High);
                SkyManager.Instance["AAMod:VoidSky"] = new VoidSky();
                VoidSky.PlanetTexture = GetTexture("Backgrounds/VoidBH");
                VoidSky.Asteroids = GetTexture("Backgrounds/Asteroids");

                Filters.Scene["AAMod:InfernoSky"] = new Filter(new InfernoSkyData("FilterMiniTower").UseColor(1f, 0.20f, 0f).UseOpacity(0.3f), EffectPriority.High);
                SkyManager.Instance["AAMod:InfernoSky"] = new InfernoSky();
                InfernoSky.PlanetTexture = GetTexture("Backgrounds/InfernoSun");

                Filters.Scene["AAMod:AkumaSky"] = new Filter(new AkumaSkyData("FilterMiniTower").UseColor(0f, 0.3f, 0.4f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                SkyManager.Instance["AAMod:AkumaSky"] = new AkumaSky();
                AkumaSky.PlanetTexture = GetTexture("Backgrounds/AkumaSun");

                Filters.Scene["AAMod:YamataSky"] = new Filter(new YamataSkyData("FilterMiniTower").UseColor(.7f, 0f, 0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                SkyManager.Instance["AAMod:YamataSky"] = new YamataSky();
                YamataSky.PlanetTexture = GetTexture("Backgrounds/YamataMoon");

                Filters.Scene["AAMod:Fog"] = new Filter(new FogScreenShaderData().UseColor(0.4f, 0, 0).UseOpacity(0.7f), EffectPriority.VeryHigh);
                SkyManager.Instance["AAMod:Fog"] = new Fog();
                
                UserInterface = new UserInterface();
                Main.itemTexture[1291] = GetTexture("Resprites/LifeFruit");
                Main.itemTexture[1327] = GetTexture("Resprites/DeathSickle");
                Main.itemTexture[3460] = GetTexture("Resprites/Luminite");
                if (WorldGen.crimson == true)
                {
                    //Main.itemTexture[521] = GetTexture("Resprites/SoulOfNight");
                }
            }
        }

        public override void Unload()
        {
            InfinityHotKey = null;
        }
        public override void AddRecipeGroups()
        {
            // Registers the new recipe group with the specified name
            RecipeGroup group0 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any Darkmatter Helmet", new int[]
            {
                ItemType<DarkmatterVisor>(),
                ItemType<DarkmatterHelm>(),
                ItemType<DarkmatterHelmet>(),
                ItemType<DarkmatterHeaddress>(),
                ItemType<DarkmatterMask>()
            });
            // Registers the new recipe group with the specified name
            RecipeGroup.RegisterGroup("AAMod:DarkmatterHelmets", group0);

            RecipeGroup group1 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any Radium Helmet", new int[]
            {
                ItemType<RadiumHat>(),
                ItemType<RadiumHelm>(),
                ItemType<RadiumHelmet>(),
                ItemType<RadiumHeadgear>(),
                ItemType<RadiumMask>()
            });
            // Registers the new recipe group with the specified name
            RecipeGroup.RegisterGroup("AAMod:RadiumHelmets", group1);

            RecipeGroup group2 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any Gold", new int[]
            {
                ItemID.GoldBar,
                ItemID.PlatinumBar
            });
            // Registers the new recipe group with the specified name
            RecipeGroup.RegisterGroup("AAMod:Gold", group2);
            RecipeGroup group3 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any Astral Station", new int[]
            {
                ItemType("RadiantArcanum"),
                ItemType("QuantumFusionAccelerator"),
            });
            RecipeGroup.RegisterGroup("AAMod:AstralStations", group3);

            RecipeGroup group4 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any Ancient Material", new int[]
            {
                ItemType("UnstableSingularity"),
                ItemType("CrucibleScale"),
                ItemType("DreadScale")
            });
            RecipeGroup.RegisterGroup("AAMod:AncientMaterials", group4);

            RecipeGroup group5 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any Superancient Material", new int[]
            {
                ItemType("ChaosSoul")
            });
            RecipeGroup.RegisterGroup("AAMod:SuperAncientMaterials", group5);

            RecipeGroup group6 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any World Evil Material", new int[]
            {
                ItemID.Ichor,
                ItemID.CursedFlame
            });
            RecipeGroup.RegisterGroup("AnyIchor", group6);

            group6 = new RecipeGroup(getName: () => Language.GetTextValue("LegacyMisc.37") + " Hardmode Forge", validItems: new int[]
            {
                ItemID.AdamantiteForge,
                ItemID.TitaniumForge
            });
            RecipeGroup.RegisterGroup("AnyHardmodeForge", group6);

            RecipeGroup group7 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any Chaos Claw", new int[]
            {
                ItemType<DragonClaw>(),
                ItemType<HydraClaw>(),
            });
            RecipeGroup.RegisterGroup("AAMod:ChaosClaw", group7);

            RecipeGroup group8 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any Iron", new int[]
            {
                ItemID.IronBar,
                ItemID.LeadBar
            });
            RecipeGroup.RegisterGroup("AAMod:Iron", group8);

            RecipeGroup group9 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any Copper", new int[]
            {
                ItemID.CopperBar,
                ItemID.TinBar
            });
            RecipeGroup.RegisterGroup("AAMod:Copper", group9);

            RecipeGroup group10 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any Silver", new int[]
            {
                ItemID.SilverBar,
                ItemID.TungstenBar
            });
            RecipeGroup.RegisterGroup("AAMod:Silver", group10);

            RecipeGroup group11 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any Evil Bar", new int[]
            {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar
            });
            RecipeGroup.RegisterGroup("AAMod:EvilBar", group11);

            RecipeGroup group12 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any Chaos Bar", new int[]
            {
                ItemType<IncineriteBar>(),
                ItemType<AbyssiumBar>(),
            });
            RecipeGroup.RegisterGroup("AAMod:ChaosBar", group12);

            RecipeGroup group13 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any Evil/Chaos Bar", new int[]
            {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar,
                ItemType<IncineriteBar>(),
                ItemType<AbyssiumBar>(),
            });
            RecipeGroup.RegisterGroup("AAMod:EvilorChaosBar", group13);

            if (RecipeGroup.recipeGroupIDs.ContainsKey("Wood"))
            {
                int index = RecipeGroup.recipeGroupIDs["Wood"];
                RecipeGroup.recipeGroups[index].ValidItems.Add(ItemType<Razewood>());
                RecipeGroup.recipeGroups[index].ValidItems.Add(ItemType<Bogwood>());
                RecipeGroup.recipeGroups[index].ValidItems.Add(ItemType<OroborosWood>());
            }
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
            if (Main.myPlayer != -1 && !Main.gameMenu && Main.LocalPlayer.active)
            {

                // Make sure your logic here goes from lowest priority to highest so your intended priority is maintained.

                if (player.HeldItem.type == ItemType("Sax"))
                {

                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/WeAreNumberOne");

                    priority = MusicPriority.BossHigh;

                }

            }
            if (AkumaMusic == true)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Akuma2");

                priority = MusicPriority.BossHigh;
                return;
            }
            if (YamataMusic == true)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Yamata2");

                priority = MusicPriority.BossHigh;
                return;
            }
            if (Slayer == true)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/ZeroDeath");

                priority = MusicPriority.BossHigh;
                return;
            }
            AAPlayer Ancients = player.GetModPlayer<AAPlayer>();
            if (Ancients.ZoneInferno)
            {
                if (player.ZoneRockLayerHeight)
                {
                    priority = MusicPriority.BiomeHigh;
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/InfernoUnderground");
                }
                else
                {
                    priority = MusicPriority.BiomeHigh;
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/InfernoSurface");
                }
            }
            if (Ancients.ZoneMush)
            {
                music = MusicID.Mushrooms;
            }
            if (Ancients.ZoneMire)
            {

                if (player.ZoneRockLayerHeight)
                {
                    priority = MusicPriority.BiomeHigh;
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/MireUnderground");
                }
                else
                {
                    priority = MusicPriority.BiomeHigh;
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/MireSurface");
                }
            }
            if (Ancients.ZoneVoid)
            {
                priority = MusicPriority.Event;
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Void");
            }
        }
        public override void AddRecipes()
        {
            RecipeFinder finder = new RecipeFinder();
            {
                finder = new RecipeFinder();
                finder.AddIngredient(ItemID.BloodButcherer, 1);
                finder.AddIngredient(ItemID.FieryGreatsword, 1);
                finder.AddIngredient(ItemID.BladeofGrass, 1);
                finder.AddIngredient(ItemID.Muramasa, 1);
                finder.AddTile(TileID.DemonAltar);
                finder.SetResult(ItemID.NightsEdge, 1);
                Recipe recipe2 = finder.FindExactRecipe();
                if (recipe2 != null)
                {
                    RecipeEditor editor = new RecipeEditor(recipe2);
                    editor.DeleteRecipe();
                }
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "HallowedOre", 4);
                recipe.AddTile(null, "HallowedForge");
                recipe.SetResult(ItemID.HallowedBar, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "TrueFleshrendClaymore", 1);
                recipe.AddIngredient(ItemID.TrueExcalibur, 1);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(ItemID.TerraBlade, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "DevilSilk", 5);
                recipe.AddIngredient(ItemID.Hay, 5);
                recipe.AddTile(null, "HellstoneAnvil");
                recipe.SetResult(ItemID.GuideVoodooDoll, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "TrueFleshrendClaymore", 1);
                recipe.AddIngredient(ItemID.TrueExcalibur, 1);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(ItemID.TerraBlade, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.Wood, 30);
                recipe.AddIngredient(ItemID.IronBar, 10);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBox, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.Wood, 30);
                recipe.AddIngredient(ItemID.LeadBar, 10);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBox, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.GrassSeeds, 10);
                recipe.AddIngredient(ItemID.DirtBlock, 10);
                recipe.AddIngredient(ItemID.Wood, 10);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxOverworldDay, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.GrassSeeds, 10);
                recipe.AddIngredient(ItemID.DirtBlock, 10);
                recipe.AddIngredient(ItemID.Wood, 10);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxAltOverworldDay, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.Lens, 3);
                recipe.AddIngredient(ItemID.FallenStar, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxNight, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.BottledWater, 5);
                recipe.AddIngredient(ItemID.UmbrellaHat, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxRain, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.SnowBlock, 30);
                recipe.AddIngredient(ItemID.BorealWood, 30);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxSnow, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.IceBlock, 30);
                recipe.AddIngredient(ItemID.BorealWood, 30);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxIce, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.SandBlock, 40);
                recipe.AddIngredient(ItemID.Cactus, 15);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxDesert, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 1);
                recipe.AddIngredient(ItemID.SharkFin, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxSandstorm, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.Coral, 3);
                recipe.AddIngredient(ItemID.Starfish, 3);
                recipe.AddIngredient(ItemID.Seashell, 3);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxOcean, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.DirtBlock, 50);
                recipe.AddIngredient(ItemID.IronOre, 10);
                recipe.AddIngredient(ItemID.StoneBlock, 50);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxUnderground, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.DirtBlock, 50);
                recipe.AddIngredient(ItemID.LeadOre, 10);
                recipe.AddIngredient(ItemID.StoneBlock, 50);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxUnderground, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.DirtBlock, 50);
                recipe.AddIngredient(ItemID.LeadOre, 10);
                recipe.AddIngredient(ItemID.StoneBlock, 50);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxAltUnderground, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.DirtBlock, 50);
                recipe.AddIngredient(ItemID.IronOre, 10);
                recipe.AddIngredient(ItemID.StoneBlock, 50);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxAltUnderground, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.Feather, 20);
                recipe.AddIngredient(ItemID.SunplateBlock, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxSpace, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.GlowingMushroom, 20);
                recipe.AddIngredient(ItemID.Mushroom, 10);
                recipe.AddIngredient(ItemID.MushroomGrassSeeds, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxMushrooms, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.MudBlock, 20);
                recipe.AddIngredient(ItemID.JungleGrassSeeds, 5);
                recipe.AddIngredient(ItemID.RichMahogany, 30);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxJungle, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.RottenChunk, 10);
                recipe.AddIngredient(ItemID.CorruptSeeds, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxCorruption, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.EbonstoneBlock, 30);
                recipe.AddIngredient(ItemID.RottenChunk, 10);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxUndergroundCorruption, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.Vertebrae, 10);
                recipe.AddIngredient(ItemID.CrimsonSeeds, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxCrimson, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.CrimstoneBlock, 30);
                recipe.AddIngredient(ItemID.Vertebrae, 10);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxUndergroundCrimson, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.CrystalShard, 10);
                recipe.AddIngredient(ItemID.HallowedSeeds, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxTheHallow, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.PearlstoneBlock, 30);
                recipe.AddIngredient(ItemID.UnicornHorn, 10);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxUndergroundHallow, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.AshBlock, 20);
                recipe.AddIngredient(ItemID.Hellstone, 15);
                recipe.AddIngredient(ItemID.ObsidianBrick, 10);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxHell, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.BlueBrick, 20);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxDungeon, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.GreenBrick, 20);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxDungeon, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.PinkBrick, 20);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxDungeon, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.TempleKey, 1);
                recipe.AddIngredient(ItemID.LihzahrdBrick, 30);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxTemple, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.ShadowScale, 15);
                recipe.AddIngredient(ItemID.DemoniteBar, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxBoss1, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.SoulofFright, 10);
                recipe.AddIngredient(ItemID.HallowedBar, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxBoss1, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.GuideVoodooDoll, 1);
                recipe.AddIngredient(null, "DevilSilk", 15);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxBoss2, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.SoulofSight, 10);
                recipe.AddIngredient(ItemID.HallowedBar, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxBoss2, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.TissueSample, 15);
                recipe.AddIngredient(ItemID.CrimtaneBar, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxBoss2, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.SoulofMight, 10);
                recipe.AddIngredient(ItemID.HallowedBar, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxBoss3, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.BeetleHusk, 8);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxBoss4, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.BeeWax, 20);
                recipe.AddIngredient(ItemID.BottledHoney, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxBoss5, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.JungleSpores, 10);
                recipe.AddIngredient(null, "PlanteraPetal", 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxPlantera, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.Meteorite, 20);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxEerie, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.Shackle, 1);
                recipe.AddIngredient(ItemID.MoneyTrough, 1);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxEerie, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.LunarTabletFragment, 8);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxEclipse, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.GoblinBattleStandard, 1);
                recipe.AddIngredient(ItemID.SpikyBall, 30);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxGoblins, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.PirateMap, 1);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxPirates, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.MartianConduitPlating, 30);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxMartians, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.PumpkinMoonMedallion, 30);
                recipe.AddIngredient(ItemID.SpookyWood, 30);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxPumpkinMoon, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.NaughtyPresent, 1);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxFrostMoon, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.FragmentNebula, 3);
                recipe.AddIngredient(ItemID.FragmentSolar, 3);
                recipe.AddIngredient(ItemID.FragmentVortex, 3);
                recipe.AddIngredient(ItemID.FragmentStardust, 3);
                recipe.AddIngredient(ItemID.FallenStar, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxTowers, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.LunarOre, 30);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxLunarBoss, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.DefenderMedal, 15);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxDD2, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.Glass, 10);
                recipe.AddIngredient(ItemID.SnowBlock, 10);
                recipe.AddRecipeGroup("Wood");
                recipe.AddTile(TileID.GlassKiln);
                recipe.SetResult(ItemID.SnowGlobe, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.SnowGlobe, 1);
                recipe.AddIngredient(ItemID.SoulofFlight, 5);
                recipe.AddIngredient(ItemID.SoulofNight, 10);
                recipe.AddIngredient(ItemID.SoulofLight, 10);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(ItemID.GravityGlobe, 1);
                recipe.AddRecipe();
            }
        }

        public static Color BuffEffects(Entity codable, Color lightColor, float shadow = 0f, bool effects = true, bool poisoned = false, bool onFire = false, bool onFire2 = false, bool hunter = false, bool noItems = false, bool blind = false, bool bleed = false, bool venom = false, bool midas = false, bool ichor = false, bool onFrostBurn = false, bool burned = false, bool honey = false, bool dripping = false, bool drippingSlime = false, bool loveStruck = false, bool stinky = false)
        {
            float cr = 1f; float cg = 1f; float cb = 1f; float ca = 1f;
            if (effects && honey && Main.rand.Next(30) == 0)
            {
                int dustID = Dust.NewDust(codable.position, codable.width, codable.height, 152, 0f, 0f, 150, default(Color), 1f);
                Main.dust[dustID].velocity.Y = 0.3f;
                Main.dust[dustID].velocity.X *= 0.1f;
                Main.dust[dustID].scale += (float)Main.rand.Next(3, 4) * 0.1f;
                Main.dust[dustID].alpha = 100;
                Main.dust[dustID].noGravity = true;
                Main.dust[dustID].velocity += codable.velocity * 0.1f;
                if (codable is Player)
                {
                    Main.playerDrawDust.Add(dustID);
                }
            }
            if (poisoned)
            {
                if (effects && Main.rand.Next(30) == 0)
                {
                    int dustID = Dust.NewDust(codable.position, codable.width, codable.height, 46, 0f, 0f, 120, default(Color), 0.2f);
                    Main.dust[dustID].noGravity = true;
                    Main.dust[dustID].fadeIn = 1.9f;
                    if (codable is Player)
                    {
                        Main.playerDrawDust.Add(dustID);
                    }
                }
                cr *= 0.65f;
                cb *= 0.75f;
            }
            if (venom)
            {
                if (effects && Main.rand.Next(10) == 0)
                {
                    int dustID = Dust.NewDust(codable.position, codable.width, codable.height, 171, 0f, 0f, 100, default(Color), 0.5f);
                    Main.dust[dustID].noGravity = true;
                    Main.dust[dustID].fadeIn = 1.5f;
                    if (codable is Player)
                    {
                        Main.playerDrawDust.Add(dustID);
                    }
                }
                cg *= 0.45f;
                cr *= 0.75f;
            }
            if (midas)
            {
                cb *= 0.3f;
                cr *= 0.85f;
            }
            if (ichor)
            {
                if (codable is NPC)
                {
                    lightColor = new Color(255, 255, 0, 255);
                }
                else
                {
                    cb = 0f;
                }
            }
            if (burned)
            {
                if (effects)
                {
                    int dustID = Dust.NewDust(new Vector2(codable.position.X - 2f, codable.position.Y - 2f), codable.width + 4, codable.height + 4, 6, codable.velocity.X * 0.4f, codable.velocity.Y * 0.4f, 100, default(Color), 2f);
                    Main.dust[dustID].noGravity = true;
                    Main.dust[dustID].velocity *= 1.8f;
                    Main.dust[dustID].velocity.Y -= 0.75f;
                    if (codable is Player)
                    {
                        Main.playerDrawDust.Add(dustID);
                    }
                }
                if (codable is Player)
                {
                    cr = 1f;
                    cb *= 0.6f;
                    cg *= 0.7f;
                }
            }
            if (onFrostBurn)
            {
                if (effects)
                {
                    if (Main.rand.Next(4) < 3)
                    {
                        int dustID = Dust.NewDust(new Vector2(codable.position.X - 2f, codable.position.Y - 2f), codable.width + 4, codable.height + 4, 135, codable.velocity.X * 0.4f, codable.velocity.Y * 0.4f, 100, default(Color), 3.5f);
                        Main.dust[dustID].noGravity = true;
                        Main.dust[dustID].velocity *= 1.8f;
                        Main.dust[dustID].velocity.Y -= 0.5f;
                        if (Main.rand.Next(4) == 0)
                        {
                            Main.dust[dustID].noGravity = false;
                            Main.dust[dustID].scale *= 0.5f;
                        }
                        if (codable is Player)
                        {
                            Main.playerDrawDust.Add(dustID);
                        }
                    }
                    Lighting.AddLight((int)(codable.position.X / 16f), (int)(codable.position.Y / 16f + 1f), 0.1f, 0.6f, 1f);
                }
                if (codable is Player)
                {
                    cr *= 0.5f;
                    cg *= 0.7f;
                }
            }
            if (onFire)
            {
                if (effects)
                {
                    if (Main.rand.Next(4) != 0)
                    {
                        int dustID = Dust.NewDust(codable.position - new Vector2(2f, 2f), codable.width + 4, codable.height + 4, 6, codable.velocity.X * 0.4f, codable.velocity.Y * 0.4f, 100, default(Color), 3.5f);
                        Main.dust[dustID].noGravity = true;
                        Main.dust[dustID].velocity *= 1.8f;
                        Main.dust[dustID].velocity.Y -= 0.5f;
                        if (Main.rand.Next(4) == 0)
                        {
                            Main.dust[dustID].noGravity = false;
                            Main.dust[dustID].scale *= 0.5f;
                        }
                        if (codable is Player)
                        {
                            Main.playerDrawDust.Add(dustID);
                        }
                    }
                    Lighting.AddLight((int)(codable.position.X / 16f), (int)(codable.position.Y / 16f + 1f), 1f, 0.3f, 0.1f);
                }
                if (codable is Player)
                {
                    cb *= 0.6f;
                    cg *= 0.7f;
                }
            }
            if (dripping && shadow == 0f && Main.rand.Next(4) != 0)
            {
                Vector2 position = codable.position;
                position.X -= 2f; position.Y -= 2f;
                if (Main.rand.Next(2) == 0)
                {
                    int dustID = Dust.NewDust(position, codable.width + 4, codable.height + 2, 211, 0f, 0f, 50, default(Color), 0.8f);
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[dustID].alpha += 25;
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[dustID].alpha += 25;
                    }
                    Main.dust[dustID].noLight = true;
                    Main.dust[dustID].velocity *= 0.2f;
                    Main.dust[dustID].velocity.Y += 0.2f;
                    Main.dust[dustID].velocity += codable.velocity;
                    if (codable is Player)
                    {
                        Main.playerDrawDust.Add(dustID);
                    }
                }
                else
                {
                    int dustID = Dust.NewDust(position, codable.width + 8, codable.height + 8, 211, 0f, 0f, 50, default(Color), 1.1f);
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[dustID].alpha += 25;
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[dustID].alpha += 25;
                    }
                    Main.dust[dustID].noLight = true;
                    Main.dust[dustID].noGravity = true;
                    Main.dust[dustID].velocity *= 0.2f;
                    Main.dust[dustID].velocity.Y += 1f;
                    Main.dust[dustID].velocity += codable.velocity;
                    if (codable is Player)
                    {
                        Main.playerDrawDust.Add(dustID);
                    }
                }
            }
            if (drippingSlime && shadow == 0f)
            {
                int alpha = 175;
                Color newColor = new Color(0, 80, 255, 100);
                if (Main.rand.Next(4) != 0)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        Vector2 position2 = codable.position;
                        position2.X -= 2f; position2.Y -= 2f;
                        int dustID = Dust.NewDust(position2, codable.width + 4, codable.height + 2, 4, 0f, 0f, alpha, newColor, 1.4f);
                        if (Main.rand.Next(2) == 0)
                        {
                            Main.dust[dustID].alpha += 25;
                        }
                        if (Main.rand.Next(2) == 0)
                        {
                            Main.dust[dustID].alpha += 25;
                        }
                        Main.dust[dustID].noLight = true;
                        Main.dust[dustID].velocity *= 0.2f;
                        Main.dust[dustID].velocity.Y += 0.2f;
                        Main.dust[dustID].velocity += codable.velocity;
                        if (codable is Player)
                        {
                            Main.playerDrawDust.Add(dustID);
                        }
                    }
                }
                cr *= 0.8f;
                cg *= 0.8f;
            }
            if (onFire2)
            {
                if (effects)
                {
                    if (Main.rand.Next(4) != 0)
                    {
                        int dustID = Dust.NewDust(codable.position - new Vector2(2f, 2f), codable.width + 4, codable.height + 4, 75, codable.velocity.X * 0.4f, codable.velocity.Y * 0.4f, 100, default(Color), 3.5f);
                        Main.dust[dustID].noGravity = true;
                        Main.dust[dustID].velocity *= 1.8f;
                        Main.dust[dustID].velocity.Y -= 0.5f;
                        if (Main.rand.Next(4) == 0)
                        {
                            Main.dust[dustID].noGravity = false;
                            Main.dust[dustID].scale *= 0.5f;
                        }
                        if (codable is Player)
                        {
                            Main.playerDrawDust.Add(dustID);
                        }
                    }
                    Lighting.AddLight((int)(codable.position.X / 16f), (int)(codable.position.Y / 16f + 1f), 1f, 0.3f, 0.1f);
                }
                if (codable is Player)
                {
                    cb *= 0.6f;
                    cg *= 0.7f;
                }
            }
            if (noItems)
            {
                cr *= 0.65f;
                cg *= 0.8f;
            }
            if (blind)
            {
                cr *= 0.7f;
                cg *= 0.65f;
            }
            if (bleed)
            {
                bool dead = (codable is Player ? ((Player)codable).dead : codable is NPC ? ((NPC)codable).life <= 0 : false);
                if (effects && !dead && Main.rand.Next(30) == 0)
                {
                    int dustID = Dust.NewDust(codable.position, codable.width, codable.height, 5, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[dustID].velocity.Y += 0.5f;
                    Main.dust[dustID].velocity *= 0.25f;
                    if (codable is Player)
                    {
                        Main.playerDrawDust.Add(dustID);
                    }
                }
                cg *= 0.9f;
                cb *= 0.9f;
            }
            if (loveStruck && effects && shadow == 0f && Main.instance.IsActive && !Main.gamePaused && Main.rand.Next(5) == 0)
            {
                Vector2 value = new Vector2((float)Main.rand.Next(-10, 11), (float)Main.rand.Next(-10, 11));
                value.Normalize();
                value.X *= 0.66f;
                int goreID = Gore.NewGore(codable.position + new Vector2((float)Main.rand.Next(codable.width + 1), (float)Main.rand.Next(codable.height + 1)), value * (float)Main.rand.Next(3, 6) * 0.33f, 331, (float)Main.rand.Next(40, 121) * 0.01f);
                Main.gore[goreID].sticky = false;
                Main.gore[goreID].velocity *= 0.4f;
                Main.gore[goreID].velocity.Y -= 0.6f;
                if (codable is Player)
                {
                    Main.playerDrawGore.Add(goreID);
                }
            }
            if (stinky && shadow == 0f)
            {
                cr *= 0.7f;
                cb *= 0.55f;
                if (effects && Main.rand.Next(5) == 0 && Main.instance.IsActive && !Main.gamePaused)
                {
                    Vector2 value2 = new Vector2((float)Main.rand.Next(-10, 11), (float)Main.rand.Next(-10, 11));
                    value2.Normalize(); value2.X *= 0.66f; value2.Y = Math.Abs(value2.Y);
                    Vector2 vector = value2 * (float)Main.rand.Next(3, 5) * 0.25f;
                    int dustID = Dust.NewDust(codable.position, codable.width, codable.height, 188, vector.X, vector.Y * 0.5f, 100, default(Color), 1.5f);
                    Main.dust[dustID].velocity *= 0.1f;
                    Main.dust[dustID].velocity.Y -= 0.5f;
                    if (codable is Player)
                    {
                        Main.playerDrawDust.Add(dustID);
                    }
                }
            }
            lightColor.R = (byte)((float)lightColor.R * cr);
            lightColor.G = (byte)((float)lightColor.G * cg);
            lightColor.B = (byte)((float)lightColor.B * cb);
            lightColor.A = (byte)((float)lightColor.A * ca);
            if (codable is NPC)
            {
                NPCLoader.DrawEffects((NPC)codable, ref lightColor);
            }
            if (hunter && (codable is NPC ? ((NPC)codable).lifeMax > 1 : true))
            {
                if (effects && !Main.gamePaused && Main.instance.IsActive && Main.rand.Next(50) == 0)
                {
                    int dustID = Dust.NewDust(codable.position, codable.width, codable.height, 15, 0f, 0f, 150, default(Color), 0.8f);
                    Main.dust[dustID].velocity *= 0.1f;
                    Main.dust[dustID].noLight = true;
                    if (codable is Player)
                    {
                        Main.playerDrawDust.Add(dustID);
                    }
                }
                byte colorR = 50, colorG = 255, colorB = 50;
                if (codable is NPC && !(((NPC)codable).friendly || ((NPC)codable).catchItem > 0 || (((NPC)codable).damage == 0 && ((NPC)codable).lifeMax == 5)))
                {
                    colorR = 255; colorG = 50;
                }
                if (!(codable is NPC) && lightColor.R < 150)
                {
                    lightColor.A = Main.mouseTextColor;
                }
                if (lightColor.R < colorR)
                {
                    lightColor.R = colorR;
                }
                if (lightColor.G < colorG)
                {
                    lightColor.G = colorG;
                }
                if (lightColor.B < colorB)
                {
                    lightColor.B = colorB;
                }
            }
            return lightColor;
        }

        public static Color GetNPCColor(NPC npc, Vector2? position = null, bool effects = true, float shadowOverride = 0f)
        {
            return npc.GetAlpha(BuffEffects(npc, GetLightColor(position != null ? (Vector2)position : npc.Center), (shadowOverride != 0f ? shadowOverride : 0f), effects, npc.poisoned, npc.onFire, npc.onFire2, Main.player[Main.myPlayer].detectCreature, false, false, false, npc.venom, npc.midas, npc.ichor, npc.onFrostBurn, false, false, npc.dripping, npc.drippingSlime, npc.loveStruck, npc.stinky));
        }

        public static Color GetLightColor(Vector2 position)
        {
            return Lighting.GetColor((int)(position.X / 16f), (int)(position.Y / 16f));
        }

        public static void DrawTexture(object sb, Texture2D texture, int shader, Entity codable, Color? overrideColor = null, bool drawCentered = false)
        {
            Color lightColor = (overrideColor != null ? (Color)overrideColor : codable is NPC ? GetNPCColor(((NPC)codable), codable.Center, false) : codable is Projectile ? ((Projectile)codable).GetAlpha(GetLightColor(codable.Center)) : GetLightColor(codable.Center));
            int frameCount = (codable is NPC ? Main.npcFrameCount[((NPC)codable).type] : 1);
            Rectangle frame = (codable is NPC ? ((NPC)codable).frame : new Rectangle(0, 0, texture.Width, texture.Height));
            float scale = (codable is NPC ? ((NPC)codable).scale : ((Projectile)codable).scale);
            float rotation = (codable is NPC ? ((NPC)codable).rotation : ((Projectile)codable).rotation);
            int spriteDirection = (codable is NPC ? ((NPC)codable).spriteDirection : ((Projectile)codable).spriteDirection);
            float offsetY = (codable is NPC ? ((NPC)codable).gfxOffY : 0f);
            DrawTexture(sb, texture, shader, codable.position + new Vector2(0f, offsetY), codable.width, codable.height, scale, rotation, spriteDirection, frameCount, frame, lightColor, drawCentered);
        }

        public static void DrawTexture(object sb, Texture2D texture, int shader, Vector2 position, int width, int height, float scale, float rotation, int direction, int framecount, Rectangle frame, Color? overrideColor = null, bool drawCentered = false)
        {
            Vector2 origin = new Vector2((float)(texture.Width / 2), (float)(texture.Height / framecount / 2));
            Color lightColor = overrideColor != null ? (Color)overrideColor : GetLightColor(position + new Vector2(width * 0.5f, height * 0.5f));
            if (sb is List<DrawData>)
            {
                DrawData dd = new DrawData(texture, GetDrawPosition(position, origin, width, height, texture.Width, texture.Height, framecount, scale, drawCentered), frame, lightColor, rotation, origin, scale, direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0)
                {
                    shader = shader
                };
                ((List<DrawData>)sb).Add(dd);
            }
            else if (sb is SpriteBatch)
            {
                bool applyDye = shader > 0;
                if (applyDye)
                {
                    ((SpriteBatch)sb).End();
                    ((SpriteBatch)sb).Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    GameShaders.Armor.ApplySecondary(shader, Main.player[Main.myPlayer], null);
                }
                ((SpriteBatch)sb).Draw(texture, GetDrawPosition(position, origin, width, height, texture.Width, texture.Height, framecount, scale, drawCentered), frame, lightColor, rotation, origin, scale, direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
                if (applyDye)
                {
                    ((SpriteBatch)sb).End();
                    ((SpriteBatch)sb).Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                }
            }
        }

        public static Vector2 GetDrawPosition(Vector2 position, Vector2 origin, int width, int height, int texWidth, int texHeight, int framecount, float scale, bool drawCentered = false)
        {
            Vector2 screenPos = new Vector2((int)Main.screenPosition.X, (int)Main.screenPosition.Y);
            if (drawCentered)
            {
                Vector2 texHalf = new Vector2(texWidth / 2, texHeight / framecount / 2);
                return (position + new Vector2(width * 0.5f, height * 0.5f)) - (texHalf * scale) + (origin * scale) - screenPos;
            }
            return position - screenPos + new Vector2(width * 0.5f, height) - new Vector2(texWidth * scale / 2f, texHeight * scale / (float)framecount) + (origin * scale) + new Vector2(0f, 5f);
        }
    }
}