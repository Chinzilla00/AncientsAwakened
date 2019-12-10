using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;


namespace AAMod.Items.Armor.StripeMan
{
    [AutoloadEquip(EquipType.Head)]
	public class StripeManHat : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("StripeMan Lucky Hat");
			Tooltip.SetDefault(@"Provides light when worn
Get the effect of Architect Gizmo Pack
When digging stones, you may get ore from them
You can put any sand into the Extractinator");

		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 16;
			item.rare = -1;
            item.value = Item.sellPrice(0, 0, 0, 1);
            item.defense = 1;
		}
		
		public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<AAPlayer>().StripeManOre = true;
			
			player.autoPaint = true;
			player.pickSpeed -= 1f;
			player.tileSpeed += 3f;
			player.wallSpeed += 3f;
			if (player.whoAmI == Main.myPlayer)
			{
				Player.tileRangeX += 6;
				Player.tileRangeY += 4;
			}

			Vector2 vector = new Vector2(player.width / 2 + 8 * player.direction, 2f);
			if (player.fullRotation != 0f)
			{
				vector = vector.RotatedBy(player.fullRotation, player.fullRotationOrigin);
			}
			int i = (int)(player.position.X + vector.X) / 16;
			int j = (int)(player.position.Y + vector.Y) / 16;
			Lighting.AddLight(i, j, 0.92f, 0.8f, 0.65f);
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("StripeManShirt") && legs.type == mod.ItemType("StripeManPants");
		}

		public override void UpdateArmorSet(Player player)
		{
			string active = "";
			if(player.GetModPlayer<AAPlayer>().StripeCrasyLucky)
			{
				active = Language.GetTextValue("Mods.AAMod.Common.StripeManSetBonusactive");
			}
			else
			{
				active = Language.GetTextValue("Mods.AAMod.Common.StripeManSetBonusunactive");
			}
			
            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.StripeManSetBonus1") + active  + "\n" + Language.GetTextValue("Mods.AAMod.Common.StripeManSetBonus2");

			player.GetModPlayer<AAPlayer>().StripeManSet = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MiningHelmet, 1);
			recipe.AddIngredient(ItemID.MiningShirt, 1);
			recipe.AddIngredient(ItemID.MiningPants, 1);
			recipe.AddIngredient(ItemID.BonePickaxe, 1);
			recipe.AddIngredient(ItemID.ArchitectGizmoPack, 1);
			recipe.AddIngredient(null, "LuckyCracker", 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}