using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;
using BaseMod;
using Terraria.Localization;

namespace AAMod.Items.BossSummons
{
	//imported from my tAPI mod because I'm lazy
	public class SubzeroCrystal : ModItem
	{
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Subzero Crystal");
            Tooltip.SetDefault(@"Summons the Subzero Serpent
Only usable at night");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 24;
			item.maxStack = 20;
			item.rare = 2;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
			item.useTime = 45;
			item.useStyle = 4;
			item.UseSound = SoundID.Item44;
			item.consumable = true;
		}

        public override bool UseItem(Player player)
        {
            AAModGlobalNPC.SpawnBoss(mod, player, "Serpent");
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (Main.dayTime )
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The crystal just sits there, melting in the sun", Color.Cyan.R, Color.Cyan.G, Color.Cyan.B, false);
                return false;
            }
            if (!player.ZoneSnow)
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The crystal shows an image of the nearby ice biome inside of it", Color.Cyan.R, Color.Cyan.G, Color.Cyan.B, false);
                return false;
            }
            if (NPC.AnyNPCs(mod.NPCType("Serpent")))
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The Subzero Serpent continues to attack", Color.Cyan.R, Color.Cyan.G, Color.Cyan.B, false);
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "SnowMana", 3);
                recipe.AddIngredient(ItemID.IceBlock, 30);
                recipe.AddTile(TileID.IceMachine);
                recipe.SetResult(this, 1);
                recipe.AddRecipe();
            }
        }
	}
}