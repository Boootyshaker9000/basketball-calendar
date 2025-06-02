from flask import Flask, jsonify, request
from flask_cors import CORS
from balldontlie import BalldontlieAPI
from datetime import datetime, timedelta

app = Flask(__name__)
CORS(app)  # Povolení CORS pro přístup z .NET aplikace

api_key = "b6d32a45-8c63-47d3-ab00-2d91f01282eb"
api = BalldontlieAPI(api_key=api_key)

@app.route('/nba_games/<date>', methods=['GET'])
def get_nba_games(date):
    try:
        search_date = datetime.strptime(date, "%Y-%m-%d")
        response = api.nba.games.list(dates=[search_date.strftime("%Y-%m-%d")])
        games = response.data

        # Pokud nejsou nalezeny žádné zápasy pro dané datum, zkusíme vyhledat i historická data
        if not games and search_date < datetime.now():
            # Zkusíme najít nejbližší den v minulosti, kdy byly nějaké zápasy
            test_date = search_date - timedelta(days=1)
            while test_date >= (datetime.now() - timedelta(days=30)):
                history_response = api.nba.games.list(dates=[test_date.strftime("%Y-%m-%d")])
                if history_response.data:
                    print(f"Nalezeny historické zápasy z {test_date.strftime('%Y-%m-%d')}")
                    response = history_response
                    games = response.data
                    break
                test_date -= timedelta(days=1)

        game_list = []
        for game in games:
            # Získáme skutečné datum a čas zápasu z API odpovědi
            game_datetime = None
            try:
                if hasattr(game, 'date') and game.date:
                    game_datetime = datetime.strptime(str(game.date), "%Y-%m-%dT%H:%M:%S.%fZ")
            except ValueError:
                # Fallback pokud datum nemá správný formát
                game_datetime = search_date

            # Pokud se nepodařilo získat datum, použijeme vyhledávané datum
            if not game_datetime:
                game_datetime = search_date

            # Vytvoříme objekt zápasu pro .NET aplikaci
            game_list.append({
                "Id": game.id,
                "HomeTeam": game.home_team.full_name,
                "AwayTeam": game.visitor_team.full_name,
                "HomeScore": game.home_team_score,
                "AwayScore": game.visitor_team_score,
                "GameDate": game_datetime.strftime("%Y-%m-%d %H:%M"),
                "Status": game.status
            })

        print(f"Pro datum {date} nalezeno {len(game_list)} zápasů")
        return jsonify(game_list), 200
    except Exception as exception:
        print(f"Chyba při získávání dat: {exception}")
        return jsonify({"error": str(exception)}), 500

@app.route('/status', methods=['GET'])
def status():
    return jsonify({"status": "running"}), 200

# Spustíme server s podporou pro přístup z .NET klienta
if __name__ == '__main__':
    print("Spouštění NBA API serveru na portu 5000...")
    app.run(host='0.0.0.0', debug=True, port=5000)