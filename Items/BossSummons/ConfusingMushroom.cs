using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.BossSummons
{
    public class ConfusingMushroom : BaseAAItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Confusing Looking Mushroom");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            Tooltip.SetDefault(@"Summons the Feudal Fungus
Can only be used in a glowing mushroom biome");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 22;
            item.maxStack = 20;
            item.value = 1000;
            item.rare = ItemRarityID.Blue;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.consumable = true;
        }

        public override bool UseItem(Player player)
        {
            Globals.AAModGlobalNPC.SpawnBoss(player, mod.NPCType("FeudalFungus"), true, 0, 0, Language.GetTextValue("Mods.AAMod.Common.FeudalFungus"), false);
            Main.PlaySound(SoundID.Roar, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.ZoneGlowshroom)
            {
                return true;
            }
            if (!player.ZoneGlowshroom)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ConfusingMushroomFalse1"), Color.SkyBlue, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.FeudalFungus.FeudalFungus>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ConfusingMushroomFalse2"), Color.SkyBlue, false);
                return false;
            }
            return false;
        }

        public void SpawnBoss(Player player, string name, string displayName)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int bossType = mod.NPCType(name);
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-2000, 2000, (float)Main.rand.NextDouble()), 1200f);
                Main.npc[npcID].netUpdate2 = true;
            }
        }

        public override void UseStyle(Player p) { BaseUseStyle.SetStyleBoss(p, item, true, true); }
        public override bool UseItemFrame(Player p) { BaseUseStyle.SetFrameBoss(p, item); return true; }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GlowingMushroom, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}