import '../core/arcade.dart';
import '../core/missions.dart';
import '../core/stats.dart';
import 'restaurant_spec.dart';
import 'tycoon_rules.dart';

class RestaurantRules extends TycoonRules {
  RestaurantRules()
      : super(restaurantSpec, 'game-027-restaurant-tycoon', const [
          GameStat('customers_served', 'Doyan müşteri', 'Guests served', scale: 200),
          GameStat('money_earned', 'Kasa geliri', 'Revenue', scale: 5000),
          GameStat('buildings_bought', 'Yatırım', 'Investments', scale: 40),
        ], const [
          MissionTemplate('customers_served', 'customers_served', [66, 133, 200]),
          MissionTemplate('money_earned', 'money_earned', [1666, 3333, 5000]),
        ]);
}

ArcadeRules createRules() => RestaurantRules();
