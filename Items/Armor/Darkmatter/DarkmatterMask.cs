using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System.Collections.Generic;

namespace AAMod.Items.Armor.Darkmatter
{
    [AutoloadEquip(EquipType.Head)]
	public class DarkmatterMask : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Darkmatter Mask");
			Tooltip.SetDefault(@"9% increased magic damage
Dark, yet still barely visible");

		}

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override void SetDefaults()
		{
			item.width = 20;
			item.height = 18;
			item.value = 300000;
			item.rare = 11;
			item.defense = 20;
		}
		
		public override void UpdateEquip(Player player)
		{
			player.magicDamage += 0.09f;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("DarkmatterBreastplate") && legs.type == mod.ItemType("DarkmatterGreaves");
		}

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = "Damage nearby enemies \n 2% of the damage dealt will heal you\n"+ (int)(100 * player.magicDamage) + " Magic Damage\n" + (player.magicCrit) +"% critical strike chance";
            player.GetModPlayer<DarkmatterMaskEffects>().setBonus = true;
            player.GetModPlayer<DarkmatterMaskEffects>().sunSiphon = false;
            player.armorEffectDrawShadowLokis = true;
        }

		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DarkMatter", 25);
            recipe.AddIngredient(null, "DarkEnergy", 10);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
    public class DarkmatterMaskEffects : ModPlayer
    {
        public bool setBonus = false;
        public int[] npcCooldown = new int[Main.npc.Length];
        public bool sunSiphon = false;
        public override void ResetEffects()
        {
            setBonus = false;
            
        }
        public override void PreUpdate()
        {
            if(setBonus)
            {
                for (int n = 0; n < Main.npc.Length; n++)
                {
                    if (npcCooldown[n] > 0)
                    {
                        npcCooldown[n]--;
                    }
                    if (Main.npc[n].CanBeChasedBy() && npcCooldown[n] == 0 && (Main.npc[n].Center - player.Center).Length() < 300)
                    {
                        
                        npcCooldown[n] = 30;
                        int type = mod.ProjectileType("DarkLeech");
                        if (sunSiphon)
                        {
                            type = mod.ProjectileType("SunSiphon");
                        }
                        
                        Projectile.NewProjectile(Main.npc[n].Center, Vector2.Zero, type, (int)(100f * player.magicDamage), 0f, player.whoAmI, n);
                    }
                }
            }
            
        }
        
    }
    
}