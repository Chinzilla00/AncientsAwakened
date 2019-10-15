using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System.Collections.Generic;
using System;

namespace AAMod.Items.Armor.Darkmatter
{
    [AutoloadEquip(EquipType.Head)]
	public class DarkmatterHelmet : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Darkmatter Helmet");
			Tooltip.SetDefault(@"10% increased melee damage
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
			item.width = 22;
			item.height = 20;
			item.value = 300000;
			item.rare = 11;
			item.defense = 34;
		}
		
		public override void UpdateEquip(Player player)
		{
			player.meleeDamage += 0.10f;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("DarkmatterBreastplate") && legs.type == mod.ItemType("DarkmatterGreaves");
		}

		public override void UpdateArmorSet(Player player)
		{
            const float effectRange = 500;
			player.setBonus = "You and nearby allies recieve 40% less damage";
            for(int p =0; p < Main.player.Length; p++)
            {
                if(Main.player[p].active && (Main.player[p].Center - player.Center).Length() < effectRange && player.team == Main.player[p].team)
                {
                    Main.player[p].GetModPlayer<HelmetEffects>().ShieldTime = 2;
                    Main.player[p].GetModPlayer<HelmetEffects>().badShield = false;
                }
            }
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
    public class HelmetEffects : ModPlayer
    {
        public int ShieldTime = 0;
        public float yetAnotherTrigCounter = 0;
        public bool badShield = false;
        
        public override void ResetEffects()
        {
            if(ShieldTime>0)
            {
                ShieldTime--;
            }
            
        }
        public override void PreUpdate()
        {
            yetAnotherTrigCounter += (float)Math.PI / 60;
        }
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (ShieldTime>0)
            {
                if(badShield)
                {
                    damage = (int)(damage * 1.4f);
                }
                else
                {
                    damage = (int)(damage * .6f);
                }
                
            }
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }
        public static readonly PlayerLayer drawShield = new PlayerLayer("AAMod", "drawShield", PlayerLayer.MiscEffectsFront, delegate (PlayerDrawInfo drawInfo)
        {

            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("AAMod");
            Texture2D texture = mod.GetTexture("Items/Armor/Darkmatter/DarkmatterShield");
            if(drawPlayer.GetModPlayer<HelmetEffects>().badShield)
            {
                texture = mod.GetTexture("Items/Armor/Radium/RadiumShield");
            }
            if (drawPlayer.GetModPlayer<HelmetEffects>().ShieldTime>0)
            {
                Vector2 Center = drawInfo.position + new Vector2(drawPlayer.width / 2, 0) + Vector2.UnitY*-30 - Main.screenPosition;

                DrawData data = new DrawData(texture, Center, null, Color.White, 0f, texture.Size() * .5f, 1f + (.1f * (float)Math.Sin(drawPlayer.GetModPlayer<HelmetEffects>().yetAnotherTrigCounter)), SpriteEffects.None, 0);
                data.shader = drawInfo.bodyArmorShader;
                Main.playerDrawData.Add(data);
            }
        });
        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {


            int frontLayer = layers.FindIndex(PlayerLayer => PlayerLayer.Name.Equals("MiscEffectsFront"));
            if (frontLayer != -1)
            {
                drawShield.visible = true;
                layers.Insert(frontLayer + 1, drawShield);
            }
        }

    }
    
}