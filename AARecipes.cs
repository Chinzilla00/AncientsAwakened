using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using AAMod.Items.Currency;

namespace AAMod
{
    internal class AARecipes
    {
        private static ModRecipe GetNewRecipe()
        {
            return new ModRecipe(AAMod.instance);
        }

        public static void AddRecipes()
        {
            RemoveNightsEdgeRecipe();
            AddMusicBoxRecipes();
            AddPotionRecipes();
            AddMushroomPotionRecipes();
            AddModdedMushroomPotionRecipes();
            AddTransmuterRecipes();

            #region Materials
            ModRecipe recipe = GetNewRecipe();
            recipe.AddIngredient(null, "HallowedOre", 4);
            recipe.AddTile(null, "HallowedForge");
            recipe.SetResult(ItemID.HallowedBar, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(null, "MushiumBar", 1);
            recipe.AddIngredient(ItemID.GlowingMushroom, 5);
            recipe.AddTile(TileID.Autohammer);
            recipe.SetResult(ItemID.ShroomiteBar, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(null, "MushroomBlock");
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ItemID.Mushroom, 3);
            recipe.AddRecipe();
            #endregion

            #region Equipment
            recipe = GetNewRecipe();
            recipe.AddIngredient(null, "TrueFleshrendClaymore", 1);
            recipe.AddIngredient(ItemID.TrueExcalibur, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ItemID.TerraBlade, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.IceBlock, 30);
            recipe.AddIngredient(ItemID.Diamond, 1);
            recipe.AddIngredient(ItemID.Sapphire, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.IceBlade);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.PlatinumBroadsword);
            recipe.AddIngredient(ItemID.FallenStar, 10);
            recipe.AddIngredient(ItemID.ManaCrystal);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.Starfury);
            recipe.AddRecipe();
            
            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.GoldBroadsword);
            recipe.AddIngredient(ItemID.FallenStar, 10);
            recipe.AddIngredient(ItemID.ManaCrystal);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.Starfury);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.PlatinumBroadsword);
            recipe.AddIngredient(ItemID.ManaCrystal, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.EnchantedSword);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.GoldBroadsword);
            recipe.AddIngredient(ItemID.ManaCrystal, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.EnchantedSword);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.EnchantedSword);
            recipe.AddIngredient(ItemID.Muramasa);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.Arkhalis);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.CobaltBar, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.CobaltShield);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(null, "PalladiumShield");
            recipe.AddIngredient(ItemID.ObsidianSkull);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(ItemID.ObsidianShield);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.SnowGlobe, 1);
            recipe.AddIngredient(ItemID.SoulofFlight, 5);
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ItemID.GravityGlobe, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.Glass, 10);
            recipe.AddIngredient(ItemID.RecallPotion, 10);
            recipe.AddTile(TileID.GlassKiln);
            recipe.SetResult(ItemID.MagicMirror);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.IceBrick, 10);
            recipe.AddIngredient(ItemID.RecallPotion, 10);
            recipe.AddTile(TileID.IceMachine);
            recipe.SetResult(ItemID.IceMirror);
            recipe.AddRecipe();
            #endregion

            #region Miscellaneous
            recipe = GetNewRecipe();
            recipe.AddIngredient(null, "DevilSilk", 5);
            recipe.AddIngredient(ItemID.Hay, 5);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(ItemID.GuideVoodooDoll, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.Glass, 10);
            recipe.AddIngredient(ItemID.SnowBlock, 10);
            recipe.AddRecipeGroup("Wood");
            recipe.AddTile(TileID.GlassKiln);
            recipe.SetResult(ItemID.SnowGlobe, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.SoulofLight, 60);
            recipe.AddIngredient(ItemID.Pearlwood, 5);
            recipe.AddIngredient(ItemID.CrystalShard, 30);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ItemID.RodofDiscord);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(null, "RadiumBar", 1);
            recipe.AddIngredient(ItemID.LunarOre, 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(ItemID.FragmentNebula);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(null, "RadiumBar", 1);
            recipe.AddIngredient(ItemID.LunarOre, 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(ItemID.FragmentSolar);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(null, "DarkMatter", 1);
            recipe.AddIngredient(ItemID.LunarOre, 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(ItemID.FragmentStardust);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(null, "DarkMatter", 1);
            recipe.AddIngredient(ItemID.LunarOre, 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(ItemID.FragmentVortex);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.EmptyBucket, 1);
            recipe.AddTile(ModContent.TileType<Tiles.Furniture.Razewood.RazewoodSink>());
            recipe.SetResult(ItemID.LavaBucket);
            recipe.AddRecipe();
            #endregion
        }

        private static void RemoveNightsEdgeRecipe()
        {
            RecipeFinder finder = new RecipeFinder();
            {
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
        }

        private static void AddMusicBoxRecipes()
        {
            // Music Box
            ModRecipe recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.Wood, 30);
            recipe.AddIngredient(ItemID.IronBar, 10);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBox, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.Wood, 30);
            recipe.AddIngredient(ItemID.LeadBar, 10);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBox, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.GrassSeeds, 10);
            recipe.AddIngredient(ItemID.DirtBlock, 10);
            recipe.AddIngredient(ItemID.Wood, 10);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxOverworldDay, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.GrassSeeds, 10);
            recipe.AddIngredient(ItemID.DirtBlock, 10);
            recipe.AddIngredient(ItemID.Wood, 10);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxAltOverworldDay, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.Lens, 3);
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxNight, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.BottledWater, 5);
            recipe.AddIngredient(ItemID.UmbrellaHat, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxRain, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.SnowBlock, 30);
            recipe.AddIngredient(ItemID.BorealWood, 30);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxSnow, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.IceBlock, 30);
            recipe.AddIngredient(ItemID.BorealWood, 30);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxIce, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.SandBlock, 40);
            recipe.AddIngredient(ItemID.Cactus, 15);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxDesert, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 1);
            recipe.AddIngredient(ItemID.SharkFin, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxSandstorm, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.Coral, 3);
            recipe.AddIngredient(ItemID.Starfish, 3);
            recipe.AddIngredient(ItemID.Seashell, 3);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxOcean, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.DirtBlock, 50);
            recipe.AddIngredient(ItemID.IronOre, 10);
            recipe.AddIngredient(ItemID.StoneBlock, 50);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxUnderground, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.DirtBlock, 50);
            recipe.AddIngredient(ItemID.LeadOre, 10);
            recipe.AddIngredient(ItemID.StoneBlock, 50);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxUnderground, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.DirtBlock, 50);
            recipe.AddIngredient(ItemID.LeadOre, 10);
            recipe.AddIngredient(ItemID.StoneBlock, 50);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxAltUnderground, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.DirtBlock, 50);
            recipe.AddIngredient(ItemID.IronOre, 10);
            recipe.AddIngredient(ItemID.StoneBlock, 50);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxAltUnderground, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.Feather, 20);
            recipe.AddIngredient(ItemID.SunplateBlock, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxSpace, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.GlowingMushroom, 20);
            recipe.AddIngredient(ItemID.Mushroom, 10);
            recipe.AddIngredient(ItemID.MushroomGrassSeeds, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxMushrooms, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.MudBlock, 20);
            recipe.AddIngredient(ItemID.JungleGrassSeeds, 5);
            recipe.AddIngredient(ItemID.RichMahogany, 30);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxJungle, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.RottenChunk, 10);
            recipe.AddIngredient(ItemID.CorruptSeeds, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxCorruption, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.EbonstoneBlock, 30);
            recipe.AddIngredient(ItemID.RottenChunk, 10);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxUndergroundCorruption, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.Vertebrae, 10);
            recipe.AddIngredient(ItemID.CrimsonSeeds, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxCrimson, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.CrimstoneBlock, 30);
            recipe.AddIngredient(ItemID.Vertebrae, 10);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxUndergroundCrimson, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.CrystalShard, 10);
            recipe.AddIngredient(ItemID.HallowedSeeds, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxTheHallow, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.PearlstoneBlock, 30);
            recipe.AddIngredient(ItemID.UnicornHorn, 10);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxUndergroundHallow, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.AshBlock, 20);
            recipe.AddIngredient(ItemID.Hellstone, 15);
            recipe.AddIngredient(ItemID.ObsidianBrick, 10);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxHell, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.BlueBrick, 20);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxDungeon, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.GreenBrick, 20);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxDungeon, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.PinkBrick, 20);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxDungeon, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.TempleKey, 1);
            recipe.AddIngredient(ItemID.LihzahrdBrick, 30);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxTemple, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.ShadowScale, 15);
            recipe.AddIngredient(ItemID.DemoniteBar, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxBoss1, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.SoulofFright, 10);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxBoss1, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.GuideVoodooDoll, 1);
            recipe.AddIngredient(null, "DevilSilk", 15);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxBoss2, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.SoulofSight, 10);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxBoss2, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.TissueSample, 15);
            recipe.AddIngredient(ItemID.CrimtaneBar, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxBoss2, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.SoulofMight, 10);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxBoss3, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.BeetleHusk, 8);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxBoss4, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.BeeWax, 20);
            recipe.AddIngredient(ItemID.BottledHoney, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxBoss5, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.JungleSpores, 10);
            recipe.AddIngredient(null, "PlanteraPetal", 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxPlantera, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.Meteorite, 20);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxEerie, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.Shackle, 1);
            recipe.AddIngredient(ItemID.MoneyTrough, 1);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxEerie, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.LunarTabletFragment, 8);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxEclipse, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.GoblinBattleStandard, 1);
            recipe.AddIngredient(ItemID.SpikyBall, 30);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxGoblins, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.PirateMap, 1);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxPirates, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.MartianConduitPlating, 30);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxMartians, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.PumpkinMoonMedallion, 30);
            recipe.AddIngredient(ItemID.SpookyWood, 30);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxPumpkinMoon, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.NaughtyPresent, 1);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxFrostMoon, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.FragmentNebula, 3);
            recipe.AddIngredient(ItemID.FragmentSolar, 3);
            recipe.AddIngredient(ItemID.FragmentVortex, 3);
            recipe.AddIngredient(ItemID.FragmentStardust, 3);
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxTowers, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.LunarOre, 30);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxLunarBoss, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.DefenderMedal, 15);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(ItemID.MusicBoxDD2, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddRecipeGroup("AAMod:DevBag");
            recipe.SetResult(null, "AncientCoin", 5);
            recipe.AddRecipe();
        }

        #region Potions
        private static void AddPotionRecipes()
        {
            ModRecipe recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Deathweed, 1);
            recipe.AddIngredient(null, "DragonClaw", 3);
            recipe.AddIngredient(null, "DragonScale", 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(ItemID.RagePotion, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Deathweed, 1);
            recipe.AddIngredient(null, "HydraClaw", 3);
            recipe.AddIngredient(null, "MirePod", 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(ItemID.WrathPotion, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Deathweed, 1);
            recipe.AddIngredient(null, "MirePod", 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(ItemID.BattlePotion, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Deathweed, 1);
            recipe.AddIngredient(null, "MirePod", 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(ItemID.BattlePotion, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Waterleaf, 1);
            recipe.AddIngredient(null, "MirePod", 2);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(ItemID.WaterWalkingPotion, 1);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Waterleaf, 1);
            recipe.AddIngredient(null, "DragonScale", 2);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(ItemID.ObsidianSkinPotion, 1);
            recipe.AddRecipe();
        }

        private static void AddMushroomPotionRecipes()
        {
            // Potion created, required mushrooms, amount of potions created
            List<Tuple<short, string[], int>> potions = new List<Tuple<short, string[], int>>()
            {
                // Blue
                Tuple.Create(ItemID.CalmingPotion, new string[] { "Blue" }, 2),
                Tuple.Create(ItemID.FeatherfallPotion, new string[] { "Blue" }, 2),
                Tuple.Create(ItemID.FlipperPotion, new string[] { "Blue" }, 2),
                Tuple.Create(ItemID.GillsPotion, new string[] { "Blue" }, 2),
                Tuple.Create(ItemID.InvisibilityPotion, new string[] { "Blue" }, 2),
                Tuple.Create(ItemID.RecallPotion, new string[] { "Blue" }, 4),
                Tuple.Create(ItemID.WaterWalkingPotion, new string[] { "Blue" }, 2),
                Tuple.Create(ItemID.WormholePotion, new string[] { "Blue" }, 2),

                // Brown
                Tuple.Create(ItemID.BuilderPotion, new string[] { "Brown"}, 2),
                Tuple.Create(ItemID.CratePotion, new string[] { "Brown" }, 2),

                // Green
                Tuple.Create(ItemID.FishingPotion, new string[] { "Green" }, 2),
                Tuple.Create(ItemID.NightOwlPotion, new string[] { "Green" }, 2),
                Tuple.Create(ItemID.SonarPotion, new string[] { "Green" }, 2),
                Tuple.Create(ItemID.SummoningPotion, new string[] { "Green" }, 2),
                Tuple.Create(ItemID.SwiftnessPotion, new string[] { "Green" }, 2),
                Tuple.Create(ItemID.ThornsPotion, new string[] { "Green" }, 2),
                Tuple.Create(ItemID.TitanPotion, new string[] { "Green" }, 2),

                // Gray
                Tuple.Create(ItemID.AmmoReservationPotion, new string[] { "Gray" } , 2),
                Tuple.Create(ItemID.EndurancePotion, new string[] { "Gray" }, 2),
                Tuple.Create(ItemID.MiningPotion, new string[] { "Gray" }, 2),

                // Orange
                Tuple.Create(ItemID.ArcheryPotion, new string[] { "Orange" }, 2),
                Tuple.Create(ItemID.HunterPotion, new string[] { "Orange" }, 2),
                Tuple.Create(ItemID.TrapsightPotion, new string[] { "Orange" }, 2),

                // Pink
                Tuple.Create(ItemID.HeartreachPotion, new string[] { "Pink" }, 2),
                Tuple.Create(ItemID.ManaRegenerationPotion, new string[] { "Pink" }, 2),
                Tuple.Create(ItemID.RegenerationPotion, new string[] { "Pink" }, 2),

                // Purple
                Tuple.Create(ItemID.BattlePotion, new string[] { "Purple" }, 2),
                Tuple.Create(ItemID.GravitationPotion, new string[] { "Purple" }, 2),
                Tuple.Create(ItemID.MagicPowerPotion, new string[] { "Purple" }, 2),
                Tuple.Create(ItemID.ObsidianSkinPotion, new string[] { "Purple" }, 2),
                Tuple.Create(ItemID.TeleportationPotion, new string[] { "Purple" }, 2),

                // Red
                Tuple.Create(ItemID.InfernoPotion, new string[] { "Red" }, 1),
                Tuple.Create(ItemID.LifeforcePotion, new string[] { "Red" }, 1),
                Tuple.Create(ItemID.RagePotion, new string[] { "Red" }, 2),
                Tuple.Create(ItemID.WrathPotion, new string[] { "Red" }, 2),

                // Yellow
                Tuple.Create(ItemID.IronskinPotion, new string[] { "Yellow" }, 2),
                Tuple.Create(ItemID.ShinePotion, new string[] { "Yellow" }, 2),
                Tuple.Create(ItemID.SpelunkerPotion, new string[] { "Yellow" }, 2),
                Tuple.Create(ItemID.WarmthPotion, new string[] { "Yellow" }, 2),
                
                // Multiple
                Tuple.Create(ItemID.GenderChangePotion, new string[] { "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Gray", "Brown", "Pink" }, 2)
            };
            ModRecipe recipe;

            foreach (Tuple<short, string[], int> potion in potions)
            {
                recipe = GetNewRecipe();
                foreach (var mushroom in potion.Item2)
                {
                    recipe.AddIngredient(null, mushroom);
                }
                recipe.AddIngredient(ItemID.BottledWater);
                recipe.AddTile(TileID.Bottles);
                recipe.SetResult(potion.Item1, potion.Item3);
                recipe.AddRecipe();

                // Rainbow recipes
                recipe = GetNewRecipe();
                recipe.AddIngredient(null, "Rainbow");
                recipe.AddIngredient(ItemID.BottledWater);
                recipe.AddTile(TileID.Bottles);
                recipe.SetResult(potion.Item1);
                recipe.AddRecipe();
            }
        }

        private static void AddModdedMushroomPotionRecipes()
        {
            #region GRealm
            Mod GRealm = ModLoader.GetMod("Grealm");

            if (GRealm != null)
            {
                // Potion created, mushrooms required, amount of mushrooms required
                List<Tuple<string, string[], int>> GRealmPotions = new List<Tuple<string, string[], int>>()
                {
                    Tuple.Create("ChitinPotion", new string[] { "Brown" }, 1),
                    Tuple.Create("CosmicChitinPotion", new string[] { "Brown" }, 2),
                    Tuple.Create("CosmicEndurancePotion", new string[] { "Gray" }, 2),
                    Tuple.Create("CosmicSummoningPotion", new string[] { "Green" }, 2),
                    Tuple.Create("CosmicArcheryPotion", new string[] { "Orange" }, 2),
                    Tuple.Create("CosmicRegenerationPotion", new string[] { "Pink" }, 2),
                    Tuple.Create("CosmicMagicPowerPotion", new string[] { "Purple" }, 2),
                    Tuple.Create("BloodbathPotion", new string[] { "Red" }, 1),
                    Tuple.Create("CosmicRagePotion", new string[] { "Red" }, 2),
                    Tuple.Create("CosmicWrathPotion", new string[] { "Red" }, 2)
                };
                ModRecipe recipe;

                foreach (Tuple<string, string[], int> potion in GRealmPotions)
                {
                    recipe = GetNewRecipe();
                    foreach (var mushroom in potion.Item2)
                    {
                        recipe.AddIngredient(null, mushroom, potion.Item3);
                    }
                    if (potion.Item1 == "BloodbathPotion" || potion.Item1 == "ChitinPotion")
                    {
                        recipe.AddIngredient(ItemID.BottledWater);
                    }
                    else
                    {
                        recipe.AddIngredient(GRealm, "CosmicContainer");
                    }
                    recipe.AddTile(TileID.Bottles);
                    recipe.SetResult(GRealm, potion.Item1);
                    recipe.AddRecipe();

                    // Rainbow recipes
                    recipe = GetNewRecipe();
                    recipe.AddIngredient(null, "Rainbow");
                    if (potion.Item1 == "BloodbathPotion" || potion.Item1 == "ChitinPotion")
                    {
                        recipe.AddIngredient(ItemID.BottledWater);
                    }
                    else
                    {
                        recipe.AddIngredient(GRealm, "CosmicContainer");
                    }
                    recipe.AddTile(TileID.Bottles);
                    recipe.SetResult(GRealm, potion.Item1);
                    recipe.AddRecipe();
                }
            }
            #endregion
        }
        #endregion

        #region Transmuter
        private static void AddTransmuterRecipes()
        {
            #region Biomes
            TransmuteRecipe(ItemID.Ebonwood, ItemID.Shadewood);
            TransmuteRecipe(ItemID.EbonstoneBlock, ItemID.CrimstoneBlock);
            TransmuteRecipe(ItemID.DemoniteBar, ItemID.CrimtaneBar);
            TransmuteRecipe(ItemID.ShadowScale, ItemID.TissueSample);
            TransmuteRecipe(ItemID.VileMushroom, ItemID.ViciousMushroom);
            TransmuteRecipe(ItemID.CursedFlame, ItemID.Ichor);
            TransmuteRecipe(ItemID.CorruptionKey, ItemID.CrimsonKey);

            TransmuteRecipe(ItemID.SoulofNight, ItemID.SoulofLight);

            TransmuteRecipe((short)AAMod.instance.ItemType("BroodScale"), (short)AAMod.instance.ItemType("HydraHide"));
            TransmuteRecipe((short)AAMod.instance.ItemType("Hotshroom"), (short)AAMod.instance.ItemType("Darkshroom"));
            TransmuteRecipe((short)AAMod.instance.ItemType("DragonFire"), (short)AAMod.instance.ItemType("HydraToxin"));
            TransmuteRecipe((short)AAMod.instance.ItemType("SoulOfSmite"), (short)AAMod.instance.ItemType("SoulOfSpite"));
            TransmuteRecipe((short)AAMod.instance.ItemType("InfernoKey"), (short)AAMod.instance.ItemType("MireKey"));
            #endregion

            #region Bars
            TransmuteRecipe(ItemID.CopperBar, ItemID.TinBar);
            TransmuteRecipe(ItemID.LeadBar, ItemID.IronBar);
            TransmuteRecipe(ItemID.SilverBar, ItemID.TungstenBar);
            TransmuteRecipe(ItemID.GoldBar, ItemID.PlatinumBar);
            TransmuteRecipe(ItemID.CobaltBar, ItemID.PalladiumBar);
            TransmuteRecipe(ItemID.MythrilBar, ItemID.OrichalcumBar);
            TransmuteRecipe(ItemID.AdamantiteBar, ItemID.TitaniumBar);

            TransmuteRecipe((short)AAMod.instance.ItemType("AbyssiumBar"), (short)AAMod.instance.ItemType("IncineriteBar"));
            TransmuteRecipe((short)AAMod.instance.ItemType("DeepAbyssium"), (short)AAMod.instance.ItemType("RadiantIncinerite"));
            TransmuteRecipe((short)AAMod.instance.ItemType("DaybreakIncinerite"), (short)AAMod.instance.ItemType("EventideAbyssium"));
            #endregion

            #region Ores
            TransmuteRecipe(ItemID.CopperOre, ItemID.TinOre);
            TransmuteRecipe(ItemID.LeadOre, ItemID.IronOre);
            TransmuteRecipe(ItemID.SilverOre, ItemID.TungstenOre);
            TransmuteRecipe(ItemID.GoldOre, ItemID.PlatinumOre);
            TransmuteRecipe(ItemID.DemoniteOre, ItemID.CrimtaneOre);
            TransmuteRecipe(ItemID.CobaltOre, ItemID.PalladiumOre);
            TransmuteRecipe(ItemID.MythrilOre, ItemID.OrichalcumOre);
            TransmuteRecipe(ItemID.TitaniumOre, ItemID.AdamantiteOre);

            TransmuteRecipe((short)AAMod.instance.ItemType("Abyssium"), (short)AAMod.instance.ItemType("Incinerite"));
            #endregion
        }

        private static void TransmuteRecipe(short item, short item2)
        { 
            ModRecipe recipe = GetNewRecipe();
            recipe.AddIngredient(item, 2);
            recipe.AddTile(AAMod.instance, "Transmuter");
            recipe.SetResult(item2);
            recipe.AddRecipe();

            recipe = GetNewRecipe();
            recipe.AddIngredient(item2, 2);
            recipe.AddTile(AAMod.instance, "Transmuter");
            recipe.SetResult(item);
            recipe.AddRecipe();
        }
        #endregion

        public static void AddRecipeGroups()
        {
            RecipeGroup group0 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.darkmatterhelmet"), new int[]
            {
                AAMod.instance.ItemType("DarkmatterVisor"),
                AAMod.instance.ItemType("DarkmatterHelm"),
                AAMod.instance.ItemType("DarkmatterHelmet"),
                AAMod.instance.ItemType("DarkmatterHeaddress"),
                AAMod.instance.ItemType("DarkmatterMask")
            });
            RecipeGroup.RegisterGroup("AAMod:DarkmatterHelmets", group0);

            RecipeGroup group1 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.radiumhelmet"), new int[]
            {
                AAMod.instance.ItemType("RadiumHat"),
                AAMod.instance.ItemType("RadiumHelm"),
                AAMod.instance.ItemType("RadiumHelmet"),
                AAMod.instance.ItemType("RadiumHeadgear"),
                AAMod.instance.ItemType("RadiumMask")
            });
            RecipeGroup.RegisterGroup("AAMod:RadiumHelmets", group1);

            RecipeGroup group2 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAMod.Common.goldbar"), new int[]
            {
                ItemID.GoldBar,
                ItemID.PlatinumBar
            });
            RecipeGroup.RegisterGroup("AAMod:Gold", group2);
           
            RecipeGroup group3 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.celestialcraftingstation"), new int[]
            {
                AAMod.instance.ItemType("RadiantArcanum"),
                AAMod.instance.ItemType("QuantumFusionAccelerator"),
            });
            RecipeGroup.RegisterGroup("AAMod:AstralStations", group3);

            RecipeGroup group4 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.ancientmaterial"), new int[]
            {
                AAMod.instance.ItemType("UnstableSingularity"),
                AAMod.instance.ItemType("CrucibleScale"),
                AAMod.instance.ItemType("DreadScale")
            });
            RecipeGroup.RegisterGroup("AAMod:AncientMaterials", group4);

            RecipeGroup group5 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.superancientmaterial"), new int[]
            {
                AAMod.instance.ItemType("ChaosScale")
            });
            RecipeGroup.RegisterGroup("AAMod:SuperAncientMaterials", group5);
            
            RecipeGroup group6 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.worldevilmaterial"), new int[]
            {
                ItemID.Ichor,
                ItemID.CursedFlame
            });
            RecipeGroup.RegisterGroup("AnyIchor", group6);
            
            RecipeGroup group7 = new RecipeGroup(getName: () => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.hardmodeforge"), validItems: new int[]
            {
                ItemID.AdamantiteForge,
                ItemID.TitaniumForge
            });
            RecipeGroup.RegisterGroup("AnyHardmodeForge", group7);

            RecipeGroup group8 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.chaosclaw"), new int[]
            {
                AAMod.instance.ItemType("DragonClaw"),
                AAMod.instance.ItemType("HydraClaw")
            });
            RecipeGroup.RegisterGroup("AAMod:ChaosClaw", group8);

            RecipeGroup group9 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.ironbar"), new int[]
            {
                ItemID.IronBar,
                ItemID.LeadBar
            });
            RecipeGroup.RegisterGroup("AAMod:Iron", group9);

            RecipeGroup group10 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.copperbar"), new int[]
            {
                ItemID.CopperBar,
                ItemID.TinBar
            });
            RecipeGroup.RegisterGroup("AAMod:Copper", group10);

            RecipeGroup group11 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.silverbar"), new int[]
            {
                ItemID.SilverBar,
                ItemID.TungstenBar
            });
            RecipeGroup.RegisterGroup("AAMod:Silver", group11);

            RecipeGroup group12 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.evilbar"), new int[]
            {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar
            });
            RecipeGroup.RegisterGroup("AAMod:EvilBar", group12);

            RecipeGroup group13 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.chaosbar"), new int[]
            {
                AAMod.instance.ItemType("IncineriteBar"),
                AAMod.instance.ItemType("AbyssiumBar")
            });
            RecipeGroup.RegisterGroup("AAMod:ChaosBar", group13);

            RecipeGroup group14 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.EvilorChaosBar"), new int[]
            {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar,
                AAMod.instance.ItemType("IncineriteBar"),
                AAMod.instance.ItemType("AbyssiumBar")
            });
            RecipeGroup.RegisterGroup("AAMod:EvilorChaosBar", group14);

            RecipeGroup group15 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.ancientcraftingstation"), new int[]
            {
                AAMod.instance.ItemType("BinaryReassembler"),
                AAMod.instance.ItemType("ChaosCrucible")
            });
            RecipeGroup.RegisterGroup("AAMod:ACS", group15);

            RecipeGroup group16 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.evilsummonstaff"), new int[]
            {
                ModContent.ItemType<Items.Summoning.EaterStaff>(),
                ModContent.ItemType<Items.Summoning.CrimsonStaff>()
            });
            RecipeGroup.RegisterGroup("AAMod:EvilStaff", group16);

            RecipeGroup group17 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.altar"), new int[]
            {
                AAMod.instance.ItemType("MireAltar"),
                AAMod.instance.ItemType("CrimsonAltar"),
                AAMod.instance.ItemType("CorruptAltar"),
                AAMod.instance.ItemType("InfernoAltar")
            });
            RecipeGroup.RegisterGroup("AAMod:Altar", group17);

            RecipeGroup group18 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.ChaosLeggings"), new int[]
            {
                AAMod.instance.ItemType("BlazingSuneate"),
                AAMod.instance.ItemType("AbyssalHakama"),
                AAMod.instance.ItemType("AtlanteanGreaves"),
                AAMod.instance.ItemType("DoomiteGreaves"),
                AAMod.instance.ItemType("RaiderLegs"),
                AAMod.instance.ItemType("DynaskullGreaves")
            });
            RecipeGroup.RegisterGroup("AAMod:ChaosBoots", group18);

            RecipeGroup group19 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.ChaosChestpiece"), new int[]
            {
                AAMod.instance.ItemType("BlazingDou"),
                AAMod.instance.ItemType("AbyssalGi"),
                AAMod.instance.ItemType("AtlanteanPlate"),
                AAMod.instance.ItemType("DoomiteBreastplate"),
                AAMod.instance.ItemType("RaiderChest"),
                AAMod.instance.ItemType("DynaskullRibguard")
            });
            RecipeGroup.RegisterGroup("AAMod:ChaosPlates", group19);

            RecipeGroup group20 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.hardmodeanvil"), new int[]
            {
                ItemID.MythrilAnvil, ItemID.OrichalcumAnvil
            });
            RecipeGroup.RegisterGroup("AAMod:HAnvil", group20);

            RecipeGroup group21 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.hardmodeforge"), new int[]
            {
                ItemID.AdamantiteForge, ItemID.TitaniumForge
            });
            RecipeGroup.RegisterGroup("AAMod:HForge", group21);

            RecipeGroup group22 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAMod.Common.ShinyCharm"), new int[]
            {
                AAMod.instance.ItemType("ShinyCharm"),
                AAMod.instance.ItemType("ShinyCharmFish")
            });
            RecipeGroup.RegisterGroup("AAMod:ShinyCharm", group22);

            RecipeGroup group23 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAMod.Common.DevBag"), new int[]
            {
                AAMod.instance.ItemType("AlphaBag"),
                AAMod.instance.ItemType("InvokerBag"),
                AAMod.instance.ItemType("CCBox"),
                AAMod.instance.ItemType("BlazenBag"),
                AAMod.instance.ItemType("AvesBag"),
                AAMod.instance.ItemType("DellyBag"),
                AAMod.instance.ItemType("OldMagiciansHat"),
                AAMod.instance.ItemType("MagiciansHat"),
                AAMod.instance.ItemType("LizBag"),
                AAMod.instance.ItemType("FezLordsBag")
            });
            RecipeGroup.RegisterGroup("AAMod:DevBag", group23);

            if (RecipeGroup.recipeGroupIDs.ContainsKey("Wood"))
            {
                int index = RecipeGroup.recipeGroupIDs["Wood"];
                RecipeGroup.recipeGroups[index].ValidItems.Add(AAMod.instance.ItemType("Razewood"));
                RecipeGroup.recipeGroups[index].ValidItems.Add(AAMod.instance.ItemType("Bogwood"));
                RecipeGroup.recipeGroups[index].ValidItems.Add(AAMod.instance.ItemType("OroborosWood"));
            }
        }
    }
}
