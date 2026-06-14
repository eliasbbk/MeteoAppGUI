# Météo App - Interface graphique

## Contexte
Application graphique développée pour me familiariser avec WinForms et la programmation événementielle en C#, en complément du projet MeteoApp (version console).

## Fonctionnalités
- Interface graphique avec champ de saisie et bouton de recherche
- Récupère la météo en temps réel via l'API wttr.in
- Affiche la température, l'humidité et les conditions météo
- Traduction des conditions météo en français

## Technologies
- C# (.NET) - WinForms
- Appels API REST (HttpClient, async/await)
- Désérialisation JSON (System.Text.Json)
- Programmation événementielle
- Dictionnaires (Dictionary<string, string>) pour la traduction

## Lancer le projet
```bash
dotnet run
```

## Pistes d'amélioration
- Gestion des erreurs (ville invalide, pas de connexion)
- Icônes météo selon les conditions
- Amélioration globale de l'interface