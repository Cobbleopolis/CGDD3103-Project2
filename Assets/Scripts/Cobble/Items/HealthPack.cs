﻿using Cobble.Lib;
using UnityEngine;

namespace Cobble.Items {
    public class HealthPack : Item {
        private const string ItemIdValue = "healthPack";
        private const string NameValue = "Health Pack";

        public override string ItemId {
            get { return ItemIdValue; }
        }

        public override string Name {
            get { return NameValue; }
        }

        public override void UseItem(GameObject usingGameObject) {
            
        }
    }
}