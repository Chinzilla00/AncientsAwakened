using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    //imported from my tAPI mod because I'm lazy
    public class EnderStaffEX : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Conflagrate Scythe");
            Tooltip.SetDefault(@"Summons a spinning construct that shreds through enemies
Conflagrate Staff EX");

            Item.staff[item.type] = true;
        }

		public override void SetDefaults()
		{
			item.damage = 400;
			item.summon = true;
			item.mana = 20;
			item.width = 64;
			item.height = 64;
			item.useTime = 26;
			item.useAnimation = 26;
			item.useStyle = 1;
			item.noMelee = true;
			item.knockBack = 3;
			item.value = Item.sellPrice(0, 20, 0, 0);
            item.shoot = mod.ProjectileType("EnderMinionEX");
            item.buffType = mod.BuffType("EnderMinionBuffEX");
            item.rare = 8;
            item.expert = true;
			item.UseSound = SoundID.Item44;
			item.shootSpeed = 7f;	//The buff added to player after used the item
            item.buffTime = 18000;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = BaseAAItem.GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EnderStaff");
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            return player.altFunctionUse != 2;
        }

        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim();
            }
            return base.UseItem(player);
        }
    }
}
