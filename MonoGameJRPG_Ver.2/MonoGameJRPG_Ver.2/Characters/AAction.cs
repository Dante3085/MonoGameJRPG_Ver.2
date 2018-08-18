﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameJRPG_Ver._2.Characters
{
    public class AAction
    {
        #region MemberVariables
        private Dictionary<EActionMethod, Action<Character>> _actionMethods = new Dictionary<EActionMethod, Action<Character>>();

        private int _hpModifier;
        private int _mpModifier;
        private int _revengeModifier;

        /// <summary>
        /// Time in seconds this AAction needs during combat before executing.
        /// </summary>
        private int _time;
        private int _timeRemaining;
        private string _description;
        private EActionMethod[] _registeredActionMethods;
        #endregion

        private AAction(int hpModifier = 0, int mpModifier = 0, int revengeModifier = 0, int time = 1, string description = "DefaultDescription", EActionMethod[] registeredActionMethods = null)
        {
            _hpModifier = hpModifier;
            _mpModifier = mpModifier;
            _revengeModifier = revengeModifier;
            _time = time;
            _timeRemaining = time;
            _description = description;
            _registeredActionMethods = registeredActionMethods;

            // TODO: Methoden, die für jede Instanz dieser Klasse gleich sind werden mit jeder
            // neuen Instanz in immer wieder das gleiche Dictionary gepackt.
            SetUpActionMethods();
        }

        public void ExecuteAction(Character target)
        {
            _actionMethods[_registeredActionMethods[0]](target);
        }

        #region PredefinedItems
        /// <summary>
        /// Returns an Item with hpModifier = 100 (i.e. A HealthPotion that heals 100 healt/damage)
        /// </summary>
        /// <returns></returns>
        public static AAction HealthPotion()
        {
            return new AAction(hpModifier: 100, description: "Item: HealthPotion", registeredActionMethods: new EActionMethod[] { EActionMethod.Heal });
        }

        /// <summary>
        /// Returns a new Item with mpModifier = 100 (i.e. A ManaPotion that refills current mp by 100)
        /// </summary>
        /// <returns></returns>
        public static AAction ManaPotion()
        {
            return new AAction(mpModifier: 100, description: "Item: ManaPotion", registeredActionMethods: new EActionMethod[] { EActionMethod.RefillMana });
        }
        #endregion

        #region PredefinedMagics
        public static AAction Fire()
        {
            return new AAction(hpModifier: 300, mpModifier: 10, revengeModifier: 1, description: "Magic: Fire", registeredActionMethods: new EActionMethod[] { EActionMethod.DealDamage });
        }

        public static AAction Water()
        {
            return new AAction(hpModifier: 500, mpModifier: 3, revengeModifier: 2, description: "Magic: Water", registeredActionMethods: new EActionMethod[] { EActionMethod.DealDamage });
        }
        #endregion

        #region PredefinedPhysicalSkills
        public static AAction Cleave()
        {
            return new AAction(hpModifier: 200, revengeModifier: 10, description: "PhysicallSkill: Cleave", registeredActionMethods: new EActionMethod[] { EActionMethod.DealDamage });
        }
        #endregion

        #region PredefinedRevengeSkills
        public static AAction Counter()
        {
            return new AAction(hpModifier: 50, description: "RevengeSkill: Counter", registeredActionMethods: new EActionMethod[] { EActionMethod.DealDamage });
        }

        public static AAction Break()
        {
            return new AAction(hpModifier: 20, description: "RevengeSkill: Break", registeredActionMethods: new EActionMethod[] { EActionMethod.DealDamage });
        }
        #endregion

        #region ActionMethods
        private void SetUpActionMethods()
        {
            _actionMethods[EActionMethod.HalfHealth] = HalfHealth;
            _actionMethods[EActionMethod.DoubleHealth] = DoubleHealth;
            _actionMethods[EActionMethod.Death] = Death;
            _actionMethods[EActionMethod.Heal] = Heal;
            _actionMethods[EActionMethod.DealDamage] = DealDamage;
            _actionMethods[EActionMethod.RefillMana] = RefillMana;
        }

        /// <summary>
        /// Reduces the target's maxHp to half of it's current maxHp.
        /// </summary>
        /// <param name="target"></param>
        private void HalfHealth(Character target)
        {
            target.MaxHp = (int)(target.MaxHp * 0.5);
        }

        /// <summary>
        /// Increases the target's maxHp to double of it's current maxHp.
        /// </summary>
        /// <param name="target"></param>
        private void DoubleHealth(Character target)
        {
            target.MaxHp = target.MaxHp * 2;
        }

        /// <summary>
        /// Kills the target by setting it's currentHp to int.MinValue.
        /// </summary>
        /// <param name="target"></param>
        private void Death(Character target)
        {
            target.CurrentHp = int.MinValue;
        }

        /// <summary>
        /// Heals target Character by _hpModifier's amount.
        /// If target Character gets healed above max hp, current hp is set to max hp.
        /// </summary>
        /// <param name="target"></param>
        private void Heal(Character target)
        {
            target.CurrentHp += _hpModifier;

            if (target.CurrentHp > target.MaxHp)
                target.CurrentHp = target.MaxHp;
        }

        private void RefillMana(Character target)
        {
            target.CurrentMp += _mpModifier;

            if (target.CurrentMp > target.MaxMp)
                target.CurrentMp = target.MaxMp;
        }

        private void DealDamage(Character target)
        {
            // Don't do anything if target's defence nullifies damage.
            if (target.Defence >= _hpModifier)
                return;

            target.CurrentHp -= (_hpModifier - target.Defence);

            // Reset target's current hp to 0 if it went below 0.
            // Also set target's isAlive flag to false.
            if (target.CurrentHp <= 0)
            {
                target.CurrentHp = 0;
                target.IsAlive = false;
            }
        }
        #endregion

        public bool IsReady()
        {
            return _timeRemaining == 0;
        }

        public int TimeRemaining()
        {
            return _timeRemaining;
        }

        public void Update(GameTime gameTime)
        {
            _timeRemaining -= (int)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timeRemaining < 0)
                _timeRemaining = 0;
        }
    }
}
