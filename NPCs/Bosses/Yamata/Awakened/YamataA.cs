using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using BaseMod;

namespace AAMod.NPCs.Bosses.Yamata.Awakened
{
    //[AutoloadBossHead]

    public class YamataA : Yamata
	{
		bool cheated = false;
		bool Panic = false;
        private bool tenthHealth = false;
        private bool threeQuarterHealth = false;
        private bool HalfHealth = false;

        public override void SetStaticDefaults()
        {
			base.SetStaticDefaults();
            displayName = "Yamata Awakened";
            Main.npcFrameCount[npc.type] = 7;			
        }	

        public override void SetDefaults()
        {
			base.SetDefaults();
			isAwakened = true;
			
            if (!AAWorld.downedYamata)
            {
                npc.lifeMax = 140000;
                if (npc.life > npc.lifeMax / 3)
                {
                    npc.damage = 80;
                    npc.defense = 80;
                }
                if (npc.life <= npc.lifeMax / 3)
                {
                    npc.damage = 100;
                    npc.defense = 90;
                }
            }
            if (AAWorld.downedYamata)
            {
                npc.lifeMax = 150000;
                if (npc.life > npc.lifeMax / 3)
                {
                    npc.damage = 90;
                    npc.defense = 80;
                }
                if (npc.life <= npc.lifeMax / 3)
                {
                    npc.damage = 110;
                    npc.defense = 90;
                }
            }
            if (Main.expertMode)
            {
                npc.value = Item.buyPrice(20, 0, 0, 0);
            }
			music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Yamata2");		
            bossBag = mod.ItemType("YamataBag");
            if (Main.expertMode)
            {
                int playerCount = 0;
                float bossHPScalar = 1f, scalarIncrement = 0.35f;
                if (Main.netMode != 0)
                {
                    for (int i = 0; i < 255; i++)
                    {
                        if (Main.player[i].active)
                        {
                            playerCount++;
                        }
                    }
                    for (int j = 1; j < playerCount; j++)
                    {
                        bossHPScalar += scalarIncrement;
                        scalarIncrement += (1f - scalarIncrement) / 3f;
                    }
                }
                ScaleExpertStats(playerCount, bossHPScalar);
            }
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (damage > npc.lifeMax / 2)
            {
                cheated = true;
                Main.NewText("CHEATER CHEATER PUMPKIN EATER! THAT HURT YOU KNOW!!!", new Color(146, 30, 68));
            }
            return false;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            if (Main.expertMode)
            {
                potionType = ItemID.SuperHealingPotion;   //boss drops
                AAWorld.downedYamata = true;
            }
            if (!AAWorld.downedYamata && !cheated)
            {
                Main.NewText("NO…! IMPOSSIBLE! EVEN IN MY AWAKENED FORM?! YOU MUST HAVE CHEATED! GYAAAAAAH..! FINE! TAKE YOUR LOOT AND GO AWAY..!", new Color(146, 30, 68));
            }
            if (AAWorld.downedYamata && !cheated)
            {
                Main.NewText("NOOOOOOOOOOOOOO!!! YOU LITTLE BRAT!!! I ALMOST HAD YOU THIS TIME!!! FINE, take your stuff, See if I care!", new Color(146, 30, 68));
            }
            if (!Main.expertMode && !cheated)
            {
                Main.NewText("HAH! Nice try! Come back in Expert mode when you don’t have to cheat to beat me! All your loot is MINE still!", new Color(146, 30, 68));
            }
        }

        public override void NPCLoot()
        {
            if (Main.expertMode)
            {
                BaseAI.DropItem(npc, mod.ItemType("YamataTrophy"), 1, 1, 15, true);
                npc.DropBossBags();
                AAWorld.downedYamata = true;
                if (Main.rand.NextFloat() < 0.1f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EXSoul"));
                }
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            int dust1 = mod.DustType<Dusts.YamataADust>();
            int dust2 = mod.DustType<Dusts.YamataADust>();
            if (npc.life <= 0)
            {
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust2, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;

            }
            if (!AAWorld.downedYamata)
            {
                if (npc.life <= ((npc.lifeMax / 4) * 3) && threeQuarterHealth == false)
                {
                    Main.NewText("YOU'RE STILL FIGHTING?! WHAT THE HELL IS WRONG WITH YOU?!", new Color(146, 30, 68));
                    threeQuarterHealth = true;
                }
                if (npc.life <= npc.lifeMax / 2 && HalfHealth == false)
                {
                    Main.NewText("I'VE HAD IT UP TO HERE WITH YOUR SHENANIGANS!!! EAT VENOM YOU LITTLE HELLSPAWN!!!", new Color(146, 30, 68));
                    HalfHealth = true;
                }
                if (npc.life <= npc.lifeMax / 10 && tenthHealth == false)
                {
                    Main.NewText("STOP IT STOP IT STOP IT!!! I'M NOT GONNA LET YOU WIN!!!", new Color(146, 30, 68));
                    tenthHealth = true;
                }
            }
            if (AAWorld.downedYamata)
            {
                if (npc.life <= ((npc.lifeMax / 4) * 3) && threeQuarterHealth == false)
                {
                    Main.NewText("You're an annoying little bugger, you know!", new Color(146, 30, 68));
                    threeQuarterHealth = true;
                }
                if (npc.life <= npc.lifeMax / 2 && HalfHealth == false)
                {
                    Main.NewText("DIE! WHY WON'T YOU JUST DIE ALREADY?!", new Color(146, 30, 68));
                    HalfHealth = true;
                }
                if (npc.life <= npc.lifeMax / 10 && tenthHealth == false)
                {
                    Main.NewText("STOP IT!!! I'M NOT GONNA LOSE AGAIN!!!", new Color(146, 30, 68));
                    tenthHealth = true;
                }
            }
            if (npc.life > npc.lifeMax / 3)
            {
                Panic = false;
            }
            if (npc.life <= npc.lifeMax / 3 && Panic == false && !AAWorld.downedYamata && npc.type == mod.NPCType<YamataA>())
            {
                Panic = true;
                Main.NewText("Wh-WHA?! DIE! DIE YOU LITTLE TWERP! DIEDIEDIEDIEDIEDIEDIE!!!!", new Color(146, 30, 68));
            }
            if (npc.life <= npc.lifeMax / 3 && Panic == false && AAWorld.downedYamata && npc.type == mod.NPCType<YamataA>())
            {
                Panic = true;
                Main.NewText("NO NO NO!!! NOT AGAIN!!! THIS TIME IMMA STOMP YOU RIGHT INTO THE GROUND!!!", new Color(146, 30, 68));
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter < 5)
            {
                npc.frame.Y = 0 * frameHeight;
            }
            else if (npc.frameCounter < 10)
            {
                npc.frame.Y = 1 * frameHeight;
            }
            else if (npc.frameCounter < 15)
            {
                npc.frame.Y = 2 * frameHeight;
            }
            else if (npc.frameCounter < 20)
            {
                npc.frame.Y = 3 * frameHeight;
            }
            else if (npc.frameCounter < 25)
            {
                npc.frame.Y = 4 * frameHeight;
            }
            else if (npc.frameCounter < 30)
            {
                npc.frame.Y = 5 * frameHeight;
            }
            else if (npc.frameCounter < 35)
            {
                npc.frame.Y = 6 * frameHeight;
            }else
            {
                npc.frameCounter = 0;
            }
        }	
	}
}