using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class TitanAxeEX : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Titan Slayer");
            Tooltip.SetDefault(@"Left click to quickly swing the axe
Right click to throw the axe
Titan Axe EX");
		}

		public override void SetDefaults()
		{
            item.CloneDefaults(ItemID.Arkhalis);
            item.damage = 300;
            item.width = 94; 
            item.height = 96;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.channel = true;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.useTime = 20;
            item.knockBack = 4f;
            item.autoReuse = false;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.shoot = mod.ProjectileType("Surasshu");
            item.shootSpeed = 15f;
            item.expert = true; item.expertOnly = true;
            item.UseSound = SoundID.Item1;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2)
            {
                item.damage = 300;
                item.useStyle = 1;
                item.thrown = true;
                item.melee = false;
                item.shoot = mod.ProjectileType("TitanAxeEX");
            }
            else
            {
                item.damage = 450;
                item.useStyle = 5;
                item.melee = true;
                item.thrown = false;
                item.shoot = mod.ProjectileType("TitanEX");
            }
            return base.CanUseItem(player);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(mod.BuffType("Moonraze"), 600);
            target.AddBuff(BuffID.Daybreak, 600);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TitanAxe", 1);
            recipe.AddIngredient(null, "EXSoul", 1);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}