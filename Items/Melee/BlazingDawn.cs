using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class BlazingDawn : BaseAAItem
    {
        
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Blazing Dawn");
            Tooltip.SetDefault("The Radiant Dawn calls");
        }
		public override void SetDefaults()
		{
			item.damage = 50;
			item.melee = true;
			item.width = 62;
			item.height = 62;
			item.useTime = 26;
			item.useAnimation = 26;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 5;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = ItemRarityID.Orange;
			item.UseSound = SoundID.Item20;
			item.autoReuse = false;

            glowmaskTexture = "Glowmasks/" + GetType().Name; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if(Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                dust = Main.dust[Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Dusts.AshRain>(), 0f, 0f, 46, default, 1.381579f)];
                dust.noGravity = true;
            }
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "FlamingFury", 1);
			recipe.AddIngredient(mod, "OceanRazor", 1);
            recipe.AddIngredient(mod, "DoomiteSaber", 1);
            recipe.AddIngredient(mod, "DesertScimitar", 1);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 400);
        }
	}
}
