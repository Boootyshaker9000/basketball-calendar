# This is a sample Python script.

# Press Shift+F10 to execute it or replace it with your code.
# Press Double Shift to search everywhere for classes, files, tool windows, actions, and settings.


def print_hi(name):
    # Use a breakpoint in the code line below to debug your script.
    print(f'Hi, {name}')  # Press Ctrl+F8 to toggle the breakpoint.


# Press the green button in the gutter to run the script.
if __name__ == '__main__':
    print_hi('PyCharm')

    from balldontlie import BalldontlieAPI
    from datetime import datetime, timedelta

    # Inicializace s vaším API klíčem
    api_key = "b6d32a45-8c63-47d3-ab00-2d91f01282eb"
    api = BalldontlieAPI(api_key=api_key)

    # Použijeme aktuální datum místo budoucího
    today = datetime.now()
    # Můžeme jít o několik dní zpět, aby byla větší šance najít zápasy
    search_date = (today - timedelta(days=7)).strftime("%Y-%m-%d")
    
    print(f"Hledám zápasy pro datum: {search_date}")
    response = api.nba.games.list(dates=[search_date])
    
    games = response.data
    
    if not games:
        print(f"Pro datum {search_date} nebyly nalezeny žádné zápasy.")
    else:
        print(f"\nNalezeno {len(games)} zápasů:")
        for game in games:
            print(f"{game.home_team.full_name} vs {game.visitor_team.full_name}")
            print(f"Status: {game.status}, Home Score: {game.home_team_score}, "
                  f"Visitor Score: {game.visitor_team_score}")
            print("-" * 50)

# See PyCharm help at https://www.jetbrains.com/help/pycharm/