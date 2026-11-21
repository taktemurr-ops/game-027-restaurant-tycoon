import 'package:flutter/material.dart';

import 'package:studio_core/app/arcade_stage.dart';
import 'package:studio_core/app/game_api.dart';
import 'package:studio_core/core/game_rules.dart';
import 'tycoon_engine.dart';
import 'rules.dart';
import 'tycoon_view.dart';

const _theme = TycoonTheme(Color(0xFFFFE8D6), Color(0xFFCB997E), Color(0xFFE63946));



final gameConfig = GameConfig(
  gameId: 'game-027-restaurant-tycoon',
  title: const ['Restoran Patronu', 'Restaurant Tycoon', 'Restaurant Tycoon', 'Restaurant Tycoon', 'Restaurant Tycoon'],
  tagline: const ['Pişir, servis et, restoranını büyüt!', 'Cook, serve and grow your restaurant!', 'Cook, serve and grow your restaurant!', 'Cook, serve and grow your restaurant!', 'Cook, serve and grow your restaurant!'],
  seedColor: const Color(0xFFFFB703),
  icon: Icons.restaurant,
  rules: RestaurantRules(),
  buildPlay: (ctx) => ArcadeStage(
    ctx: ctx,
    create: () => TycoonSim((ctx.generated as GeneratedLevel<TycoonLevel>).puzzle),
    paint: (c, s, p, t) => paintTycoon(c, s, p, t, ctx.lang == 'tr', _theme),
    sound: tycoonSound,
    boosterIcon: Icons.bolt,
    boosterLabel: (l) => l == 'tr' ? 'Çılgın üretim' : 'Production frenzy',
    howTo: const [
      'Üretim ve personel kur; müşteriler sabırları bitmeden servis alsın.',
      'Build production and staff; serve customers before they leave.',
      'Baue Produktion und Personal; bediene Kunden rechtzeitig.',
      'Construye y contrata; atiende antes de que se vayan.',
      'Construisez et embauchez ; servez avant qu\'ils partent.',
    ],
  ),
  tutorial: const [
    TutorialStep(Icons.touch_app, [
      'Kartlara dokun: bina al. Sarı fiyat = alınabilir.',
      'Tap cards to buy. Yellow price = affordable.',
      'Karten antippen zum Kaufen.',
      'Toca las cartas para comprar.',
      'Touchez les cartes pour acheter.',
    ]),
    TutorialStep(Icons.pan_tool, [
      'Sahneye dokun: çalış, hasat et veya ilk müşteriye hemen servis yap.',
      'Tap the scene: work, harvest or serve the first customer.',
      'Szene antippen: arbeiten oder ernten.',
      'Toca la escena: trabajar o cosechar.',
      'Touchez la scène : travailler ou récolter.',
    ]),
    TutorialStep(Icons.bolt, [
      'Çılgın üretim güçlendirmesi 20 sn boyunca üretimi ikiye katlar.',
      'Production frenzy doubles output for 20 s.',
      'Produktionsrausch verdoppelt 20 s.',
      'Frenesí duplica la producción 20 s.',
      'Frénésie : production ×2 pendant 20 s.',
    ]),
  ],
);
