using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AAMod.Items.BossSummons
{
    public class CursedSkull : BaseAAItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cursed Skull");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            Tooltip.SetDefault(@"Summons Skeletron
Can only be used at night");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 22;
            item.maxStack = 20;
            item.value = 1000;
            item.rare = ItemRarityID.Green;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.consumable = true;
        }

        public override bool UseItem(Player player)
        {
            AAModGlobalNPC.SpawnBoss(player, NPCID.SkeletronHead, true, 0, 0, Language.GetTextValue("Mods.AAMod.Common.Skeletron"), false);
            Main.PlaySound(SoundID.Roar, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (!Main.dayTime && !NPC.AnyNPCs(NPCID.SkeletronHead))
            {
                return true;
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bone, 50);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}