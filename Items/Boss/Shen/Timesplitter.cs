using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Shen
{
    public class Timesplitter : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Timesplitter");
            Tooltip.SetDefault(@"It has been said that this spear was used to divide time into day and night
Inflicts Daybroken and Moonraze");
        }

        public override void SetDefaults()
        {
            item.damage = 265;
            item.melee = true;
            item.width = 96;
            item.height = 96;
            item.scale = 1.1f;
            item.useTime = 16;
            item.useAnimation = 16;
            item.knockBack = 4.7f;
            item.UseSound = SoundID.Item20;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useTurn = true;
			item.autoReuse = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.value = Item.sellPrice(1, 50, 0, 0);
            item.rare = ItemRarityID.Cyan;
            item.shoot = mod.ProjectileType("TimesplitterP");  //put your Spear projectile name
            item.shootSpeed = 9f;
            AARarity = 14;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Globals.AAColor.Rarity14;
                }
            }
        }

        public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Discordium", 5);
            recipe.AddIngredient(null, "ChaosScale", 5);
            recipe.AddIngredient(null, "AbyssalYari");
			recipe.AddIngredient(null, "SunSpear");
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
