# Mock-Tests-Controller

## Description

**Mocking et Tests Unitaires**  
Ce projet vise à implémenter des tests unitaires avec une couverture complète du code, en utilisant des mocks pour simuler les services et les contrôleurs. Il est conçu pour aider à tester des contrôleurs en gérant des dépendances comme la propriété `UserId` à l'aide de mocks.

## Étapes

1. Mettre en place le projet sur GitHub.
2. Générer des tests unitaires pour le projet.
3. Ajouter des tests pour assurer une couverture de code complète (voir la section sur la couverture de code: https://info.cegepmontpetit.ca/5W5-Web-Avancee/info/testsUnitaires#la-couverture-de-code).
4. Utiliser des mocks pour simuler les services et les contrôleurs, en particulier pour contrôler des propriétés comme `UserId` dans les contrôleurs.
   - Exemple de mock pour `CatsService` et `CatsController` :
     ```csharp
     Mock<CatsService> serviceMock = new Mock<CatsService>();
     Mock<CatsController> controller = new Mock<CatsController>(serviceMock.Object) { CallBase = true };
     ```
5. Dans chaque test, vérifier que le type de l'objet retourné est correct et que les valeurs retournées sont celles attendues (si applicable).

## Prérequis

- .NET (version spécifique si nécessaire)
- Moq (ou autre framework de mock)
- Unité de tests (NUnit, MSTest, etc.)

## Installation

1. Clonez ce repository :  
   `git clone https://github.com/CEM-420-5W5/Mock-Controller`
2. Installez les dépendances nécessaires :  
   `dotnet restore`
3. Exécutez les tests :  
   `dotnet test`
