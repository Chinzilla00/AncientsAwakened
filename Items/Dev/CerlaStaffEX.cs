using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class CerlaStaffEX : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Gardener's Blessing");
            Tooltip.SetDefault(@"Summons a Cerla Blossom that fires rapid petal bolts at enemies
Only one Cerla Blossom may be active at a time
Takes up a Minion Slot
The Lifegiver's Gift EX");
        }

		public override void SetDefaults()
		{
			item.damage = 200;
			item.summon = true;
			item.mana = 20;
			item.width = 64;
			item.height = 64;
			item.useTime = 26;
			item.useAnimation = 26;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.noMelee = true;
			item.knockBack = 3;
			item.value = Item.sellPrice(0, 20, 0, 0);
            item.shoot = mod.ProjectileType("CerlaFlowerEX");
            item.buffType = mod.BuffType("CerlaFlowerEX");
            item.rare = ItemRarityID.Yellow;
            item.expert = true; item.expertOnly = true;
			item.UseSound = SoundID.Item44;
            item.shootSpeed = 7f;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow";
            glowmaskDrawType = GLOWMASKTYPE_SWORD;	
            glowmaskDrawColor = Color.White;
        }
		
		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(item.buffType, 3600, true);
			}
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.ownedProjectileCounts[mod.ProjectileType("CerlaFlower")] > 0 || player.ownedProjectileCounts[mod.ProjectileType("CerlaFlowerEX")] > 0)
			{
				return false;
			}
			return true;
		}

		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CerlaStaff");
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
