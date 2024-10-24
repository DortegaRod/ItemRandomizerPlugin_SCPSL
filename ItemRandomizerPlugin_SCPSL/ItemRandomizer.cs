﻿namespace ItemRandomizerPlugin {

    using Exiled.API.Features;
    using ItemRandomizerPlugin_SCPSL;
    using System;
    using Player = Exiled.Events.Handlers.Player;
    using Server = Exiled.Events.Handlers.Server;

    public class ItemRandomizer : Plugin<Config> {
        public PlayerHandler _playerHandler;
        public override string Name => "ItemRandomizerPlugin";
        public override string Prefix => "IRndPl";
        public override string Author => "Megador";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(8, 9, 11);

        public override void OnEnabled() {
            Log.Info("ItemRandomizerPlugin loaded successfully");
            _playerHandler = new PlayerHandler();
            Player.DroppingItem += _playerHandler.OnItemDropped;
            Player.FlippingCoin += _playerHandler.OnFlip;
            Server.RoundStarted += _playerHandler.CoinSpawn;
            Server.RoundEnded += _playerHandler.ClearCoinList;
            base.OnEnabled();
        }

        public override void OnDisabled() {
            Player.DroppingItem -= _playerHandler.OnItemDropped;
            Player.FlippingCoin -= _playerHandler.OnFlip;
            Server.RoundStarted -= _playerHandler.CoinSpawn;
            Server.RoundEnded -= _playerHandler.ClearCoinList;
            _playerHandler = null;
            base.OnDisabled();
        }
    }
}