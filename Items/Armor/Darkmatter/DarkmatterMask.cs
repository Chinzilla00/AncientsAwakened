using Microsoft.Xna.Framework.Graphics;
using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;

namespace AAMod.Items.Armor.Darkmatter
{
    [AutoloadEquip(EquipType.Head)]
	public class DarkmatterMask : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Darkmatter Mask");
			Tooltip.SetDefault(@"10% increased magic damage
15% decreased mana usage
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
			item.defense = 20;
            item.rare = ItemRarityID.Cyan;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity12;
                }
            }
        }

        public override void UpdateEquip(Player player)
		{
			player.magicDamage += 0.10f;
            player.manaCost *= .85f;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("DarkmatterBreastplate") && legs.type == mod.ItemType("DarkmatterGreaves");
		}

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.DarkmatterMaskBonus1") + (int)(100 * player.magicDamage) + " " + Language.GetTextValue("Mods.AAMod.Common.DarkmatterMaskBonus2") + player.magicCrit + Language.GetTextValue("Mods.AAMod.Common.DarkmatterMaskBonus3");
            player.GetModPlayer<DarkmatterMaskEffects>().setBonus = true;
            player.GetModPlayer<DarkmatterMaskEffects>().sunSiphon = false;
            player.armorEffectDrawShadowLokis = true;
            
            for (int i = 0; i < 15; i++)
            {
                Vector2 offset = new Vector2();
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                offset.X += (float)(Math.Sin(angle) * 300);
                offset.Y += (float)(Math.Cos(angle) * 300);
                Dust dust = Main.dust[Dust.NewDust(player.Center + offset - new Vector2(4, 4), 0, 0,  mod.DustType("DarkmatterDust"), 0, 0, 100, default, 1f)];
                dust.velocity = player.velocity;
                dust.noGravity = true;
            }
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